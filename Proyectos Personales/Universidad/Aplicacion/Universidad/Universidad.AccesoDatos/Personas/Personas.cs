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

        public PER_PERSONAS BuscarPersona(string idPersonaLink)
        {
            using (var r = new Repositorio<PER_PERSONAS>())
            {
                return r.Extraer(a => a.ID_PER_LINKID == idPersonaLink);
            }
        }

        public DatosCompletosPersona BuscaPersonaCompleta(string idPersonaLink)
        {
            var persona = (
                from pp in _contexto.PER_PERSONAS

                join pd in _contexto.DIR_DIRECCIONES on pp.IDDIRECCION equals pd.IDDIRECCION into td
                from pd in td.DefaultIfEmpty()

                join de in _contexto.DIR_CAT_ESTADO on pd.IDESTADO equals de.IDESTADO into tde
                from de in tde.DefaultIfEmpty()

                join pt in _contexto.PER_CAT_TELEFONOS on pp.ID_TELEFONOS equals pt.ID_TELEFONOS into tt
                from pt in tt.DefaultIfEmpty()

                where pp.ID_PER_LINKID == idPersonaLink

                select new DatosCompletosPersona
                {
                    IdLinkPersona = pp.ID_PER_LINKID,
                    Nombre = pp.NOMBRE,
                    ApellidoP = pp.A_PATERNO,
                    ApellidoM = pp.A_MATERNO,
                    NombreCompleto = pp.NOMBRE_COMPLETO,
                    FechaNacimiento = pp.FECHAINGRESO,
                    FechaIngreso = pp.FECHAINGRESO,
                    Sexo = pp.SEXO,
                    Curp = pp.CURP,
                    Rfc = pp.RFC,
                    Nss = pp.IMSS,
                    Estado = pp.IDDIRECCION == null ? "Sin Estado" : de.NOMBREESTADO

                }).ToList().FirstOrDefault();

            return persona;

        }
    }
}
