
/****** Object:  StoredProcedure [dbo].[OrdenesAMover]    Script Date: 25/04/2016 11:44:17 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/01/28
-- Description:	Quita una orden y respuesta de un credito que sufrio una reasignacion, en el caso de que se encuentre en el movil se manda la cancelacion
-- =============================================
ALTER PROCEDURE [dbo].[OrdenesAMover] @Ordenes varchar(1500)
AS
BEGIN
	SET NOCOUNT ON;
	
	
	DECLARE @tabla table(idOrden int,idUsuario int,Estatus int);

	DECLARE @sql nvarchar(2000)

	SET @sql=' SELECT idOrden,idUsuario,Estatus FROM Ordenes WITH(NOLOCK) WHERE idOrden in ('
	SET @sql+=@Ordenes
	SET @sql+=')'

	INSERT  @tabla
	EXEC (@sql)


	UPDATE Ordenes  WITH (HOLDLOCK, ROWLOCK) SET idUsuarioAnterior=idUsuario,idUsuario=0 
	WHERE idOrden IN(SELECT  idOrden FROM @tabla WHERE idUsuario<>0 AND Estatus IN(1,6))

	INSERT INTO BitacoraEnvio(idOrden,EstatusEnvio,EstatusAnterior,Fecha)
	SELECT idOrden,12 EstatusEnvio,Estatus EstatusAnterior,GETDATE() Fecha FROM Ordenes WHERE idOrden IN(SELECT idOrden FROM @tabla WHERE idUsuario<>0 AND Estatus IN(1,6))

	
	declare @TextoOrdenes nvarchar (max)
	DECLARE @indice INT
	declare @orden VARCHAR(20)

	SELECT @TextoOrdenes= isnull(+ @TextoOrdenes + ', ', '') +idOrden FROM Ordenes WHERE idOrden IN(SELECT idOrden FROM @tabla WHERE idUsuario<>0 AND Estatus IN(3,4))
	AND idUsuarioAnterior >0  AND idUsuarioPadre >0 AND idUsuario=-111 AND tipo LIKE '%C%'

	
	SET @indice = charindex(',', @TextoOrdenes)
	WHILE (@indice != 0)
	BEGIN
		SET @orden = left(@TextoOrdenes, @indice - 1)
		SET @TextoOrdenes = RIGHT(@TextoOrdenes, len(@TextoOrdenes) - @indice)
		EXEC ActualizarEstatusUsuarioOrdenes @orden, 12, 0, 1, 0
		SET @indice = charindex(',', @TextoOrdenes)
	END
	
	IF (@TextoOrdenes != '')
		EXEC ActualizarEstatusUsuarioOrdenes @TextoOrdenes, 12, 0, 1, 0
	
	INSERT INTO BitacoraOrdenes(idOrden,idPool,num_Cred,idUsuario,idUsuarioPadre,idUsuarioAlta,idDominio,idVisita,FechaAlta,Estatus,usuario,FechaModificacion,FechaEnvio,FechaRecepcion,Auxiliar,idUsuarioAnterior,Tipo,cvDelegacion,CvRuta)
	SELECT idOrden,idPool,num_Cred,idUsuario,idUsuarioPadre,idUsuarioAlta,idDominio,idVisita,FechaAlta,Estatus,usuario,FechaModificacion,FechaEnvio,FechaRecepcion,Auxiliar,idUsuarioAnterior,Tipo,cvDelegacion,CvRuta 
	FROM Ordenes WITH(NOLOCK) WHERE idOrden IN (SELECT idOrden FROM @tabla)

	WHILE EXISTS(SELECT 1 FROM BitacoraEnvio WITH(NOLOCK) WHERE FechaEnvio is NULL AND EstatusEnvio=12 AND idORden IN(SELECT idOrden FROM @tabla WHERE idUsuario<>0 AND Estatus IN(1,3,4,6)))
	BEGIN
		WAITFOR DELAY '000:00:01'
	END


	DELETE FROM Ordenes WHERE idOrden IN(SELECT idOrden FROM @tabla)

	INSERT INTO BitacoraRespuestas
	SELECT idOrden,idCampo,Valor,GETDATE(),idDominio,idUsuarioPadre,idFormulario FROM Respuestas WHERE idOrden IN(SELECT idOrden FROM @tabla)

	DELETE FROM Respuestas WHERE idOrden IN(SELECT idOrden FROM @tabla)
    
END



 