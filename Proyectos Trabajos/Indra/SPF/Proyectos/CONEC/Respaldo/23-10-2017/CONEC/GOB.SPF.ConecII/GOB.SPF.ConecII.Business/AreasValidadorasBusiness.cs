using System.Data.SqlClient;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;

    using System.Linq;
    using System.Collections.Generic;

    public class AreasValidadorasBusiness
    {
        public IEnumerable<AreasValidadoras> Obtener(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                return new RepositoryAreasValidadoras(uow).Obtener(paging);
            }
        }

        public bool InsertarTabla(List<AreasValidadoras> entity)
        {
            bool result;
            try
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var dataTable = ConversorEntityDatatable.TransformarADatatable(entity.ToArray());
                    result = new RepositoryAreasValidadoras(uow).InsertarTabla(dataTable);
                    uow.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return result;
        }
        public bool ActualizarTabla(List<AreasValidadoras> entity)
        {
            bool result;
            try
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var dataTable = ConversorEntityDatatable.TransformarADatatable(entity.ToArray());

                    result = new RepositoryAreasValidadoras(uow).ActualizarTabla(dataTable);

                    uow.SaveChanges();

                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return result;
        }

        public bool CambiarEstatus(AreasValidadoras entity)
        {
            bool result;
            try
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    result = new RepositoryAreasValidadoras(uow).CambiarEstatus(entity) > 0;

                    uow.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return result;
        }
    }
}
