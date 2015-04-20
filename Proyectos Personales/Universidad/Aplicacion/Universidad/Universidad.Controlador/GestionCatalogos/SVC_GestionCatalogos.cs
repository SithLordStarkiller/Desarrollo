using System.Collections.Generic;
using Universidad.Controlador.SRV_GestionCatalogos;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.Controlador.GestionCatalogos
{
    public class SVC_GestionCatalogos
    {
        #region Propiedades de la clase

        private readonly S_GestionCatalogosClient _servicio = null;

        public SVC_GestionCatalogos(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new S_GestionCatalogosClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"GestionCatalogos/", "S_GestionCatalogos.svc"));
        }

        public System.EventHandler Logeo_Finalizado = null;

        #endregion

        #region Obten Tipo de Usuario

        public delegate void ObtenTipoUsuarioArgs(US_CAT_TIPO_USUARIO tipoUsuario);

        public event ObtenTipoUsuarioArgs ObtenTipoUsuarioFinalizado;

        public void ObtenTipoUsuario(int idTipoPersona)
        {
            _servicio.ObtenCatTipoUsuarioAsync(idTipoPersona);
            _servicio.ObtenCatTipoUsuarioCompleted += _servicio_ObtenCatTipoUsuarioCompleted;
        }

        private void _servicio_ObtenCatTipoUsuarioCompleted(object sender, ObtenCatTipoUsuarioCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var tipoUsuario = resultado;
            ObtenTipoUsuarioFinalizado(tipoUsuario);
            _servicio.ObtenCatTipoUsuarioCompleted -= _servicio_ObtenCatTipoUsuarioCompleted;
        }

        #endregion

        #region Obten catalogos nacionalidades

        public delegate void ObtenCatNacionalidadArgs(List<PER_CAT_NACIONALIDAD> lista);

        public event ObtenCatNacionalidadArgs ObtenCatNacionalidadFinalizado;

        public void ObtenCatNacionalidad()
        {
            _servicio.ObtenCatalogoNacionalidadesCompleted += _servicio_ObtenCatalogoNacionalidadesCompleted;
            _servicio.ObtenCatalogoNacionalidadesAsync();
        }

        private void _servicio_ObtenCatalogoNacionalidadesCompleted(object sender, ObtenCatalogoNacionalidadesCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var persona = resultado;
            ObtenCatNacionalidadFinalizado(persona);

            _servicio.ObtenCatalogoNacionalidadesCompleted -= _servicio_ObtenCatalogoNacionalidadesCompleted;
        }

        #endregion

        #region Obten catalogos tipo Persona

        public delegate void ObtenCatTipoPersonaArgs(List<PER_CAT_TIPO_PERSONA> lista);

        public event ObtenCatTipoPersonaArgs ObtenCatTipoPersonaFinalizado;

        public void ObtenCatTipoPersona()
        {
            _servicio.ObtenCatTipoPersonaCompleted += _servicio_ObtenCatTipoPersonaCompleted;
            _servicio.ObtenCatTipoPersonaAsync();
        }

        private void _servicio_ObtenCatTipoPersonaCompleted(object sender, ObtenCatTipoPersonaCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var lista = resultado;
            ObtenCatTipoPersonaFinalizado(lista);

            _servicio.ObtenCatTipoPersonaCompleted -= _servicio_ObtenCatTipoPersonaCompleted;
        }

        #endregion

        #region Obten Colonias por codigo postal

        public delegate void ObtenColoniasPorCpArgs(List<DIR_CAT_COLONIAS> lista);

        public event ObtenColoniasPorCpArgs ObtenColoniasPorCpFinalizado;

        public void ObtenColoniasPorCpPersona(int codigoPostal)
        {
            _servicio.ObtenColoniasPorCpCompleted += _servicio_ObtenColoniasPorCpCompleted;
            _servicio.ObtenColoniasPorCpAsync(codigoPostal);
        }

        private void _servicio_ObtenColoniasPorCpCompleted(object sender, ObtenColoniasPorCpCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var lista = resultado;
            ObtenColoniasPorCpFinalizado(lista);

            _servicio.ObtenColoniasPorCpCompleted -= _servicio_ObtenColoniasPorCpCompleted;
        }

        #endregion

        #region Obten Estados

        public delegate void ObtenCatEstadosArgs(List<DIR_CAT_ESTADO> lista);

        public event ObtenCatEstadosArgs ObtenCatEstadosFinalizado;

        public void ObtenCatEstados()
        {
            _servicio.ObtenCatEstadosCompleted += _servicio_ObtenCatEstadosCompleted;
            _servicio.ObtenCatEstadosAsync();
        }

        private void _servicio_ObtenCatEstadosCompleted(object sender, ObtenCatEstadosCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var lista = resultado;
            ObtenCatEstadosFinalizado(lista);

            _servicio.ObtenCatEstadosCompleted -= _servicio_ObtenCatEstadosCompleted;
        }

        #endregion

        #region Obten Municipios

        public delegate void ObtenMunicipiosArgs(List<DIR_CAT_DELG_MUNICIPIO> lista);

        public event ObtenMunicipiosArgs ObtenMunicipiosFinalizado;

        public void ObtenMunicipios(int estado)
        {
            _servicio.ObtenMunicipiosCompleted += _servicio_ObtenMunicipiosCompleted;
            _servicio.ObtenMunicipiosAsync(estado);
        }

        private void _servicio_ObtenMunicipiosCompleted(object sender, ObtenMunicipiosCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var lista = resultado;
            ObtenMunicipiosFinalizado(lista);
            _servicio.ObtenMunicipiosCompleted -= _servicio_ObtenMunicipiosCompleted;
        }

        #endregion

        #region Obten Colonias

        public delegate void ObtenColoniasArgs(List<DIR_CAT_COLONIAS> lista);

        public event ObtenColoniasArgs ObtenColoniasFinalizado;

        public void ObtenColonias(int estado, int municipio)
        {
            _servicio.ObtenColoniasCompleted += _servicio_ObtenColoniasCompleted;
            _servicio.ObtenColoniasAsync(estado, municipio);
        }

        private void _servicio_ObtenColoniasCompleted(object sender, ObtenColoniasCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var lista = resultado;
            ObtenColoniasFinalizado(lista);
            _servicio.ObtenColoniasCompleted -= _servicio_ObtenColoniasCompleted;
        }


        #endregion
    }
}
