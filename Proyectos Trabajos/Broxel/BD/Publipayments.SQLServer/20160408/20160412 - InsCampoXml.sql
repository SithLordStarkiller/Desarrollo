declare @maxId int
declare @idFormulario int
select @maxId=max(idCampo)  from CamposXML2
set @maxId=@maxId+1
select  @idFormulario = idFormulario from formulario where Estatus=1 and captura=1 and Ruta='CSD'


INSERT INTO [dbo].[CamposXML2]([idCampo],[Nombre],[Valor],[Orden],[Activo],[idFormulario])
     VALUES
			(@maxId,'SMSDuplicado','[Campo]SMSDuplicado',@maxId,1,@idFormulario)

GO
