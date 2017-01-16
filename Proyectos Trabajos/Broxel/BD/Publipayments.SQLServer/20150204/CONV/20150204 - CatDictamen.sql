GO
alter table [CatDictamen] drop column  [idCampo]
GO
alter table [CatDictamen] add  CV_RUTA varchar(10)

GO
update [CatDictamen] set CV_RUTA='CSD'
GO
delete from CatDictamen where idCatalogo in (
53
,24
,59
,58
,54
,47
,56
,55
,46
,52
,50
,62
,51
,57
,61
,75
,74
)
GO
CREATE UNIQUE INDEX IX_CatDictamen_01  ON dbo.CatDictamen (Nombre,CV_RUTA) INCLUDE (Clave)