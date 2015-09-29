using System;
using System.Collections.Generic;
using System.Linq;
using Universidad.Entidades;
using Universidad.Entidades.Personas;

namespace Universidad.AccesoDatos.Personas
{
    public class Personas
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public PER_PERSONAS InsertaPersonaLinq(PER_PERSONAS persona)
        {
            using (var r = new Repositorio<PER_PERSONAS>())
            {
                return r.Agregar(persona);
            }
        }

        public bool ActualizaPersonaLinq(PER_PERSONAS persona)
        {
            using (var r = new Repositorio<PER_PERSONAS>())
            {
                return r.Actualizar(persona);
            }
        }

        public PER_PERSONAS BuscarPersonaLinq(string idPersonaLink)
        {
            using (var r = new Repositorio<PER_PERSONAS>())
            {
                return r.Extraer(a => a.ID_PER_LINKID == idPersonaLink);
            }
        }

        public DatosCompletosPersona BuscaPersonaCompletaLinq(string idPersonaLink)
        {
            var persona = (
                from pp in _contexto.PER_PERSONAS

                join pn in _contexto.PER_CAT_NACIONALIDAD on pp.CVE_NACIONALIDAD equals pn.CVE_NACIONALIDAD into tn
                from pn in tn.DefaultIfEmpty()

                join ptp in _contexto.PER_CAT_TIPO_PERSONA on pp.ID_TIPO_PERSONA equals ptp.ID_TIPO_PERSONA into tptp
                from ptp in tptp.DefaultIfEmpty()

                join pd in _contexto.DIR_DIRECCIONES on pp.IDDIRECCION equals pd.IDDIRECCION into td
                from pd in td.DefaultIfEmpty()

                join de in _contexto.DIR_CAT_ESTADO on pd.IDESTADO equals de.IDESTADO into tde
                from de in tde.DefaultIfEmpty()

                join dm in _contexto.DIR_CAT_DELG_MUNICIPIO on new { idMunicipio = pd.IDMUNICIPIO, idEstado = pd.IDESTADO } equals new { idMunicipio = dm.IDMUNICIPIO, idEstado = dm.IDESTADO} into tdm
                from dm in tdm.DefaultIfEmpty()

                join dc in _contexto.DIR_CAT_COLONIAS on pd.IDCOLONIA equals dc.IDCOLONIA into tdc
                from dc in tdc.DefaultIfEmpty()

                join pt in _contexto.PER_CAT_TELEFONOS on pp.ID_TELEFONOS equals pt.ID_TELEFONOS into tt
                from pt in tt.DefaultIfEmpty()

                join pm in _contexto.PER_MEDIOS_ELECTRONICOS on pp.ID_MEDIOS_ELECTRONICOS equals pm.ID_MEDIOS_ELECTRONICOS into tm
                from pm in tm.DefaultIfEmpty()

                join pf in _contexto.PER_FOTOGRAFIA on pp.IDFOTO equals pf.IDFOTO into tf
                from pf in tf.DefaultIfEmpty()

                join pu in _contexto.US_USUARIOS on pp.ID_USUARIO equals pu.ID_USUARIO into tu
                from pu in tu.DefaultIfEmpty()

                where pp.ID_PER_LINKID == idPersonaLink

                select new DatosCompletosPersona
                {
                    IdPersona = pp.ID_PERSONA,
                    IdLinkPersona = pp.ID_PER_LINKID,
                    Nombre = pp.NOMBRE,
                    ApellidoP = pp.A_PATERNO,
                    ApellidoM = pp.A_MATERNO,
                    NombreCompleto = pp.NOMBRE_COMPLETO,
                    FechaNacimiento = pp.FECHA_NAC,
                    FechaIngreso = pp.FECHAINGRESO,
                    Sexo = pp.SEXO,
                    Curp = pp.CURP,
                    Rfc = pp.RFC,
                    Nss = pp.IMSS,
                    TipoPersona = ptp.TIPO_PERSONA,
                    Nacionalidad = pn.NOMBRE_PAIS,
                    Estado = pp.IDDIRECCION == null ? "Sin direccion" : de.NOMBREESTADO,
                    Municipio = pp.IDDIRECCION == null ? "Sin direccion" : dm.NOMBREDELGMUNICIPIO,
                    Colonia = pp.IDDIRECCION == null ? "Sin direccion" : dc.NOMBRECOLONIA,
                    CodigoPostal = pp.IDDIRECCION == null ? "Sin direccion" : dc.CODIGOPOSTAL.ToString(),
                    Calle = pp.IDDIRECCION == null ? "Sin Direccion" : pd.CALLE,
                    NoExt = pp.IDDIRECCION == null || pd.NOEXT != "" ? "" : pd.NOEXT,
                    NoInt = pp.IDDIRECCION == null || pd.NOINT != "" ? "" : pd.NOINT,
                    Referencias = pp.IDDIRECCION == null ? "Sin Direccion" : pd.REFERENCIAS,
                    TelefonoFijoDomicilio = pt.ID_TELEFONOS == null ? "Sin Telefono" : pt.TELEFONO_FIJO_DOMICILIO,
                    TelefonoFijoTrabajo = pt.ID_TELEFONOS == null ? "Sin Telefono" : pt.TELEFONO_FIJO_TRABAJO,
                    TelefonoMovilPersonal = pt.ID_TELEFONOS == null ? "Sin Telefono" : pt.TELEFONO_CELULAR_PERSONAL,
                    TelefonoMovilTrabajo = pt.ID_TELEFONOS == null ? "Sin Telefono" : pt.TELEFONO_CELULAR_TRABAJO,
                    Fax = pp.ID_TELEFONOS == null ? "Sin Telefono" : pt.FAX,
                    CorreoPersonal = pp.ID_MEDIOS_ELECTRONICOS == null ? "Sin correo" : pm.CORREO_ELECTRONICO_PERSONAL,
                    CorreoUniversidad = pp.ID_MEDIOS_ELECTRONICOS == null ? "Sin corrreo" : pm.CORREO_ELECTRONICO_UNIVERSIDAD,
                    RedSocial1 = pp.ID_MEDIOS_ELECTRONICOS == null ? "Sin RedSocial" : pm.FACEBOOK,
                    RedSocial2 = pp.ID_MEDIOS_ELECTRONICOS == null ? "Sin RedSocial" : pm.TWITTER,
                    NombreFoto = pp.IDFOTO == null ? "Sin Fotografia" : pf.NOMBRE,
                    ExtencionFoto = pp.IDFOTO == null ? "Sin Fotografia" : pf.EXTENCION,
                    Fotografia = pp.IDFOTO == null ? null : pf.FOTOGRAFIA,
                    Usuario = pp.ID_USUARIO == null ? "Sin usuario" : pu.USUARIO,
                    Contrasena = pp.ID_USUARIO == null ? "Sin Usuario" : pu.CONTRASENA
                }).ToList().FirstOrDefault();

            return persona;
        }

