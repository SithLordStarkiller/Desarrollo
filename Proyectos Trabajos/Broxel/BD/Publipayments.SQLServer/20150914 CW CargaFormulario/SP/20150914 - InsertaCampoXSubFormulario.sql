
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/09/14
-- Description:	Inserta un nuevo elemento
-- =============================================
CREATE PROCEDURE InsertaCampoXSubFormulario (
	@idSubFormulario int,
	@idTipoCampo int,
	@NombreCampo nvarchar(250),
	@Texto nvarchar(500),
	@Valor nvarchar(250)= null ,
	@ClasesLinea nvarchar(50) = null,
	@ClasesTexto nvarchar(50) = null,
	@ClasesValor nvarchar(50) = null,
	@Orden int,
	@Validacion nvarchar(200) = null)
AS
BEGIN

DECLARE @idCampoFormulario int

	SET NOCOUNT ON;

		BEGIN TRY	
			INSERT INTO [dbo].[CamposXSubFormulario]
				   ([idSubFormulario]
				   ,[idTipoCampo]
				   ,[NombreCampo]
				   ,[Texto]
				   ,[Valor]
				   ,[ClasesLinea]
				   ,[ClasesTexto]
				   ,[ClasesValor]
				   ,[Orden]
				   ,[Validacion])
			 VALUES
				   (@idSubFormulario
				   ,@idTipoCampo
				   ,@NombreCampo
				   ,@Texto
				   ,@Valor
				   ,@ClasesLinea
				   ,@ClasesTexto
				   ,@ClasesValor
				   ,@Orden
				   ,@Validacion)
				   
		SELECT @idCampoFormulario = SCOPE_IDENTITY()
		END TRY

		BEGIN CATCH
			SET @idCampoFormulario = -1
		END CATCH

	SELECT @idCampoFormulario idCampoFormulario

END
GO