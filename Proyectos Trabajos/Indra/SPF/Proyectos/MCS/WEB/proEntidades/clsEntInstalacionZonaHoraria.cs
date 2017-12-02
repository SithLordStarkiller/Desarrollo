using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICOGUA.Entidades
{
    public class clsEntInstalacionZonaHoraria
    {
   


             private int _idInstalacion;
        private int _idServicio;
        private int _idZonaHoraria;

        public int idInstalacion
        {
            get { return _idInstalacion; }
            set { _idInstalacion = value; }
        }

        public int idServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public int idZonaHoraria
        {
            get { return _idZonaHoraria; }
            set { _idZonaHoraria = value; }
        }


    }
}
