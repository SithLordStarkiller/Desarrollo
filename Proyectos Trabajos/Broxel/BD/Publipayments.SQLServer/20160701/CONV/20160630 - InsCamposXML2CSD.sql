declare @maxId int
declare @idFormulario int
select @maxId=max(idCampo)  from CamposXML2
set @maxId=@maxId+1
select  @idFormulario = idformulario from formulario where estatus=1 and captura=1 and ruta='CSD'



set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenNoInteresa','No le interesa sin informacion',@maxId,1,@idFormulario)


set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenNoInteresaVSM2','No le interesa con informacion',@maxId,1,@idFormulario)


set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenAgendaCita','Agendar Cita',@maxId,1,@idFormulario)


set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenAceptacion','Aceptacion del cambio',@maxId,1,@idFormulario)