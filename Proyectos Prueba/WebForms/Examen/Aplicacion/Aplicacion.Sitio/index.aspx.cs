using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Aplicacion.Entidades;
using Aplicacion.LogicaNegocio;
using Newtonsoft.Json;

namespace Aplicacion.Sitio
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private PER_PERSONAS _persona;
        private List<CAT_AREANEGOCIO> _areanegocio; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
            if (string.IsNullOrEmpty(Request.QueryString["Token"])) return;
            
            var logicaNegocio = new LoginL();
            _persona = JsonConvert.DeserializeObject<PER_PERSONAS>(Request.QueryString["Token"]);
            _areanegocio = logicaNegocio.ListarAreanegocios();
            var areanegocio =_areanegocio.FirstOrDefault(r => r.IDAREANEGOCIO == _persona.IDAREANEGOCIO);
            if (areanegocio != null) lblAreaNegocio.Text = areanegocio.NOMBREAREANEGOCIO ?? "";
            lblCorreoElectronico.Text = _persona.CORREOELECTRONICO;

            var menus = new MenusL();
            var listaAplicacion =
                menus.ListaAplicacionness().Where(r => r.IDAREANEGOCIO == _persona.IDAREANEGOCIO).ToList();


            var tabla = new Table();
        }
    }
}