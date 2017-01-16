using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class ProcesarTelefonos
    {
        /// <summary>
        /// Se encarga de validar y obtener el tipo de teléfono que se tiene almacenado 
        /// </summary>
        /// <param name="telefono">orden a procesar</param>
        /// <returns>modelo con la información del teléfono</returns>
        public List<TelefonoModel> ValidarTipoTelefono(string telefono)
        {
            return new EntTelefonos().ValidarTipoTelefono(telefono);
        }

        /// <summary>
        /// Inserta el tipo de linea del teléfono que se tiene almacenado en la tabla de respuestas
        /// </summary>
        /// <param name="idOrden">Orden a procesar</param>
        public void InsertarTipoTelefono(int idOrden)
        {
            new EntTelefonos().InsertarTipoTelefono(idOrden); 
        }

    }
}
