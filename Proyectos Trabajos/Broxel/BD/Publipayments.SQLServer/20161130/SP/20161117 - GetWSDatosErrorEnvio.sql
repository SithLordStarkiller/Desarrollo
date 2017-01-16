/****************************************************************************
* Proyecto:				portal.publipayments.com
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	14/10/2016
* Descripción:			Selecciona los datos de error para procesar nuevamente
						en el ws
* Parametros:
*	@ID_ORDEN:			Id de la orden
*	@ID_VISITA:			Id de la visita
*	@CV_ORIGEN:			Estado de la gestion >> P, V, R <<
*****************************************************************************/
CREATE PROCEDURE [dbo].[GetWSDatosErrorEnvio] (
@ID_ORDEN INT,
@ID_VISITA INT,	
@CV_ORIGEN VARCHAR (3)
)
AS
BEGIN
	BEGIN TRY
	
		SELECT 
			[ID_ORDEN], 
			[ID_VISITA],		
			[CV_CREDITO],
			[FECHA_CAPTURA],
			[CV_RUTA],
			[CV_DESPACHO],
			[CV_USER],
			[CV_ALTER_USER],
			[CV_ORIGEN],
			[CV_MOVIL],
			[NUM_MOV], 
			[CV_REGISTRO]
		FROM WSDatosErroresEnvio WITH(NOLOCK) WHERE ID_ORDEN = @ID_ORDEN AND ID_VISITA = @ID_VISITA AND CV_ORIGEN = @CV_ORIGEN
			
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS Codigo, ERROR_MESSAGE() AS Descripcion;
	
	END CATCH
END