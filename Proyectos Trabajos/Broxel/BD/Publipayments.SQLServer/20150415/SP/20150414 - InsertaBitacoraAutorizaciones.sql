
/****** Object:  StoredProcedure [dbo].[InsertaBitacoraAutorizaciones]    Script Date: 14/04/2015 12:20:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/11
-- Description:	Borra datos de la tabla AutorizacionSMS y manda a bitacora los registros
-- =============================================
ALTER PROCEDURE [dbo].[InsertaBitacoraAutorizaciones]
					@Credito varchar(15)
	
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
		   ,GETDATE() as Fecha from [AutorizacionSMS] WHERE num_Cred=@Credito
		   
		   DELETE from  [AutorizacionSMS] WHERE num_Cred=@Credito
     
	 COMMIT TRANSACTION 
	 select 1 AS exito
	 END TRY
	 BEGIN CATCH
	 ROLLBACK TRANSACTION
	 select -1 AS exito
	 END CATCH
END


