using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.LogicaNegocios.OperacionSistema;

namespace AplicacionFragancias.SitioWeb
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        List<SIS_MENUARBOL> _lista = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaMenuArbol();
            
        }

        private void CargaMenuArbol()
        {
            menuBar.Items.Clear();
            _lista = new OperacionSistema().ObtenListaMenuArbol();

            var listaPadre = _lista.Where(r => r.IDMENUPADRE == null).ToList();

            foreach (var item in listaPadre)
            {
                var padre = new MenuItem();

                padre = new MenuItem(item.NOMBRE,
                               item.IDMENU.ToString(),
                               "", item.DIRECCION);

                padre.ChildItems.Add(InsertaHijo(item,padre));

                menuBar.Items.Add(padre);
            }
        }

        private MenuItem InsertaHijo(SIS_MENUARBOL item, MenuItem padre)
        {

            var listaHijos = _lista.Where(r => r.IDMENUPADRE == item.IDMENU).ToList();

            //Ejemplo sin linq
            foreach (var subItem in listaHijos)
            {
                if (subItem.IDMENUPADRE != null)
                {
                    var hijo = new MenuItem(subItem.NOMBRE)
                    {
                        NavigateUrl = subItem.DIRECCION
                    };
                    hijo = InsertaHijo(subItem, hijo);
                    padre.ChildItems.Add(hijo);
                }
            }

            return padre;
        }

        protected void btnInicio_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}