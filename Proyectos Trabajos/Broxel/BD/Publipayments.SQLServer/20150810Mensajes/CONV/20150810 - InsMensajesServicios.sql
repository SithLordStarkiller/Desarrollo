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
           ,'Nuevo usuario creado'
           ,'Estimado Usuario:<br> Su cuenta se ha creado correctamente. Su nombre de usuario es {0} y su contraseña {1}<br> Autentícate en {2} para obtener accesso a la plataforma.'
           ,'NuevoUsr'
           ,'Nuevo Usuario'
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
           ,'Actualización de datos exitosa'
           ,'Estimado Usuario:<br> Sus datos se han cambiado exitosamente. <br> Nombre: {0} <br> Email: {1}'
           ,'ActUsrDatos'
           ,'Actualizacion de datos de usuario'
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
           ,'Contraseña actualizada'
           ,'Estimado Usuario:<br> Su contraseña se ha cambiado exitosamente. <br> Su nueva contraseña es {0}'
           ,'ActUsrContrasenia'
           ,'Actualizacion de contraseña'
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
           ,'Usuario bloqueado'
           ,'Estimado Usuario.<br> Ha sobrepasado el número de intentos permitidos (ip:{0}) , su cuenta permanecerá bloqueada hasta: {1},<br> si desea desbloquear en este momento, navegue al siguiente vínculo para crear una nueva contraseña:<br> {2}/Home/RecuperarContrasena?key={3}<br> Si usted no reconoce esta acción, favor de reportarlo al area de soporte tecnico.'
           ,'BloqueoUsr'
           ,'Bloqueo de usuario'
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
           ,'Recuperar contraseña'
           ,'Estimado Usuario:<br> Para recuperar su cuenta, navegue al siguiente vínculo para crear una nueva contraseña:<br> {0}/Home/RecuperarContrasena?key={1}<br> Si usted no ha solicitado ayuda con su contraseña, ignore este correo.'
           ,'RecUsrContrasenia'
           ,'Recuperar contraseña de usuario'
           ,1
           ,1)

GO

