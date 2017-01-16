using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubliPayments.Entidades
{
    public class ArchivosModel
    {
        public Int32 id { get; set; }
        public string Archivo { get; set; }
        public string Tipo { get; set; }
        public string NombreDominio { get; set; }
        public string Usuario { get; set; }
        public Int32 Tiempo { get; set; }
        public Int32 Registros { get; set; }
        public DateTime Fecha { get; set; }
        public string Estatus { get; set; }
        public Int32 Error { get; set; }
    }
}
