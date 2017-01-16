
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*************************************************************************************************
-- Author:		Alberto Rojas
-- Create date: 20160121
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

	DECLARE @sqlOrigen NVARCHAR(2000)=''
	DECLARE @sqlWhere NVARCHAR(2000)=''
	DECLARE @sql NVARCHAR(4000)=''

	
	Select 'CV_CREDITO,CV_PROVEEDOR,CV_CONTRATO,CV_DELEGACION,TX_NOMBRE_DESPACHO,CV_DESPACHO,CC_DESPACHO,FECHA_ULT_ACTUALIZACION,FECHA_REPORTE|'
			+'CV_CREDITO,CV_PROVEEDOR,CV_CONTRATO,CV_DELEGACION,TX_NOMBRE_DESPACHO,CV_DESPACHO,CC_DESPACHO,FECHA_ULT_ACTUALIZACION,FECHA_REPORTE' as cabecero


	SET @sqlOrigen+='SELECT c.CV_CREDITO,c.CV_PROVEEDOR,c.CV_CONTRATO,c.CV_DELEGACION,c.TX_NOMBRE_DESPACHO,c.CV_DESPACHO,c.CC_DESPACHO,a.fecha as FECHA_ULT_ACTUALIZACION,GETDATE() as  FECHA_REPORTE'
				  +' FROM creditos c WITH(NOLOCK) INNER JOIN Archivos a WITH(NOLOCK) ON c.id_archivo=a.id'
		
	 IF(@Delegacion !='%')
		SELECT @sqlWhere += CASE LTRIM(RTRIM(@sqlWhere)) WHEN '' THEN ' WHERE c.CV_DELEGACION ='''+@Delegacion+'''' ELSE ' AND c.CV_DELEGACION ='''+@Delegacion+'''' END
	 
	 IF(@Despacho !='%')
		SELECT @sqlWhere += CASE LTRIM(RTRIM(@sqlWhere)) WHEN '' THEN ' WHERE c.TX_NOMBRE_DESPACHO ='''+@Despacho+'''' ELSE ' AND c.TX_NOMBRE_DESPACHO ='''+@Despacho+'''' END
	
	
	IF(@idSupervisor != '%')
	BEGIN
		SET @sqlOrigen +=' INNER JOIN ordenes o WITH(NOLOCK) ON o.num_cred=c.CV_CREDITO '
		SELECT @sqlWhere += CASE LTRIM(RTRIM(@sqlWhere)) WHEN '' THEN ' WHERE o.idUsuarioPadre ='''+@idSupervisor+'''' ELSE ' AND o.idUsuarioPadre ='''+@idSupervisor+'''' END
	END
		
	SET @sql = CONVERT(VARCHAR(4000),(@sqlOrigen+@sqlWhere))
	Execute sp_Executesql @sql

	END

