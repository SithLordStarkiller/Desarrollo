

DECLARE	@return_value int

EXEC	@return_value = [dbo].[ActualizaFormulario]
		@idAplicacion = 1,
		@nombre = N'RDST',
		@version = N'2.0'

SELECT	'Return Value' = @return_value

GO
