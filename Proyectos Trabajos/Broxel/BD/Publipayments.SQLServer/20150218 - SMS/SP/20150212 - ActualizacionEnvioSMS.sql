
-- ==========================================================================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/11
-- Description:	Actualiza los campos correspondientes al envio de SMS en tabla AutorizacionSMS
-- ==========================================================================================
CREATE PROCEDURE ActualizacionEnvioSMS
				  @idOrden INT
				  ,@LogId INT
AS
BEGIN
	declare @encontrado int=0
	declare @TotalEnvio int=0

	select @encontrado=id,@TotalEnvio=ISNULL(TotalEnvio,0) FROM AutorizacionSMS WHERE idOrden=@idOrden
	
	if @encontrado!=0
	begin 
	set @TotalEnvio=@TotalEnvio+1
		UPDATE  AutorizacionSMS SET FechaEnvio=GETDATE(),TotalEnvio=@TotalEnvio,LogIdProb=@LogId WHERE idOrden=@idOrden
		RETURN @idOrden;
	end
	else
	begin 
		RETURN - 1;
	end
END
GO

