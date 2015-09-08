using System.Collections.Generic;
using System.Threading.Tasks;
using Universidad.Controlador.SVRPersonas;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;
using Universidad.Entidades.Personas;

namespace Universidad.Controlador.Personas
{
    public class SvcPersonas
    {
        #region Propiedades de la clase

        private readonly SPersonasClient _servicio;

        public SvcPersonas(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new SPersonasClient(configServicios.ObtenBasicHttpBinding(),
                configServicios.ObtenEndpointAddress(sesion, @"Personas/", "SPersonas.svc"));
        }

        #endregion

        public Task<bool> ExisteCorreoUniversidad(string correo)
        {
            return Task.Run(() => _servicio.ExisteCorreoUniversidadAsync(correo));
        }

        public Task<PER_PERSONAS> InsertarPersona(PER_CAT_TELEFONOS personaTelefonos,
            PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona,
            DIR_DIRECCIONES personaDirecciones)
        {
            return
                Task.Run(
                    () =>
                        _servicio.InsertarPersonaAsync(personaTelefonos, personaMediosElectronicos, personaFotografia,
                            persona, personaDirecciones));
        }

        public Task<PER_PERSONAS> BuscarPersona(string idPersonaLink)
        {
            return Task.Run(() => _servicio.BuscarPersonaAsync(idPersonaLink));
        }

        public Task<DatosCompletosPersona> BuscarPersonaCompleta(string idPersonaLink)
        {
            return Task.Run(() => _servicio.BuscarPersonaCompletaAsync(idPersonaLink));
        }

        public Task<List<PER_PERSONAS>> ObtenListaPersonas()
        {
            return Task.Run(() => _servicio.ObtenListaPersonasAsync());
        }

        public Task<List<PER_CAT_TIPO_PERSONA>> ObtenCatTipoPersona()
        {
            return Task.Run(() => _servicio.ObtenCatTipoPersonaAsync());
        }
    }
}
