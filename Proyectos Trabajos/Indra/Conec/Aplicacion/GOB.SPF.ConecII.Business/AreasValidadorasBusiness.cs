using System.Data.SqlClient;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;

    using System.Linq;
    using System.Collections.Generic;

    public class AreasValidadorasBusiness
    {
        public IEnumerable<AreasValidadoras> Obtener(Paging paging)
        {
            return new RepositoryAreasValidadoras(UnitOfWorkFactory.Create()).Obtener(paging);
        }

        public bool InsertarTabla(List<AreasValidadoras> entity)
        {
            try
            {
                var dataTable = ConversorEntityDatatable.TransformarADatatable(entity.ToArray());
                return new RepositoryAreasValidadoras(UnitOfWorkFactory.Create()).InsertarTabla(dataTable);
                //var ops =
                //    entity.Select(item => new RepositoryAreasValidadoras(UnitOfWorkFactory.Create()).Insertar(item))
                //        .ToList();
                //return true;

                //return ops.Count == entity.Count;
            }
            catch (SqlException e)
            {
                new RepositoryAreasValidadoras(UnitOfWorkFactory.Create()).Eliminar(
                    entity.FirstOrDefault().IdTipoServicio);
                throw e;
            }
        }
        public bool ActualizarTabla(List<AreasValidadoras> entity)
        {
            var dataTable = ConversorEntityDatatable.TransformarADatatable(entity.ToArray());

            return new RepositoryAreasValidadoras(UnitOfWorkFactory.Create()).ActualizarTabla(dataTable);

            //return true;
        }

        public int CambiarEstatus(AreasValidadoras entity)
        {
            //return 1;
            return new RepositoryAreasValidadoras(UnitOfWorkFactory.Create()).CambiarEstatus(entity);
        }
    }
}
