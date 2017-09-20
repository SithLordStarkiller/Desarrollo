namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class DependenciasBusiness
    {

        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public bool Guardar(Dependencia entity)
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
                    var repositoryCoutas = new RepositoryDependencias(uow);

                    if (entity.Identificador > 0)
                        result = repositoryCoutas.Actualizar(entity) > 0;
                    else
                        result = repositoryCoutas.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<Dependencia> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryDependencias(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<Dependencia> ObtenerPorCriterio(Paging paging, Dependencia entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryDependencias(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public Dependencia ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryDependencias(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(Dependencia entity)
        {
            bool result = false;
            Dependencia dependencia = ObtenerPorId(entity.Identificador);
            string messageValidation = ValidacionRegistro(dependencia);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryCoutas = new RepositoryDependencias(uow);
                    result = repositoryCoutas.CambiarEstatus(entity) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public string ValidacionRegistro(Dependencia entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryDependencias = new RepositoryDependencias(uow);
                resultValidacion = repositoryDependencias.ValidarRegistro(entity);
            }
            return resultValidacion;
        }
    }
}
