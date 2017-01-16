
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/09/22
-- Description:	Inserta los elementos relacionados a elemento lista
-- =============================================
CREATE PROCEDURE InsertaCatalogoXCampo ( 
				@idCampoFormulario int,
				@Texto nvarchar(250),
				@Valor nvarchar(250),
				@Auxiliar nvarchar(250) = null,
				@Ayuda nvarchar(250) = null
) 

AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @idCatalogoCampo int

	BEGIN TRY	
		INSERT INTO [dbo].[CatalogoXCampo]
			   ([idCampoFormulario]
			   ,[Texto]
			   ,[Valor]
			   ,[Auxiliar]
			   ,[Ayuda])
		 VALUES
			   (@idCampoFormulario
			   ,@Texto
			   ,@Valor
			   ,@Auxiliar
			   ,@Ayuda)
		SELECT @idCatalogoCampo = SCOPE_IDENTITY()
	END TRY

	BEGIN CATCH
			SET @idCatalogoCampo = -1
	END CATCH

	SELECT @idCatalogoCampo as idCatalogoCampo
END
GO
