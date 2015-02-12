using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.AccesoDatos.Personas;
using Universidad.Entidades;

namespace Universidad.LogicaNegocios.Personas
{
    public class EventoOperaciones : EventArgs
    {
        private readonly PER_PERSONAS Persona;
        private DIR_DIRECCIONES _direccion;
        private bool _direccionFinalizado;
        private PER_MEDIOS_ELECTRONICOS _mediosElectronicos;
        private bool _mediosElectronicosFinalizado = false;
        private PER_CAT_TELEFONOS _telefonos;
        private bool _telefonosFinalizado;
        private PER_FOTOGRAFIA _fotografia;
        private bool _fotografiaFinalizado;

        public delegate void EventoHandler(PER_CAT_TELEFONOS personaTelefonos,
            PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona, DIR_DIRECCIONES personaDirecciones);
        public event EventoHandler AlfinalizarOperaciones;

        public EventoOperaciones(PER_PERSONAS persona)
        {
            Persona = persona;
        }

        public void DireccionesCompleto(DIR_DIRECCIONES direcciones)
        {
            _direccion = direcciones;
            _direccionFinalizado = true;
        }

        public void MediosEletronicosCompleto(PER_MEDIOS_ELECTRONICOS mediosElectronicos)
        {
            this._mediosElectronicos = mediosElectronicos;
            _mediosElectronicosFinalizado = true;
        }

        public void TelefonosCompleto(PER_CAT_TELEFONOS telefonos)
        {
            _telefonos = telefonos;
            _telefonosFinalizado = true;
        }

        public void FotografiasCompleto(PER_FOTOGRAFIA fotografia)
        {
            _fotografia = fotografia;
            _fotografiaFinalizado = true;
        }

        public void Finalizado()
        {
            if (_direccionFinalizado && _mediosElectronicosFinalizado && _telefonosFinalizado && _fotografiaFinalizado)
                AlfinalizarOperaciones(_telefonos,_mediosElectronicos,_fotografia,Persona,_direccion);
        }
    }
    public class Persona
    {
        private EventoOperaciones _evento;
        public PER_PERSONAS InsertarPersona(PER_CAT_TELEFONOS personaTelefonos,
            PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona, DIR_DIRECCIONES personaDirecciones)
        {
            _evento = new EventoOperaciones(persona);
            _evento.AlfinalizarOperaciones += _evento_AlfinalizarOperaciones;
            return null;
        }

        void _evento_AlfinalizarOperaciones(PER_CAT_TELEFONOS personaTelefonos, PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona, DIR_DIRECCIONES personaDirecciones)
        {
            persona.IDDIRECCION = personaDirecciones.IDDIRECCION;
            persona.IDFOTO = personaFotografia.IDFOTO;
            persona.ID_MEDIOS_ELECTRONICOS = personaMediosElectronicos.ID_MEDIOS_ELECTRONICOS;
            persona.ID_TELEFONOS = personaTelefonos.ID_TELEFONOS;
            var cve = persona.A_PATERNO[0] + persona.A_MATERNO[0] + persona.NOMBRE[0] + persona.SEXO[0] +
                            DateTime.Now.Day.ToString("D") + DateTime.Now.Month.ToString("D") +
                            DateTime.Now.Year.ToString("D")+persona.SEXO[0] + persona.ID_TIPO_PERSONA;
            
            persona.ID_PER_LINKID = cve;
            persona.FECHAINGRESO = DateTime.Now;

        }

        public async void InsertaPersonasDirecciones(DIR_DIRECCIONES personaDirecciones)
        {
            var personaDireccion = await new AccesoDatos.Personas.Direcciones().InsertaDireccionesLinqAsync(personaDirecciones);
            _evento.DireccionesCompleto(personaDireccion);
            _evento.Finalizado();
        }

        public async void InsertaPersonaMediosElectronicos(PER_MEDIOS_ELECTRONICOS mediosElectronicos)
        {
            var personaMediosElectronicos =
                await
                    new AccesoDatos.Personas.MediosElectronicos().InsertaMediosElectronicosLinqAsync(mediosElectronicos);
            _evento.MediosEletronicosCompleto(personaMediosElectronicos);
            _evento.Finalizado();
        }

        public async void InsetaPersonaTelefonos(PER_CAT_TELEFONOS personaTelefonos)
        {
            var personaTelefono = await new AccesoDatos.Personas.Telefonos().InsertaDireccionesLinqAsync(personaTelefonos);
            _evento.TelefonosCompleto(personaTelefono);
            _evento.Finalizado();
        }

        public async void InsertaPersonaFoto(PER_FOTOGRAFIA personaFotografia)
        {
            var personaFoto = await new AccesoDatos.Personas.Fotografias().InsertaFotografiaLinqAsync(personaFotografia);
            _evento.FotografiasCompleto(personaFoto);
            _evento.Finalizado();
        }
    }
}
