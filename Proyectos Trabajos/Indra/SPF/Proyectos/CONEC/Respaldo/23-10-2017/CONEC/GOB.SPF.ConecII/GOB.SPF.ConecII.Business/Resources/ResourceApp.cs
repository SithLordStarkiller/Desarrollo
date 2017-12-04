using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Business.Resources
{
    public static class ResourceApp
    {
        public static string RutaDocumentosCliente => "solicitudes/Clientes/";
        public enum TipoPersona
        {
            Solicitante = 1,
            Contacto = 2
        }

    }
}
