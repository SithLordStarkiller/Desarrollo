SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2016-01-15
-- Description:	Proceso para monitorear en caso de caida del proceso de Envio de ordenes
-- =============================================
CREATE PROCEDURE [dbo].[MonitorearOrdenesEnvio]
AS
BEGIN
	SET NOCOUNT ON;

	-- Cuenta las ordenes que falta por enviar y que tengan mas de una hora
	SELECT COUNT(FechaEnvio)
	FROM BitacoraEnvio WITH (NOLOCK)
	WHERE FechaEnvio IS NULL
		AND DATEDIFF(hour, Fecha, GETDATE()) > 1
END
