/****** Object:  StoredProcedure [dbo].[ObtieneEstatusPago]    Script Date: 11/11/2016 10:59:27 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	11/11/2016
* Descripción:			Obtiene el catalogo de los estatus de los pagos London
*****************************************************************************/
CREATE PROCEDURE [dbo].[ObtieneEstatusPago]
AS
BEGIN
	BEGIN TRY
	
		SELECT 
			[ID_ESTATUS],
			[CV_ESTATUS_PAGO]
		FROM [CatEstatusPagos] WITH(NOLOCK)
			
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
	
	END CATCH
END