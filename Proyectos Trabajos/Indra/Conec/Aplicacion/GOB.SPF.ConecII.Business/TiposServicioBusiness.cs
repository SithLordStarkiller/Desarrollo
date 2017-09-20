namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class TiposServicioBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(TipoServicio entity)
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
                    var repositoryTipoServicio = new RepositoryTiposServicio(uow);

                    if (entity.Identificador > 0)
                        result = repositoryTipoServicio.Actualizar(entity) > 0;
                    else
                        result = repositoryTipoServicio.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<TipoServicio> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoServicio = new RepositoryTiposServicio(uow);
                return repositoryTipoServicio.Obtener(paging);
            }
        }

        public IEnumerable<TipoServicio> ObtenerPorCriterio(Paging paging, TipoServicio entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoServicio = new RepositoryTiposServicio(uow);
                return repositoryTipoServicio.ObtenerPorCriterio(paging, entity);
            }
        }

        public TipoServicio ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoServicio = new RepositoryTiposServicio(uow);
                return repositoryTipoServicio.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(TipoServicio tServicio)
        {
            //int result = 0;
            TipoServicio instancia = ObtenerPorId(tServicio.Identificador);
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
                    var repositoryTipoServicio = new RepositoryTiposServicio(uow);
                    result = repositoryTipoServicio.CambiarEstatus(tServicio)>0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public string ValidacionRegistro(TipoServicio entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoServicio = new RepositoryTiposServicio(uow);
                resultValidacion = repositoryTipoServicio.ValidarRegistro(entity);
            }

            return resultValidacion;
        }


    }
}
