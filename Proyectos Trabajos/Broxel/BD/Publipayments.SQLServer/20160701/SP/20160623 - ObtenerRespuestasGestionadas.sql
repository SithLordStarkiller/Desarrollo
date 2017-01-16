
/****** Object:  StoredProcedure [dbo].[ObtenerRespuestasGestionadas]    Script Date: 23/06/2016 09:58:15 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
* Proyecto:				PubliPayments
* Autor:				Maximiliano Silva
* Fecha de creación:	18/06/2014
* Descripción:			Obtiene las respuestas de las ordenes asignadas
	@tipo		Cantidad de campos, 0 = todos
* Fecha Modificacion:   01/02/2015
* Modifico:				Alberto Rojas
* Modificacion:			se agrega los filtros para no repetir los campos de camposRespuesta
**********************************************************************/
ALTER PROCEDURE [dbo].[ObtenerRespuestasGestionadas]  (
	@tipo INT = 0
	,@reporte INT
	,@tipoArchivo int = 0
	)
AS
BEGIN
	SET NOCOUNT ON;

	IF OBJECT_ID('tempdb.dbo.##tablaTemporalRespuestasGestionadas', 'U') IS NOT NULL
		DROP TABLE ##tablaTemporalRespuestasGestionadas

	DECLARE @campos NVARCHAR(max),
		@camposCabecero NVARCHAR(max)
		,@sql NVARCHAR(max)
		,@Estatus1 NVARCHAR = '4'
		,@Estatus2 NVARCHAR = '4'
		,@Rutas1 varchar(10) ='' 
		,@Rutas2 varchar(10)=''
		,@RutasNa Nvarchar(500)
		,@camposSql nvarchar(4000)
		,@Fecha varchar(10) = CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121)

	IF (@reporte > 0)
	BEGIN
		SET @Estatus1 = '3'
	END
	
	
	
	SET @RutasNa = CASE @tipoArchivo WHEN 6 THEN '''vsmp'',''RA''' WHEN 10 THEN '''VBD'',''CSD'',''RDST'',''RA''' END

	SET @camposSql= N'Select  @campos =  isnull(+ @campos + '','','''')+ ''[''+ Nombre + '']'' '
	SET @camposSql += ' FROM CamposRespuesta WITH (NOLOCK) '
	SET @camposSql +='  WHERE Tipo >= '+convert(nvarchar(2),@tipo)+'  and idFormulario in ( select distinct f.idFormulario FROM Formulario f WITH (NOLOCK) INNER JOIN Aplicacion a WITH (NOLOCK) ON a.idAplicacion = f.idAplicacion '
	SET @camposSql +=' WHERE a.idAplicacion = (SELECT valor FROM [CatalogoGeneral] WITH (NOLOCK) WHERE Llave = ''idAplicacion'') AND f.ruta NOT IN ('
	SET @camposSql += @RutasNa
	SET @camposSql +=')) group by Nombre'

	
	exec sp_executesql @camposSql, N'@campos NVARCHAR(MAX) out', @campos out
	
	set @camposCabecero = REPLACE ( @campos , '[FormiikResponseSource]' , '[FormiikResponseSource] as [Captura]' )

	
	SET @sql = 'SELECT idOrden, ' + @camposCabecero + ' '
	SET @sql += 'INTO ##tablaTemporalRespuestasGestionadas '
	SET @sql += 'FROM( SELECT r.idOrden,c.Nombre,r.Valor '
	SET @sql += 'FROM [CamposRespuesta] c WITH (NOLOCK) '
	SET @sql += 'LEFT JOIN [Respuestas] r WITH (NOLOCK) '
	SET @sql += 'ON c.idCampo = r.idCampo and c.idformulario=r.idFormulario '
	SET @sql += 'INNER JOIN [ordenes] o WITH (NOLOCK) ON o.idorden=r.idorden '
	SET @sql += 'where CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE ''%'+ @Fecha + '%'' '
	SET @sql +=' AND r.idFormulario in ( select distinct f.idFormulario FROM Formulario f WITH (NOLOCK)  INNER JOIN Aplicacion a WITH (NOLOCK) ON a.idAplicacion = f.idAplicacion WHERE a.idAplicacion = (SELECT valor FROM [CatalogoGeneral] WITH (NOLOCK) WHERE Llave = ''idAplicacion'') AND f.ruta NOT IN ('+@RutasNa+'))'
	SET @sql += ') d PIVOT ('
	SET @sql += 'MAX(Valor) '
	SET @sql += 'FOR Nombre IN '
	SET @sql += '( ' + @campos
	SET @sql += ') ) piv '
	SET @sql += 'WHERE idOrden > 0; '
	print @sql
	EXEC (@sql)
	
	SELECT o.num_Cred num_cred
		,o.idVisita
		,cat.Estado as 'Resultado_final'
		,ISNULL(Dictamen.Valor, 'Sin dictamen') AS 'Estatus_final'
		,t.*
		,@Fecha as FH_INFORMACION
	FROM ##tablaTemporalRespuestasGestionadas t
	INNER JOIN dbo.Ordenes o WITH (NOLOCK) ON t.idOrden = o.idOrden
	INNER JOIN dbo.Creditos C WITH (NOLOCK) ON c.CV_CREDITO=o.num_Cred
	INNER JOIN dbo.CatEstatusOrdenes cat WITH (NOLOCK) ON o.Estatus = cat.Codigo 
	INNER JOIN dbo.Usuario u WITH (NOLOCK) ON o.idUsuario = u.idUsuario
	LEFT JOIN 
	(SELECT distinct r.idOrden, r.Valor FROM 
	(SELECT idOrden, idCampo, Valor FROM Respuestas WITH (NOLOCK)
	WHERE Respuestas.idOrden IN
	(SELECT Ordenes.idOrden FROM Ordenes WITH (NOLOCK))) r
		INNER JOIN CamposRespuesta c WITH (NOLOCK)
		ON r.idCampo = c.idCampo
		WHERE c.Nombre like 'dictamen%') Dictamen 
		ON Dictamen.idOrden = o.idOrden
	WHERE o.Estatus IN (
			@Estatus1
			,@Estatus2
			)
		AND CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE CASE @reporte
			WHEN 0
				THEN '%%'
			ELSE '%' + @Fecha + '%'
			END
END 
