--SELECT * FROM (VALUES
--('US_CAT_ESTATUS_USUARIO'),
--('US_CAT_NIVEL_USUARIO'),
--('US_CAT_TIPO_USUARIO'),
--('US_HISTORIAL'),
--('SIS_CAT_TABPAGES'),
--('SIS_AADM_ARBOLMENUS'),
--('SIS_AADM_APLICACIONES'),
--('SIS_WADM_ARBOLMENU'),
--('SIS_WADM_ARBOLMENU_MVC'),
--('PER_CAT_NACIONALIDAD'),
--('PER_CAT_TELEFONOS'),
--('PER_CAT_TIPO_PERSONA'),
--('PER_MEDIOS_ELECTRONICOS'),
--('US_USUARIOS'),
--('DIR_DIRECCIONES'),
--('SIS_AADM_APLICACIONES'),
--('PER_PERSONAS')) E(TABLAS)
 
DELETE FROM PER_PERSONAS
DELETE FROM SIS_AADM_APLICACIONES
DELETE FROM US_USUARIOS
DELETE FROM PER_MEDIOS_ELECTRONICOS
DELETE FROM PER_CAT_TIPO_PERSONA
DELETE FROM PER_CAT_TELEFONOS
DELETE FROM PER_CAT_NACIONALIDAD
DELETE FROM SIS_WADM_ARBOLMENU_MVC
DELETE FROM SIS_WADM_ARBOLMENU
DELETE FROM SIS_AADM_APLICACIONES
DELETE FROM SIS_AADM_ARBOLMENUS
DELETE FROM SIS_CAT_TABPAGES
DELETE FROM US_HISTORIAL
DELETE FROM US_CAT_TIPO_USUARIO
DELETE FROM US_CAT_NIVEL_USUARIO
DELETE FROM US_CAT_ESTATUS_USUARIO