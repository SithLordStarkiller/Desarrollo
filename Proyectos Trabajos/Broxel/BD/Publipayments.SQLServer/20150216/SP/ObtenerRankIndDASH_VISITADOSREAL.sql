USE [SistemasCobranzaDesarrollo]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerRankIndDASH_VISITADOSREAL]    Script Date: 02/09/2015 08:27:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 27/10/2014
-- Description:	Calcula tabla de valores para indicador VISITADOSREAL
-- Modification Date: 05/11/2014
-- Modification Description: Se agrega calculo para total en rol supervisor
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerRankIndDASH_VISITADOSREAL]
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
				FROM dominio l
				WHERE L.idDominio > 2
				) AS tablaDespachos
			LEFT JOIN (
				SELECT isnull(t1.TX_NOMBRE_DESPACHO, t2.TX_NOMBRE_DESPACHO) AS TX_NOMBRE_DESPACHO, isnull(t1.valor, 0) + isnull(t2.valor, 0) valor
				FROM (
					SELECT c.TX_NOMBRE_DESPACHO, isnull(sum(o.idVisita - 1), 0) AS Valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1, 11, 12, 15,17)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY c.TX_NOMBRE_DESPACHO
					) AS t1
				FULL OUTER JOIN (
					SELECT c.TX_NOMBRE_DESPACHO, isnull(SUM(o.idVisita), 0) AS Valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3, 4,37,47,6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY c.TX_NOMBRE_DESPACHO
					) AS t2 ON t1.TX_NOMBRE_DESPACHO = t2.TX_NOMBRE_DESPACHO
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
				FROM CatDelegaciones
				) AS tablaDelegaciones
			LEFT JOIN (
				SELECT isnull(t1.CV_DELEGACION, t2.CV_DELEGACION) AS CV_DELEGACION, isnull(t1.valor, 0) + isnull(t2.valor, 0) valor
				FROM (
					SELECT c.CV_DELEGACION, isnull(sum(o.idVisita - 1), 0) AS Valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1, 11, 12, 15,17)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY c.CV_DELEGACION
					) AS t1
				FULL OUTER JOIN (
					SELECT c.CV_DELEGACION, isnull(SUM(o.idVisita), 0) AS Valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3, 4,37,47,6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY c.CV_DELEGACION
					) AS t2 ON t1.CV_DELEGACION = t2.CV_DELEGACION
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
				FROM dominio l
				WHERE L.idDominio > 2
				) AS tablaDespachos
			LEFT JOIN (
				SELECT isnull(t1.TX_NOMBRE_DESPACHO, t2.TX_NOMBRE_DESPACHO) AS TX_NOMBRE_DESPACHO, isnull(t1.valor, 0) + isnull(t2.valor, 0) valor
				FROM (
					SELECT c.TX_NOMBRE_DESPACHO, isnull(sum(o.idVisita - 1), 0) AS Valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1, 11, 12, 15,17)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY c.TX_NOMBRE_DESPACHO
					) AS t1
				FULL OUTER JOIN (
					SELECT c.TX_NOMBRE_DESPACHO, isnull(SUM(o.idVisita), 0) AS Valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3, 4,37,47,6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY c.TX_NOMBRE_DESPACHO
					) AS t2 ON t1.TX_NOMBRE_DESPACHO = t2.TX_NOMBRE_DESPACHO
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
				FROM CatDelegaciones
				) AS tablaDelegaciones
			LEFT JOIN (
				SELECT isnull(t1.CV_DELEGACION, t2.CV_DELEGACION) AS CV_DELEGACION, isnull(t1.valor, 0) + isnull(t2.valor, 0) valor
				FROM (
					SELECT c.CV_DELEGACION, isnull(sum(o.idVisita - 1), 0) AS Valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1, 11, 12, 15,17)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY c.CV_DELEGACION
					) AS t1
				FULL OUTER JOIN (
					SELECT c.CV_DELEGACION, isnull(SUM(o.idVisita), 0) AS Valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3, 4,37,47,6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY c.CV_DELEGACION
					) AS t2 ON t1.CV_DELEGACION = t2.CV_DELEGACION
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
				FROM Usuario u
				JOIN RelacionUsuarios ru ON u.idUsuario = ru.idHijo
				WHERE ru.idPadre = @idUsuarioPadre
				) AS tablaGestores
			LEFT JOIN (
				SELECT isnull(t1.idUsuario, t2.idUsuario) AS idUsuario, isnull(t1.valor, 0) + ISNULL(t2.valor, 0) AS valor
				FROM (
					SELECT o.idUsuario, isnull(SUM(o.idVisita), 0) valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3, 4,37,47,6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND o.idUsuarioPadre = @idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY O.IdUsuario
					) AS t1
				FULL OUTER JOIN (
					SELECT o.idUsuario, isnull(sum(o.idVisita - 1), 0) valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1, 11, 12, 15,17)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND o.idUsuarioPadre = @idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY O.IdUsuario
					) AS t2 ON t1.idUsuario = t2.idUsuario
				) AS tablaResultados ON tablaGestores.Value = tablaResultados.idUsuario
			ORDER BY tablaResultados.valor DESC, tablaGestores.Description
		END
	END
	ELSE
	BEGIN
		IF @fc_Delegacion IS NULL --Obtener valor del supervisor para calculo de porcentajes en Rol supervisor
		BEGIN
			SELECT 0 AS Posicion, tablaSupervisores.Value AS Identificador, '' AS Usuario, 'TOTAL' AS Nombre, ISNULL(tablaResultados.valor, 0) AS Valor, 100 AS Porcentaje
			FROM (
				SELECT DISTINCT cast(idUsuario AS VARCHAR(15)) Value, usuario Description, Nombre
				FROM VUsuarios
				WHERE idRol = 3
					AND nom_corto = @fc_Despacho
					AND idUsuario = @idUsuarioPadre
				) AS tablaSupervisores
			LEFT JOIN (
				SELECT isnull(t1.idUsuarioPadre, t2.idUsuarioPadre) AS idUsuarioPadre, isnull(t1.valor, 0) + ISNULL(t2.valor, 0) AS valor
				FROM (
					SELECT o.idUsuarioPadre, isnull(SUM(o.idVisita), 0) valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3, 4,37,47,6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND o.idUsuarioPadre = @idUsuarioPadre
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY o.idUsuarioPadre
					) AS t1
				FULL OUTER JOIN (
					SELECT o.idUsuarioPadre, isnull(sum(o.idVisita - 1), 0) valor
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1, 11, 12, 15,17)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND o.idUsuarioPadre = @idUsuarioPadre
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario
					GROUP BY o.idUsuarioPadre
					) AS t2 ON t1.idUsuarioPadre = t2.idUsuarioPadre
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
					SELECT isnull(t1.idUsuarioPadre, t2.idUsuarioPadre) AS idUsuarioPadre, isnull(t1.valor, 0) + ISNULL(t2.valor, 0) AS valor
					FROM (
						SELECT o.idUsuarioPadre, isnull(SUM(o.idVisita), 0) valor
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3, 4,37,47,6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND c.CV_RUTA = @TipoFormulario
						GROUP BY o.idUsuarioPadre
						) AS t1
					FULL OUTER JOIN (
						SELECT o.idUsuarioPadre, isnull(sum(o.idVisita - 1), 0) valor
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (1, 11, 12, 15,17)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND c.CV_RUTA = @TipoFormulario
						GROUP BY o.idUsuarioPadre
						) AS t2 ON t1.idUsuarioPadre = t2.idUsuarioPadre
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
					FROM Usuario u
					JOIN RelacionUsuarios ru ON u.idUsuario = ru.idHijo
					WHERE ru.idPadre = @idUsuarioPadre
					) AS tablaGestores
				LEFT JOIN (
					SELECT isnull(t1.idUsuario, t2.idUsuario) AS idUsuario, isnull(t1.valor, 0) + ISNULL(t2.valor, 0) AS valor
					FROM (
						SELECT o.idUsuario, isnull(SUM(o.idVisita), 0) valor
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3, 4,37,47,6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND o.idUsuarioPadre = @idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario
						GROUP BY O.IdUsuario
						) AS t1
					FULL OUTER JOIN (
						SELECT o.idUsuario, isnull(sum(o.idVisita - 1), 0) valor
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (1, 11, 12, 15,17)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND o.idUsuarioPadre = @idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario
						GROUP BY O.IdUsuario
						) AS t2 ON t1.idUsuario = t2.idUsuario
					) AS tablaResultados ON tablaGestores.Value = tablaResultados.idUsuario
				ORDER BY tablaResultados.valor DESC, tablaGestores.Description
			END
		END
	END
END
