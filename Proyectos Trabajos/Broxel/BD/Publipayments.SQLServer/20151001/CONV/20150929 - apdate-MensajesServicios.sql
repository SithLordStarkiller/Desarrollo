
update mensajesservicios set titulo='Cambio de contraseņa',clave='ResetUsrContrasenia',Descripcion='Reset de contraseņa' where titulo='Contraseņa actualizada'
GO
INSERT INTO [dbo].[MensajesServicios]([idAplicacion],[Titulo],[Mensaje],[Clave],[Descripcion],[EsHtml],[Tipo]) VALUES (0,'Contraseņa actualizada','Estimado Usuario:<br> Su contraseņa se ha cambiado exitosamente.','ActUsrContrasenia','Actualizacion de contraseņa',1,1)
GO




