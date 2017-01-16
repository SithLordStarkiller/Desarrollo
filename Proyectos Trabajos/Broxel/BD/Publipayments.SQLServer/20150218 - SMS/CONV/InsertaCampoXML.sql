declare @idcampo int
declare @idformulario int

select @idcampo=max(idcampo)  from  [CamposXML2]
set @idcampo=@idcampo+1
select @idformulario=idformulario from formulario where Ruta='CSD' and captura=1 and estatus=1 and idaplicacion in (select valor from catalogogeneral where Llave='idAplicacion')

INSERT INTO [dbo].[CamposXML2]
           ([idCampo]
           ,[Nombre]
           ,[Valor]
           ,[Orden]
           ,[Activo]
           ,[idFormulario])
     VALUES
           (@idcampo
           ,'ContinuarGestion'
           ,'Si'
           ,@idcampo
           ,1
           ,@idformulario)
GO


