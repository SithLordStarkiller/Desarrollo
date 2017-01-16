
/****** Object:  StoredProcedure [dbo].[InsertaReporte]    Script Date: 25/01/2016 06:57:46 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ALberto Rojas
-- Create date: 2014-09-11
-- Description:	Actualiza el reporte solicitado
-- =============================================
ALTER PROCEDURE [dbo].[InsertaReporte] @idReporte INT
	,@ReporteTxt NVARCHAR(max)
	,@idPadre INT = 0
AS
BEGIN
	IF (@idReporte > 0)
	BEGIN
		UPDATE [dbo].[Reportes]
		SET [Estatus] = 1
			,[ReporteTxt] = @ReporteTxt
			,[Fecha] = GETDATE()
		WHERE idReporte = @idReporte
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Reportes]
		SET [Estatus] = 1
			,[ReporteTxt] = @ReporteTxt
			,[Fecha] = GETDATE()
		WHERE idPadre = @idPadre
		AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) 
	END
END
