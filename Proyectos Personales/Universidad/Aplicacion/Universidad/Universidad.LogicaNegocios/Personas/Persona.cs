namespace Universidad.LogicaNegocios.Personas
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Reflection;
    using Universidad.AccesoDatos.Personas;
    using Universidad.Entidades;
    using Universidad.Entidades.Personas;
    using Universidad.Helpers;

    public class Persona
    {
        public PER_PERSONAS InsertarPersona(PER_CAT_TELEFONOS personaTelefonos,
            PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona,
            DIR_DIRECCIONES personaDirecciones)
        {
            var log = new LogManager();

            try
            {
                var telefonos = personaTelefonos;
                var taskTelefonos =
                    Task<PER_CAT_TELEFONOS>.Factory.StartNew(() => new Telefonos().InsertaTelefonosLinq(telefonos));
                var electronicos = personaMediosElectronicos;
                var taskMediosElectronicos =
                    Task<PER_MEDIOS_ELECTRONICOS>.Factory.StartNew(
                        () => new AccesoDatos.Personas.MediosElectronicos().InsertaMediosElectronicosLinq(electronicos));
                var fotografia = personaFotografia;
                var taskFotografia =
                    Task<PER_FOTOGRAFIA>.Factory.StartNew(() => new Fotografias().InsertaFotografiaLinq(fotografia));
                var direcciones = personaDirecciones;
                var taskDirecciones =
                    Task<DIR_DIRECCIONES>.Factory.StartNew(() => new Direcciones().InsertaDireccionesLinq(direcciones));

                var resultados = new Task[] {taskTelefonos, taskMediosElectronicos, taskFotografia, taskDirecciones};

                Task.WaitAll(resultados);

                personaTelefonos = ((Task<PER_CAT_TELEFONOS>) resultados[0]).Result;
                personaMediosElectronicos = ((Task<PER_MEDIOS_ELECTRONICOS>) resultados[1]).Result;
                personaFotografia = ((Task<PER_FOTOGRAFIA>) resultados[2]).Result;
                personaDirecciones = ((Task<DIR_DIRECCIONES>) resultados[3]).Result;

                persona.IDDIRECCION = personaDirecciones.IDDIRECCION;
                persona.IDFOTO = personaFotografia.IDFOTO;
                persona.ID_MEDIOS_ELECTRONICOS = personaMediosElectronicos.ID_MEDIOS_ELECTRONICOS;
                persona.ID_TELEFONOS = personaTelefonos.ID_TELEFONOS;

                var datos = ((persona.A_PATERNO.ToCharArray())[0]) + ((persona.A_MATERNO.ToCharArray())[0]) +
                            ((persona.NOMBRE.ToCharArray())[0]) + ((persona.SEXO.ToCharArray())[0]) +
                            (persona.FECHA_NAC.Day) +
                            (persona.FECHA_NAC.Month) + (persona.FECHA_NAC.Year) + (new Random().Next(10, 99));

                var fRegistro = DateTime.Now.Day.ToString("D") + DateTime.Now.Month.ToString("D") +
                                DateTime.Now.Year.ToString("D");

                var cve = datos + fRegistro + persona.ID_TIPO_PERSONA;
                persona.NOMBRE_COMPLETO = persona.NOMBRE + " " + persona.A_PATERNO + " " + persona.A_MATERNO;
                persona.ID_PER_LINKID = cve.ToUpper();
                persona.FECHAINGRESO = DateTime.Now;

                persona = new AccesoDatos.Personas.Personas().InsertaPersonaLinq(persona);

                return persona;
            }
            catch (Exception e)
            {
                log.RegistraExcepcion(e, GetType().Name, MethodBase.GetCurrentMethod().Name, "Servidor Interno");
                return null;
            }
        }

        public PER_PERSONAS BuscarPersona(string idPersonaLink)
        {
            return new Personas().BuscarPersonaLinq(idPersonaLink);
        }

        public DatosCompletosPersona BuscarPersonaCompleta(string idPersonaLink)
        {
            return new Personas().BuscaPersonaCompletaLinq(idPersonaLink);
        }

        public List<PER_PERSONAS> ObtenListaPersonas()
        {
            return new Personas().ObtenListaPersonasLinq();
        }

        public List<PER_PERSONAS> ObtenListaPersonasFiltro(string idPersona, DateTime? fechaInicio, DateTime? fechaFinal,
            int? idTipoPersona)
        {
            return new Personas().ObtenPersonasFiltroLinq(idPersona, fechaInicio, fechaFinal,
                idTipoPersona);
        }

        public DIR_DIRECCIONES ObtenDirecciones(PER_PERSONAS persona)
        {
            return new Personas().ObtenDireccionLinq(persona);
        }

        public PER_CAT_TELEFONOS ObtenTelefonos(PER_PERSONAS persona)
        {
            return new Personas().ObtenTelefonosLinq(persona);
        }

        public PER_MEDIOS_ELECTRONICOS ObtenMediosElectronicos(PER_PERSONAS personas)
        {
            return new Personas().ObtenMediosElectronicosLinq(personas);
        }

        public PER_FOTOGRAFIA ObtenFotografia(PER_PERSONAS personas)
        {
            return new Personas().ObtenFotografiaLinq(personas);
        }

        public PER_CAT_TIPO_PERSONA ObtenTipoPersona(int idTipoPersona)
        {
            return new Personas().ObtentipoPersonaLinq(idTipoPersona);
        }

        public PER_CAT_NACIONALIDAD ObtenPersonaPais(int idPersonaNacionalidad)
        {
            return new Personas().ObtenPersonaPais(idPersonaNacionalidad);
        }
    }
}
