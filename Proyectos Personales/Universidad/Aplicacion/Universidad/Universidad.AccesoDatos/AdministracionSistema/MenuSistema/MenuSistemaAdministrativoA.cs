namespace Universidad.AccesoDatos.AdministracionSistema.MenuSistema
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Entidades.ControlUsuario;

    public class MenuSistemaAdministrativoA
    {
        #region Propiedades de la clase

        /// <summary>
        ///  Instancia hacia el contexto de la DB
        /// </summary>
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        #endregion

        #region Metodos de Extraccion

        public List<SIS_AADM_ARBOLMENUS> TraeArbolLinq()
        {
            using (var r = new Repositorio<SIS_AADM_ARBOLMENUS>())
            {
                return r.TablaCompleta();
            }
        }

        public List<MenuSistemaE> TrerMenusLinq(US_USUARIOS usuario)
        {
            var lista = (
                from saa in _contexto.SIS_AADM_APLICACIONES
                join saam in _contexto.SIS_AADM_ARBOLMENUS on saa.IDMENU equals saam.IDMENU
                join sctp in _contexto.SIS_CAT_TABPAGES on saa.IDTABPAGES equals sctp.IDTABPAGES
                join ucnu in _contexto.US_CAT_NIVEL_USUARIO on saa.ID_NIVEL_USUARIO equals ucnu.ID_NIVEL_USUARIO
                join uctu in _contexto.US_CAT_TIPO_USUARIO on saa.ID_TIPO_USUARIO equals uctu.ID_TIPO_USUARIO

                where saa.ID_NIVEL_USUARIO == usuario.ID_NIVEL_USUARIO && saa.ID_TIPO_USUARIO == usuario.ID_TIPO_USUARIO

                select new MenuSistemaE
                {
                    IdMenuHijo = saam.IDMENU,
                    IdMenuPadre = saam.IDMENUPADRE,
                    NombreNodo = saam.NOMBRENODO,
                    RutaNodo = saam.RUTA,
                    IdTipoUsuario = uctu.ID_TIPO_USUARIO,
                    TipoUsuario = uctu.TIPO_USUARIO,
                    IdNivelUsuario = ucnu.ID_NIVEL_USUARIO,
                    NivelUsuario = ucnu.NIVEL_USUARIO,
                    IdTabPage = sctp.IDTABPAGES,
                    RutaTapPage = sctp.RUTATAB,
                    NombreTabPage = sctp.NOMBRETABPAGE
                }
                ).ToList();
            return lista;
        }

        public List<SIS_WADM_PERMISOS_ARBOLMENU_MVC> TraeListaMenuPermisosMvcLinq(US_USUARIOS usuario)
        {
            using (var aux = new Repositorio<SIS_WADM_PERMISOS_ARBOLMENU_MVC>())
            {
                return aux.Filtro(x => x.ID_NIVEL_USUARIO == usuario.ID_NIVEL_USUARIO && x.ID_TIPO_USUARIO == usuario.ID_TIPO_USUARIO);
            }
        }

        public List<SIS_WADM_ARBOLMENU_MVC> TraeArbolMenuArbolMvcLinq(US_USUARIOS usuario)
        {
            using (var aux = new Repositorio<SIS_WADM_ARBOLMENU_MVC>())
            {
                return aux.TablaCompleta();
            }
        }
        #endregion
    }
}
