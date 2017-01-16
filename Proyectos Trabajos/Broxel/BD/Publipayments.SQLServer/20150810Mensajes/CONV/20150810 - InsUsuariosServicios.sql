-- Open the symmetric key with which to encrypt the data.
OPEN SYMMETRIC KEY ServiciosPW_Key11
   DECRYPTION BY CERTIFICATE ServiciosPW;

INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo]) 
VALUES (1,'noreply@publipayments.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130),'67896789'), 1, HashBytes('SHA1', CONVERT( varbinary, 1))),1,1)

GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo]) 
values (1,'no-reply@publipayments.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130),'Publi-6789') , 1, HashBytes('SHA1', CONVERT( varbinary, 1))),2,1)

GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo])
 values ( 1,'broxel4',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130),'evKA3McJ'), 1, HashBytes('SHA1', CONVERT( varbinary, 1))),1,2)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo]) 
values (2,'noreply@publipayments.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130),'67896789'), 1, HashBytes('SHA1', CONVERT( varbinary, 2))),1,1)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo]) 
values (2,'no-reply@publipayments.com', EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130),'Publi-6789'), 1, HashBytes('SHA1', CONVERT( varbinary, 2))),2,1)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo])
values (2,'broxel4', EncryptByKey(Key_GUID('ServiciosPW_Key11') ,CONVERT(nvarchar(130),'evKA3McJ'), 1, HashBytes('SHA1', CONVERT( varbinary, 2))),1,2)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo]) 
values (3,'noreply@publipayments.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') ,CONVERT(nvarchar(130),'67896789'), 1, HashBytes('SHA1', CONVERT( varbinary, 3))),1,1)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo])
 VALUES (3,'no-reply@publipayments.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130),'Publi-6789'), 1, HashBytes('SHA1', CONVERT( varbinary, 3))),2,1)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo])
 VALUES (3,'broxel4',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130),'evKA3McJ'), 1, HashBytes('SHA1', CONVERT( varbinary, 3))),1,2)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo])
 VALUES (6,'noreply@01800pagos.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') ,CONVERT(nvarchar(130),'6y&PL=hT'), 1, HashBytes('SHA1', CONVERT( varbinary, 6))),1,1)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo])
 VALUES (7,'noreply@01800pagos.com', EncryptByKey(Key_GUID('ServiciosPW_Key11') ,CONVERT(nvarchar(130),'6y&PL=hT'), 1, HashBytes('SHA1', CONVERT( varbinary, 7))),1,1)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo]) 
VALUES (8,'noreply@publipayments.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130), '67896789'), 1, HashBytes('SHA1', CONVERT( varbinary, 8))),1,1)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo])
 VALUES (8,'no-reply@publipayments.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130), 'Publi-6789'), 1, HashBytes('SHA1', CONVERT( varbinary, 8))),2,1)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo])
 VALUES (8,'broxel4',EncryptByKey(Key_GUID('ServiciosPW_Key11') , CONVERT(nvarchar(130), 'evKA3McJ'), 1, HashBytes('SHA1', CONVERT( varbinary, 8))),1,2)
GO
INSERT INTO [dbo].[UsuariosServicios]([idAplicacion],[Usuario],[Password],[Orden],[Tipo]) 
VALUES (9,'noreply@publipayments.com',EncryptByKey(Key_GUID('ServiciosPW_Key11') ,  CONVERT(nvarchar(130), '67896789'), 1, HashBytes('SHA1', CONVERT( varbinary, 9))),1,1)

