using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadosListaGenerica
    {

        public int idHorario { get; set; }
        public string horDescripcion { get; set; }
        public Guid idEmpleado { get; set; }
        public byte idEstatus { get; set; }
        public string tipoAsistencia { get; set; }
        public string empPaterno { get; set; }
        public string empMaterno { get; set; }
        public string empNombre { get; set; }
        public string empNumero { get; set; }
        public string asiHora {get;set;}
        public string asiHoraSalida { get; set; }
        public byte idMonta { get; set; }
        public int diasAgregados { get; set; }
        public DateTime fechaAsistencia { get; set; }
        public DateTime fechaAsistenciaSal { get; set; }
        /* ACTUALIZACIÓN MARZO 2017 PARA QUITAR INCONSISTENCIAS */

        public int idEmpleadoAsignacion { get; set; }
        public int idServicio { get; set; }
        public int idInstalacion { get; set; }
        public DateTime eaFechaIngreso { get; set; }
        public DateTime eaFechaBaja { get; set; }
        public int idFuncionAsignacion { get; set; }
        public string funcionAsignacion { get; set; }
        public string serDescripcion { get; set; }
        public string insNombre { get; set; }

    }
}
