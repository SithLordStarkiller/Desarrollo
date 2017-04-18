

namespace SunCorp.DataAccessSqlServer.CatalogsDataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using System.Threading.Tasks;
    using Entities;
    using Entities.Generic;

    public class CatalogsDataAccess
    {
        private readonly SunCorpEntities _contexto = new SunCorpEntities();

        #region ListCatalogsSystem

        public List<GenericTable> GetListCatalogsSystem()
        {
            var uspResult = _contexto.Usp_GetListCatalogsSystem();

            //var list = uspResult.Convetidor<GenericTable>();

            var result = uspResult.AsEnumerable().Select(x => new GenericTable
            {
                IdTable = (int) Convert.ToInt64(x.IdTable),
                TableName = x.TableName,
                TableDescription = x.Descriptions,
                Deleted = false
            }).ToList();

            return result;
        }

        #endregion

        //    #region ListCatalogProducts

        public List<GenericTable> GetListCatalogsProducts()
        {
            var obj = _contexto.Usp_GetListCatalogsSystem();
            
            return  null;
        }

        //#endregion

        #region TreeMenu

        public async Task<List<SisArbolMenu>> GetListMenu()
        {
            using (var aux = new Repositorio<SisArbolMenu>())
            {
                return await aux.ObtenerTabla();
            }
        }

        #endregion
    }
}
