/****** Object:  StoredProcedure [dbo].[MonitoreoBaseDatosUltima]    Script Date: 28/11/2016 10:41:24 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************
* Proyecto:				portal.Monitoreo
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	23/11/2016
* Descripción:			Regresa los minutos del ultimo envio en la tabla 
*						BitacoraEnvio
*****************************************************************************/
CREATE PROCEDURE [dbo].[MonitoreoBaseDatosUltima]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT TOP 1 DATEDIFF(minute,fecha,GETDATE()) Valor FROM BitacoraEnvio WITH (NOLOCK) WHERE DATEDIFF(minute,fecha,GETDATE()) > 10 AND FechaEnvio IS NULL Order by id Asc
END
