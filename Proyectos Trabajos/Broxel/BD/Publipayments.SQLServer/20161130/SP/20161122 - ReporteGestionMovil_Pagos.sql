
/****** Object:  StoredProcedure [dbo].[ReporteGestionMovil_Pagos]    Script Date: 22/11/2016 12:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/09/22
-- Description:	Obtiene la informacion referente al Reporte de Pagos
-- =============================================
ALTER PROCEDURE [dbo].[ReporteGestionMovil_Pagos]
		@idDominio INT=0,
		@Ruta VARCHAR(10)='',
		@Delegacion VARCHAR(2)='',
		@Proceso INT = 0
AS
BEGIN
	
	SET NOCOUNT ON;

				
		
		IF(@Proceso = 1)
		BEGIN

				
		SELECT Pago.iddominio,Pago.NombreDominio,(NoPago.Sinpago-Pago.Total) AS 'SinPago',Pago.PagoParcial,Pago.PagoTotal,Pago.Total FROM (
				SELECT iddominio,NombreDominio,ISNULL(Parcial,0) AS 'PagoParcial',ISNULL(Total,0) AS 'PagoTotal',SUM(ISNULL(Parcial,0) + ISNULL(Total,0)) as Total 
				 FROM (
						SELECT  d.iddominio,d.NombreDominio,cEp.CV_ESTATUS_PAGO,COUNT(p.CV_ESTATUS_PAGO) AS TotalPago FROM pagos p WITH(NOLOCK) 
						INNER JOIN catEstatuspagos cEp ON cEp.ID_ESTATUS=p.CV_ESTATUS_PAGO
						INNER JOIN creditos c WITH(NOLOCK) ON p.CV_CREDITO=c.CV_CREDITO 
						INNER JOIN dominio d  WITH(NOLOCK) ON d.nom_corto=c.TX_NOMBRE_DESPACHO  
						WHERE d.iddominio = CASE WHEN @idDominio > 0 THEN @idDominio ELSE d.iddominio END 
						AND c.CV_RUTA = CASE WHEN @Ruta !='' THEN @Ruta ELSE c.CV_RUTA END 
						GROUP BY d.iddominio,d.NombreDominio ,cEp.CV_ESTATUS_PAGO
					) AS tabla  PIVOT (
							MAX (TotalPago)
							FOR CV_ESTATUS_PAGO in ([Parcial],[Total] )
							)
						AS pvt
				GROUP BY iddominio,NombreDominio,Parcial,Total
				) AS Pago INNER JOIN (
					SELECT   d.iddominio,d.nombreDominio,COUNT(d.iddominio) AS 'Sinpago'  FROM creditos c WITH(NOLOCK)
					INNER JOIN dominio d  WITH(NOLOCK) ON d.nom_corto=c.TX_NOMBRE_DESPACHO  
					WHERE  c.CV_RUTA = CASE WHEN @Ruta !='' THEN @Ruta ELSE c.CV_RUTA END 
					GROUP BY  d.iddominio,d.nombreDominio
				) AS NoPago ON Pago.iddominio=NoPago.iddominio



		END
		ELSE IF (@Proceso=2)
		BEGIN
				SELECT Pago.iddominio,Pago.NombreDominio,Pago.Delegacion,(NoPago.Sinpago-Pago.Total) AS 'SinPago',Pago.PagoParcial,Pago.PagoTotal,Pago.Total FROM (

				SELECT iddominio,NombreDominio,Delegacion,ISNULL(Parcial,0) AS 'PagoParcial',ISNULL(Total,0) AS 'PagoTotal',SUM(ISNULL(Parcial,0) + ISNULL(Total,0)) as Total 
			 FROM (
					SELECT  d.iddominio,d.NombreDominio,cd.Descripcion AS Delegacion ,cEp.CV_ESTATUS_PAGO,COUNT(p.CV_ESTATUS_PAGO) AS TotalPago FROM pagos p WITH(NOLOCK) 
					INNER JOIN catEstatuspagos cEp ON cEp.ID_ESTATUS=p.CV_ESTATUS_PAGO
					INNER JOIN creditos c WITH(NOLOCK) ON p.CV_CREDITO=c.CV_CREDITO 
					INNER JOIN dominio d WITH(NOLOCK) ON d.nom_corto=c.TX_NOMBRE_DESPACHO  
					INNER JOIN CatDelegaciones cd WITH(NOLOCK) ON cd.Delegacion=c.CV_DELEGACION 
					WHERE d.iddominio = CASE WHEN @idDominio > 0 THEN @idDominio ELSE d.iddominio END 
					AND c.CV_RUTA = CASE WHEN @Ruta !='' THEN @Ruta ELSE c.CV_RUTA END  
					AND cd.Delegacion= CASE WHEN @Delegacion !='' THEN @Delegacion ELSE cd.Delegacion END
					GROUP BY d.iddominio,d.NombreDominio ,cd.Descripcion,cEp.CV_ESTATUS_PAGO
				) AS tabla  PIVOT (
				MAX (TotalPago)
					FOR CV_ESTATUS_PAGO IN ([Parcial],[Total] )
					)
				AS pvt
			 GROUP BY iddominio,NombreDominio,Delegacion,Parcial,Total
				) AS Pago INNER JOIN (
					SELECT   d.iddominio,d.nombreDominio,COUNT(d.iddominio) AS 'Sinpago'  FROM creditos c WITH(NOLOCK)
					INNER JOIN dominio d  WITH(NOLOCK) ON d.nom_corto=c.TX_NOMBRE_DESPACHO  
					WHERE  c.CV_RUTA = CASE WHEN @Ruta !='' THEN @Ruta ELSE c.CV_RUTA END 
					GROUP BY  d.iddominio,d.nombreDominio
				) AS NoPago ON Pago.iddominio=NoPago.iddominio

		END
END
