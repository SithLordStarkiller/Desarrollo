using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class MesesBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public int Save(Meses entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryMeses = new RepositoryMeses(uow);

                if (entity.Identificador > 0)
                    result = repositoryMeses.Actualizar(entity);
                else
                    result = repositoryMeses.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Meses> ObtenerTodos(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryMeses = new RepositoryMeses(uow);
                return repositoryMeses.Obtener(paging);
            }
        }

        public IEnumerable<Meses> ObtenerPorCriterio(IPaging paging, Meses entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryMeses = new RepositoryMeses(uow);
                return repositoryMeses.ObtenerPorCriterio(paging, entity);
            }
        }

        public Meses ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryMeses = new RepositoryMeses(uow);
                return repositoryMeses.ObtenerPorId(id);
            }
        }
    }
}
