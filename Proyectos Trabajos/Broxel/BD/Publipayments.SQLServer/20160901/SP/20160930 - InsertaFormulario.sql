-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/09/14
-- Description:	Inserta un nuevo elemento
-- Modifcacion: Laura Dotor
-- Fecha Modificacion: 2016/09/30
-- =============================================
ALTER PROCEDURE [dbo].[InsertaFormulario] (
	@Nombre varchar(50),
	@Descripcion varchar(100),
	@Version varchar(10),
	@Captura int,
	@Ruta varchar(10),
	@EnvioMovil int = 1
	)
AS
BEGIN

	DECLARE 
		@idFormulario int,
		@idAplicacion int

	SELECT @idAplicacion = valor FROM catalogogeneral WITH (NOLOCK) where id=3

	SET NOCOUNT ON;

	BEGIN TRY	
		INSERT INTO [dbo].[Formulario]
        (
			[idAplicacion]
		   ,[Nombre]
		   ,[Descripcion]
		   ,[Version]
		   ,[Estatus]
		   ,[FechaAlta]
		   ,[Captura]
		   ,[Ruta]
		   ,[EnviarMovil]
		)
		VALUES
        (
		   @idAplicacion
           ,@Nombre
           ,@Descripcion
           ,@Version
           ,1
           ,GETDATE()
           ,@Captura
           ,@Ruta
		   ,@EnvioMovil
		)

		SELECT @idFormulario = SCOPE_IDENTITY()
		
	END TRY
	BEGIN CATCH
	
		SET @idFormulario = -1
	
	END CATCH

	SELECT @idFormulario AS idFormulario

END
