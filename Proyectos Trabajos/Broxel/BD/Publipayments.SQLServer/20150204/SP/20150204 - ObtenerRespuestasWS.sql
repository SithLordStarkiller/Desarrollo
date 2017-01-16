SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 04/02/2015
-- Description:	Obtiene las respuestas para enviar al webService
-- =============================================
CREATE PROCEDURE ObtenerRespuestasWS @idOrden INT
	,@Ruta VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (
			SELECT TOP 1 1
			FROM [dbo].[Respuestas] r WITH (NOLOCK)
			INNER JOIN [dbo].[CamposRespuesta] c WITH (NOLOCK) ON r.idCampo = c.idCampo
			LEFT JOIN dbo.CatRespuestasWS w WITH (NOLOCK) ON c.Nombre = w.Nombre
				AND w.Ruta = @Ruta
			WHERE r.idorden = @idOrden
				AND r.Valor IS NOT NULL
				AND w.id IS NULL
			ORDER BY w.id
			)
	BEGIN
		BEGIN TRY
			BEGIN TRAN respuestasWS

			DECLARE @ultimo INT

			SELECT @ultimo = MAX(id)
			FROM CatRespuestasWS

			INSERT INTO CatRespuestasWS
			SELECT @ultimo + ROW_NUMBER() OVER (
					ORDER BY c.idCampo
					) id
				,c.Nombre
				,@Ruta Ruta
				,'' Desripcion
			FROM [dbo].[Respuestas] r
			INNER JOIN [dbo].[CamposRespuesta] c ON r.idCampo = c.idCampo
			LEFT JOIN dbo.CatRespuestasWS w ON c.Nombre = w.Nombre
				AND w.Ruta = @Ruta
			WHERE r.idorden = @idOrden
				AND r.Valor IS NOT NULL
				AND w.id IS NULL
			ORDER BY c.idCampo

			COMMIT TRAN respuestasWS
		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0
				ROLLBACK TRAN respuestasWS;

			DECLARE @ErrorMessage NVARCHAR(4000);
			DECLARE @ErrorSeverity INT;
			DECLARE @ErrorState INT;

			SELECT @ErrorMessage = ERROR_MESSAGE()
				,@ErrorSeverity = ERROR_SEVERITY()
				,@ErrorState = ERROR_STATE();

			RAISERROR (
					@ErrorMessage
					,@ErrorSeverity
					,@ErrorState
					)

			RETURN - 1
		END CATCH
	END

	SELECT (ltrim(str(ISNULL(w.id, '0'), 10, 0)) + '|' + left(r.Valor, 350)) AS Dato
	FROM [dbo].[Respuestas] r WITH (NOLOCK)
	INNER JOIN [dbo].[CamposRespuesta] c WITH (NOLOCK) ON r.idCampo = c.idCampo
	LEFT JOIN dbo.CatRespuestasWS w WITH (NOLOCK) ON c.Nombre = w.Nombre
		AND w.Ruta = @Ruta
	WHERE r.idorden = @idOrden
		AND r.Valor IS NOT NULL
	ORDER BY w.id
END
GO