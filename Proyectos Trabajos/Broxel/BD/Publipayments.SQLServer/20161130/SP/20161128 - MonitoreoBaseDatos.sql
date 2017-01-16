/****** Object:  StoredProcedure [dbo].[MonitoreoBaseDatos]    Script Date: 28/11/2016 10:47:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************
* Proyecto:				portal.Monitoreo
* Autor:				Laura Anayeli Dotor Mejia
* Fecha de creación:	23/11/2016
* Descripción:			Revisa que la base de datos esté disponible
*****************************************************************************/
CREATE PROCEDURE [dbo].[MonitoreoBaseDatos]
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT TOP 1 'OK' OK FROM Usuario WITH (NOLOCK) WHERE idUsuario = 0
END
