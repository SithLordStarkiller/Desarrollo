
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
ALTER PROCEDURE [dbo].[CancelarOrdenes] @TextoOrdenes VARCHAR(1500)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @indice INT, @orden VARCHAR(20), @cantidad INT = 0

	SET @indice = charindex(',', @TextoOrdenes)

	WHILE (@indice != 0)
	BEGIN
		SET @orden = left(@TextoOrdenes, @indice - 1)
		SET @TextoOrdenes = RIGHT(@TextoOrdenes, len(@TextoOrdenes) - @indice)

		IF EXISTS (
				SELECT 1
				FROM Ordenes
				WHERE idOrden = CONVERT(INT, @orden)
					AND Tipo LIKE 'C%'
				)
		BEGIN
			BEGIN TRY
				BEGIN TRAN cancelCC

				UPDATE Ordenes
				SET idUsuarioPadre = - 110
				WHERE idOrden = CONVERT(INT, @orden)
					AND Estatus IN (3,4)

				IF @@ROWCOUNT > 0
				BEGIN
					UPDATE Respuestas
					SET idUsuarioPadre = - 110
					WHERE idOrden = CONVERT(INT, @orden)
				END

				COMMIT TRAN cancelCC
			END TRY

			BEGIN CATCH
				IF @@TRANCOUNT > 0
				BEGIN
					ROLLBACK TRAN cancelCC
				END

				INSERT INTO Tracelog
				VALUES (3, 0, 'SP_ CancelarOrdenes', 'Error: ' + error_number() + ' - ' + ERROR_MESSAGE(), getdate()) 
			END CATCH
		END
		ELSE
		BEGIN
			IF (
					(
						SELECT 1
						FROM bitacoraenvio WITH (NOLOCK)
						WHERE idorden = CONVERT(INT, @orden)
							AND FechaEnvio IS NULL
						) IS NULL
					)
			BEGIN
				DELETE
				FROM [BitacoraOrdenes]
				WHERE idOrden = CONVERT(INT, @orden)

				INSERT INTO [BitacoraOrdenes]
				SELECT [idOrden], [idPool], [num_Cred], [idUsuario], [idUsuarioPadre], [idUsuarioAlta], [idDominio], [idVisita], [FechaAlta], [Estatus], [usuario], [FechaModificacion], [FechaEnvio], [FechaRecepcion], [Auxiliar], ISNULL([idUsuarioAnterior], 0) AS idUsuarioAnterior, [Tipo], [cvDelegacion], [CvRuta]
				FROM [Ordenes]
				WHERE idOrden = CONVERT(INT, @orden)

				SET @cantidad += @@ROWCOUNT;

				DELETE
				FROM [Ordenes]
				WHERE idOrden = CONVERT(INT, @orden)
			END
		END

		SET @indice = charindex(',', @TextoOrdenes)
	END

	IF (@TextoOrdenes != '')
	BEGIN
		IF EXISTS (
				SELECT 1
				FROM Ordenes
				WHERE idOrden = CONVERT(INT, @TextoOrdenes)
					AND Tipo LIKE 'C%'
				)
		BEGIN
			BEGIN TRY
				BEGIN TRAN cancelCC

				UPDATE Ordenes
				SET idUsuarioPadre = - 110
				WHERE idOrden = CONVERT(INT, @TextoOrdenes)
					AND Estatus IN (3,4)
				SET @cantidad += @@ROWCOUNT;
				IF @@ROWCOUNT > 0
				BEGIN
					UPDATE Respuestas
					SET idUsuarioPadre = - 110
					WHERE idOrden = CONVERT(INT, @TextoOrdenes)
				END

				COMMIT TRAN cancelCC
			END TRY

			BEGIN CATCH
				IF @@TRANCOUNT > 0
				BEGIN
					ROLLBACK TRAN cancelCC
				END

				INSERT INTO Tracelog
				VALUES (3, 0, 'SP_ CancelarOrdenes', 'Error: ' + error_number() + ' - ' + ERROR_MESSAGE(), getdate()) --
			END CATCH
		END
		ELSE
		BEGIN
			IF (
					(
						SELECT 1
						FROM bitacoraenvio WITH (NOLOCK)
						WHERE idorden = CONVERT(INT, @TextoOrdenes)
							AND FechaEnvio IS NULL
						) IS NULL
					)
			BEGIN
				DELETE
				FROM [BitacoraOrdenes]
				WHERE idOrden = CONVERT(INT, @TextoOrdenes)

				INSERT INTO [BitacoraOrdenes]
				SELECT [idOrden], [idPool], [num_Cred], [idUsuario], [idUsuarioPadre], [idUsuarioAlta], [idDominio], [idVisita], [FechaAlta], [Estatus], [usuario], [FechaModificacion], [FechaEnvio], [FechaRecepcion], [Auxiliar], ISNULL([idUsuarioAnterior], 0) AS idUsuarioAnterior, [Tipo], [cvDelegacion], [CvRuta]
				FROM [Ordenes]
				WHERE idOrden = CONVERT(INT, @TextoOrdenes)

				SET @cantidad += @@ROWCOUNT;

				DELETE
				FROM [Ordenes]
				WHERE idOrden = CONVERT(INT, @TextoOrdenes)
			END
		END
	END

	SELECT @cantidad AS Exito
END
