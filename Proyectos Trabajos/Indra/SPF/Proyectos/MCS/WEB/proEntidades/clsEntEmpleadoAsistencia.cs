using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntEmpleadoAsistencia
    {
      

        public Guid IdEmpleado {get; set;}
        public string EmpPaterno { get; set; }
        public string EmpMaterno {get; set;}
        public string EmpNombre { get; set; }
        public int EmpNumero { get; set; }
        public short idHorario { get; set; }
        public string horario { get; set; }
        public bool desactivarPase { get; set; }
        public int idAsignacionHorario { get; set; }        
        public string entradaHM { get; set; }        
        public DateTime FechaEntrada { get; set; }
        public int idAsistencia { get; set; }
        public bool salida { get; set; }

        /*las siguientes tres variables muestran las observaciones = Procedimiento
         estDescripcion = falta, retardo o algo e incDescripcion = incidencias*/
        public string observaciones { get; set; }
        public string estDescripcion { get; set; }
        public string incDescripcion { get; set; }
        /*para indicar si esta o no franco*/
        public int franco { get; set; }
        /*para que muestre la información en el textbox siempre y cuando no se trate de vacaciones, lm o descanso*/
        public bool textInfo { get; set; }
        /*para deshabilitar la lista xq ya paso un tiempo en MCS*/
        public bool deshabilitarLista { get; set; }
        public DateTime FECHAFINREAL { get; set; }
        public string retardo { get; set; }
        public string falta { get; set; }
        
    }
}
