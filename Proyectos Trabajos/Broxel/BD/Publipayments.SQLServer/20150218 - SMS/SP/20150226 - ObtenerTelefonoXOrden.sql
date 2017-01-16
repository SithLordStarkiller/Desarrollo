
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/26
-- Description:	Obtiene los registros que se tengan en la tabla de AutorizacionSMS 
-- =============================================
CREATE PROCEDURE ObtenerTelefonoXOrden
				 @idOrden int
AS
BEGIN
	SET NOCOUNT ON;

	select Telefono from AutorizacionSMS where idOrden=@idOrden

	SET NOCOUNT OFF;
END
GO
