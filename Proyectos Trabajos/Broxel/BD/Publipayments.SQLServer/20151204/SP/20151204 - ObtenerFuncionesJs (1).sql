

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2014-11-26
-- Description:	Obtiene las funciones a ejecutar para el manejo visual del formulario
-- si el valor de entrada es 0 el resultado sera informacion del ultimo fromulario cargado
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerFuncionesJs] @idformulario INT
AS
BEGIN
	SELECT *
	FROM CatFuncionesJS WITH (NOLOCK)
	WHERE idformulario = CASE 
			WHEN @idformulario = 0
				THEN (
						SELECT MAX(idFormulario)
						FROM Formulario WITH (NOLOCK)
						)
			ELSE @idformulario
			END
	ORDER BY Nombre
		,idFuncionJS
END
