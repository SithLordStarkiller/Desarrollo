
/****** Object:  StoredProcedure [dbo].[ObtenerRespuestasUsuario]    Script Date: 04/08/2015 06:44:52 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Maximiliano Silva
* Fecha de creación:	04/08/2014
* Fecha Modificación:   22/01/2015
* Descripción:			Obtiene las respuestas de las ordenes por usuario
	@tipo		Cantidad de campos, 0 = todos
	@idOrden	Numero de la orden o parte
	@reporte	Si es reporte es distinto de 0
	@idUsuarioPadre Supervisor o 0 para todos
**********************************************************************/
ALTER PROCEDURE [dbo].[ObtenerRespuestasUsuario] (
	@tipo INT = 0
	,@idOrden VARCHAR(20) = ''
	,@reporte INT = 0
	,@idUsuarioPadre INT = 0
	,@Estatus VARCHAR(8000) = '3,4'
	,@TipoFormulario VARCHAR(10) = ''
	)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Actualizado DATETIME = NULL
		,@TablaRespuestasUsuario VARCHAR(50) = ''
		,@Intentos INT = 0;
	DECLARE @campos NVARCHAR(max)
		,@sql NVARCHAR(max)
		,@formularios nvarchar(max);

	SELECT @formularios = isnull(+ @formularios + ',', '')  + convert(varchar(2),f.idFormulario)
	FROM Formulario f  INNER JOIN Aplicacion a ON a.idAplicacion = f.idAplicacion
	WHERE 
		 a.idAplicacion = (SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion')
		AND f.Ruta = CASE 
			WHEN ISNULL(@TipoFormulario, '') != ''
				THEN @TipoFormulario
			ELSE f.Ruta
			END
			
			
	IF (@tipo != 1)
	BEGIN
		SET @TablaRespuestasUsuario = '##TablaRespuesta' + CONVERT(VARCHAR(10), @idUsuarioPadre);

		--Reviso si la tabla temporal del usuario existe y esta actualizada
		IF (OBJECT_ID('tempdb.dbo.' + @TablaRespuestasUsuario, 'U') IS NOT NULL)
		BEGIN
			DECLARE @ParmDefinition NVARCHAR(500);
			DECLARE @respuestasActuales INT
				,@respuestasTablaTemporal INT;

			SET @sql = 'SELECT @retvalOUT = COUNT(idOrden) FROM ' + @TablaRespuestasUsuario
			SET @ParmDefinition = N'@retvalOUT int OUTPUT';

			EXEC sp_executesql @sql
				,@ParmDefinition
				,@retvalOUT = @respuestasTablaTemporal OUTPUT;

			SELECT @respuestasActuales = COUNT(DISTINCT (idOrden))
			FROM Respuestas

			IF (@respuestasActuales = @respuestasTablaTemporal)
				SET @Actualizado = GETDATE();-- Si esta actualizada la pongo la fecha
		END

		IF (@Actualizado IS NULL)
		BEGIN

			SELECT  @campos = isnull(+ @campos + ', ', '') + '[' + Nombre + ']'
			FROM CamposRespuesta
			WHERE Tipo >= @tipo and idFormulario in (  select distinct f.idFormulario FROM Formulario f  INNER JOIN Aplicacion a ON a.idAplicacion = f.idAplicacion
														WHERE a.idAplicacion = (SELECT valor FROM [CatalogoGeneral]WHERE Llave = 'idAplicacion')
														AND f.Ruta = CASE 
														WHEN ISNULL(@TipoFormulario, '') != ''
															THEN @TipoFormulario
														ELSE f.Ruta
														END )
			GROUP BY Nombre

			IF OBJECT_ID('tempdb.dbo.' + @TablaRespuestasUsuario, 'U') IS NOT NULL
			BEGIN
				SET @sql = 'DROP TABLE ' + @TablaRespuestasUsuario;
				EXEC (@sql)
			END

			SET @sql = 'SELECT idOrden, ' + @campos + ' '
			SET @sql += 'INTO ' + @TablaRespuestasUsuario
			SET @sql += ' FROM( SELECT r.idOrden,c.Nombre,r.Valor '
			SET @sql += 'FROM [CamposRespuesta] c '
			SET @sql += 'LEFT JOIN [Respuestas] r '
			SET @sql += 'ON c.idCampo = r.idCampo and c.idFormulario=r.idFormulario  and r.idformulario in ('+@formularios+')'
			SET @sql += ') d PIVOT ('
			SET @sql += 'MAX(Valor) '
			SET @sql += 'FOR Nombre IN '
			SET @sql += '( ' + @campos
			SET @sql += ') ) piv '
			SET @sql += 'WHERE idOrden > 0;'

			EXEC (@sql)

			SET @sql = 'CREATE NONCLUSTERED INDEX IX_TEMP' + CONVERT(VARCHAR(10), @idUsuarioPadre)
			SET @sql += ' ON [dbo].[' + @TablaRespuestasUsuario + '] ([idOrden])'

			EXEC (@sql)
		END

		WHILE (@Intentos < 3)
		BEGIN
			IF OBJECT_ID('tempdb.dbo.' + @TablaRespuestasUsuario, 'U') IS NULL
				WAITFOR DELAY '00:00:02';
			ELSE
				SET @Intentos = 3;-- Sale

			SET @Intentos = @Intentos + 1;
		END

		IF OBJECT_ID('tempdb.dbo.' + @TablaRespuestasUsuario, 'U') IS NOT NULL
		BEGIN
			SET @sql = 'SELECT l.ID_ARCHIVO id_Carga'
			SET @sql += ' ,l.CV_CREDITO num_cred'
			SET @sql += ' ,l.CV_ETIQUETA desc_etiq'
			SET @sql += ' ,l.TX_SOLUCIONES soluciones'
			SET @sql += ' ,l.TX_NOMBRE_ACREDITADO nombre'
			SET @sql += ' ,l.TX_CALLE calle'
			SET @sql += ' ,l.TX_COLONIA colonia'
			SET @sql += ' ,l.TX_MUNICIPIO municipio'
			SET @sql += ' ,l.CV_CODIGO_POSTAL cp'
			SET @sql += ' ,cd.Descripcion estado'
			SET @sql += ' ,l.CV_DESPACHO nom_corto'
			SET @sql += ' ,u.Usuario'
			SET @sql += ' ,o.idVisita'
			SET @sql += ' ,o.Estatus'
			SET @sql += ' ,o.FechaAlta'
			SET @sql += ' ,o.num_Cred'
			SET @sql += ' ,o.FechaModificacion'
			SET @sql += ' ,o.FechaEnvio'
			SET @sql += ' ,o.FechaRecepcion'
			SET @sql += ' ,l.CV_CANAL'
			SET @sql += ' ,o.Auxiliar auxiliar'
			SET @sql += ' ,ISNULL(Dictamen.Valor, ''Sin dictamen'') AS Dictamen'
			SET @sql += ' ,RTRIM(CONVERT(VARCHAR(2),o.Estatus)+o.Tipo) EstatusExtra'
			SET @sql += ' ,t.*'
			SET @sql += ' FROM ' + @TablaRespuestasUsuario + ' t '
			SET @sql += ' INNER JOIN dbo.Ordenes o ON t.idOrden = o.idOrden'
			SET @sql += ' INNER JOIN dbo.Creditos l ON o.num_Cred = l.CV_CREDITO'
			SET @sql += ' INNER JOIN dbo.Usuario u ON o.idUsuario = u.idUsuario'
			SET @sql += ' INNER JOIN CatDelegaciones cd ON l.CV_DELEGACION = cd.Delegacion'
			SET @sql += ' LEFT JOIN ('
			SET @sql += '	SELECT r.idOrden'
			SET @sql += '		,r.Valor'
			SET @sql += '	FROM Respuestas r'
			SET @sql += '	INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo and r.idFormulario=c.idFormulario'
		    SET @sql += '	WHERE c.Nombre LIKE ''dictamen%'' ' 
			SET @sql += '	) Dictamen ON Dictamen.idOrden = o.idOrden'
			SET @sql += ' WHERE o.Estatus IN (' + @Estatus + ')'

			IF (@reporte != 0)
				SET @sql += '	AND CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE  ''%'' + CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121) + ''%'''

			IF (@idUsuarioPadre != 0)
				SET @sql += '	AND o.idUsuarioPadre = ' + CONVERT(VARCHAR(10), @idUsuarioPadre)

			IF (
					@idOrden != '0'
					AND @idOrden != ''
					)
				SET @sql += '	AND t.idOrden = ' + @idOrden
			EXEC (@sql)
		END
	END
	ELSE
	BEGIN
		SET @sql = 'SELECT l.CV_CREDITO num_cred'
		SET @sql += ' ,l.TX_SOLUCIONES soluciones'
		SET @sql += ' ,l.TX_NOMBRE_ACREDITADO nombre'
		SET @sql += ' ,u.Usuario'
		SET @sql += ' ,o.idOrden'
		SET @sql += ' ,o.idVisita'
		SET @sql += ' ,o.Estatus'
		SET @sql += ' ,o.FechaModificacion'
		SET @sql += ' ,o.FechaEnvio'
		SET @sql += ' ,o.FechaRecepcion'
		SET @sql += ' ,ISNULL(Dictamen.Valor, ''Sin dictamen'') AS Dictamen'
		SET @sql += ' , RTRIM(CONVERT(VARCHAR(2),o.Estatus)+o.Tipo) EstatusExtra'
		SET @sql += ' ,l.TX_COLONIA'
		SET @sql += ' ,l.TX_MUNICIPIO'
		SET @sql += ' ,l.CV_CANAL'
		SET @sql += ' ,o.Auxiliar auxiliar'
		SET @sql += ' FROM (SELECT DISTINCT(idOrden) FROM Respuestas where idFormulario in ('+@formularios+')) t '
		SET @sql += ' INNER JOIN dbo.Ordenes o ON t.idOrden = o.idOrden'
		SET @sql += ' INNER JOIN dbo.Creditos l ON o.num_Cred = l.CV_CREDITO'
		SET @sql += ' INNER JOIN dbo.Usuario u ON o.idUsuario = u.idUsuario'
		SET @sql += ' LEFT JOIN ('
		SET @sql += '	SELECT r.idOrden'
		SET @sql += '		,r.Valor'
		SET @sql += '	FROM Respuestas r'
		SET @sql += '	INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo'
		SET @sql += '	WHERE c.Nombre LIKE ''dictamen%'' ' 
		SET @sql += '	) Dictamen ON Dictamen.idOrden = o.idOrden'
		SET @sql += ' WHERE o.Estatus IN (' + @Estatus + ')'

		IF (@reporte != 0)
			SET @sql += '	AND CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE  ''%'' + CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121) + ''%'''

		IF (@idUsuarioPadre != 0)
			SET @sql += '	AND o.idUsuarioPadre = ' + CONVERT(VARCHAR(10), @idUsuarioPadre)

		IF (
				@idOrden != '0'
				AND @idOrden != ''
				)
			SET @sql += '	AND t.idOrden = ' + @idOrden
		EXEC (@sql)
	END
END

