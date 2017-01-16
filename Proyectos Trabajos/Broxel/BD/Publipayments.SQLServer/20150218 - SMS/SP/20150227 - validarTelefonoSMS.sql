
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/27
-- Description:	valida si un numero de telefono puede ser utilizado
-- =============================================
CREATE PROCEDURE ValidarTelefonoSMS
				@Telefono char(10)
AS
BEGIN
	SET NOCOUNT ON;
		SELECT 1 as valido FROM AutorizacionSMS WHERE Telefono=@Telefono
	SET NOCOUNT OFF;

END
GO
