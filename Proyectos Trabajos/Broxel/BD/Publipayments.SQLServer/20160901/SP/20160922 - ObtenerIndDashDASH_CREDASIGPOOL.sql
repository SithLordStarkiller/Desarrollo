-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 28/01/2015
-- Description:	Obtiene Indicadores Para Dashboard
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerIndDashDASH_CREDASIGPOOL] @idUsuario INT, @delegacion VARCHAR(10), @despacho VARCHAR(10), @supervisor VARCHAR(10), @gestor VARCHAR(10), @tipoFormulario VARCHAR(50), @callCenter VARCHAR(6) = 'false'
AS
BEGIN
	DECLARE @ruta VARCHAR(10) = '', @valor VARCHAR(10) = '', @porcentaje VARCHAR(10) = '', @parte VARCHAR(10) = '', @nombreDisplay VARCHAR(100) = '', @rol INT

	SELECT @ruta = Ruta
	FROM Formulario WITH (NOLOCK)
	WHERE idFormulario = @tipoFormulario

	SELECT @parte = fi_Parte, @nombreDisplay = fc_Descripcion
	FROM dbo.Utils_Descripciones WITH (NOLOCK)
	WHERE fc_Clave = 'DASH_CREDASIGPOOL'

	SELECT @rol = idRol
	FROM Usuario WITH (NOLOCK)
	WHERE idUsuario = @idUsuario

	IF @rol IN (2,3,4)
	BEGIN
		SELECT @despacho = idDominio
		FROM Usuario WITH (NOLOCK)
		WHERE idUsuario = @idUsuario
	END

	IF @rol IN (3)
	BEGIN
		SELECT @supervisor = @idUsuario
	END

	IF @rol IN (4)
	BEGIN
		SELECT @supervisor = idPadre
		FROM RelacionUsuarios WITH (NOLOCK)
		WHERE idHijo = @idUsuario

		SELECT @gestor = @idUsuario
	END

	IF @rol IN (5)
	BEGIN
		SELECT @delegacion = Delegacion
		FROM RelacionDelegaciones WITH (NOLOCK)
		WHERE idUsuario = @idUsuario
	END

	IF @callCenter = 'false'
	BEGIN
		IF @rol IN (0,1,2,3,4,5,6)
		BEGIN
			IF(@ruta = 'RDST')
			BEGIN
				SELECT @valor = COUNT(c.CV_CREDITO), @porcentaje = 100
					FROM  dbo.Creditos c with (NOLOCK)
					INNER JOIN dbo.Dominio d with (nolock) ON d.nom_corto = c.TX_NOMBRE_DESPACHO			
					WHERE c.CV_Ruta = @ruta
						AND (@delegacion = '%' OR c.CV_DELEGACION = @delegacion )
						AND d.idDominio > 1
						AND d.idDominio = CASE  WHEN @despacho = '%' THEN d.idDominio ELSE @despacho END 
			END
			ELSE
			BEGIN

				IF (@gestor<>'%' OR @supervisor<>'%')
				BEGIN
					DECLARE @tablaCredito TABLE (CV_CREDITO VARCHAR(50) PRIMARY KEY)
					INSERT INTO @tablaCredito 
					SELECT o.num_Cred FROM dbo.Ordenes o with (nolock) 
					where o.idUsuarioPadre=case when @supervisor='%' then o.idUsuarioPadre else @supervisor end
					and o.idUsuario=case when @gestor='%' then o.idUsuario else @gestor end
					and o.idDominio=case when @despacho='%' then o.idDominio else @despacho end
		
					SELECT @valor = COUNT(c.CV_CREDITO), @porcentaje = 100
					FROM  dbo.Creditos c with (NOLOCK)
					INNER JOIN @tablaCredito o ON  c.CV_CREDITO = o.CV_CREDITO
					LEFT JOIN dbo.Dominio d with (nolock) ON d.nom_corto = c.TX_NOMBRE_DESPACHO			
					WHERE c.CV_Ruta = @ruta
						AND d.idDominio > 1
						AND ( @delegacion = '%' OR c.CV_DELEGACION = @delegacion )
						AND d.idDominio = CASE  WHEN @despacho = '%' THEN d.idDominio ELSE @despacho END 
				END
				ELSE
				BEGIN
					SELECT @valor = COUNT(c.CV_CREDITO), @porcentaje = 100
					FROM  dbo.Creditos c with (NOLOCK)
					INNER JOIN dbo.Dominio d with (nolock) ON d.nom_corto = c.TX_NOMBRE_DESPACHO			
					WHERE c.CV_Ruta = @ruta
						AND (@delegacion = '%' OR c.CV_DELEGACION = @delegacion )
						AND d.idDominio > 1
						AND d.idDominio = CASE  WHEN @despacho = '%' THEN d.idDominio ELSE @despacho END 
				END
			END
		END
	END
	ELSE
	BEGIN
		IF @rol IN (0,1,2,3,4,5,6)
		BEGIN
			SELECT @valor = COUNT(c.CV_CREDITO), @porcentaje = 100
			FROM (select CV_CREDITO,TX_NOMBRE_DESPACHO,CV_Ruta,CV_DELEGACION,CC_DESPACHO from creditos with (nolock)) c
			LEFT JOIN (select idUsuario,idUsuarioPadre,num_Cred from Ordenes with (nolock)) o ON o.num_Cred = c.CV_CREDITO
			LEFT JOIN (select nom_corto,idDominio from Dominio with (nolock)) d ON d.nom_corto = c.CC_DESPACHO
			WHERE d.idDominio > 1 and CV_Ruta = @ruta
				AND (
					@despacho = '%'
					OR d.idDominio = CASE 
						WHEN @despacho = '%'
							THEN - 99
						ELSE @despacho
						END
					)
		END
	END

	SELECT @valor + '|' + @porcentaje + '|' + @nombreDisplay + '|' + @parte
END