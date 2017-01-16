SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas 
-- Create date: 2016/06/16
-- Description:	Se encarga de insertar el tipo de telefono de acuerdo a la orden que se indique 
-- =============================================
CREATE PROCEDURE InsertarTipoTelefono (@idorden int)
	
AS
BEGIN
	
	SET NOCOUNT ON;

	
DECLARE @TIPO_TEL VARCHAR (10)='TIPO_TEL'
DECLARE @Telefono VARCHAR(20)='';
DECLARE @TIPO_RED_VALOR VARCHAR(50)='';
DECLARE @idFormulario INT;

DECLARE @TbTelefonoDato TABLE (TIPO_RED VARCHAR(20) NULL,POBLACION VARCHAR(200) NULL,MUNICIPIO  VARCHAR(200) NULL,ESTADO VARCHAR(5) NULL );

		BEGIN  TRY
				SELECT  @Telefono=r.valor,@idFormulario=r.idformulario FROM Respuestas r  WITH (NOLOCK) 
				INNER JOIN CamposRespuesta cr WITH (NOLOCK) on cr.idCampo=r.idCampo 
				WHERE cr.Nombre LIKE 'CelularSMS_%' AND cr.Nombre != 'CelularSMS_Ant'
				AND r.idorden=@idorden

				IF(@Telefono!='')

				BEGIN
					INSERT INTO @TbTelefonoDato  EXEC ValidarTipoTelefono @Telefono

					SELECT @TIPO_RED_VALOR=TIPO_RED FROM  @TbTelefonoDato

					IF NOT EXISTS( SELECT 1 FROM respuestas r INNER JOIN camposrespuesta cr ON r.idcampo=cr.idcampo WHERE r.idorden=@idorden AND cr.nombre=@TIPO_TEL)
					BEGIN 
						EXEC InsertarRespuesta @idOrden,@TIPO_TEL,@TIPO_RED_VALOR,@idFormulario
					END
				END
		END TRY
		BEGIN CATCH
				INSERT INTO [dbo].[TraceLog]
					   ([idTipoLog]
					   ,[idUsuario]
					   ,[Pagina]
					   ,[Texto]
					   ,[Fecha])
				 VALUES
					   (3
					   ,1
					   ,'InsertarTipoTelefono'
					   ,'idorden: '+CONVERT( VARCHAR(20),@idorden) + ' '+ ERROR_MESSAGE()
					   ,GETDATE())
		END CATCH
END
GO

