using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.DataAgents;
using GOB.SPF.ConecII.Entities.Amatzin;

namespace GOB.SPF.ConecII.Business
{
    public class AmatzinBusiness
    {
        public Documento Insertar(Documento archivo)
        {
            ServiceAmatzin amatzinService = new ServiceAmatzin();
            archivo.ArchivoId = amatzinService.RegistrarArchivo(archivo);
            return archivo;
        }
        public Documento Consultar(long archivoId)
        {
            var amatzinService = new ServiceAmatzin();
            var documento= amatzinService.ConsultarArchivo(archivoId);
            return documento;
        }
    }
}
