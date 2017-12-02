using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class TipoInstalacionBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(TipoInstalacion entity)
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
                    var repositoryTipoInstalacion = new RepositoryTipoInstalacion(uow);

                    if (entity.Identificador > 0)
                        result = repositoryTipoInstalacion.Actualizar(entity) > 0;
                    else
                        result = repositoryTipoInstalacion.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<TipoInstalacion> ObtenerTodos(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoInstalacion = new RepositoryTipoInstalacion(uow);
                return repositoryTipoInstalacion.Obtener(paging);
            }
        }

        public IEnumerable<TipoInstalacion> ObtenerPorCriterio(IPaging paging, TipoInstalacion entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoInstalacion = new RepositoryTipoInstalacion(uow);
                return repositoryTipoInstalacion.ObtenerPorCriterio(paging, entity);
            }
        }

        public TipoInstalacion ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoInstalacion = new RepositoryTipoInstalacion(uow);
                return repositoryTipoInstalacion.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(TipoInstalacion tServicio)
        {
            //int result = 0;
            TipoInstalacion instancia = ObtenerPorId(tServicio.Identificador);
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
                    var repositoryTipoInstalacion = new RepositoryTipoInstalacion(uow);
                    result = repositoryTipoInstalacion.CambiarEstatus(tServicio) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public string ValidacionRegistro(TipoInstalacion entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoInstalacion = new RepositoryTipoInstalacion(uow);
                //resultValidacion = repositoryTipoInstalacion.ValidarRegistro(entity);
            }

            return resultValidacion;
        }


    }
}
