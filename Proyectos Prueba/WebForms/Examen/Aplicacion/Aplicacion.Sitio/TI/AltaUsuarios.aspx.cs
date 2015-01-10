using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion.LogicaNegocio;
using Aplicacion.Entidades;
using Microsoft.Ajax.Utilities;

namespace Aplicacion.Sitio.TI
{
    public partial class AltaUsuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var listaAreaNegocio = new LoginL().ListarAreanegocios();
            ddlAreaNegocio.DataSource = listaAreaNegocio;
            ddlAreaNegocio.DataValueField = "IdAreaNegocio";
            ddlAreaNegocio.DataTextField = "NombreAreaNegocio";
            ddlAreaNegocio.DataBind();
        }

        protected void btnRegistrar_OnClick(object sender, EventArgs e)
        {
            if (txbContrasena.Text == txbContrasenaConfimada.Text)
            {
                try
                {
                    var persona = new PER_PERSONAS()
                    {
                        NOMBRES = txbNombre.Text,
                        APELLIDOPATERNO = txbAPaterno.Text,
                        APELLIDOMATERNO=txbAMaterno.Text,
                        CORREOELECTRONICO = txbCorreoElectronico.Text,
                        CONTRASENA = txbContrasena.Text,
                        IDAREANEGOCIO = Convert.ToInt32(ddlAreaNegocio.SelectedValue)
                    };

                    new LoginL().NuevoUsuario(persona);

                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                           "Mensaje",
                           "alert('Se agrago nuevo usuario');", true);
                    
                    txbNombre.Text = string.Empty;
                    txbAPaterno.Text = string.Empty;
                }
                catch (Exception err)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                        "Mensaje",
                        "alert('" + err.Message + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                           "Mensaje",
                           "alert('Las contraseñas no coinciden porfavor colocar nuevamente la contraseña');", true);
            }
        }
    }
}