using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntMonitoreo
    {
        private int _idAplicacion; 
        public EntMonitoreo(int idAplicacion )
        {
            _idAplicacion = idAplicacion;
        }
        public List<Monitoreo> ObtenerMonitoreos()
        {
            using (var context = new SistemasCobranzaEntities())
            {
                var monitoreo = from m in context.Monitoreo
                    where m.idAplicacion == _idAplicacion
                    select m;

                return monitoreo.ToList();
            }
        }

        public int EjecutarStoredProcedure(string sp)
        {
            var cnn = ConnectionDB.Instancia;
            var r = cnn.EjecutarEscalar("SqlDefault", sp);
            var result = Convert.ToInt32(r);
            return result;
        }
    }
}
