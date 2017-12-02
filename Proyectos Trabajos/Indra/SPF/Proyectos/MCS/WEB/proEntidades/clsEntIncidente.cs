using System;
using System.Collections.Generic;
using System.Text;

namespace SICOGUA.Entidades
{
    public class clsEntIncidente
    {
        private long _idIncidente;
        private short _idTipoIncidente;
        private int _idLugar;
        private short _idAccion;
        private int _idServicio;
        private int _idInstalacion;
        private string _indDescripcion;
        private DateTime _indFechaHora;

        
        public long IdIncidente
        {
            get { return _idIncidente; }
            set { _idIncidente = value; }
        }

        public short IdTipoIncidente
        {
            get { return _idTipoIncidente; }
            set { _idTipoIncidente = value; }
        }

        public int IdLugar
        {
            get { return _idLugar; }
            set { _idLugar = value; }
        }

        public short IdAccion
        {
            get { return _idAccion; }
            set { _idAccion = value; }
        }

        public int IdServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public int IdInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }

        public string IndDescripcion
        {
            get { return _indDescripcion; }
            set { _indDescripcion = value; }
        }

        public DateTime IndFechaHora
        {
            get { return _indFechaHora; }
            set { _indFechaHora = value; }
        }


    }
}
