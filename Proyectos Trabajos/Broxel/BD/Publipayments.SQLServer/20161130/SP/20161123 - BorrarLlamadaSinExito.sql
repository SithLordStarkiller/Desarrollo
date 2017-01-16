
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/11/23
-- Description:	Se encarga de borrar un registro de la tabla de llamadas sin exito y pasar el registro a la tabla de bitacora
-- =============================================
CREATE PROCEDURE BorrarLlamadaSinExito
	@cv_credito VARCHAR(15)
AS
BEGIN
	
	SET NOCOUNT ON;
  
	DECLARE @borrados INT=0

	BEGIN TRY
			INSERT INTO BitacoraLlamadasSinExito 
			SELECT  ID_ARCHIVO,CV_CREDITO,FECHA_LLAMADA,FECHA_ALTA,GETDATE() AS FECHA_MODIFICACION,TELEFONO,ID_RESULTADO FROM llamadassinexito WHERE CV_CREDITO = @cv_credito
		
			DELETE FROM llamadassinexito  WHERE CV_CREDITO = @cv_credito
			SET @borrados =@@rowcount

	END TRY
	BEGIN CATCH 
			IF @@TRANCOUNT > 0
				ROLLBACK TRANSACTION;
			  THROW;
	END CATCH 
	
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;

		SELECT @borrados AS Borrados
			
END
GO

