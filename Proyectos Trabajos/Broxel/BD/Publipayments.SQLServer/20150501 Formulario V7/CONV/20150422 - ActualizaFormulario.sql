

DECLARE	@return_value int

EXEC	@return_value = [dbo].[ActualizaFormulario]
		@idAplicacion = 1,
		@nombre = N'CobSocial',
		@version = N'7.0'

SELECT	'Return Value' = @return_value

GO
