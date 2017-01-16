
/****** Object:  StoredProcedure [dbo].[ObtenerFuncionesJs]    Script Date: 06/06/2016 04:43:04 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


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
	ORDER BY 
		Nombre
END
