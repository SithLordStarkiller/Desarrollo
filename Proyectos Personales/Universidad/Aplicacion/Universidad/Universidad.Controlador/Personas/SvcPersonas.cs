using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Universidad.Controlador.SVRPersonas;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

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
    }
}
