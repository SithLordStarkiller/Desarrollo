namespace Universidad.ServidorExterno.Usuarios
{
    using System.Web.Services;
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WsUsuarios : WebService
    {

        [WebMethod]
        public UsUsuarios ObtenUsuario(string usuario)
        {
            var user = new LogicaNegocios.Usuarios.Usuarios().ObtenUsuario(usuario);
            var xmlUser = new UsUsuarios
            {
                IdUsuario = user.ID_USUARIO,
                Usuario = user.USUARIO,
                Contrasena = user.CONTRASENA
            };
            return xmlUser;
        }

        [WebMethod]
        public List<UsUsuarios> ObtenerListaUsarios(string a)
        {
            var lista = new List<UsUsuarios>
            {
                new UsUsuarios {IdUsuario = 1, Usuario = "Esteban", Contrasena = "bjhbjhbjh"},
                new UsUsuarios {IdUsuario = 2, Usuario = "Alberto", Contrasena = "kjjknkj"},
                new UsUsuarios {IdUsuario = 3, Usuario = "Sara", Contrasena = "lknlknnoi"}
            };

            return lista;
        }
    }

    public class UsUsuarios
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }
}
