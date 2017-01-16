
/****** Object:  StoredProcedure [dbo].[ObtenerRespuestasGestionadas]    Script Date: 06/04/2016 10:48:27 a.m. ******/
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

set  @Rutas1 = CASE @tipoArchivo WHEN 6 THEN 'vsmp' WHEN 10 THEN 'VBD' END 
set  @Rutas2 = CASE @tipoArchivo WHEN 6 THEN '' WHEN 10 THEN 'CSD' END 

	IF (@reporte > 0)
	BEGIN
		SET @Estatus1 = '3'
	END

	SELECT  @campos = isnull(+ @campos + ', ', '') + '[' + Nombre + ']'
	FROM CamposRespuesta WITH (NOLOCK)
	WHERE Tipo >= @tipo  and idFormulario in ( select distinct f.idFormulario FROM Formulario f WITH (NOLOCK) INNER JOIN Aplicacion a WITH (NOLOCK) ON a.idAplicacion = f.idAplicacion
											  WHERE a.idAplicacion = (SELECT valor FROM [CatalogoGeneral] WITH (NOLOCK) WHERE Llave = 'idAplicacion') AND f.ruta != @Rutas1 AND f.ruta != @Rutas2 AND f.ruta!='RA')
							group by Nombre
	set @camposCabecero = REPLACE ( @campos , '[FormiikResponseSource]' , '[FormiikResponseSource] as [Captura]' )


	SET @sql = 'SELECT idOrden, ' + @camposCabecero + ' '
	SET @sql += 'INTO ##tablaTemporalRespuestasGestionadas '
	SET @sql += 'FROM( SELECT r.idOrden,c.Nombre,r.Valor '
	SET @sql += 'FROM [CamposRespuesta] c WITH (NOLOCK) '
	SET @sql += 'LEFT JOIN [Respuestas] r WITH (NOLOCK) '
	SET @sql += 'ON c.idCampo = r.idCampo and c.idformulario=r.idFormulario '
	SET @sql += ' where  r.idFormulario in ( select distinct f.idFormulario FROM Formulario f WITH (NOLOCK)  INNER JOIN Aplicacion a WITH (NOLOCK) ON a.idAplicacion = f.idAplicacion WHERE a.idAplicacion = (SELECT valor FROM [CatalogoGeneral] WITH (NOLOCK) WHERE Llave = ''idAplicacion'') AND f.ruta != '''+@Rutas1+''' AND f.ruta != '''+@Rutas2+''')'
	SET @sql += ') d PIVOT ('
	SET @sql += 'MAX(Valor) '
	SET @sql += 'FOR Nombre IN '
	SET @sql += '( ' + @campos
	SET @sql += ') ) piv '
	SET @sql += 'WHERE idOrden > 0; '

	EXEC (@sql)

	SELECT o.num_Cred num_cred
		,o.idVisita
		,cat.Estado as 'Resultado_final'
		,ISNULL(Dictamen.Valor, 'Sin dictamen') AS 'Estatus_final'
		,t.*
		,CONVERT(varchar, GETDATE(),112) as FH_INFORMACION
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
			ELSE '%' + CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121) + '%'
			END
END 
