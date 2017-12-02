using System;
using System.Collections.Generic;
using System.Text;

namespace REA.Entidades
{
   public class clsEntAsignacionHorarioREA
    {
       private Guid _idEmpleado;
       private int _idHorario;
       private int _idAsignacionHorario;
       private DateTime _ahFechaInicio;
       private DateTime _ahFechaFin;
       private Boolean _ahVigente;
       private string _horNombre;
       private int _idInstalacion;
       private int _idServicio;
       private Int16 _horJornada;
       private Int16 _horDescanso;

      

       public Guid IdEmpleado
       {
           get { return _idEmpleado; }
           set { _idEmpleado = value; }
       }
       public int idHorario
       {
           get { return _idHorario; }
           set { _idHorario = value; }
       }
       public int idAsignacionHorario
       {
           get { return _idAsignacionHorario; }
           set { _idAsignacionHorario = value; }
       }
       public DateTime ahFechaInicio
       {
           get { return _ahFechaInicio; }
           set { _ahFechaInicio = value; }
       }
       public DateTime ahFechaFin
       {
           get { return _ahFechaFin; }
           set { _ahFechaFin = value; }
       }
       public Boolean ahVigente
       {
           get { return _ahVigente; }
           set { _ahVigente = value; }
       }

       public string horNombre
       {
           get { return _horNombre; }
           set { _horNombre = value; }
       }

       public int idInstalacion
       {
           get { return _idInstalacion; }
           set { _idInstalacion = value; }
       }

       public int idServcio
       {
           get { return _idServicio; }
           set { _idServicio = value; }
       }

       public Int16 horJornada
       {
           get { return _horJornada; }
           set { _horJornada = value; }
       }

       public Int16 horDescanso
       {
           get { return _horDescanso; }
           set { _horDescanso = value; }
       }

    }
}
