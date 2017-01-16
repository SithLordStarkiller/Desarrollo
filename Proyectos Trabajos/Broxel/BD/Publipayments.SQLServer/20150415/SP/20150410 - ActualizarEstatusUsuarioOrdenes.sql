

/****** Object:  StoredProcedure [dbo].[ActualizarEstatusUsuarioOrdenes]    Script Date: 08/04/2015 11:54:13 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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
			OR @Estatus = 15
			)
	BEGIN
		-- Si es una cancelación o una reasignación voy a mover las respuestas  que tenga a bitacora
		SET @sql = @sql + 'SET NOCOUNT ON; INSERT INTO [BitacoraRespuestas] SELECT idOrden,idCampo,Valor,GETDATE() AS Fecha,idDominio,idUsuarioPadre,idFormulario FROM [Respuestas] WHERE idOrden IN ( SELECT idOrden FROM Ordenes WHERE idOrden IN (' + @TextoOrdenes + ') AND Estatus != 4); '
		SET @sql = @sql + 'DELETE FROM [Respuestas] WHERE idOrden IN (SELECT idOrden FROM Ordenes WHERE idOrden IN (' + @TextoOrdenes + ') AND Estatus != 4 ); SET NOCOUNT OFF; '
	END

	-- Si es una asignacion tengo que validar si la orden no se esta cancelando, si se esta cancelando la convierto en una reasignación
	IF (
			@Estatus = 11
			OR @Estatus = 4
			)
		SET @sql = @sql + 'UPDATE Ordenes SET Estatus = CASE WHEN Estatus = 12 AND ' + CONVERT(VARCHAR(10), @Estatus) + ' = 11 THEN 15 ELSE ' + CONVERT(VARCHAR(10), @Estatus) + ' END '
	ELSE
		SET @sql = @sql + 'UPDATE Ordenes SET Estatus = ' + CONVERT(VARCHAR(10), @Estatus) + ' , idVisita = CASE WHEN Estatus = 3 THEN idVisita + 1 ELSE idVisita END '

	IF (@Estatus = 4) -- Se quita el tipo cuando es una autorización por este SP
		SET @sql = @sql + ', Tipo = '' '''

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
			SET @sql = @sql + ' , idUsuarioAnterior = idUsuario '

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
		SET @sql = @sql + ' AND ESTATUS != 4'
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
GO


