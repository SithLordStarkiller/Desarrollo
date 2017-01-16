USE [SistemasCobranzaDesarrollo]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerRespuestas]    Script Date: 13/02/2015 05:39:24 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Maximiliano Silva
* Fecha de creación:	02/04/2014
* Descripción:			Obtiene las respuestas de las ordenes asignadas
	@tipo		Cantidad de campos, 0 = todos
	@idOrden	Numero de la orden o parte
	@num_Cred	Numero del credito
	@fechaAlta	Fecha de la asignacion
**********************************************************************/
ALTER PROCEDURE [dbo].[ObtenerRespuestas] (
	@tipo INT = 0
	,@idOrden VARCHAR(20) = ''
	,@reporte INT = 0
	,@idUsuarioPadre INT = 0
	)
AS
BEGIN
	SET NOCOUNT ON;

	IF OBJECT_ID('tempdb.dbo.##tablaTemporal', 'U') IS NOT NULL
		DROP TABLE ##tablaTemporal

	DECLARE @campos NVARCHAR(max)
		,@sql NVARCHAR(max)
		,@Estatus1 NVARCHAR = '3'
		,@Estatus2 NVARCHAR = '4'
		,@Estatus3 NVARCHAR = '4'
		,@Dominio NVARCHAR = 'pp'
		,@idFormulario INT

	IF (@reporte > 0)
	BEGIN
		SET @Estatus1 = '4'
	END
	
  IF (@reporte = -1)
	BEGIN
		SET @Estatus1 = '2'
		SET @Estatus2 = '3'
		SET @Estatus3 = '4'
	END
	
	SELECT top 1 @idFormulario = idFormulario
	FROM Respuestas
	WHERE  idOrden=@idOrden

	SELECT @campos = isnull(+ @campos + ', ', '') + '[' + Nombre + ']'
	FROM CamposRespuesta 
	WHERE  Tipo >= @tipo  and idFormulario=@idFormulario

	SET @sql = 'SELECT idOrden, ' + @campos + ' '
	SET @sql += 'INTO ##tablaTemporal '
	SET @sql += 'FROM( SELECT r.idOrden,c.Nombre,r.Valor '
	SET @sql += 'FROM [CamposRespuesta] c '
	SET @sql += 'LEFT JOIN [Respuestas] r '
	SET @sql += 'ON c.idCampo = r.idCampo and r.idFormulario=r.idFormulario '

	IF (@idOrden > 0)
		SET @sql += 'WHERE CONVERT(VARCHAR(12), r.idOrden) LIKE ''%' + @idOrden + '%'''
	SET @sql += ') d PIVOT ('
	SET @sql += 'MAX(Valor) '
	SET @sql += 'FOR Nombre IN '
	SET @sql += '( ' + @campos
	SET @sql += ') ) piv '
	SET @sql += 'WHERE idOrden > 0; '

	EXEC (@sql)

	SELECT l.ID_ARCHIVO id_Carga
		,l.CV_CREDITO num_cred
		,l.CV_ETIQUETA desc_etiq
		,l.TX_SOLUCIONES soluciones
		,l.TX_NOMBRE_ACREDITADO nombre
		,l.TX_CALLE calle
		,l.TX_COLONIA colonia
		,l.TX_MUNICIPIO municipio
		,l.CV_CODIGO_POSTAL cp
		,cd.Descripcion estado
		,l.CV_DESPACHO nom_corto
		,u.Usuario
		,o.idVisita
		,o.Estatus
		,o.FechaAlta
		,o.num_Cred
		,o.FechaModificacion
		,o.FechaEnvio
		,ISNULL(Dictamen.Valor, 'Sin dictamen') AS Dictamen
		,t.*
	FROM ##tablaTemporal t
	INNER JOIN dbo.Ordenes o ON t.idOrden = o.idOrden
	INNER JOIN dbo.Creditos l ON o.num_Cred = l.CV_CREDITO
	INNER JOIN dbo.Usuario u ON o.idUsuario = u.idUsuario
	INNER JOIN CatDelegaciones cd ON l.CV_DELEGACION = cd.Delegacion
	LEFT JOIN (
		SELECT r.idOrden
			,r.Valor
		FROM Respuestas r
		INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo and r.idFormulario=r.idFormulario
		WHERE c.Nombre LIKE 'dictamen%'
		) Dictamen ON Dictamen.idOrden = o.idOrden
	WHERE o.Estatus IN (
			@Estatus1
			,@Estatus2
			)
		AND CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE CASE @reporte
			WHEN 0
				THEN '%%'
			WHEN - 1
				THEN '%%'
			ELSE '%' + CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121) + '%'
			END
		AND o.idUsuarioPadre = CASE @idUsuarioPadre
			WHEN 0
				THEN o.idUsuarioPadre
			ELSE @idUsuarioPadre
			END
END
