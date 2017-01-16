INSERT INTO [dbo].[MensajesServicios]
           ([idAplicacion]
           ,[Titulo]
           ,[Mensaje]
           ,[Clave]
           ,[Descripcion]
           ,[EsHtml]
           ,[Tipo])
     VALUES
           (0
           ,'Resultado del archivo procesado: {0}'
           ,'Se procesaron {0} registros. <br>Se encontraron {1} errores durante el proceso. <br>Archivo: {2} - favor de consultar la plataforma para obtener más detalles. Fecha: {3}'
           ,'ArchivoProcesado'
           ,'Archivo procesado'
           ,1
           ,1)
GO



INSERT INTO [dbo].[MensajesServicios]
           ([idAplicacion]
           ,[Titulo]
           ,[Mensaje]
           ,[Clave]
           ,[Descripcion]
           ,[EsHtml]
           ,[Tipo])
     VALUES
           (0
           ,'Creacion exitosa de archivo {0}'
           ,'El archivo {0}, procesó {1} registros exitosamente, fecha: {2}'
           ,'ArchivoCreado'
           ,'Archivo creado exitosamente'
           ,1
           ,1)
GO



