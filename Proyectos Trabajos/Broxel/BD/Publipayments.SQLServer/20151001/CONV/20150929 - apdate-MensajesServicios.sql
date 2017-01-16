
update mensajesservicios set titulo='Cambio de contraseña',clave='ResetUsrContrasenia',Descripcion='Reset de contraseña' where titulo='Contraseña actualizada'
GO
INSERT INTO [dbo].[MensajesServicios]([idAplicacion],[Titulo],[Mensaje],[Clave],[Descripcion],[EsHtml],[Tipo]) VALUES (0,'Contraseña actualizada','Estimado Usuario:<br> Su contraseña se ha cambiado exitosamente.','ActUsrContrasenia','Actualizacion de contraseña',1,1)
GO




