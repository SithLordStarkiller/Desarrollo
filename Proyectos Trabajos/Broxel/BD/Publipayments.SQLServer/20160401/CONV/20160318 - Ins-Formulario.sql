
DECLARE @idAplicacion INT 

SELECT @idAplicacion=CONVERT(INT,valor) FROM catalogogeneral where llave='idaplicacion'

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
           (@idAplicacion
           ,'RegistroUsuario'
           ,'Registro de asesores'
           ,'1.0'
           ,1
           ,GETDATE()
           ,1
           ,'RA'
		   )
GO


