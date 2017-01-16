
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/01/25
-- Description:	Inserta o actualiza registro de un reporte que esta por generarse para tener solo un registro por mes
-- =============================================
ALTER PROCEDURE ActualizarEstatusReporte (@idUsuarioReporte INT, @TipoReporte INT)
	
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @idDominio INT = 0
		,@permitirReporte INT = - 1
		,@fechaExpiro INT = - 1

    SELECT @idDominio = idDominio
	FROM Usuario WITH (NOLOCK)
	WHERE idUsuario = @idUsuarioReporte

	IF (@idDominio < 0)
		BEGIN
			 SELECT 'Error: Dominio no encontrado' Resultado
			RETURN
		END
	
	IF(@TipoReporte=1)
		BEGIN
			SELECT @fechaExpiro = count(idReporte)
			FROM [dbo].[Reportes]  WITH (NOLOCK)
			WHERE Tipo = @TipoReporte
			AND idPadre = @idUsuarioReporte
			AND CONVERT(VARCHAR(20), Fecha, 120) > CONVERT(VARCHAR(20), CAST(getdate() - .0822 AS DATETIME), 121)
			AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) 
			AND Estatus=1

			IF (@fechaExpiro > 0)
				BEGIN
					SELECT 'Error: Bloqueado por fecha' Resultado
					RETURN
				END
		END
	ELSE IF(@TipoReporte=2)
		BEGIN
			IF((SELECT valor FROM catalogoGeneral WHERE llave='BloqueoReasignaciones')='1')
				BEGIN
					SELECT 'Error: Bloqueado por proceso de reasignaciones' Resultado
					RETURN
				END
		END
	
		SELECT @permitirReporte = count(idReporte)
		FROM [dbo].[Reportes]  WITH (NOLOCK)
		WHERE Tipo = @TipoReporte
		AND idPadre =  @idUsuarioReporte 
		AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) 
	
		IF (@permitirReporte < 1)
		BEGIN
			INSERT INTO [dbo].[Reportes] (
				[Tipo]
				,[idPadre]
				,[Estatus]
				,[Fecha]
				,[ReporteTxt]
				)
			VALUES (
				@TipoReporte
				,@idUsuarioReporte
				,'2'
				,GETDATE()
				,''
				)
		END
		ELSE
			BEGIN
				UPDATE [dbo].[Reportes]
				SET [Estatus] = '2'
				WHERE Tipo = @TipoReporte
					AND idPadre = @idUsuarioReporte
					AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) 
			END

			SELECT 'OK' Resultado
			RETURN
END
GO
