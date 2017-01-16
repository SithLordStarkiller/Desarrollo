declare @maxId int
declare @idFormulario int
select @maxId=max(idCampo)  from CamposXML2
set @maxId=@maxId+1
select  @idFormulario = idFormulario from formulario where Estatus=1 and captura=1 and Ruta='CSD'


INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'CantidadPagos','[Campo]CantidadPagos',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'TX_PAGA1','[Campo]TX_PAGA1',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'TX_PAGA2','[Campo]TX_PAGA2',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'TX_PAGA3','[Campo]TX_PAGA3',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'TX_PAGA4','[Campo]TX_PAGA4',@maxId,1,@idFormulario)


set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'TX_PAGA5','[Campo]TX_PAGA5',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'TX_PAGA6','[Campo]TX_PAGA6',@maxId,1,@idFormulario)
GO