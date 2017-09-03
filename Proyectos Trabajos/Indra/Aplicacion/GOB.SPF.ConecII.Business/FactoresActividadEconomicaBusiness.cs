namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class FactoresActividadEconomicaBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public bool Guardar(FactorActividadEconomica entity)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);

                if (entity.Identificador > 0)
                    result = repositoryFactoresActividadEconomica.Actualizar(entity)>0;
                else
                    result = repositoryFactoresActividadEconomica.Insertar(entity)>0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<FactorActividadEconomica> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                return repositoryFactoresActividadEconomica.Obtener(paging);
            }
        }

        public IEnumerable<FactorActividadEconomica> ObtenerPorCriterio(Paging paging, FactorActividadEconomica entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                return repositoryFactoresActividadEconomica.ObtenerPorCriterio(paging, entity);
            }
        }

        public FactorActividadEconomica ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                return repositoryFactoresActividadEconomica.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(FactorActividadEconomica tServicio)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                result = repositoryFactoresActividadEconomica.CambiarEstatus(tServicio)>0;

                uow.SaveChanges();
            }
            return result;
        }
    }
}
