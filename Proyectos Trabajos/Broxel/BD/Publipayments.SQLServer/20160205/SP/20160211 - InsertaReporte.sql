
/****** Object:  StoredProcedure [dbo].[InsertaReporte]    Script Date: 11/02/2016 06:59:38 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ALberto Rojas
-- Create date: 2014-09-11
-- Description:	Actualiza el reporte solicitado
-- Modificacion: JARO - 2016-02-11 - Se agrega el parametro tipo, el cual hace la diferencia entre distintos reportes al mismo usuario
-- =============================================
ALTER PROCEDURE [dbo].[InsertaReporte] @idReporte INT
	,@ReporteTxt NVARCHAR(max)
	,@idPadre INT = 0
	,@tipo INT=1
AS
BEGIN
	IF (@idReporte > 0)
	BEGIN
		UPDATE [dbo].[Reportes]
		SET [Estatus] = 1
			,[ReporteTxt] = @ReporteTxt
			,[Fecha] = GETDATE()
		WHERE idReporte = @idReporte
		AND tipo=@tipo
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Reportes]
		SET [Estatus] = 1
			,[ReporteTxt] = @ReporteTxt
			,[Fecha] = GETDATE()
		WHERE idPadre = @idPadre
		AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) 
		AND tipo=@tipo
	END
END
