using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;
    public class FasesBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public int Guardar(Fase entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFase = new RepositoryFase(uow);

                if (entity.Identificador > 0)
                    result = repositoryFase.Actualizar(entity);
                else
                    result = repositoryFase.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Fase> ObtenerTodos(IPaging paging)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFase = new RepositoryFase(uow);
                return repositoryFase.Obtener(paging);
            }
        }

        public Fase ObtenerPorId(long id)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFase = new RepositoryFase(uow);
                return repositoryFase.ObtenerPorId(id);
            }
        }

        public int CambiarEstatus(Fase entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFase = new RepositoryFase(uow);
                result = repositoryFase.CambiarEstatus(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Fase> ObtenerPorCriterio(IPaging paging, Fase entity)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFase = new RepositoryFase(uow);
                return repositoryFase.ObtenerPorCriterio(paging, entity);
            }
        }

    }
}
