using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Universidad.Entidades;

namespace Universidad.ServidorInterno.MenuSistema
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMenusSistemaS" in both code and config file together.
    [ServiceContract]
    public interface IMenusSistemaS
    {
        [OperationContract]
        string TraeArbol();

        [OperationContract]
        string TraerMenus(US_USUARIOS usuario);
    }
}
