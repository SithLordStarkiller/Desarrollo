namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using Entities.DTO;
    using System.Collections.Generic;
    using System.Linq;

    public class FactoresMunicipioBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(FactorMunicipioDTO entity)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                result = repositoryFactoresMunicipio.Insertar(Convert(entity))>0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<FactorMunicipio> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                return repositoryFactoresMunicipio.Obtener(paging);
            }
        }

        public IEnumerable<FactorMunicipio> ObtenerPorCriterio(Paging paging, FactorMunicipio entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                return repositoryFactoresMunicipio.ObtenerPorCriterio(paging, entity);
            }
        }

        public FactorMunicipio ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                return repositoryFactoresMunicipio.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(FactorMunicipio tServicio)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                result = repositoryFactoresMunicipio.CambiarEstatus(tServicio) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        public FactorMunicipioDTO Obtener(Paging paging)
        {
            FactorMunicipioDTO dto = new FactorMunicipioDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryClasificacion = new RepositoryClasificacionFactor(uow);
                var repositoryFactor = new RepositoryFactores(uow);
                McsBusiness business = new McsBusiness();

                dto.Clasificaciones.AddRange(repositoryClasificacion.Obtener(paging));
                dto.Estados = business.ObtenerEstados();
            }
            return dto;
        }

        private System.Data.DataTable Convert(FactorMunicipioDTO dto)
        {

            IEnumerable<Entities.TablaDefinidaPorUsuario.FactoresMunicipios> factoresMunicipios = dto.Municipios.Select(m => new Entities.TablaDefinidaPorUsuario.FactoresMunicipios { IdMunicipio = m.Identificador, IdClasificacion = dto.IdClasificacion, IdEstado = dto.IdEstado, IdFactor = dto.IdFactor, Descripcion = dto.Descripcion });
            System.Data.DataTable dataTable = ConversorEntityDatatable.TransformarADatatable(factoresMunicipios);
            return dataTable;
        }
    }
}

