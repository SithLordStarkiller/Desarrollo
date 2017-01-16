declare @maxId int
declare @idFormulario int
select @maxId=max(idCampo)  from CamposXML2
set @maxId=@maxId+1
select  @idFormulario = idFormulario from formulario where Estatus=1 and captura=1 and Ruta='CSD'


INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenBCNPrivado','Borrón y cuenta nueva P',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenBCNJudicial','Borrón y cuenta nueva J',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenSTMPrivado','Solución a tu medida P',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenSTMJudicial','Solución a tu medida J',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenDCPPrivado','Dictamen de Capacidad de Pago P',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenDCPJudicial','Dictamen de Capacidad de Pago J',@maxId,1,@idFormulario)


set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'IN_OPCION_JUDICIAL','[Campo]IN_OPCION_JUDICIAL',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'OP_REGIMEN','[Campo]OP_REGIMEN',@maxId,1,@idFormulario)
GO