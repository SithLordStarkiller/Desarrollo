using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Login;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativaPrueba
{
    public partial class _Default : Page
    {
        private Sesion _sesion;
        private US_USUARIOS _usuario;

    protected void Page_Load(object sender, EventArgs e)
        {

            _usuario = ((US_USUARIOS)Session["Usuario"]);
            _sesion = ((Sesion)Session["Sesion"]);
            var servicioPersona = new SVC_LoginAdministrativos(_sesion);
            servicioPersona.ObtenNombreCompleto(_usuario);
            servicioPersona.ObtenNombreCompletoFinalizado += _servicioPersona_ObtenNombreCompletoFinalizado;
        }

        private void _servicioPersona_ObtenNombreCompletoFinalizado(PER_PERSONAS persona)
        {
            var gestioncatalogos = new SVC_GestionCatalogos(_sesion);
            //var tipoUsuario = gestioncatalogos.ObtenTipoUsuario((int)_usuario.ID_TIPO_USUARIO);
            ((Label) (Master.FindControl("lblNombre"))).Text = persona.NOMBRE_COMPLETO;
            //((Label) (Master.FindControl("lblTipoUsuario"))).Text = tipoUsuario.TIPO_USUARIO;
        }
    }
}