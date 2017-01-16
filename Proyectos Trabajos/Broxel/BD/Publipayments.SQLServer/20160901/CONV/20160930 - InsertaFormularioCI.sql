DECLARE	@return_value int

EXEC	@return_value = [dbo].[InsertaFormulario]
		@Nombre = N'CedulaInspeccion',
		@Descripcion = N'Asignacion de inspecciones',
		@Version = N'1.0',
		@Captura = 1,
		@Ruta = N'CI',
		@EnvioMovil = 1

SELECT	'Return Value' = @return_value