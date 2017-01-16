
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author		Alberto Rojas
-- Create date: 2016/05/31
-- Description:	Genera reporte del registro de los asesores
-- =============================================
CREATE PROCEDURE ObtenerReporteRegistroAsesores
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE 
	@sql NVARCHAR (max)
	,@campos NVARCHAR(max)
	,@camposCabecero NVARCHAR(max)
	,@Ruta NVARCHAR(10)='Ra'

	DECLARE @fecha VARCHAR(25) = '%' + CONVERT(VARCHAR(10), CAST(getdate() AS DATETIME), 121) + '%'
	

	
	
	IF OBJECT_ID('tempdb.dbo.##RegistroAsesoresTemp') IS NOT NULL
	BEGIN
		DROP TABLE ##RegistroAsesoresTemp
	END


	SELECT @campos = ISNULL(+ @campos + ', ', '') + '[' + Nombre + ']'
	FROM CamposRespuesta  WITH(NOLOCK)
	WHERE  Tipo >= 0  AND idFormulario IN (SELECT idformulario FROM formulario WHERE RUTA = @Ruta)
	GROUP BY Nombre

	SET @camposCabecero = REPLACE ( @campos , '[FormiikResponseSource]' , '[FormiikResponseSource] as [Captura]' )

	SET @sql = 'SELECT idOrden, ' + @camposCabecero + ' '
	SET @sql += 'INTO ##RegistroAsesoresTemp '
	SET @sql += 'FROM( SELECT r.idOrden,c.Nombre as  NombreCr,r.Valor '
	SET @sql += 'FROM [CamposRespuesta] c WITH(NOLOCK) '
	SET @sql += 'LEFT JOIN [Respuestas] r  WITH(NOLOCK)'
	SET @sql += 'ON c.idCampo = r.idCampo and c.idFormulario=r.idFormulario '
	SET @sql += ' WHERE R.idformulario IN (SELECT idformulario FROM formulario WHERE RUTA = '''+@Ruta+''') ) d PIVOT ('
	SET @sql += 'MAX(Valor) '
	SET @sql += 'FOR NombreCr IN '
	SET @sql += '( ' + @campos
	SET @sql += ') ) piv '
	SET @sql += 'WHERE idOrden > 0; '

	EXEC (@sql)
	
		SET @sql='SELECT '
		SET @sql+='d.NombreDominio'
		SET @sql+=',d.nom_corto'
		SET @sql+=',t.*'
		SET @sql+=' FROM ##RegistroAsesoresTemp t '
		SET @sql+=' INNER JOIN dbo.Ordenes o WITH(NOLOCK) ON t.idOrden = o.idOrden'
		SET @sql+=' INNER JOIN dbo.usuario u WITH(NOLOCK) ON u.idUsuario = o.idUsuario'
		SET @sql+=' INNER JOIN dbo.dominio d WITH(NOLOCK) ON d.idDominio = u.idDominio'
		SET @sql+=' WHERE o.Estatus IN (3) '
		SET @sql+=' AND CONVERT(VARCHAR(20), o.FechaModificacion, 120) LIKE '''+ @fecha +''''

	EXEC (@sql)

END
GO
