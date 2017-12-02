using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    [Serializable]
    public class clsEntEquipoCatalogo
    {
     
        private int _idEquipo;
        private string _equDescripcion;
        private decimal _ieCantidad;
        private string _ieFechaInicio;
        private string _ieFechaFin;
        private int _idInstalacionEquipo;
        private int _idServicio;
        private int _idInstalacion;
        private string _ieVigente;
        private int _semaforo;


          public int idEquipo
          {
              get { return _idEquipo; }
              set { _idEquipo = value; }
          }


          public string equDescripcion
          {
              get { return _equDescripcion; }
              set { _equDescripcion = value; }
          }

          public decimal ieCantidad
          {
              get { return _ieCantidad; }
              set { _ieCantidad = value; }
          }

          public string ieFechaInicio
          {
              get { return _ieFechaInicio; }
              set { _ieFechaInicio = value; }
          }

          public string ieFechaFin
          {
              get { return _ieFechaFin; }
              set { _ieFechaFin = value; }
          }

          public int idInstalacionEquipo
          {
              get { return _idInstalacionEquipo; }
              set { _idInstalacionEquipo = value; }
          }

          public int idServicio
          {
              get { return _idServicio; }
              set { _idServicio = value; }
          }

          public int idInstalacion
          {
              get { return _idInstalacion; }
              set { _idInstalacion = value; }
          }

          public string ieVigente
          {
              get { return _ieVigente; }
              set { _ieVigente = value; }
          }

          public int semaforo
          {
              get { return _semaforo; }
              set { _semaforo = value; }
          }

        
    }
}
