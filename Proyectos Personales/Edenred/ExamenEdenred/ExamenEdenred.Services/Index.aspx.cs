namespace ExamenEdenred.Services
{
    using System;
    using BusinessLogic.Usuarios;

    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Usuarios().ObtenUsuario(1);
        }
    }
}