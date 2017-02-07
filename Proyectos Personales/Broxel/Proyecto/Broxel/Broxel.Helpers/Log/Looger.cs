using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broxel.Helpers.Log
{
    public class Looger
    {
        public void EscribeLog(TipoLog tipo, string mensage, string log, Exception e)
        {
        }

        public void GuardaLogBaseDatos()
        {
        
        }

        private void CreaArchivoPlano()
        {
        }

        private void ConexionBaseDatos()
        {
        }

        private void EscribeLogArchivo()
        {
        }

        public enum TipoLog
        {
            Informe = 1,
            Preventivo = 2,
            Error = 3,
            ErrorCritico = 4
        }

        public enum Alamacenamiento
        {
            BaseDeDatos = 0,
            ArchivoPlano = 1,
            ArchivoyBd = 2,
            Consola = 4
        }
    }
}
