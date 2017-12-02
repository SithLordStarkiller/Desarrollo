using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoHorarioREA

    {
        public int idHorario { get; set; }
        public Guid idEmpleado { get; set; }
        public int idAsignacionHorario { get; set; }
        public DateTime ahFechaInicio { get; set; }
        public DateTime ahFechaFin { get; set; }
        public Boolean ahVigente { get; set; }
        public string horNombre { get; set; }
        public string strFechaFin { get; set; }
        public string strFechaInicio { get; set; }
        public int intAccion { get; set; }
     

    }
}
