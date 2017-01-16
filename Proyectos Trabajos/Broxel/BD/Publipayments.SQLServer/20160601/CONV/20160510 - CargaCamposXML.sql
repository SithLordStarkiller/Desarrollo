	
--************************************Una vez cargado el formulario CW se carga el de asignacion************************************************--
	insert into formulario
   select idAplicacion,Nombre,Descripcion,Version,Estatus,FechaAlta,1 AS Captura,Ruta from formulario where ruta='RDST'
--**********************************************************************************************************************************************--
	
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

