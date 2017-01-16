-- =============================================
-- Author:		Pablo Rendon
-- Create date: 25/09/2015
-- Description:	Obtener datos de la orden por ID
-- =============================================
CREATE PROCEDURE ObtenerOrdenXId(
@idOrden int
)
AS
BEGIN
	select * from ordenes WITH(NOLOCK)
	where idOrden = @idOrden
END