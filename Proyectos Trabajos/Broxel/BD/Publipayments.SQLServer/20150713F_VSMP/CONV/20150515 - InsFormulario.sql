

INSERT INTO [dbo].[Formulario]
           ([idAplicacion]
           ,[Nombre]
           ,[Descripcion]
           ,[Version]
           ,[Estatus]
           ,[FechaAlta]
           ,[Captura]
           ,[Ruta])
     VALUES
           (1
           ,'VSMP'
           ,'VSM a Pesos'
           ,'1.0'
           ,1
           ,GETDATE()
           ,1
           ,'CSP')
GO


