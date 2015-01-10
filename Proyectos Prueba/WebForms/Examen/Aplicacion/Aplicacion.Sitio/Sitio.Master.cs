using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion.Entidades;
using Aplicacion.LogicaNegocio;

namespace Aplicacion.Sitio
{
    public partial class Sitio : System.Web.UI.MasterPage
    {

        private PER_PERSONAS _persona;
        protected void Page_Load(object sender, EventArgs e)
        {
            _persona = (PER_PERSONAS)Session["UserInfo"];

            if (_persona != null)
            {
                _persona = (PER_PERSONAS)Session["UserInfo"];

                var areaNegocio = (new LoginL()).ListarAreanegocios().FirstOrDefault(r => r.IDAREANEGOCIO == _persona.IDAREANEGOCIO);

                if (areaNegocio != null) lblAreaNegocio.Text = "Area de negocio: " + areaNegocio.NOMBREAREANEGOCIO;
                lblCorreoElectronico.Text = "Bienvenido " + _persona.CORREOELECTRONICO;

                CargarMenu();
            }
            else
            {
                Response.Redirect(Environment.CurrentDirectory + "Login.aspx");
            }
        }

        private void CargarMenu()
        {
            var listaAplicacion = new MenusL().ListaAplicacionness().Where(r => r.IDAREANEGOCIO == _persona.IDAREANEGOCIO && r.IDPAGINAPADRE == 0).ToList();

            trvDirecctorio.Nodes.Clear();

            //Exprecion normal
            foreach (var item in listaAplicacion)
            {
                var padre = new TreeNode(item.NOMBREPAGINA) { NavigateUrl = item.URL };
                padre = InsertaHijo(item, padre);
                trvDirecctorio.Nodes.Add(padre);
            }
            trvDirecctorio.CollapseAll();
        }

        private static TreeNode InsertaHijo(SIS_APLICACIONNES item, TreeNode padre)
        {

            var listaHijos = new MenusL().ListaAplicacionness().Where(r => r.IDPAGINAPADRE == item.IDPAGINA).ToList();

            //Ejemplo sin linq
            foreach (var subItem in listaHijos)
            {
                if (subItem.IDPAGINAPADRE != 0)
                {
                    var hijo = new TreeNode(subItem.NOMBREPAGINA)
                    {
                        NavigateUrl= subItem.URL
                    };
                    hijo = InsertaHijo(subItem, hijo);
                    padre.ChildNodes.Add(hijo);
                }
            }

            return padre;
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            if (Session["UserInfo"] != null && Session["UserInfo"].ToString() != string.Empty)
            {
                HttpContext.Current.Cache.Remove(Session["UserInfo"].ToString());
            }
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");

        }
    }
}