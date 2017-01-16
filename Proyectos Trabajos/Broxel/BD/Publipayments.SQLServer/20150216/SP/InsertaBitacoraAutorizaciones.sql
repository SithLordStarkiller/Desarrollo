
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
           ,[TextoRespuestaSMS]
           ,[TotalEnvio]
		   ,GETDATE() as Fecha from [AutorizacionSMS] WHERE idOrden=@idOrden
		   
		   DELETE from  [AutorizacionSMS] WHERE idOrden=@idOrden
     
END


