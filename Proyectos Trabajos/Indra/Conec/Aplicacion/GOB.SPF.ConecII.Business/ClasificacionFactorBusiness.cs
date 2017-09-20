namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class ClasificacionFactorBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public bool Guardar(ClasificacionFactor entity)
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
                    var repositoryCoutas = new RepositoryClasificacionFactor(uow);

                    if (entity.Identificador > 0)
                        result = repositoryCoutas.Actualizar(entity) > 0;
                    else
                        result = repositoryCoutas.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<ClasificacionFactor> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryClasificacionFactor(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<ClasificacionFactor> ObtenerPorCriterio(Paging paging, ClasificacionFactor entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryClasificacionFactor(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public ClasificacionFactor ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryClasificacionFactor(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(ClasificacionFactor coutas)
        {
            ClasificacionFactor instancia = ObtenerPorId(coutas.Identificador);
            string messageValidation = ValidacionRegistro(instancia);

            bool result = false;
            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryCoutas = new RepositoryClasificacionFactor(uow);
                    result = repositoryCoutas.CambiarEstatus(coutas) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public string ValidacionRegistro(ClasificacionFactor entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryClasificacionFactores = new RepositoryClasificacionFactor(uow);
                resultValidacion = repositoryClasificacionFactores.ValidarRegistro(entity);
            }
            return resultValidacion;
        }

    }
}
