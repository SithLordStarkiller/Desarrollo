
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDatosFormulario]    Script Date: 17/02/2015 02:15:36 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
	FROM Formulario f
	INNER JOIN Aplicacion a ON a.idAplicacion = f.idAplicacion
	WHERE f.Estatus = 1
		AND a.idAplicacion = (
			SELECT valor
			FROM [CatalogoGeneral]
			WHERE Llave = 'idAplicacion'
			)
		AND f.Ruta = CASE 
			WHEN ISNULL(@Ruta, '') != ''
				THEN @Ruta
			ELSE f.Ruta
			END
			ORDER BY RUTA
END