        public List<PER_PERSONAS> ObtenListaPersonasLinq()
        {
            using (var r = new Repositorio<PER_PERSONAS>())
            {
                return r.TablaCompleta();
            }
        }

        public List<PER_PERSONAS> ObtenPersonasFiltroLinq(string idPersona, DateTime? fechaInicio, DateTime? fechaFinal, int? idTipoPersona)
        {
            List<PER_PERSONAS> personas;

            using (var r = new Repositorio<PER_PERSONAS>())
            {
                 personas = r.TablaCompleta();
            }

            if (fechaInicio != null && fechaFinal != null && idPersona == "" && idTipoPersona == null)
            {
                personas = personas.Where(r => r.FECHAINGRESO >= fechaInicio && r.FECHAINGRESO <= fechaFinal).ToList();
            }
            else if (fechaInicio == null && fechaFinal == null && idPersona != "" && idTipoPersona == null)
            {
                personas = personas.Where(r => r.ID_PER_LINKID == idPersona).ToList();
            }
            else if (fechaInicio == null && fechaFinal == null && idPersona == "" && idTipoPersona != null)
            {
                personas = personas.Where(r => r.ID_TIPO_PERSONA == idTipoPersona).ToList();
            }
            else if (fechaInicio == null && fechaFinal == null && idPersona != "" && idTipoPersona != null)
            {
                personas = personas.Where(r => r.ID_PER_LINKID == idPersona && r.ID_TIPO_PERSONA == idTipoPersona).ToList();
            }
            else if (fechaInicio != null && fechaFinal != null && idPersona != "" && idTipoPersona == null)
            {
                personas = personas.Where(r => r.FECHAINGRESO >= fechaInicio && r.FECHAINGRESO <= fechaFinal && r.ID_PER_LINKID == idPersona).ToList();
            }
            else if (fechaInicio != null && fechaFinal != null && idPersona == "" && idTipoPersona != null)
            {
                personas = personas.Where(r => r.FECHAINGRESO >= fechaInicio && r.FECHAINGRESO <= fechaFinal && r.ID_TIPO_PERSONA == idTipoPersona).ToList();
            }
            else if (fechaInicio != null && fechaFinal != null && idPersona != "" && idTipoPersona != null)
            {
                personas = personas.Where(r => r.FECHAINGRESO >= fechaInicio && r.FECHAINGRESO <= fechaFinal && r.ID_PER_LINKID == idPersona && r.ID_TIPO_PERSONA == idTipoPersona).ToList();
            }

            return personas;
        }

        public DIR_DIRECCIONES ObtenDireccionLinq(PER_PERSONAS persona)
        {
            using (var x = new Repositorio<DIR_DIRECCIONES>())
            {
                return x.Extraer(r => r.IDDIRECCION == persona.IDDIRECCION);
            }
        }

        public PER_CAT_TELEFONOS ObtenTelefonosLinq(PER_PERSONAS persona)
        {
            using (var x = new Repositorio<PER_CAT_TELEFONOS>())
            {
                return x.Extraer(r => r.ID_TELEFONOS == persona.ID_TELEFONOS);
            }
        }

        public PER_MEDIOS_ELECTRONICOS ObtenMediosElectronicosLinq(PER_PERSONAS personas)
        {
            using (var x = new Repositorio<PER_MEDIOS_ELECTRONICOS>())
            {
                return x.Extraer(r => r.ID_MEDIOS_ELECTRONICOS == personas.ID_MEDIOS_ELECTRONICOS);
            }
        }

        public PER_FOTOGRAFIA ObtenFotografiaLinq(PER_PERSONAS persona)
        {
            using (var x = new Repositorio<PER_FOTOGRAFIA>())
            {
                return x.Extraer(r => r.IDFOTO == persona.ID_PERSONA);
            }
        }
    }
}
