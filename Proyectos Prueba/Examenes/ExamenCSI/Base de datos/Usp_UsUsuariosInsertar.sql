CREATE PROCEDURE Usp_UsUsuarioInsertar

	@Usuario VARCHAR(50),
	@Contrasena Varchar(50),
	@IdTipoUsuario INT

AS

BEGIN

	BEGIN TRY

		BEGIN TRAN
			INSERT INTO UsUsuario (IdTipoUsuario,Usuario,Contrasena) VALUES (@IdTipoUsuario, @Usuario, @Contrasena)
			SELECT @@IDENTITY AS 'Identity'; 
			COMMIT
    END TRY

    BEGIN CATCH

      ROLLBACK
      
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