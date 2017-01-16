				
	DECLARE @idCampo INT
	DECLARE @idFuncionJs INT
	DECLARE @idformulario INT
	
	select @idformulario=idformulario from formulario where estatus=1 and captura=2 and Ruta='RDST'


	--**********************--
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='Acreditado' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('Acreditado','if(ValC("Acreditado", "No","=="))'
				,'$("#AcreditadoFin").val("5");$("#AcreditadoFin").trigger("blur");'
				,'$("#AcreditadoFin").val("1");$("#AcreditadoFin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='VerificarDatos' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('VerificarDatos','if(ValC("VerificarDatos", "No","=="))'
				,'$("#VerificarDatosFin").val("0");$("#VerificarDatosFin").trigger("blur");'
				,'$("#VerificarDatosFin").val("2");$("#VerificarDatosFin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='DatosCorrectos' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('DatosCorrectos','if(ValC("DatosCorrectos", "No","=="))'
				,'$("#DatosCorrectosFin").val("5");$("#DatosCorrectosFin").trigger("blur");'
				,'$("#DatosCorrectosFin").val("1");$("#DatosCorrectosFin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
		SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='ActitudHostil' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('ActitudHostil','if(ValC("ActitudHostil", "No","=="))'
				,'$("#ActitudHostilFin").val("2");$("#DatosCorrectosFin").trigger("blur");'
				,'$("#ActitudHostilFin").val("0");$("#DatosCorrectosFin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AplicaFPP' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('AplicaFPP','if(ValC("AplicaFPP", "No","=="))'
				,'$("#AplicaFPPFin").val("2");$("#AplicaFPPFin").trigger("blur");'
				,'$("#AplicaFPPFin").val("0");$("#AplicaFPPFin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP1' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('AceptaFPP1','if(ValC("AceptaFPP1", "No","=="))'
				,'$("#AceptaFPP1Fin").val("0");$("#AceptaFPP1Fin").trigger("blur");'
				,'$("#AceptaFPP1Fin").val("1");$("#AceptaFPP1Fin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
				SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP2' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('AceptaFPP2','if(ValC("AceptaFPP2", "No","=="))'
				,'$("#AceptaFPP2Fin").val("0");$("#AceptaFPP2Fin").trigger("blur");'
				,'$("#AceptaFPP2Fin").val("1");$("#AceptaFPP2Fin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP3' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('AceptaFPP3','if(ValC("AceptaFPP3", "No","=="))'
				,'$("#AceptaFPP3Fin").val("0");$("#AceptaFPP3Fin").trigger("blur");'
				,'$("#AceptaFPP3Fin").val("1");$("#AceptaFPP3Fin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP4' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('AceptaFPP4','if(ValC("AceptaFPP4", "No","=="))'
				,'$("#AceptaFPP4Fin").val("0");$("#AceptaFPP4Fin").trigger("blur");'
				,'$("#AceptaFPP4Fin").val("1");$("#AceptaFPP4Fin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP5' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('AceptaFPP5','if(ValC("AceptaFPP5", "No","=="))'
				,'$("#AceptaFPP5Fin").val("0");$("#AceptaFPP5Fin").trigger("blur");'
				,'$("#AceptaFPP5Fin").val("1");$("#AceptaFPP5Fin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP6' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('AceptaFPP6','if(ValC("AceptaFPP6", "No","=="))'
				,'$("#AceptaFPP6Fin").val("0");$("#AceptaFPP6Fin").trigger("blur");'
				,'$("#AceptaFPP6Fin").val("1");$("#AceptaFPP6Fin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************--
			SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('AceptaFPP','if(ValC("AceptaFPP", "No","=="))'
				,'$("#AceptaFPPFin").val("2");$("#AceptaFPPFin").trigger("blur");'
				,'$("#AceptaFPPFin").val("0");$("#AceptaFPPFin").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************-
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('TotalizaA','if(true)'
				,'$("#TotalizaA").trigger("blur");'
				,'$("#TotalizaA").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AcreditadoFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='VerificarDatosFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='DatosCorrectosFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='ActitudHostilFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************-
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('TotalizaC','if(true)'
				,'$("#TotalizaC").trigger("blur");'
				,'$("#TotalizaC").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP1Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP2Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP3Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP4Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP5Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPP6Fin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	--**********************-
	INSERT INTO [CatFuncionesJS]([Nombre],[Validacion],[FuncionSI],[FuncionNo],[idFormulario])
				values ('TotalizaB','if(true)'
				,'$("#TotalizaB").trigger("blur");'
				,'$("#TotalizaB").trigger("blur");',@idFormulario)
	SELECT @idFuncionJs = SCOPE_IDENTITY()
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AplicaFPPFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	SELECT @idCampo=idCampoFormulario from CamposXSubFormulario where NombreCampo='AceptaFPPFin' and idSubFormulario in (select idSubFormulario from SubFormulario where idFormulario=@idformulario) 
	INSERT  INTO [dbo].[FuncionesXCampos] (idFuncionJS,idCampoFormulario) VALUES (@idFuncionJs,@idCampo)
	