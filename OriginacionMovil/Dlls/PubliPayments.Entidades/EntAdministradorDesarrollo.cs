using System.Data;

namespace PubliPayments.Entidades
{
    public class EntAdministradorDesarrollo
    {
        public DataSet EjecutarQuery(string dataBase,string sql)
        {
            var instancia = ConnectionDB.Instancia ;
            return instancia.EjecutarDataSet(dataBase, sql);
        }
    }
}
