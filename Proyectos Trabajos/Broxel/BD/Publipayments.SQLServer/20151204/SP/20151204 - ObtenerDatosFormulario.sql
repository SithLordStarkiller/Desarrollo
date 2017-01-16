

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015-01-14
-- Description:	obtiene los datos de un formulario por la ruta
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerDatosFormulario] @Ruta VARCHAR(10)
AS
BEGIN
	SELECT f.idFormulario
		,f.idAplicacion
		,f.Nombre
		,f.Version
		,a.Productivo
		,f.Captura
		,f.Ruta
		,f.Descripcion
		,f.FechaAlta
		,f.Estatus
	FROM Formulario f WITH (NOLOCK)
	INNER JOIN Aplicacion a WITH (NOLOCK) ON a.idAplicacion = f.idAplicacion
	WHERE f.Estatus = 1
		AND a.idAplicacion = (
			SELECT valor
			FROM [CatalogoGeneral] WITH (NOLOCK)
			WHERE Llave = 'idAplicacion'
			)
		AND f.Ruta = CASE 
			WHEN ISNULL(@Ruta, '') != ''
				THEN @Ruta
			ELSE f.Ruta
			END
			ORDER BY RUTA
END
