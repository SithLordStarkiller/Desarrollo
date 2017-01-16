
/****** Object:  StoredProcedure [dbo].[CancelarOrdenCWBitacoraEnvio]    Script Date: 04/04/2016 06:32:33 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ALberto Rojas
-- Create date: 2014-12-15
-- Description:  inserta registro en bitacora envio, para el caso de cancelar Ordenes directamente en el móvil 
-- =============================================
ALTER PROCEDURE [dbo].[CancelarOrdenCWBitacoraEnvio]
							@idOrdenTxt varchar(5000)
AS
BEGIN
	
DECLARE @sql VARCHAR(8000)
	SET @sql = 'INSERT INTO BitacoraEnvio (idOrden,EstatusEnvio,EstatusAnterior,RespuestaEnvio)'
	SET @sql += 'SELECT idOrden,12 EstatusEnvio,1 EstatusAnterior,''Origen-CapturaWeb'' RespuestaEnvio FROM Ordenes WHERE idOrden IN  (' + @idOrdenTxt + ');' 

	-- Se actualiza el usuario Anterior con el mismo valor al usuario actual para mandar la cancelacion al Movil
	SET @sql +='UPDATE Ordenes SET idUsuarioAnterior=idUsuario WHERE idOrden IN (' + @idOrdenTxt + ')'
	EXECUTE sp_sqlexec @sql


END
