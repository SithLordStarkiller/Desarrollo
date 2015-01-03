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

                if (areaNegocio != null) lblAreaNegocio.Text = "Area de negocio " + areaNegocio.NOMBREAREANEGOCIO;
                lblCorreoElectronico.Text = "Bienvenido " + _persona.CORREOELECTRONICO;

                var listaAplicacion = new MenusL().ListaAplicacionness().Where(r => r.IDAREANEGOCIO == _persona.IDAREANEGOCIO).ToList();

                foreach (var nodo in listaAplicacion.Select(sisAplicacionnes => new TreeNode(sisAplicacionnes.NOMBREPAGINA)
                {
                    NavigateUrl = sisAplicacionnes.URL
                        
                }))
                {
                    trvDirecctorio.Nodes.Add(nodo);
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}