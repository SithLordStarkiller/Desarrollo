using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Universidad.Entidades;

namespace Universidad.ServidorInterno.GestionCatalogos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IS_GestionCatalogos" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IS_GestionCatalogos
    {
        [OperationContract]
        string ObtenTablaUsCatTipoUsuarios();

        [OperationContract]
        string ObtenCatTipoUsuario(int Id_tipoUsuario);

        [OperationContract]
        string ObtenCatalogoNacionalidades();
    }
}
