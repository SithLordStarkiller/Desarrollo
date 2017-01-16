
GO
/****** Object:  StoredProcedure [dbo].[ValidaAutorizacionSMS]    Script Date: 06/04/2015 02:04:35 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************************************
* Proyecto:				London-PubliPayments-Formiik
* Autor:				Maximiliano Silva
* Fecha de creación:	24/02/2015
* Descripción:			Valida la autorizacion de los SMS
* Regresa:				Resultado y Mensaje
**********************************************************************/
ALTER PROCEDURE [dbo].[ValidaAutorizacionSMS] (
	@telefono CHAR(10)
	,@autorizacion CHAR(8)
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idOrden INT
		,@id INT
		,@clave CHAR(8)
		,@resultado INT
		,@mensaje VARCHAR(250)
		,@Estatus INT=0

	SELECT TOP 1 @id = id
		,@idOrden = idOrden
		,@clave = Clave
	FROM AutorizacionSMS WITH (NOLOCK)
	WHERE Telefono = @telefono

	IF (@id IS NULL)
	BEGIN
		SELECT @resultado = - 3
			,@mensaje = 'No se encontró el teléfono'
	END
	ELSE
	BEGIN
		IF (@clave != @autorizacion)
		BEGIN
			SELECT @resultado = - 1
				,@mensaje = 'La clave de autorización no coincide'
		END
		ELSE
		BEGIN
			BEGIN TRY
				BEGIN TRAN AutorizaSMS

				EXEC AutorizaSMS @idOrden, 1;

				UPDATE AutorizacionSMS
				SET FechaRespEnvioProb = GETDATE()
				WHERE id = @id

				COMMIT TRAN AutorizaSMS

				SELECT @resultado = 1
					,@mensaje = 'El SMS se autorizó correctamente'
			END TRY

			BEGIN CATCH
				IF @@TRANCOUNT > 0
					ROLLBACK TRAN AutorizaSMS;

				SELECT @resultado = - 2
					,@mensaje = 'Error al intentar autorizar el SMS : ' + ERROR_MESSAGE()
			END CATCH
		END
	END

	SET NOCOUNT OFF;

	IF @idOrden is not null 
	BEGIN
	SELECT @Estatus=Estatus FROM Ordenes where idOrden=@idOrden
	END
	

	SELECT @resultado Resultado
		,@mensaje Mensaje
		,@Estatus Estatus
END
	--ValidaAutorizacionSMS '5514782527', '28852298'
	--SELECT * FROM AutorizacionSMS


	--ObtenerSMSPorProcesar