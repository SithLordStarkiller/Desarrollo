
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/06/27
-- Description:	Obtiene el ultimo Telefono relacionado a una orden con el mensaje correspondiente para la reasignacion
-- =============================================
CREATE PROCEDURE ObtenerUsoTelefono  @idOrden INT
AS
BEGIN

	SET NOCOUNT ON;

		DECLARE @MensajeTelSMS varchar(100)=''
		DECLARE @CelularSMS varchar(15)=''
		DECLARE @FechaBitacora DATETIME

		SELECT  @FechaBitacora=max(Fecha) FROM BitacoraRespuestas WITH(NOLOCK) where idOrden=@idOrden
	
		IF NOT EXISTS (SELECT 1 FROM AutorizacionSMS WITH(NOLOCK) WHERE idOrden = @idOrden)
			BEGIN

			 IF EXISTS (SELECT 1 FROM respuestas WITH(NOLOCK) WHERE idOrden = @idOrden  )
				 BEGIN
							SELECT @CelularSMS=Celular.valor,  @MensajeTelSMS = CASE  WHEN UPPER (LTRIM(RTRIM( TipoCap.valor))) = 'CAPTURAWEB' THEN ' Este telefono ya esta siendo utilizado por otro credito en plataforma' ELSE  'Este telefono ya esta siendo utilizado por otro movil'  END  from 
								(
								SELECT r.*			
								FROM Respuestas r WITH(NOLOCK)
								INNER JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = r.idCampo
								WHERE idOrden = @idOrden
								AND cr.Nombre LIKE 'CelularSMS_%' AND cr.Nombre != 'CelularSMS_Ant'
								) as Celular 
								LEFT JOIN (
								SELECT r.*
								FROM Respuestas r WITH(NOLOCK)
								INNER JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = r.idCampo
								WHERE idOrden = @idOrden
								AND cr.Nombre = 'FormiikResponseSource'
								) AS TipoCap ON Celular.idorden=TipoCap.idorden
				 END
			 ELSE
			 BEGIN
		
					SELECT @MensajeTelSMS = CASE  WHEN  UPPER (LTRIM(RTRIM(br.Valor))) !='MOVIL' THEN 'Telefono invalido' ELSE '' END 
					FROM BitacoraRespuestas br WITH(NOLOCK)
					INNER JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = br.idCampo
					WHERE idOrden = @idOrden
					AND cr.Nombre like 'TIPO_TEL'
					AND br.Fecha = @FechaBitacora

					SELECT @CelularSMS=br.valor			
					FROM BitacoraRespuestas br WITH(NOLOCK)
					INNER JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = br.idCampo
					WHERE idOrden = @idOrden
					AND cr.Nombre LIKE 'CelularSMS_%' AND cr.Nombre != 'CelularSMS_Ant'
					AND br.Fecha = @FechaBitacora

					IF (@MensajeTelSMS='')
					BEGIN	
							select @CelularSMS=Celular.valor,  @MensajeTelSMS = CASE  WHEN UPPER (LTRIM(RTRIM( TipoCap.valor))) = 'CAPTURAWEB' THEN ' Este telefono ya esta siendo utilizado por otro credito en plataforma' ELSE  'Este telefono ya esta siendo utilizado por otro movil'  END  from 
							(
							SELECT br.*			
							FROM BitacoraRespuestas br WITH(NOLOCK)
							INNER JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = br.idCampo
							WHERE idOrden = @idOrden
							AND cr.Nombre LIKE 'CelularSMS_%' AND cr.Nombre != 'CelularSMS_Ant'
							AND br.Fecha = @FechaBitacora
							) as Celular 
					
							LEFT JOIN (
							SELECT br.*
							FROM BitacoraRespuestas br WITH(NOLOCK)
							INNER JOIN CamposRespuesta cr WITH(NOLOCK) ON cr.idCampo = br.idCampo
							WHERE idOrden = @idOrden
							AND cr.Nombre = 'FormiikResponseSource'
							AND br.Fecha = @FechaBitacora
							) as TipoCap ON Celular.idorden=TipoCap.idorden


					END
			 END
			END
		ELSE
			BEGIN
					SELECT @CelularSMS=Telefono FROM AutorizacionSMS WITH(NOLOCK) WHERE idOrden = @idOrden
					SET @MensajeTelSMS = 'Telefono valido'
			END

			SELECT @CelularSMS AS TELEFONO ,@MensajeTelSMS AS MENSAJE

END
GO
