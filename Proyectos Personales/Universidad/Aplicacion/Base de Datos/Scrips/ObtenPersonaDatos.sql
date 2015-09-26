SELECT 
pp.ID_PERSONA as IdPersona,
pp.ID_PER_LINKID as IdLinkPersona,
pp.NOMBRE as Nombre,
pp.A_PATERNO as ApellidoP,
pp.A_MATERNO as ApellidoM,
pp.NOMBRE_COMPLETO as NombreCompleto,
pp.FECHA_NAC as FechaNacimiento,
pp.FECHAINGRESO as FechaIngreso,
pp.SEXO as Sexo,
pp.CURP as Curp,
pp.RFC as Rfc,
pp.IMSS as Nss,
ptp.TIPO_PERSONA as TipoPersona,
pn.NOMBRE_PAIS as Nacionalidad,
de.NOMBREESTADO as Estado,
dm.NOMBREDELGMUNICIPIO as Municipio,
dc.NOMBRECOLONIA as Colonia,
dc.CODIGOPOSTAL as CodigoPostal,
pd.CALLE as Calle,
pd.NOEXT as NoExt,
pd.NOINT as NoInt,
pd.REFERENCIAS as Referencias,
pt.TELEFONO_FIJO_DOMICILIO as TelefonoFijoDomicilio,
pt.TELEFONO_FIJO_TRABAJO as TelefonoFijoTrabajo,
pt.TELEFONO_CELULAR_PERSONAL as TelefonoMovilPersonal,
pt.TELEFONO_CELULAR_TRABAJO as TelefonoMovilTrabajo,
pt.FAX as Fax,
pm.CORREO_ELECTRONICO_PERSONAL as CorreoPersonal,
pm.CORREO_ELECTRONICO_UNIVERSIDAD as CorreoUniversidad,
pm.FACEBOOK as RedSocial1,
pm.TWITTER as RedSocial2,
pf.NOMBRE as NombreFoto,
pf.EXTENCION as ExtencionFoto,
pf.FOTOGRAFIA as Fotografia,
pu.USUARIO as Usuario,pu.CONTRASENA as Contrasena
from PER_PERSONAS AS pp
left outer join PER_CAT_NACIONALIDAD AS pn on pp.CVE_NACIONALIDAD = pn.CVE_NACIONALIDAD
left outer join PER_CAT_TIPO_PERSONA as ptp on pp.ID_TIPO_PERSONA = ptp.ID_TIPO_PERSONA 
left outer join DIR_DIRECCIONES as pd on pp.IDDIRECCION = pd.IDDIRECCION
left outer join DIR_CAT_ESTADO as de on pd.IDESTADO = de.IDESTADO 
left outer join DIR_CAT_DELG_MUNICIPIO as dm on dm.IDMUNICIPIO = pd.IDMUNICIPIO  and pd.IDESTADO = dm.IDESTADO
left outer join DIR_CAT_COLONIAS as dc on pd.IDCOLONIA = dc.IDCOLONIA 
left outer join  PER_CAT_TELEFONOS as pt on pp.ID_TELEFONOS = pt.ID_TELEFONOS
left outer join PER_MEDIOS_ELECTRONICOS as pm on pp.ID_MEDIOS_ELECTRONICOS = pm.ID_MEDIOS_ELECTRONICOS
left outer join PER_FOTOGRAFIA as pf on pp.IDFOTO = pf.IDFOTO 
left outer join US_USUARIOS as pu on pp.ID_USUARIO = pu.ID_USUARIO 

                --where pp.ID_PER_LINKID = idPersonaLink