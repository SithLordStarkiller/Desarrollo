
/****** Object:  StoredProcedure [dbo].[CancelarOrdenes]    Script Date: 12/08/2015 05:22:26 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Alberto Rojas
* Fecha de creación:	08/09/2014
* Descripción:			Borra registro en tabla Orden y respalda en BitacoraOrdenes
*Modificacion:			20150813-JARO- Se agrego el bloqueo cuando las ordenes a cancelar no se encuentren en la tabla de bitacora envio
**********************************************************************/
ALTER PROCEDURE [dbo].[CancelarOrdenes] 
	@TextoOrdenes VARCHAR(1500)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @indice INT
		,@orden VARCHAR(20)
		,@cantidad INT = 0

	SET @indice = charindex(',', @TextoOrdenes)

	WHILE (@indice != 0)
	BEGIN
		SET @orden = left(@TextoOrdenes, @indice - 1)
		SET @TextoOrdenes = RIGHT(@TextoOrdenes, len(@TextoOrdenes) - @indice)
		
		IF((select 1 from bitacoraenvio  WITH (NOLOCK)  where idorden=CONVERT(INT, @orden) and FechaEnvio is null) is null)
		BEGIN 
			DELETE FROM [BitacoraOrdenes]   where idOrden = CONVERT(INT, @orden)
			INSERT INTO [BitacoraOrdenes] SELECT  [idOrden]
		  ,[idPool]
		  ,[num_Cred]
		  ,[idUsuario]
		  ,[idUsuarioPadre]
		  ,[idUsuarioAlta]
		  ,[idDominio]
		  ,[idVisita]
		  ,[FechaAlta]
		  ,[Estatus]
		  ,[usuario]
		  ,[FechaModificacion]
		  ,[FechaEnvio]
		  ,[FechaRecepcion]
		  ,ISNULL([idUsuarioAnterior] , 0) AS idUsuarioAnterior
		  ,[Auxiliar] FROM [Ordenes] where idOrden = CONVERT(INT, @orden)
		  SET @cantidad += @@ROWCOUNT;
			DELETE FROM [Ordenes]   where idOrden = CONVERT(INT, @orden)
		END
		SET @indice = charindex(',', @TextoOrdenes)
	END

	IF (@TextoOrdenes != '')
	BEGIN
	
	IF((select 1 from bitacoraenvio  WITH (NOLOCK)  where idorden=CONVERT(INT, @TextoOrdenes) and FechaEnvio is null) is null)
		BEGIN 
			DELETE FROM [BitacoraOrdenes]   where idOrden = CONVERT(INT, @TextoOrdenes)
			INSERT INTO [BitacoraOrdenes] SELECT  [idOrden]
			  ,[idPool]
			  ,[num_Cred]
			  ,[idUsuario]
			  ,[idUsuarioPadre]
			  ,[idUsuarioAlta]
			  ,[idDominio]
			  ,[idVisita]
			  ,[FechaAlta]
			  ,[Estatus]
			  ,[usuario]
			  ,[FechaModificacion]
			  ,[FechaEnvio]
			  ,[FechaRecepcion]
			  ,ISNULL([idUsuarioAnterior] , 0) AS idUsuarioAnterior
			  ,[Auxiliar] FROM [Ordenes] where idOrden = CONVERT(INT, @TextoOrdenes)
			  SET @cantidad += @@ROWCOUNT;
				DELETE FROM [Ordenes]   where idOrden = CONVERT(INT, @TextoOrdenes)
		END
	END
	select  @cantidad as Exito
END


