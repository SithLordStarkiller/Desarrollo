
DECLARE	@return_value int

EXEC	@return_value = [dbo].[InsertaFormulario]
		@Nombre = N'RDST',
		@Descripcion = N'Call Center Out-Bound',
		@Version = N'1.0',
		@Captura = 1,
		@Ruta = N'RDST'

SELECT	'Return Value' = @return_value

GO
	
	DECLARE @NombreXML VARCHAR(100)
			,@ValorXML VARCHAR(100)
			,@idCampo INT
			,@idFormularioNuevo INT 
			
select @idFormularioNuevo=idformulario from formulario where ruta='RDST' and captura=1

		DECLARE camposXMLCursor CURSOR
		FOR
		SELECT [Nombre]
			,[Valor]
		FROM [CamposXML2]
		WHERE idFormulario = (select idformulario from formulario where ruta='csd' and captura=1 and estatus=1)
		ORDER BY idcampo

		OPEN camposXMLCursor

		FETCH camposXMLCursor
		INTO @NombreXML
			,@ValorXML

		WHILE (@@fetch_status = 0)
		BEGIN
			SELECT @idCampo = MAX(idCampo)
			FROM CamposXML2

			SET @idCampo = @idCampo + 1

			INSERT INTO [CamposXML2] (
				[idCampo]
				,[Nombre]
				,[Valor]
				,[Orden]
				,[Activo]
				,[idFormulario]
				)
			VALUES (
				@idCampo
				,@NombreXML
				,@ValorXML
				,@idCampo
				,1
				,@idFormularioNuevo
				)

			FETCH camposXMLCursor
			INTO @NombreXML
				,@ValorXML
		END

		CLOSE camposXMLCursor

		DEALLOCATE camposXMLCursor

