/****** Object:  StoredProcedure [dbo].[MonitoreoBaseDatosRecibida]    Script Date: 28/11/2016 10:42:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************
* Proyecto:				portal.Monitoreo
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	23/11/2016
* Descripción:			Regresa los minutos de la ultima fecha de recepcion en la tabla 
*						Ordenes
*****************************************************************************/
CREATE PROCEDURE [dbo].[MonitoreoBaseDatosRecibida]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT TOP 1 DATEDIFF(MINUTE,FechaRecepcion,GETDATE()) Valor FROM Ordenes WITH (NOLOCK) WHERE FechaRecepcion IS NOT NULL ORDER BY FechaRecepcion DESC
END
