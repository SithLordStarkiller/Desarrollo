using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICOGUA.Entidades
{
   public class clsEntZonaHoraria
    {
        private int _idZonaHoraria;
        private string _zhDescripcion;
        private int _zhDiferenciaCentro;
        private Boolean _zhVigente;


        public int idZonaHoraria
        {
            get { return _idZonaHoraria; }
            set { _idZonaHoraria = value; }
        }

        public string zhDescripcion
        {
            get { return _zhDescripcion; }
            set { _zhDescripcion = value; }
        }

        public int zhDiferenciaCentro
        {
            get { return _zhDiferenciaCentro; }
            set { _zhDiferenciaCentro = value; }
        }

        public Boolean zhVigente
        {
            get { return _zhVigente; }
            set { _zhVigente = value; }
        }
    }
}
