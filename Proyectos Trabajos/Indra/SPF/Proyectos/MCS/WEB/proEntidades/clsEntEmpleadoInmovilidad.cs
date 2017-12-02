using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoInmovilidad
    {
        public Guid idEmpleado { get; set; }
        public int idEmpleadoInmovilidad { get; set; }
        public byte idMotivoInmovilidad { get; set; }
        public string miDescripcion { get; set; }
        public Guid idAutoriza { get; set; }
        public byte idJerarquiaAutoriza { get; set; }
        public string PersonaAutoriza { get; set; }
        public string eiDescripcion { get; set; }
        public DateTime eiFechaInicio { get; set; }
        public DateTime eiFechaFin { get; set; }
        public byte[] eiImagen { get; set; }
        public bool estatus { get; set; }
    }
}
