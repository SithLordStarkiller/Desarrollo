/****** Object:  StoredProcedure [dbo].[InsertaCamposXml]    Script Date: 06/12/2016 10:17:16 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Laura Anayeli Dotor Mejía
-- Create date: 05/12/2016
-- Description:	Inserta campos de archivo Xml
-- =============================================
CREATE PROCEDURE [dbo].[InsertaCamposXml]
@Ruta VARCHAR(10),
@NombreCampo NVARCHAR(50),
@Valor NVARCHAR(350)
AS
BEGIN
	SET NOCOUNT ON;

	--Declaraciones
	DECLARE @maxIdCampo INT = 1 --Max + 1 del id del campo que se quiere insertar
	
	BEGIN TRY
		
		BEGIN TRANSACTION Campo

		IF EXISTS(SELECT 1 FROM [dbo].[CatRespuestasWS] WITH(NOLOCK) WHERE Nombre = @NombreCampo AND Ruta = @Ruta)
		BEGIN
			
			UPDATE [dbo].[CatRespuestasWS] SET Descripcion = @Valor WHERE Nombre = @NombreCampo AND Ruta = @Ruta
			
			SELECT @maxIdCampo = Id FROM [dbo].[CatRespuestasWS] WHERE Nombre = @NombreCampo AND Ruta = @Ruta
		END
		ELSE
		BEGIN

			SELECT @maxIdCampo = MAX(Id) + 1 FROM [dbo].[CatRespuestasWS]
		
			INSERT INTO [dbo].[CatRespuestasWS] 
				(
					[Id],
					[Nombre],
					[Ruta],
					[Descripcion]
				)
				VALUES 
				(
					@maxIdCampo,
					@NombreCampo,
					@Ruta,					
					@Valor
				)

				SELECT @maxIdCampo AS NvoCampo, 'OK' AS Descripcion;

				COMMIT TRANSACTION Campo
		END
	END TRY
	BEGIN CATCH
		SELECT -1  AS Codigo, ERROR_MESSAGE() AS Descripcion;
		ROLLBACK TRANSACTION Campo;	
	END CATCH
END