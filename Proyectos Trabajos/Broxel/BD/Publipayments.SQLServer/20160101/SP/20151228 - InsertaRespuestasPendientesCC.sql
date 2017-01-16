

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 28/12/2015
-- Description:	Guarda lo relacionado a una gestion que temporalmente no se puede procesar
-- =============================================
CREATE PROCEDURE [dbo].[InsertaRespuestasPendientes] (
		@idorden INT,
		@idUsuario INT,
		@ValoresRespuesta VARCHAR(max)
	)
AS
BEGIN
DECLARE @indice INT
		,@LineaRespuesta VARCHAR(400)
		,@CampoRespuesta VARCHAR(50)
		,@valor VARCHAR(350)

		SET @indice = charindex('|', @ValoresRespuesta)

	WHILE (@indice != 0)
	BEGIN
	SET @LineaRespuesta = left(@ValoresRespuesta, @indice - 1)
	SET @ValoresRespuesta = RIGHT(@ValoresRespuesta, len(@ValoresRespuesta) - @indice)
	
	set @CampoRespuesta= left(@LineaRespuesta,charindex(',', @LineaRespuesta)-1)
	set @valor = right(@LineaRespuesta, len(@LineaRespuesta)-charindex(',', @LineaRespuesta))
	
	INSERT INTO [dbo].[RespuestasPendientes] (
	   [idOrden]
      ,[NombreCampo]
      ,[Valor]
      ,[idUsuario]
      ,[Fecha])
	 VALUES(
	 @idorden
	 ,@CampoRespuesta
	 ,@valor
	 ,@idUsuario
	 ,GETDATE()
	 )

	SET @indice = charindex('|', @ValoresRespuesta)
	END
	
	IF (@ValoresRespuesta != '')
	BEGIN
	set @CampoRespuesta= left(@ValoresRespuesta,charindex(',', @ValoresRespuesta)-1)
	set @valor = right(@ValoresRespuesta, len(@ValoresRespuesta)-charindex(',', @ValoresRespuesta))
	
	INSERT INTO [dbo].[RespuestasPendientes] (
	   [idOrden]
      ,[NombreCampo]
      ,[Valor]
      ,[idUsuario]
      ,[Fecha])
	 VALUES(
	 @idorden
	 ,@CampoRespuesta
	 ,@valor
	 ,@idUsuario
	 ,GETDATE()
	 )
	 END
END