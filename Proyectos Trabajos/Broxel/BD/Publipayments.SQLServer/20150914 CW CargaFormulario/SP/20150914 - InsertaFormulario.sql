
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/09/14
-- Description:	Inserta un nuevo elemento
-- =============================================
CREATE PROCEDURE InsertaFormulario (
	@Nombre varchar(50),
	@Descripcion varchar(100),
	@Version varchar(10),
	@Captura int,
	@Ruta varchar(10)
	)
AS
BEGIN

DECLARE @idFormulario int,@idAplicacion int

select @idAplicacion=valor from catalogogeneral where id=3

	SET NOCOUNT ON;

		BEGIN TRY	
			INSERT INTO [dbo].[Formulario]
           ([idAplicacion]
           ,[Nombre]
           ,[Descripcion]
           ,[Version]
           ,[Estatus]
           ,[FechaAlta]
           ,[Captura]
           ,[Ruta])
     VALUES
           (@idAplicacion
           ,@Nombre
           ,@Descripcion
           ,@Version
           ,1
           ,GETDATE()
           ,@Captura
           ,@Ruta)

		SELECT @idFormulario = SCOPE_IDENTITY()
		END TRY

		BEGIN CATCH
			SET @idFormulario = -1
		END CATCH

	SELECT @idFormulario as idFormulario

END
GO