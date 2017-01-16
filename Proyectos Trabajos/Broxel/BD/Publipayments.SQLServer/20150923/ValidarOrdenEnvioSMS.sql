/*
 Proyecto				London-PubliPayments-Formiik
 Autor				Pablo Rendon
 Fecha de creación	03092015
 Descripción			Valida situacion de orden para envio de SMS
 */
CREATE PROCEDURE [dbo].[ValidarOrdenEnvioSMS] (
	@Telefono CHAR(10)
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @situacion INT = - 1

	select @situacion = ord.estatus from autorizacionsms us
	inner join ordenes ord
	on ord.idOrden = us.idOrden
	where us.telefono = @Telefono

	IF(@situacion = 4)	
		BEGIN
			SELECT -1 'enviar'
		END
	ELSE
		BEGIN
			SELECT 1 'enviar'
		END
END
