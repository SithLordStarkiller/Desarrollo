namespace Universidad.ServidorExterno.Usuarios
{
    using System;
    using System.Runtime.Serialization;
    using System.Web.Services;
    using Entidades;
    using LogicaNegocios;

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
                Usuario=user.USUARIO,
                Contrasena=user.CONTRASENA
            };
            return xmlUser;
        }

        [WebMethod]
        public Suma Sumar(int a, int b)
        {
            var res = new Suma
            {
                A = a,
                B = b,
                Resultado = a + b
            };
            return res;
        }
    }
    public class UsUsuarios
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }


    //[DataContract]
    public class Suma
    {
        //[DataMember]
        public int A { get; set; }

        //[DataMember]
        public int B { get; set; }

        //[DataMember]
        public int Resultado { get; set; }
    }
}
