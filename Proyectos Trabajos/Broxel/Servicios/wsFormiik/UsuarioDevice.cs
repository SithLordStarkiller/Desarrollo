using System.Runtime.Serialization;

namespace PubliPayments
{
    [DataContract(Namespace = "")]
    public class UsuarioDevice
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string SerialNumber { get; set; }

        public string Login()
        {
            //Conectarse a su base
            //Hacer tu consulta la base de datos 
            //Si lo encuentra regresar true de lo contrario false
            bool existe = false;

            if (existe)
            {
                return "";
            }
            else
            {
                return "Usuario/Password no valido";
            }
        }
    }
}