
DECLARE	@return_value int

EXEC	@return_value = [dbo].[ActualizaFormulario]
		@idAplicacion = 1,
		@nombre = N'CobSocial',
		@version = N'12.0'

SELECT	'Return Value' = @return_value

GO
