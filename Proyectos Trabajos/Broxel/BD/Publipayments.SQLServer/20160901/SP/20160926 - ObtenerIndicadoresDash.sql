-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2016-09-26
-- Description:	Obtiene Indicadores a mostrar para el rol indicado y para la ruta indicada
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerIndicadoresDash] @parte INT, @rol INT, @ruta VARCHAR(10) = ''
AS
BEGIN
	DECLARE @NombreRuta VARCHAR(10);

	SELECT @NombreRuta = Ruta
	FROM dbo.Formulario
	WHERE idFormulario = CONVERT(INT, @ruta);

	SELECT [fc_Clave], [fc_Descripcion], [fi_Parte], [fi_Orden], [Ruta], [idRol]
	FROM dbo.Utils_Descripciones with (nolock)
	WHERE fi_Activo = 1
		AND (
			idRol = @rol
			OR idRol = - 99
			)
		AND (Ruta = @NombreRuta OR RUTA = '*')
		AND [fi_Parte] = CASE @parte
			WHEN - 1
				THEN [fi_parte]
			ELSE @parte
			END
	ORDER BY fi_Parte, fi_Orden
END
