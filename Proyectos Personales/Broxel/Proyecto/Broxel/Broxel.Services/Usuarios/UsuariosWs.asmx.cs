namespace Broxel.Services.Usuarios
{
    using System.Web.Services;
    using BusinessLogic.Usuarios;
    using Entities.Entidades;
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
        public UsUsuariosEntity ObtenUsUsuarionPorLogin(string usuario, string contrasena)
        {
            return new BlUsUsuario().ObtenUsUsuarion(usuario, contrasena);
        }
    }
}
