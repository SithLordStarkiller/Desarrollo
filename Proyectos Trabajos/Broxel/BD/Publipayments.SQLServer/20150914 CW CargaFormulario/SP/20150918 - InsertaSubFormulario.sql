
-- =============================================
-- Author:		Alberto rojas
-- Create date: 2015/09/18
-- Description:	Guarda los registros pertenecientes a un subformulario
-- =============================================
CREATE PROCEDURE InsertaSubFormulario (@idformulario int,@Subformulario nvarchar(50),@Clase nvarchar(50))
	
AS
BEGIN
	SET NOCOUNT ON;

	
DECLARE @idSubformulario int

	SET NOCOUNT ON;
		BEGIN TRY	
		INSERT INTO [dbo].[SubFormulario]
           ([idFormulario]
           ,[SubFormulario]
           ,[Clase])
     VALUES
           (@idformulario
           ,@Subformulario
           ,@Clase)

		SELECT @idSubformulario = SCOPE_IDENTITY()
		END TRY

		BEGIN CATCH
			SET @idSubformulario = -1
		END CATCH

	SELECT @idSubformulario as idSubFormulario
END
GO