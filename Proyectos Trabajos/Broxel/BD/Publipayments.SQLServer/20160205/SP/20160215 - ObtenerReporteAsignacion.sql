
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*************************************************************************************************
-- Author:		Alberto Rojas
-- Create date: 2016/012/1
-- Description:	Reporte que muestra la asignacion actual que se tiene para PPM
**************************************************************************************************/


alter PROCEDURE [dbo].[ObtenerReporteAsignacion] (
	@Delegacion VARCHAR(2)='%',
	@Despacho VARCHAR(20)='%',
	@idSupervisor VARCHAR(5)='%'
	)
AS
BEGIN
	SET NOCOUNT ON;

	IF OBJECT_ID('tempdb.dbo.#ClaveDespachos', 'U') IS NOT NULL
		DROP TABLE #ClaveDespachos
	
	Select 'CV_CREDITO,CV_PROVEEDOR,CV_CONTRATO,CV_DELEGACION,TX_NOMBRE_DESPACHO,CV_DESPACHO,CC_DESPACHO,FECHA_ULT_ACTUALIZACION|'
			+'CV_CREDITO,CV_PROVEEDOR,CV_CONTRATO,CV_DELEGACION,ClaveDespachos,FECHA_ULT_ACTUALIZACION' as cabecero


	-- Se llena tabla temporal para reducir datos  
		 SELECT  DISTINCT IDENTITY(int, 1,1) idClave,TX_NOMBRE_DESPACHO,CV_DESPACHO,COALESCE(CC_DESPACHO,'') AS CC_DESPACHO INTO  #ClaveDespachos FROM  CREDITOS  WITH(NOLOCK)

		-- Tabla catalogo de datos clave
		SELECT * FROM #ClaveDespachos

	IF(@idSupervisor != '%')
	BEGIN
		SELECT c.CV_CREDITO,c.CV_PROVEEDOR,c.CV_CONTRATO,c.CV_DELEGACION,cd.idClave as ClaveDespachos,a.fecha as FECHA_ULT_ACTUALIZACION
			FROM creditos c WITH(NOLOCK) INNER JOIN Archivos a WITH(NOLOCK) ON c.id_archivo=a.id
			INNER JOIN ordenes o WITH(NOLOCK) ON o.num_cred=c.CV_CREDITO
			INNER JOIN #ClaveDespachos cd on cd.TX_NOMBRE_DESPACHO=c.TX_NOMBRE_DESPACHO AND cd.CV_DESPACHO=c.CV_DESPACHO AND cd.CC_DESPACHO=COALESCE(c.CC_DESPACHO,'')
			WHERE c.CV_DELEGACION =  CASE @Delegacion WHEN '%' THEN c.CV_DELEGACION ELSE @Delegacion END 
			AND o.iddominio =  CASE @Despacho WHEN '%' THEN  o.iddominio ELSE  CONVERT(INT,@Despacho)  END 
			AND o.idUsuarioPadre =  CONVERT(INT,@idSupervisor)
	END
	ELSE
	BEGIN 
		SELECT c.CV_CREDITO,c.CV_PROVEEDOR,c.CV_CONTRATO,c.CV_DELEGACION,cd.idClave as ClaveDespachos,a.fecha as FECHA_ULT_ACTUALIZACION
			FROM creditos c WITH(NOLOCK) INNER JOIN Archivos a WITH(NOLOCK) ON c.id_archivo=a.id
			INNER JOIN #ClaveDespachos cd on cd.TX_NOMBRE_DESPACHO=c.TX_NOMBRE_DESPACHO AND cd.CV_DESPACHO=c.CV_DESPACHO AND cd.CC_DESPACHO=COALESCE(c.CC_DESPACHO,'')
			WHERE c.CV_DELEGACION =  CASE @Delegacion WHEN '%' THEN c.CV_DELEGACION ELSE @Delegacion END 
			AND c.TX_NOMBRE_DESPACHO =  CASE @Despacho WHEN '%' THEN c.TX_NOMBRE_DESPACHO ELSE @Despacho END 
 
	END 
END