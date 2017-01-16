-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2016/09/20
-- Description:	Actualiza el dictamen en la orden, e inserta el dictamen en el catalogo si no existe
-- =============================================
ALTER PROCEDURE [dbo].[ActualizarDictamenOrden]
	-- Add the parameters for the stored procedure here
	@idOrden INT
AS
BEGIN
	SET NOCOUNT ON;
	 
	DECLARE @ID_ARCHIVO INT = 0

	-- Inserta el dictamen de la orden si este no existe
	INSERT INTO CatDictamen ([Clave], [Descripcion], [Nombre], [CV_RUTA])
	SELECT '' Clave, r.Valor Descripcion, c.Nombre Nombre, o.CvRuta
	FROM dbo.Respuestas r
	INNER JOIN [dbo].[CamposRespuesta] c ON r.idCampo = c.idCampo
	LEFT JOIN dbo.Ordenes o ON r.idOrden = o.idOrden
	LEFT JOIN dbo.CatDictamen d ON c.Nombre = d.Nombre
		AND d.CV_RUTA = o.CvRuta
	WHERE LEFT(c.Nombre, 8) = 'Dictamen'
		AND r.idOrden = @idOrden
		AND d.idCatalogo IS NULL
		AND o.idOrden IS NOT NULL

	SELECT @ID_ARCHIVO = SCOPE_IDENTITY() 
	-- Actualizar la orden 
	UPDATE o
	SET o.idDictamen = d.idCatalogo
	FROM dbo.Respuestas r
	INNER JOIN [dbo].[CamposRespuesta] c ON r.idCampo = c.idCampo
	LEFT JOIN dbo.Ordenes o ON r.idOrden = o.idOrden
	LEFT JOIN dbo.CatDictamen d ON c.Nombre = d.Nombre
		AND d.CV_RUTA = o.CvRuta
	WHERE LEFT(c.Nombre, 8) = 'Dictamen'
		AND d.idCatalogo IS NOT NULL
		AND o.idOrden = @idOrden
		AND o.Estatus IN (3, 4)

	EXEC dbo.ActualizaClaveDictamen @ID_ARCHIVO
END

