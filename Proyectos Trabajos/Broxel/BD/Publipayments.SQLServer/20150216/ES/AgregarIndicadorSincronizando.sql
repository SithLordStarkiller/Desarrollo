INSERT INTO [dbo].[Utils_Descripciones]
           ([fc_Clave]
           ,[fc_Descripcion]
           ,[fi_Parte]
           ,[fc_Modulo]
           ,[fi_Orden]
           ,[fi_Activo]
           ,[fc_Usuario]
           ,[fd_FechaMod]
           ,[fi_idPermisos])
     VALUES
           ('DASH_GESTSINCRO'
           ,'Gestiones sincronizando'
           ,2
           ,'DASHBOARD'
           ,160
           ,1
           ,'pjaimes'
           ,GETDATE()
           ,1)