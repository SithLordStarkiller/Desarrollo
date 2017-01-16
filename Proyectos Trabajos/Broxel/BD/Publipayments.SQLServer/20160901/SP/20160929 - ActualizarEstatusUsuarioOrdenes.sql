-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 27/08/2014
-- Description:	Actualiza estatus de ordenes separadas por coma (maximo 100 ordenes)
-- Modificacion: 20140902 Actulización de fecha de envio
-- Modificacion: 20140926 Insert a BitacoraEnvio
-- Modificacion: 20141114 No permite cancelar cuando se esta asignando o se esta reasignando
-- Modificacion: 20141218 Se aplican las restricciones para asignacion a visita mayor a 3
-- Modificacion: 20150218 Se aplica bloqueo a estatus Sincronizando y no se autorizan ordenes con tipo SMS
-- Modificacion: 20150323 Se quitan las restricciones para asignacion a visita mayor a 3 -- London asi lo solicito
-- Modificacion: 20150408 - MJNS - Cuando sea estatus 4 se agrega filtrado de ordenes de SMS y se quita Tipo SMS
-- Modificacion: 20151001 - PEJV - Se agrega cambio de Tipo CC cuando sea cancelacion o reasignacion
-- =============================================
ALTER PROCEDURE [dbo].[ActualizarEstatusUsuarioOrdenes] (
	@TextoOrdenes VARCHAR(1500)
	,@Estatus INT
	,@ActualizaUsuario INT = - 1
	,@ActualizaFecha BIT = 0
	,@ActSiEstatusIgual BIT = 0
	)
