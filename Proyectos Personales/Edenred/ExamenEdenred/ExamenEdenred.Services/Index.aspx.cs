namespace ExamenEdenred.Services
{
    using System;

    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new BusinessLogic.Usuarios.Usuarios().ObtenUsuario(1);
        }
    }
}