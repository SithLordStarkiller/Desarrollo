namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using Entities.Request;
    using System.Collections.Generic;

    public class FactoresBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public bool Guardar(Factor entity)
        {
            bool result = false;
            string messageValidation = ValidacionRegistro(entity);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {

                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryFactores = new RepositoryFactores(uow);

                    if (entity.Identificador > 0)
                        result = repositoryFactores.Actualizar(entity) > 0;
                    else
                        result = repositoryFactores.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<Factor> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<Factor> ObtenerPorCriterio(Paging paging, Factor entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public Factor ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(Factor coutas)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                result = repositoryCoutas.CambiarEstatus(coutas)>0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Factor> ObtenerPorClasificacion(Factor entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.ObtenerPorClasificacion(entity);
            }
        }


        public IEnumerable<Factor> ClasificacionObtieneFactor(RequestFactor entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.ClasificacionObtieneFactor(entity);
            }
        }

        public string ValidacionRegistro(Factor entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactores = new RepositoryFactores(uow);
                resultValidacion = repositoryFactores.ValidarRegistro(entity);
            }
            return resultValidacion;
        }

    }
}