AS
BEGIN
	DECLARE @TextoOrdenesFiltrados VARCHAR(1500) = '';
	DECLARE @sql VARCHAR(8000) = '';
	DECLARE @Bloque INT = 1;
	DECLARE @Afectados INT = 0;

	IF (@Estatus = 4)
	BEGIN
		EXEC FiltrarEstatusUsuarioOrdenes @TextoOrdenes
			,@TextoOrdenesFiltrados OUTPUT

		SET @TextoOrdenes = @TextoOrdenesFiltrados
	END

	IF (
			@Estatus = 11
			OR @Estatus = 12
			OR @Estatus = 15
			)
	BEGIN
		SET @sql = @sql + 'SET NOCOUNT ON; INSERT INTO BitacoraEnvio (idOrden, EstatusEnvio, EstatusAnterior) SELECT idOrden, ' + CONVERT(VARCHAR(10), @Estatus) + ' EstatusEnvio, Estatus EstatusAnterior FROM Ordenes'

		GOTO SentenciaWhere
	END

	Bloque1:

	IF (
			@Estatus = 12
			
			)
	BEGIN
		-- Si es una cancelación voy a mover las respuestas  que tenga a bitacora
		SET @sql = @sql + 'SET NOCOUNT ON; INSERT INTO [BitacoraRespuestas] SELECT idOrden,idCampo,Valor,GETDATE() AS Fecha,idDominio,idUsuarioPadre,idFormulario FROM [Respuestas] WHERE idOrden IN ( SELECT idOrden FROM Ordenes WHERE idOrden IN (' + @TextoOrdenes + ') AND Estatus != 4 ) AND idOrden NOT IN (SELECT  idorden FROM  autorizacionsms WITH(NOLOCK) WHERE idorden in ( ' + @TextoOrdenes + ') AND  FechaEnvio IS NULL );  '
		SET @sql = @sql + 'DELETE FROM [Respuestas] WHERE idOrden IN (SELECT idOrden FROM Ordenes WHERE idOrden IN (' + @TextoOrdenes + ') AND Estatus != 4 ) AND idOrden NOT IN (SELECT  idorden FROM  autorizacionsms WITH(NOLOCK) WHERE idorden in ( ' + @TextoOrdenes + ') AND  FechaEnvio IS NULL ) ; SET NOCOUNT OFF; '
	END
	
	IF (
			@Estatus = 15
			)
			Begin
			SET @sql = @sql + 'SET NOCOUNT ON; INSERT INTO [BitacoraRespuestas] SELECT idOrden,idCampo,Valor,GETDATE() AS Fecha,idDominio,idUsuarioPadre,idFormulario FROM [Respuestas] WHERE idOrden IN ( SELECT idOrden FROM Ordenes WHERE idOrden IN (' + @TextoOrdenes + ') AND Estatus != 4  AND ((Tipo not like ''C%'' and '+CONVERT(VARCHAR(10), @ActualizaUsuario)+'<0) or ( '+CONVERT(VARCHAR(10), @ActualizaUsuario)+'>=0)) ) AND idOrden NOT IN (SELECT  idorden FROM  autorizacionsms WITH(NOLOCK) WHERE idorden in ( ' + @TextoOrdenes + ') AND  FechaEnvio IS NULL ) ; '
		SET @sql = @sql + 'DELETE FROM [Respuestas] WHERE idOrden IN (SELECT idOrden FROM Ordenes WHERE idOrden IN (' + @TextoOrdenes + ') AND Estatus != 4  AND ((Tipo not like ''C%'' and '+CONVERT(VARCHAR(10), @ActualizaUsuario)+'<0) or ( '+CONVERT(VARCHAR(10), @ActualizaUsuario)+'>=0)) ) AND idOrden NOT IN (SELECT  idorden FROM  autorizacionsms WITH(NOLOCK) WHERE idorden in ( ' + @TextoOrdenes + ') AND  FechaEnvio IS NULL ) ; SET NOCOUNT OFF; '
		
		end
	

	-- Si es una asignacion tengo que validar si la orden no se esta cancelando, si se esta cancelando la convierto en una reasignación
	IF (
			@Estatus = 11
			OR @Estatus = 4
			)
		SET @sql = @sql + 'UPDATE Ordenes SET Estatus = CASE WHEN Estatus = 12 AND ' + CONVERT(VARCHAR(10), @Estatus) + ' = 11 THEN 15 ELSE ' + CONVERT(VARCHAR(10), @Estatus) + ' END '
	ELSE
		SET @sql = @sql + 'UPDATE Ordenes SET Estatus = ' + CONVERT(VARCHAR(10), @Estatus) + ' , idVisita = CASE WHEN Estatus = 3 THEN idVisita + 1 ELSE idVisita END '

	IF (@Estatus = 4) -- Se quita el tipo cuando es una autorización por este SP
		SET @sql = @sql + ', Tipo = case when Tipo=''C'' then ''C'' when Tipo=''CS'' then ''C'' else '' '' end ' 

	IF (@ActualizaFecha = 1)
	BEGIN
		SET @sql = @sql + ' ,  FechaModificacion = GETDATE() '

		IF (
				@Estatus = 11
				OR @Estatus = 15
				)
		BEGIN
			SET @sql = @sql + ' ,  FechaEnvio = GETDATE() '
		END
	END

	-- Si no es una cancelación de administrador
	IF (@Estatus != 2)
	BEGIN
		-- Si es una cancelacion o una reasignacion guardo el ususario anterior
		IF (
				@Estatus = 12
				OR @Estatus = 15
				)
				BEGIN
					-- Se quita Tipo CallCenter
					SET @sql = @sql + ' , idUsuarioAnterior = case when Tipo LIKE ''%C%'' then idUsuarioAnterior else idUsuario end,Tipo=case when Tipo=''C'' then '' '' when Tipo=''CS'' then ''S'' else Tipo end '

					-- Se quita dictamen anterior
					SET @sql = @sql + ' , idDictamen = 0 '
				END
		-- Actualiza el ususario siempre que sea distinto de una actualizacion
		IF (@ActualizaUsuario >= 0)
		BEGIN
			SET @sql = @sql + ' , idUsuario = ' + CONVERT(VARCHAR(10), @ActualizaUsuario)
		END
	END

	SET @Bloque = 2

	GOTO SentenciaWhere

	Bloque2:

	PRINT @sql ------------------------------------------------

	EXECUTE sp_sqlexec @sql

	GOTO Finalizar;

	SentenciaWhere:

	-- Este Bloque de codigo tiene que ser igual al de abajo
	SET @sql = @sql + ' WHERE idOrden IN (' + @TextoOrdenes + ') and Estatus!=6 '

	-- Permite la actualizacion siempre que el estatus sea distinto
	IF (@ActSiEstatusIgual = 0)
		SET @sql = @sql + ' AND Estatus != ' + CONVERT(VARCHAR(10), @Estatus)

	--Si es una cancelación de un administrador se revisa que no este asignada la orden
	IF (@Estatus = 2)
		SET @sql = @sql + ' AND idUsuario = 0'

	--No se permite cancelar ni reasignar una orden que este autorizada
	IF (
			@Estatus = 12
			OR @Estatus = 15
			)
		SET @sql = @sql + ' AND ESTATUS != 4 AND idOrden NOT IN (SELECT  idorden FROM  autorizacionsms WITH(NOLOCK) WHERE idorden in ( ' + @TextoOrdenes + ') AND  FechaEnvio IS NULL ) '
	
	-- No se permite cancelar una orden que este en estatus 15 - 20160223 MJNS
	IF (@Estatus = 12)
		SET @sql = @sql + ' AND ESTATUS != 15'

	IF (
			@Estatus = 15
			)
		SET @sql = @sql + ' AND ((Tipo not like ''C%'' and '+CONVERT(VARCHAR(10), @ActualizaUsuario)+'<0) or ( '+CONVERT(VARCHAR(10), @ActualizaUsuario)+'>=0)) '
	
	SET @sql = @sql + ';'

	IF (@Bloque = 1)
	BEGIN
		SET @sql = @sql + ' SET NOCOUNT OFF; '

		GOTO Bloque1;
	END
	ELSE
		GOTO Bloque2;

	Finalizar:
END
