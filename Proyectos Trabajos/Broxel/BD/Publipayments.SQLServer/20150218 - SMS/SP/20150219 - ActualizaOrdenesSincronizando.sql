
/****** Object:  StoredProcedure [dbo].[ActualizaEstatusOrdenes]    Script Date: 02/20/2015 14:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Pablo Jaimes
-- Create date: 19/02/2015
-- Description:	Actualiza estatus de orden a Sincronizando 		
-- =============================================
Create PROCEDURE [dbo].[ActualizaOrdenesSincronizando] (
	@TextoOrden VARCHAR(1500) 
	)
AS
BEGIN
	DECLARE @sql VARCHAR(8000) = '';

	SET @sql = @sql + 'UPDATE Ordenes SET Estatus = 6 ,  FechaModificacion = GETDATE()
	 WHERE idOrden IN (' + @TextoOrden + ') and Estatus=1 and idUsuario<>0 '

	EXECUTE sp_sqlexec @sql
END
