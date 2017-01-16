
/****** Object:  StoredProcedure [dbo].[AutorizaSMS]    Script Date: 08/08/2016 11:42:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 24/02/2015
-- Description:	revive la respuesta de BitacoraRespuestas, si es necesario, autoriza gestiones por SMS
-- Modificacion: PEJV - 20151001 - Si respuesta anterior viene de Callcenter, Se pega Tipo C a la orden
-- Modificacion: JARO - 20160218 - Se agrega manejo de errores y se cambia el count(*)
-- =============================================
ALTER PROCEDURE [dbo].[AutorizaSMS] @idOrden INT
	,@Autorizar INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @fechaGestion DATETIME
		,@Estatus INT

	SELECT @Estatus = Estatus
	FROM Ordenes WITH (NOLOCK)
	WHERE idOrden = @idOrden
	
	BEGIN TRY 
		BEGIN TRAN 
		IF @Estatus = 1
		AND @Autorizar = 1
	BEGIN
		INSERT INTO BitacoraEnvio (
			idOrden
			,EstatusEnvio
			,EstatusAnterior
			,RespuestaEnvio
			)
		SELECT idOrden
			,12 EstatusEnvio
			,1 EstatusAnterior
			,'Origen-SMS Autorizado' RespuestaEnvio
		FROM Ordenes
		WHERE idOrden IN (@idOrden);

		-- Se actualiza el usuario Anterior con el mismo valor al usuario actual para mandar la cancelacion al Movil
		UPDATE Ordenes
		SET idUsuarioAnterior = idUsuario
		WHERE idOrden IN (@idOrden)
	END

		IF @Estatus = 1
		OR @Estatus = 3
	BEGIN
		IF NOT EXISTS (
				SELECT 1
				FROM Respuestas WITH (NOLOCK)
				WHERE idOrden = @idOrden
					AND (
						SELECT  COUNT(idOrden)
						FROM Respuestas WITH (NOLOCK)
						WHERE idOrden = @idOrden
							AND idCampo IN (
								SELECT idCampo
								FROM CamposRespuesta WITH (NOLOCK)
								WHERE Nombre = 'gps_automatico'
									OR Nombre LIKE 'CelularSMS_%'
								)
						) > 1
				)
		BEGIN
			SELECT @fechaGestion = MAX(Fecha)
			FROM BitacoraRespuestas WITH (NOLOCK)
			WHERE idOrden = @idOrden
				AND (
					SELECT COUNT(idOrden)
					FROM BitacoraRespuestas WITH (NOLOCK)
					WHERE idOrden = @idOrden
						AND idCampo IN (
							SELECT idCampo
							FROM CamposRespuesta WITH (NOLOCK)
							WHERE Nombre = 'gps_automatico'
								OR Nombre LIKE 'CelularSMS_%'
							)
					) > 1

			DELETE
			FROM Respuestas
			WHERE idOrden = @idOrden
				AND idCampo NOT IN (
					SELECT idCampo
					FROM CamposRespuesta
					WHERE Nombre IN (
							'AssignedTo_SMS'
							,'FinalDate_SMS'
							,'CelularSMS_Ant'
							,'CodSMS'
							)
					)

			DECLARE @idCampoTelSMS INT
				,@telAnteriorSMS VARCHAR(10)
				,@idCampoTelSMSAnt INT
				,@telAnteriorSMSAnt VARCHAR(10)

			SELECT @idCampoTelSMSAnt = br.idCampo
				,@telAnteriorSMSAnt = br.Valor
			FROM BitacoraRespuestas br
			LEFT JOIN CamposRespuesta cr ON cr.idCampo = br.idCampo
			WHERE Fecha = @fechaGestion
				AND idOrden = @idOrden
				AND cr.Nombre LIKE 'CelularSMS_%'
				AND cr.Nombre <> 'CelularSMS_Ant'					-- Este campo trae el celular que se pegara como celular sms anterior

			SELECT @idCampoTelSMS = r.idCampo
				,@telAnteriorSMS = r.Valor
			FROM Respuestas r
			LEFT JOIN CamposRespuesta cr ON cr.idCampo = r.idCampo
			WHERE idOrden = @idOrden
				AND cr.Nombre = 'CelularSMS_Ant'                   --Este campo trae el telefono actualizado, posteriormente se sustituye por el celular sms anterior

			INSERT INTO Respuestas
			SELECT idOrden
				,idCampo
				,Valor
				,idDominio
				,idUsuarioPadre
				,idFormulario
			FROM BitacoraRespuestas
			WHERE Fecha = @fechaGestion
				AND idOrden = @idOrden
				AND idCampo NOT IN (
					SELECT idCampo
					FROM CamposRespuesta
					WHERE Nombre IN (
							'AssignedTo_SMS'
							,'FinalDate_SMS'
							,'CelularSMS_Ant'
							,'CodSMS'
							)
					)

			IF @telAnteriorSMS IS NOT NULL
			BEGIN
				UPDATE Respuestas
				SET Valor = @telAnteriorSMSAnt
				WHERE idOrden = @idOrden
					AND idCampo = @idCampoTelSMS

				UPDATE Respuestas
				SET Valor = @telAnteriorSMS
				WHERE idOrden = @idOrden
					AND idCampo = @idCampoTelSMSAnt
			END
						
			update o set 
				o.Tipo = case 
							when LTrim(RTrim(o.Tipo))='' then 'C' 
							when LTrim(RTrim(o.Tipo))=' ' then 'C' 
							when LTrim(RTrim(o.Tipo)) like 'C%' then LTrim(RTrim(o.Tipo)) 
							else 'C'+LTrim(RTrim(o.Tipo)) 
						end 
			from Ordenes o
			left join Respuestas r on o.idOrden=r.idOrden
			left join CamposRespuesta cr on cr.idCampo=r.idcampo 
			where  o.idOrden=@idOrden and cr.Nombre='FormiikResponseSource' and r.Valor='CapturaWebCC'			
			
			
		END

		IF @Autorizar = 1
		BEGIN
			UPDATE Ordenes
			SET Estatus = 4
			,FechaModificacion= GETDATE()
			WHERE idOrden = @idOrden
				AND Estatus IN (
					1
					,3
					)
		END
	END
	SET NOCOUNT OFF;
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		  THROW;
	END CATCH 
	
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END
