-- =============================================
-- Author:		Laura Dotor
-- Create date: 06/10/2016
-- Description:	Calcula tabla de valores para indicador 
-- CREDITOS CON MARJANO NO EXITOSO
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerRankIndRDST_MARCAJENOEXITOSO] 
	@Master VARCHAR(100) = NULL, 
	@fc_DashBoard VARCHAR(100) = NULL, 
	@Indicador VARCHAR(100) = NULL, 
	@fc_Despacho VARCHAR(100) = NULL, 
	@idUsuarioPadre VARCHAR(100) = NULL, 
	@valorSuperior INT = 1, 
	@fc_Delegacion VARCHAR(100) = NULL,
	@TipoFormulario VARCHAR(10) = NULL
AS
BEGIN
	IF @Master IS NOT NULL ---Tablas Master
	BEGIN
		IF @Master = 'Despacho' --Ranking Por Despacho Rol: Administrador
		BEGIN
			SELECT cast(ROW_NUMBER() OVER (
						ORDER BY tablaResultados.valor DESC
						) AS INT) AS Posicion, tablaDespachos.Value AS Identificador, tablaDespachos.Description AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor
			FROM (
				SELECT l.nom_corto Value, l.NombreDominio Description
				FROM dominio l WITH (NOLOCK)
				) AS tablaDespachos
			LEFT JOIN (
				SELECT c.TX_NOMBRE_DESPACHO, COALESCE(COUNT(c.CV_CREDITO), 0) valor
				FROM LlamadasSinExito a WITH (NOLOCK)
				INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
				LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
				INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
				WHERE c.cv_Ruta = @TipoFormulario
				AND d.idDominio > 1
				GROUP BY c.TX_NOMBRE_DESPACHO
				) AS tablaResultados ON tablaDespachos.Value = tablaResultados.TX_NOMBRE_DESPACHO
			ORDER BY tablaResultados.valor DESC, tablaDespachos.Description
		END

		IF @Master = 'DelegacionAdministrador' --Ranking Por delegacion  Rol: Administrador
		BEGIN
			SELECT cast(ROW_NUMBER() OVER (
						ORDER BY tablaResultados.valor DESC, tablaDelegaciones.Description
						) AS INT) AS Posicion, tablaDelegaciones.Value AS Identificador, tablaDelegaciones.Description AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor
			FROM (
				SELECT Delegacion Value, Descripcion Description
				FROM CatDelegaciones WITH (NOLOCK)
				) AS tablaDelegaciones
			LEFT JOIN (
				SELECT c.CV_DELEGACION, COALESCE(COUNT(c.CV_CREDITO), 0) valor
				FROM LlamadasSinExito a WITH (NOLOCK)
				INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
				LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
				INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
				WHERE c.cv_Ruta = @TipoFormulario
				AND d.idDominio > 1
				GROUP BY c.CV_DELEGACION
				) AS tablaResultados ON tablaDelegaciones.Value = tablaResultados.CV_DELEGACION
			ORDER BY tablaResultados.valor DESC, tablaDelegaciones.Description
		END

		IF @Master = 'Delegacion' --Ranking Por Despacho  Rol: Delegacion
		BEGIN
			SELECT cast(ROW_NUMBER() OVER (
						ORDER BY tablaResultados.valor DESC
						) AS INT) AS Posicion, tablaDespachos.Value AS Identificador, tablaDespachos.Description AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor, CASE 
					WHEN @valorSuperior = 0
						THEN '0'
					ELSE (ISNULL(tablaResultados.valor, 0) * 100) / @valorSuperior
					END AS Porcentaje
			FROM (
				SELECT l.nom_corto Value, l.NombreDominio Description
				FROM dominio l WITH (NOLOCK)
				) AS tablaDespachos
			LEFT JOIN (
				SELECT c.TX_NOMBRE_DESPACHO, COALESCE(COUNT(c.CV_CREDITO ), 0) valor
				FROM LlamadasSinExito a WITH (NOLOCK)
				INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
				LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
				INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
				WHERE c.cv_Ruta = @TipoFormulario
				AND d.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion	
				GROUP BY c.TX_NOMBRE_DESPACHO
				) AS tablaResultados ON tablaDespachos.Value = tablaResultados.TX_NOMBRE_DESPACHO
			ORDER BY tablaResultados.valor DESC, tablaDespachos.Description
		END

		IF @Master = 'DelegacionDespacho' --Ranking Por Delegacion Rol: Despacho
		BEGIN
			SELECT cast(ROW_NUMBER() OVER (
						ORDER BY tablaResultados.valor DESC, tablaDelegaciones.Description
						) AS INT) AS Posicion, tablaDelegaciones.Value AS Identificador, tablaDelegaciones.Description AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor, CASE 
					WHEN @valorSuperior = 0
						THEN '0'
					ELSE (ISNULL(tablaResultados.valor, 0) * 100) / @valorSuperior
					END AS Porcentaje
			FROM (
				SELECT Delegacion Value, Descripcion Description
				FROM CatDelegaciones WITH (NOLOCK)
				) AS tablaDelegaciones
			LEFT JOIN (
				SELECT c.CV_DELEGACION, COALESCE(COUNT(c.CV_CREDITO), 0) valor
				FROM LlamadasSinExito a WITH (NOLOCK)
				INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
				LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
				INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
				WHERE c.cv_Ruta = @TipoFormulario
				AND d.idDominio > 1
				AND c.TX_NOMBRE_DESPACHO = @fc_Despacho	
				GROUP BY c.CV_DELEGACION
				) AS tablaResultados ON tablaDelegaciones.Value = tablaResultados.CV_DELEGACION
			ORDER BY tablaResultados.valor DESC, tablaDelegaciones.Description
		END

		IF @Master = 'Supervisor' --Ranking gestores Rol: Supervisor
		BEGIN
			SELECT cast(ROW_NUMBER() OVER (
						ORDER BY tablaResultados.valor DESC, tablaGestores.Description
						) AS INT) AS Posicion, tablaGestores.Value AS Identificador, tablaGestores.Description AS Usuario, tablaGestores.Nombre AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor, CASE 
					WHEN @valorSuperior = 0
						THEN '0'
					ELSE (ISNULL(tablaResultados.valor, 0) * 100) / @valorSuperior
					END AS Porcentaje
			FROM (
				SELECT DISTINCT cast(u.idUsuario AS VARCHAR(15)) Value, u.Usuario Description, Nombre
				FROM Usuario u WITH (NOLOCK)
				JOIN RelacionUsuarios ru WITH (NOLOCK) ON u.idUsuario = ru.idHijo
				WHERE ru.idPadre = @idUsuarioPadre
				) AS tablaGestores
			LEFT JOIN (
				SELECT o.idUsuario, COALESCE(COUNT(c.CV_CREDITO), 0) valor
				FROM LlamadasSinExito a WITH (NOLOCK)
				INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
				LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
				INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
				WHERE c.cv_Ruta = @TipoFormulario
				AND d.idDominio > 1
				AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND o.idUsuarioPadre = @idUsuarioPadre
				GROUP BY O.IdUsuario
				) AS tablaResultados ON tablaGestores.Value = tablaResultados.idUsuario
			ORDER BY tablaResultados.valor DESC, tablaGestores.Description
		END
	END
	ELSE
	BEGIN
		IF @fc_Delegacion IS NULL --Obtener valor del supervisor para calculo de porcentajes en Rol supervisor
		BEGIN
			SELECT 0 AS Posicion, tablaSupervisores.Value AS Identificador, '' AS Usuario, 'TOTAL' AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor, 
			100 AS Porcentaje
			FROM (
				SELECT DISTINCT cast(idUsuario AS VARCHAR(15)) Value, usuario Description, Nombre
				FROM VUsuarios 
				WHERE idRol = 3
					AND nom_corto = @fc_Despacho
					AND idUsuario = @idUsuarioPadre
				) AS tablaSupervisores
			LEFT JOIN (
				SELECT o.idUsuarioPadre, COALESCE(COUNT(c.CV_CREDITO), 0) valor
				FROM LlamadasSinExito a WITH (NOLOCK)
				INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
				LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
				INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
				WHERE c.cv_Ruta = @TipoFormulario
				AND d.idDominio > 1
				AND o.idUsuarioPadre = @idUsuarioPadre
				AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
				GROUP BY O.IdUsuarioPadre
				) AS tablaResultados ON tablaSupervisores.Value = tablaResultados.idUsuarioPadre
			ORDER BY tablaResultados.valor DESC, tablaSupervisores.Description
		END
		ELSE
		BEGIN
			IF @idUsuarioPadre IS NULL --tabla supervisores hija
			BEGIN
				SELECT cast(ROW_NUMBER() OVER (
							ORDER BY tablaResultados.valor DESC, tablaSupervisores.Description
							) AS INT) AS Posicion, tablaSupervisores.Value AS Identificador, tablaSupervisores.Description AS Usuario, tablaSupervisores.Nombre AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor, CASE 
						WHEN @valorSuperior = 0
							THEN '0'
						ELSE (ISNULL(tablaResultados.valor, 0) * 100) / @valorSuperior
						END AS Porcentaje
				FROM (
					SELECT DISTINCT cast(idUsuario AS VARCHAR(15)) Value, usuario Description, Nombre
					FROM VUsuarios
					WHERE idRol = 3
						AND nom_corto = @fc_Despacho
					) AS tablaSupervisores
				LEFT JOIN (
					SELECT o.idUsuarioPadre, COALESCE(COUNT(c.CV_CREDITO), 0) valor
					FROM LlamadasSinExito a WITH (NOLOCK)
					INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
					LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
					INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
					WHERE c.cv_Ruta = @TipoFormulario
					AND d.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
					GROUP BY O.IdUsuarioPadre
					) AS tablaResultados ON tablaSupervisores.Value = tablaResultados.idUsuarioPadre
				ORDER BY tablaResultados.valor DESC, tablaSupervisores.Description
			END
			ELSE --tabla gestores hija
			BEGIN
				SELECT cast(ROW_NUMBER() OVER (
							ORDER BY tablaResultados.valor DESC, tablaGestores.Description
							) AS INT) AS Posicion, tablaGestores.Value AS Identificador, tablaGestores.Description AS Usuario, tablaGestores.Nombre AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor, CASE 
						WHEN @valorSuperior = 0
							THEN '0'
						ELSE (ISNULL(tablaResultados.valor, 0) * 100) / @valorSuperior
						END AS Porcentaje
				FROM (
					SELECT DISTINCT cast(u.idUsuario AS VARCHAR(15)) Value, u.Usuario Description, Nombre
					FROM Usuario u WITH (NOLOCK)
					JOIN RelacionUsuarios ru WITH (NOLOCK) ON u.idUsuario = ru.idHijo
					WHERE ru.idPadre = @idUsuarioPadre
					) AS tablaGestores
				LEFT JOIN (
					SELECT o.idUsuario, COALESCE(COUNT(c.CV_CREDITO), 0) valor
					FROM LlamadasSinExito a WITH (NOLOCK)
					INNER JOIN Creditos c WITH (NOLOCK) on a.CV_CREDITO = c.CV_CREDITO 
					LEFT JOIN Ordenes o WITH (NOLOCK) on a.CV_CREDITO  = o.num_Cred
					INNER JOIN Dominio d WITH (NOLOCK) on d.nom_corto = c.TX_NOMBRE_DESPACHO
					WHERE c.cv_Ruta = @TipoFormulario
					AND d.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
					AND o.idUsuarioPadre = @idUsuarioPadre
					GROUP BY O.IdUsuario
					) AS tablaResultados ON tablaGestores.Value = tablaResultados.idUsuario
				ORDER BY tablaResultados.valor DESC, tablaGestores.Description
			END
		END
	END
END
