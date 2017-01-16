declare @maxId int
declare @idFormulario int
select @maxId=max(idCampo)  from CamposXML2
set @maxId=@maxId+1
select  @idFormulario = idformulario from formulario where estatus=1 and captura=1 and ruta='RDST'


INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenCONFPRParcial','Prorroga Parcial',@maxId,1,@idFormulario)

set @maxId=@maxId+1
INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'DictamenCONFPRTotal','Prorroga Total',@maxId,1,@idFormulario)
