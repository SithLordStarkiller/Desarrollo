namespace Universidad.AccesoDatos.Horarios
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Entidades.Catalogos;

    public class Horarios
    {
        public List<CatalogosSistema> ObtenCatalogosTSql()
        {
            var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                "Usp_ObtenCatalogosSistema", null);

            var resultado = new List<CatalogosSistema>();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new CatalogosSistema
                             {
                                 NombreTabla = (string)row["TableName"]
                             }).ToList();
            }

            return resultado;
        }
    }
}
