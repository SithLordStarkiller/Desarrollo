namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;
    public class PeriodosBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public int Guardar(Periodo entity)
        {
            int result = 0;
            string messageValidation = ValidacionRegistro(entity);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryPeriodo = new RepositoryPeriodo(uow);

                    if (entity.Identificador > 0)
                        result = repositoryPeriodo.Actualizar(entity);
                    else
                        result = repositoryPeriodo.Insertar(entity);

                    uow.SaveChanges();
                }
            } 
            return result;
        }

        public IEnumerable<Periodo> ObtenerTodos(Paging paging)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryPeriodo = new RepositoryPeriodo(uow);
                return repositoryPeriodo.Obtener(paging);
            }
        }

        public Periodo ObtenerPorId(long id)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryPeriodo = new RepositoryPeriodo(uow);
                return repositoryPeriodo.ObtenerPorId(id);
            }
        }

        public int CambiarEstatus(Periodo entity)
        {
            int result = 0;
            Periodo periodo = ObtenerPorId(entity.Identificador);
            string messageValidation = "";

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryPeriodo = new RepositoryPeriodo(uow);
                    result = repositoryPeriodo.CambiarEstatus(entity);

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public IEnumerable<Periodo> ObtenerPorCriterio(Paging paging, Periodo entity)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryPeriodo = new RepositoryPeriodo(uow);
                return repositoryPeriodo.ObtenerPorCriterio(paging, entity);
            }
        }

        public string ValidacionRegistro(Periodo entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryDivisiones = new RepositoryPeriodo(uow);
                resultValidacion = repositoryDivisiones.ValidarRegistro(entity);
            }
            return resultValidacion;
        }

    }
}
