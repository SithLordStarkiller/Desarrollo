CREATE NONCLUSTERED INDEX IX_CamposRespuesta01 ON [dbo].[CamposRespuesta] ([Nombre]) INCLUDE ([idCampo])
GO

SET IDENTITY_INSERT dbo.CatDictamen ON
GO

INSERT INTO CatDictamen (idCatalogo, Clave, Descripcion, Nombre, CV_RUTA)
VALUES (0, '', 'Sin dictamen', 'DictamenSinDictamen', '')
GO

SET IDENTITY_INSERT dbo.CatDictamen OFF
GO

ALTER TABLE dbo.Ordenes ADD idDictamen INT NOT NULL CONSTRAINT DF_Ordenes_idDictamen DEFAULT 0
GO

ALTER TABLE dbo.Ordenes ADD CONSTRAINT FK_Ordenes_CatDictamen FOREIGN KEY (idDictamen) 
REFERENCES dbo.CatDictamen (idCatalogo) ON UPDATE CASCADE ON DELETE CASCADE
GO

CREATE NONCLUSTERED INDEX IX_Ordenes008
ON [dbo].[Ordenes] ([Estatus],[CvRuta],[idUsuario],[idDominio])
INCLUDE ([idUsuarioPadre],[cvDelegacion])
GO

ALTER TABLE dbo.Utils_Descripciones
	DROP CONSTRAINT UQ__Utils_De__09EC173A0EA330E9
GO

ALTER TABLE dbo.Utils_Descripciones ADD CONSTRAINT
	UQ__Utils_De__09EC173A0EA330E9 UNIQUE NONCLUSTERED 
	(
	fc_Clave,
	Ruta
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE	dbo.Utils_Descripciones ADD 
	[Ruta] varchar(10) NULL,
	[idRol] int NULL

INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('RDST_CREDPAGOS','Créditos con Pago Completo','1','DASHBOARD','50','1','msilva','Sep 26 2016 12:00AM','1','RDST','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_CREDASIGPOOL','Créditos asignados pool','0','DASHBOARD','0','0','msilva','Sep 26 2016 12:00AM','1','*','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_CREDASIGMOVIL','Créditos asignados móvil','0','DASHBOARD','1','1','msilva','Sep 26 2016 12:00AM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_TOTALMES','Días del Mes Actual','1','DASHBOARD','0','1','msilva','Sep 26 2016 12:00AM','1','*','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_DIASREST','Días Restantes','1','DASHBOARD','10','1','msilva','Sep 26 2016 12:00AM','1','*','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_CREDVISITADOS','Créditos visitados','1','DASHBOARD','20','1','msilva','Sep 26 2016 12:00AM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_GESTNVAVISITA','Créditos por volver a visitar','1','DASHBOARD','30','1','msilva','Sep 26 2016  2:01PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_CREDSINASIG','Créditos sin asignar a móvil','1','DASHBOARD','40','1','msilva','Sep 26 2016  2:02PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_CREDSINORD','Créditos sin asignar a supervisor','1','DASHBOARD','50','1','msilva','Sep 26 2016  2:03PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_CREDENMOVIL','Créditos en móvil','1','DASHBOARD','60','1','msilva','Sep 26 2016  2:04PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_VISITADOSREAL','Visitas realizadas','2','DASHBOARD','0','1','msilva','Sep 26 2016  2:04PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_CREDCONSOLUCI','Gestiones autorizadas','2','DASHBOARD','10','1','msilva','Sep 26 2016  2:05PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_GESTSINAUTH','Gestiones no autorizadas','2','DASHBOARD','20','1','msilva','Sep 26 2016  2:05PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_GESTREASIG','Gestiones por reasignar','2','DASHBOARD','30','1','msilva','Sep 26 2016  2:05PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_GESTVISPROM','Gestiones visitadas promedio','2','DASHBOARD','40','1','msilva','Sep 26 2016  2:06PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('DASH_GESTSINCRO','Gestiones sincronizando','2','DASHBOARD','50','1','msilva','Sep 26 2016  2:06PM','1','CSD','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('RDST_MARCAJENOEXITOSO','Créditos Marcaje no Exitoso','2','DASHBOARD','30','1','ldotor','Sep 27 2016 12:18PM','1','RDST','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('RDST_CREDSOLUCIONADOS','Créditos Solucionados','1','DASHBOARD','60','1','msilva','Sep 27 2016 12:00AM','1','RDST','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('RDST_CREDGEST','Créditos gestionados','0','DASHBOARD','1','1','msilva','Sep 27 2016 12:00AM','1','RDST','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('RDST_CREDSINSOLUCION','Créditos sin Solución','2','DASHBOARD','20','1','ldotor','Sep 27 2016  4:18PM','1','RDST','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('RDST_CREDICTAMINADOS','Créditos Dictaminados','2','DASHBOARD','10','1','msilva','Sep 28 2016 12:00AM','1','RDST','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('RDST_GESTPROMDIA','Gestiones promedio por día','2','DASHBOARD','40','1','ldotor','Sep 28 2016 12:43PM','1','RDST','-99')
INSERT INTO [Utils_Descripciones]([fc_Clave],[fc_Descripcion],[fi_Parte],[fc_Modulo],[fi_Orden],[fi_Activo],[fc_Usuario],[fd_FechaMod],[fi_idPermisos],[Ruta],[idRol])VALUES('RDST_GESTPROMASESOR','Gestiones promedio por asesor','2','DASHBOARD','50','1','ldotor','Sep 28 2016 12:43PM','1','RDST','-99')
