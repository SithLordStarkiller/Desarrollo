namespace SunCorp.DataAccessSqlServer.CatalogsDataAccess
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Generic;
    using Entities;

    public class CatalogsDataAccess
    {
        private readonly SunCorpEntities _contexto = new SunCorpEntities();

        #region ListCatalogsSystem

        public List<GenericTable> GetListCatalogsSystem()
        {
            var obj = ControllerSqlServer.ExecuteDataTable(ParametersSql.StrConDbLsWebApp, CommandType.StoredProcedure,
                "Usp_GetListCatalogsSystem", null);

            var resultado = new List<GenericTable>();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new GenericTable
                             {
                                 IdTable = Convert.ToInt32(row["IdTable"]),
                                 TableName = (string)row["TableName"],
                                 TableDescription = (string)row["Descriptions"],
                                 Deleted = false
                             }).ToList();
            }

            return resultado;
        }

        #endregion

        #region TreeMenu

        public List<SisArbolMenu> GetListMenu()
        {
            using (var aux = new Repositorio<SisArbolMenu>())
            {
                return aux.TablaCompleta();
            }
        }

        #endregion
    }
}
