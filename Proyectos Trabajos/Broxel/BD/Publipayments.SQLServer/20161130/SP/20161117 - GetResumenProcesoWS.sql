/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	18/10/2016
* Descripción:			Obtiene el resumen de los registros procesados
						en el ws, por día
*****************************************************************************/
CREATE PROCEDURE [dbo].[GetResumenProcesoWS]
AS
BEGIN
	DECLARE @FECHA_RESUMEN DATE = CONVERT (DATE, GETDATE() - 1)

	BEGIN TRY
	
		SELECT 
			[CV_RUTA], 
			[TOTAL_ENVIADO], 
			[TOTAL_ERROR], 	
			[TOTAL_MOVIMIENTOS], 
			[EDO_GESTION],
			[FECHA]			
		FROM WSTotalesEnvio WITH(NOLOCK) WHERE [FECHA] = @FECHA_RESUMEN
			
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
	
	END CATCH
END