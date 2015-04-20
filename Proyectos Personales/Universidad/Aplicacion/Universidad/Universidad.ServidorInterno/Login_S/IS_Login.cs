using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using Universidad.Entidades;

namespace Universidad.ServidorInterno.Login_S
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IS_Login" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IS_Login
    {
        [OperationContract]
        string LoginAdministrador(string Usuario, string Contrasena);

        [OperationContract]
        string ObtenPersona(US_USUARIOS usuario);

        [OperationContract]
        bool Funciona();

        [OperationContract]
        PER_PERSONAS ObtenPersonas(US_USUARIOS usuario);
    }
}
