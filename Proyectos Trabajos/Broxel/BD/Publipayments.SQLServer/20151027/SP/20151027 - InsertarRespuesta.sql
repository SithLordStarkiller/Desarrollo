SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 20151027
-- Description:	Inserta la respuesta para el campo, orden, formulario dado
--				Si no se pasa el formulario se obtiene de las respuestas
--				En caso de que no se enuentre el formulario el sp manda un error
-- =============================================
CREATE PROCEDURE InsertarRespuesta @idOrden INT
	,@NombreCampo NVARCHAR(50)
	,@Valor NVARCHAR(350)
	,@idFormulario INT = - 1
AS
BEGIN
	SET NOCOUNT ON;

	--Declaraciones
	DECLARE @idCampo INT = - 1 --Id del campo que se quiere insertar
	DECLARE @maxIdCampo INT = 1 --Max + 1 del id del campo que se quiere insertar
	DECLARE @idUsuarioPadre INT = - 1 --Usuario padre de la orde
	DECLARE @idDominio INT = - 1 --Dominio de la orden

	-- Valida el formulario
	IF (@idFormulario = - 1)
		SELECT TOP 1 @idFormulario = idFormulario
		FROM Respuestas WITH (NOLOCK)
		WHERE idOrden = @idOrden
	ELSE IF NOT EXISTS (
			SELECT 1
			FROM [dbo].[Formulario]
			WHERE idFormulario = @idFormulario
			)
		THROW 50001
		,'No se pudo obtener el formulario'
		,1
	
	IF (@idFormulario != - 1)
	BEGIN
		-- Busca el idcampo si existe
		SELECT @idCampo = idCampo
		FROM [dbo].[CamposRespuesta]
		WHERE idFormulario = @idFormulario
			AND Nombre = @NombreCampo

		-- Si no existe el campo para el formulario lo inserta
		IF (@idCampo = - 1)
		BEGIN TRY
			BEGIN TRANSACTION Campo

			SELECT @maxIdCampo = MAX(idCampo) + 1
			FROM [dbo].[CamposRespuesta]

			INSERT INTO [dbo].[CamposRespuesta] (
				[idCampo]
				,[Nombre]
				,[Tipo]
				,[Referencia]
				,[idFormulario]
				)
			VALUES (
				@maxIdCampo
				,@NombreCampo
				,1
				,NULL
				,@idFormulario
				)

			SET @idCampo = @maxIdCampo

			COMMIT TRANSACTION Campo
		END TRY

		BEGIN CATCH
			IF (@@TRANCOUNT > 0)
				ROLLBACK TRANSACTION Campo;
				
				THROW;
		END CATCH

		--Obtengo los valores de la orden
		SELECT @idUsuarioPadre = [idUsuarioPadre]
			,@idDominio = [idDominio]
		FROM Ordenes
		WHERE idOrden = @idOrden

		-- Si todo esta bien inserta la respuesta
		IF (
				@idUsuarioPadre != - 1
				AND @idDominio != - 1
				)
		BEGIN
			INSERT INTO [dbo].[Respuestas] (
				[idOrden]
				,[idCampo]
				,[Valor]
				,[idDominio]
				,[idUsuarioPadre]
				,[idFormulario]
				)
			VALUES (
				@idOrden
				,@idCampo
				,@Valor
				,@idDominio
				,@idUsuarioPadre
				,@idFormulario
				)
		END
	END
	ELSE
	BEGIN
		-- No se pudo obtener el formulario de las respuestas
		-- Posible causa ya se haya reasignado
		THROW 50002
			,'No se pudo obtener el formulario para la orden dada'
			,1
	END
END
