namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;
    public class RegimenFiscalesBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public int Guardar(RegimenFiscal entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposRegimenFiscal = new RepositoryRegimenFiscal(uow);

                if (entity.Identificador > 0)
                    result = repositoryTiposRegimenFiscal.Actualizar(entity);
                else
                    result = repositoryTiposRegimenFiscal.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<RegimenFiscal> ObtenerTodos(Paging paging)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposRegimenFiscal = new RepositoryRegimenFiscal(uow);
                return repositoryTiposRegimenFiscal.Obtener(paging);
            }
        }

        public RegimenFiscal ObtenerPorId(long id)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposRegimenFiscal = new RepositoryRegimenFiscal(uow);
                return repositoryTiposRegimenFiscal.ObtenerPorId(id);
            }
        }

        public int CambiarEstatus(RegimenFiscal entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposRegimenFiscal = new RepositoryRegimenFiscal(uow);
                result = repositoryTiposRegimenFiscal.CambiarEstatus(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<RegimenFiscal> ObtenerPorCriterio(Paging paging, RegimenFiscal entity)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposRegimenFiscal = new RepositoryRegimenFiscal(uow);
                return repositoryTiposRegimenFiscal.ObtenerPorCriterio(paging, entity);
            }
        }

    }
}
