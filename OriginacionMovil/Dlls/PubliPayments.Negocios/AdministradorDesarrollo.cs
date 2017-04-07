using System.Data;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class AdministradorDesarrollo
    {
        public DataTable EjecutarQuery(string sql)
        {
            var ent = new EntAdministradorDesarrollo();
            var resultado = ent.EjecutarQuery("SqlDefault", sql);
            if (resultado != null && resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
            {
                return resultado.Tables[0];
            }
            else
            {
                var tabla = new DataTable();
                tabla.Columns.Add("Resultado");
                var r = tabla.NewRow();
                r["Resultado"] = "No se encontraron datos";
                tabla.Rows.Add(r);
                return tabla;
            }
        }
    }
}
