
/****** Object:  StoredProcedure [dbo].[CancelarOrdenCWBitacoraEnvio]    Script Date: 08/04/2016 04:00:53 p.m. ******/
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
							@idOrdenTxt varchar(20)
AS
BEGIN


	DECLARE @TextoOrdenesFiltrados VARCHAR(20) = '';
	DECLARE @idOrdenTxtTemp VARCHAR(20) = @idOrdenTxt;

		IF((SELECT 1 FROM ordenes WITH(NOLOCK) where idorden=convert (int,@idOrdenTxt) and tipo like '%C%') = 1)
		BEGIN
			EXEC FiltrarEstatusUsuarioOrdenes @idOrdenTxt
			,@TextoOrdenesFiltrados OUTPUT

			IF(len(@TextoOrdenesFiltrados)>0)
				SET @idOrdenTxt = '0'
		END
	
	
DECLARE @sql VARCHAR(8000)
	SET @sql = 'INSERT INTO BitacoraEnvio (idOrden,EstatusEnvio,EstatusAnterior,RespuestaEnvio)'
	SET @sql += 'SELECT idOrden,12 EstatusEnvio,1 EstatusAnterior,''Origen-CapturaWeb'' RespuestaEnvio FROM Ordenes WHERE idOrden IN  (' + @idOrdenTxt + ');' 

	-- Se actualiza el usuario Anterior con el mismo valor al usuario actual para mandar la cancelacion al Movil
	SET @sql +='UPDATE Ordenes SET idUsuarioAnterior=idUsuario, idusuario= case tipo WHEN ''C'' then -111 when ''CS'' then -111 else idusuario end WHERE idOrden IN (' + @idOrdenTxtTemp + ')'
	EXECUTE sp_sqlexec @sql



END
