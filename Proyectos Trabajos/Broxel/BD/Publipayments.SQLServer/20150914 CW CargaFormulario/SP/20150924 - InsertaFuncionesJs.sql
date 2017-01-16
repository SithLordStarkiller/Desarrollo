
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/09/24
-- Description:	Inserta los elementos relacionados a elemento lista
-- =============================================
CREATE PROCEDURE InsertaFuncionesJs ( 
				@Nombre varchar(50)='',
				@Validacion varchar(8000)=null,
				@FuncionSI varchar(8000),
				@FuncionNo varchar(8000)=null,
				@idFormulario int 
) 

AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @idFuncionJS int

	BEGIN TRY	
		INSERT INTO [dbo].[CatFuncionesJS]
           ([Nombre]
           ,[Validacion]
           ,[FuncionSI]
           ,[FuncionNo]
           ,[idFormulario])
     VALUES
           (@Nombre
           ,@Validacion
           ,@FuncionSI
           ,@FuncionNo
           ,@idFormulario)

		   SELECT @idFuncionJS = SCOPE_IDENTITY()
	END TRY

	BEGIN CATCH
			SET @idFuncionJS = -1
	END CATCH

	SELECT @idFuncionJS as idFuncionJS
END
GO
