 
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/11
-- Description:	Borra datos de la tabla AutorizacionSMS y manda a bitacora los registros
-- =============================================
CREATE PROCEDURE InsertaBitacoraAutorizaciones
					@idOrden INT
	
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

	BEGIN TRY
		INSERT INTO [BitacoraAutorizacionSMS] SELECT 
			[num_Cred]
           ,[idOrden]
           ,[Telefono]
           ,[Clave]
           ,[FechaAlta]
           ,[FechaEnvio]
		   ,[LogIdProb]
           ,[FechaRespEnvioProb]
           ,[FechaRespuestaSMS]
           ,[TotalEnvio]
		   ,GETDATE() as Fecha from [AutorizacionSMS] WHERE idOrden=@idOrden
		   
		   DELETE from  [AutorizacionSMS] WHERE idOrden=@idOrden
     
	 COMMIT TRANSACTION 
	 select 1 AS exito
	 END TRY
	 BEGIN CATCH
	 ROLLBACK TRANSACTION
	 select -1 AS exito
	 END CATCH
END


