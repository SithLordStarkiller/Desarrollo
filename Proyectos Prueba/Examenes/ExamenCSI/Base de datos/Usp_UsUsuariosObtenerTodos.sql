CREATE PROCEDURE Usp_UsUsuarioObtenerTodos
AS

BEGIN

	BEGIN TRY

		
			SELECT * FROM UsUsuario
    END TRY

    BEGIN CATCH

	  SELECT  
         ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage;  

    END CATCH

  END
GO