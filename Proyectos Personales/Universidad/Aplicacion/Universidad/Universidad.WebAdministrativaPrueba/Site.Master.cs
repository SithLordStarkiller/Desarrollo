using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Universidad.Controlador.MenuSistema;
using Universidad.Entidades.ControlUsuario;
using WebGrease;
using Universidad.Entidades;

namespace Universidad.WebAdministrativaPrueba
{
    public partial class SiteMaster : MasterPage
    {
        private List<SIS_WADM_ARBOLMENU> _listaMenuArbol;
        private SvcMenuSistemaC _servidorMenuSistema;
        private Sesion _sesion;
        private US_USUARIOS _usuario;
        private PER_PERSONAS _persona;

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            Page.Load += Page_Load;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _sesion = (Sesion)Session["Sesion"];
            _servidorMenuSistema = new SvcMenuSistemaC(_sesion);
            _usuario = (US_USUARIOS)Session["Usuario"];
            CargaArbolMenu(_usuario);
        }

        private void CargaArbolMenu(US_USUARIOS usuarios)
        {
            _listaMenuArbol = _servidorMenuSistema.TraeArbolMenuWadm(usuarios); 
            var listaPadre = _listaMenuArbol.Where(r => r.IDMENUPADRE == null).ToList();

            foreach (var item in listaPadre)
            {
                var padre = new MenuItem();

                padre = new MenuItem(item.NOMBRE,
                               item.IDMENU.ToString(),
                               "", item.LINK);

                padre.ChildItems.Add(InsertaHijo(item, padre));

                menuBar.Items.Add(padre);
            }
        }

        private MenuItem InsertaHijo(SIS_WADM_ARBOLMENU item, MenuItem padre)
        {

            var listaHijos = _listaMenuArbol.Where(r => r.IDMENUPADRE == item.IDMENU).ToList();

            //Ejemplo sin linq
            foreach (var subItem in listaHijos)
            {
                if (subItem.IDMENUPADRE != null)
                {
                    var hijo = new MenuItem(subItem.NOMBRE)
                    {
                        NavigateUrl = subItem.LINK
                    };
                    hijo = InsertaHijo(subItem, hijo);
                    padre.ChildItems.Add(hijo);
                }
            }

            return padre;
        }
    }

}