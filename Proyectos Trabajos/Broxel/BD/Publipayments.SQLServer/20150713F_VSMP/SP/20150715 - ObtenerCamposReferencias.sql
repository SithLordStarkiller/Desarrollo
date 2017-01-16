
/****** Object:  StoredProcedure [dbo].[ObtenerCamposReferencias]    Script Date: 15/07/2015 05:10:11 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
* Proyecto:				PubliPayments
* Autor:				Alberto Rojas
* Fecha de creación:	04/07/2014
* Descripción:			Obtiene los datos que estan actualizados en respuestas
	@tipo		Cantidad de campos, 0 = todos
* Fecha Modificacion:   01/02/2015
* Modifico:				Alberto Rojas
* Modificacion:			se agrega los filtros para no repetir los campos de camposRespuesta
**********************************************************************/
ALTER PROCEDURE [dbo].[ObtenerCamposReferencias]
				@Tipo INT
				,@reporte INT
AS
BEGIN
	SET NOCOUNT ON;

	IF OBJECT_ID('tempdb.dbo.##tablaTemporalR', 'U') IS NOT NULL
		DROP TABLE ##tablaTemporalR
	
	DECLARE @campos VARCHAR(max) 
			,@sql VARCHAR(max) = ''
	DECLARE @RutaNA VARCHAR (10) ='CSP';
    
	SELECT  @campos = isnull(+ @campos + ', ', '') + '[' + Nombre + ']'
	FROM CamposRespuesta 
	WHERE Tipo >= @tipo  and idFormulario in ( select distinct f.idFormulario FROM Formulario f  INNER JOIN Aplicacion a ON a.idAplicacion = f.idAplicacion
											   WHERE a.idAplicacion = (SELECT valor FROM [CatalogoGeneral] WHERE Llave = 'idAplicacion') AND f.Ruta NOT IN (@RutaNA)) 
							group by Nombre

	SET @sql = 'SELECT idOrden, ' + @campos + ' '
	SET @sql += 'INTO ##tablaTemporalR '
	SET @sql += 'FROM( SELECT r.idOrden,c.Nombre,r.Valor '
	SET @sql += 'FROM [CamposRespuesta] c '
	SET @sql += 'LEFT JOIN [Respuestas] r '
	SET @sql += 'ON c.idCampo = r.idCampo and c.idformulario=r.idFormulario '
	SET @sql += ') d PIVOT ('
	SET @sql += 'MAX(Valor) '
	SET @sql += 'FOR Nombre IN '
	SET @sql += '( ' + @campos
	SET @sql += ') ) piv '
	SET @sql += 'WHERE idOrden > 0; '

	EXEC (@sql)
	
	SELECT o.num_Cred CV_CREDITO
		,t.*
	FROM ##tablaTemporalR t
	INNER JOIN dbo.Ordenes o ON t.idOrden = o.idOrden
	INNER JOIN dbo.Usuario u ON o.idUsuario = u.idUsuario
	WHERE o.Estatus = 4
		AND CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE CASE @reporte
			WHEN 0
				THEN '%%'
			ELSE '%' + CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121) + '%'
			END
END

