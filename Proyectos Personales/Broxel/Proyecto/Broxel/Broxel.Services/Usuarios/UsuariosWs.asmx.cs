namespace Broxel.Services.Usuarios
{
    using System.Web.Services;
    using BusinessLogic.Usuarios;
    using Entities;
    using System.Web.Script.Services;

    /// <summary>
    /// Descripción breve de UsuariosWs
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class UsuariosWs : WebService
    {

        [WebMethod, ScriptMethod]
        public USUSUARIOS ObtenUsUsuarionPorLogin(string usuario, string contrasena)
        {
            return new BlUsUsuario().ObtenUsUsuarionPorLogin(usuario, contrasena);
        }
    }
}
