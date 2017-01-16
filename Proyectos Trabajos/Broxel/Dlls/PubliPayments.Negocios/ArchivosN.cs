using System.Collections.Generic;
using System.Data;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class ArchivosN
    {
        public DataSet ObtenerArchivos(int idUsuario, int proceso)
        {
           var entArchivos = new EntArchivos();
           return entArchivos.ObtieneArchivos(idUsuario, proceso);
        }
    }
}
