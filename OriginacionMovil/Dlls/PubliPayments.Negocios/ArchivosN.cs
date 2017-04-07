using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class ArchivosN
    {
        public void ObtenerArchivos(ref string tipo,ref bool errorCompleto, ref List<ArchivosModel> archivos)
        {
            var entArchivos = new EntArchivos();
            switch (tipo)
            {
                case ("zip"):
                   // archivos.AddRange(entArchivos.ObtenerArchivosConError(ref tipo, ref errorCompleto));
                    break;
                case ("txt"):
                   // archivos.AddRange(entArchivos.ObtenerArchivosConError(ref tipo, ref errorCompleto));
                    break;
                default:
                    break;
            }
        }
    }
}
