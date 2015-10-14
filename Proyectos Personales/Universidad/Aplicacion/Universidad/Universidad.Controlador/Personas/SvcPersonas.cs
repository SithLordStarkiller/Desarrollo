﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universidad.Controlador.SvcPersonas;
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

        public Task<List<PER_PERSONAS>> ObtenListaPersonasFiltro(string idPersona,DateTime? fechaInicial,DateTime? fechaFinal, int? idTipoPersona)
        {
            return Task.Run(() => _servicio.ObtenListaPersonaFiltroAsync(idPersona,fechaInicial,fechaFinal,idTipoPersona));
        }

        public Task<List<PER_CAT_TIPO_PERSONA>> ObtenCatTipoPersona()
        {
            return Task.Run(() => _servicio.ObtenCatTipoPersonaAsync());
        }

        public Task<DIR_DIRECCIONES> ObtenDireccion(PER_PERSONAS persona)
        {
            return Task.Run(() => _servicio.ObtenDireccionesAsync(persona));
        }

        public Task<PER_CAT_TELEFONOS> ObtenTelefonos(PER_PERSONAS persona)
        {
            return Task.Run(() => _servicio.ObtenTelefonosAsync(persona));
        }

        public Task<PER_MEDIOS_ELECTRONICOS> ObtenMediosElectronicos(PER_PERSONAS persona)
        {
            return Task.Run(() => _servicio.ObtenMediosElectronicosAsync(persona));
        }

        public Task<PER_FOTOGRAFIA> ObtenFotografia(PER_PERSONAS persona)
        {
            return Task.Run(() => _servicio.ObtenFotografiaAsync(persona));
        }
    }
}
