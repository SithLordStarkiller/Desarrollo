namespace Universidad.Controlador.GestionCatalogos
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Universidad.Controlador.SvcGestionCatalogos;
    using Entidades;
    using Entidades.Catalogos;
    using Entidades.ControlUsuario;

    public class SvcGestionCatalogos
    {
        private readonly S_GestionCatalogosClient _servicio;

        public SvcGestionCatalogos(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new S_GestionCatalogosClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"GestionCatalogos/", "S_GestionCatalogos.svc"));
        }

        public Task<List<US_CAT_ESTATUS_USUARIO>> ObtenTablaUsCatEstatusUsuario()
        {
            return Task.Run(() => _servicio.ObtenTablaUsCatEstatusUsuarioAsync());
        }

        public Task<List<US_CAT_NIVEL_USUARIO>> ObtenTablaUsCatNivelUsuario()
        {
            return Task.Run(() => _servicio.ObtenTablaUsCatNivelUsuarioAsync());
        }

        public Task<List<US_CAT_TIPO_USUARIO>> ObtenTablaUsCatTipoUsuario()
        {
            return Task.Run(() => _servicio.ObtenTablaUsCatTipoUsuariosAsync());
        }

        public Task<US_CAT_TIPO_USUARIO> ObtenTipoUsuario(int idTipoPersona)
        {
            return Task.Run(() => _servicio.ObtenCatTipoUsuarioAsync(idTipoPersona));
        }

        public Task<List<PER_CAT_NACIONALIDAD>> ObtenCatNacionalidad()
        {
            return Task.Run(() => _servicio.ObtenCatalogoNacionalidadesAsync());
        }

        public Task<List<PER_CAT_TIPO_PERSONA>> ObtenCatTipoPersona()
        {
            return Task.Run(() => _servicio.ObtenCatTipoPersonaAsync());
        }

        public Task<List<DIR_CAT_COLONIAS>> ObtenColoniasPorCpPersona(int codigoPostal)
        {
            return Task.Run(() => _servicio.ObtenColoniasPorCpAsync(codigoPostal));
        }

        public Task<List<DIR_CAT_ESTADO>> ObtenCatEstados()
        {
            return Task.Run(() => _servicio.ObtenCatEstadosAsync());
        }

        public Task<List<DIR_CAT_DELG_MUNICIPIO>> ObtenMunicipios(int estado)
        {
            return Task.Run(() => _servicio.ObtenMunicipiosAsync(estado));
        }

        public Task<List<DIR_CAT_COLONIAS>> ObtenColonias(int estado, int municipio)
        {
            return Task.Run(() => _servicio.ObtenColoniasAsync(estado, municipio));
        }
        public Task<DIR_CAT_COLONIAS> ObtenCodigoPostal(int estado, int municipio, int colonia)
        {
            return Task.Run(() => _servicio.ObtenCodigoPostalAsync(estado, municipio, colonia));
        }

        public Task<List<DIR_CAT_COLONIAS>> ObtenCatalogosColonias()
        {
            return Task.Run(() => _servicio.ObtenCatalogosColoniasAsync());
        }

        public Task<List<DIR_CAT_DELG_MUNICIPIO>> ObtenCatalogosMunicipios()
        {
            return Task.Run(() => _servicio.ObtenCatalogosMunicipiosAsync());
        }

        public Task<List<ListasGenerica>> ObtenTablasCatalogos()
        {
            return Task.Run(() => _servicio.ObtenTablasCatalogosAsync());
        }

        public Task<List<CatalogosSistema>> ObtenCatalogosSistema()
        {
            return Task.Run(() => _servicio.ObtenCatalogosSistemasAsync());
        }

        public Task<List<AUL_CAT_TIPO_AULA>> ObntenListaAUL_CAT_TIPO_AULA()
        {
            return Task.Run(() => _servicio.ObtenListaAUL_CAT_TIPO_AULAAsync());
        }

        public Task<bool> ActualizaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro)
        {
            return Task.Run(() => _servicio.ActualizaRegistroAUL_CAT_TIPO_AULAAsync(registro));
        }
    }
}
