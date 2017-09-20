namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class TiposDocumentoBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();
        public int Pages { get { return pages; } }
        
        public bool Guardar(TipoDocumento entity)
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
                    var repositoryTiposDocumento = new RepositoryTiposDocumento(uow);

                    if (entity.Identificador > 0)
                        result = repositoryTiposDocumento.Actualizar(entity) > 0;
                    else
                        result = repositoryTiposDocumento.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<TipoDocumento> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                return repositoryTipoDocumento.Obtener(paging);
            }
        }

        public IEnumerable<TipoDocumento> ObtenerPorCriterio(Paging paging, TipoDocumento entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                return repositoryTipoDocumento.ObtenerPorCriterio(paging, entity);
            }
        }

        public TipoDocumento ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                return repositoryTipoDocumento.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(TipoDocumento tipoDocumento)
        {
            TipoDocumento instancia = ObtenerPorId(tipoDocumento.Identificador);
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
                    var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                    result = repositoryTipoDocumento.CambiarEstatus(tipoDocumento) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public string ValidacionRegistro(TipoDocumento tipoDocumento)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                resultValidacion = repositoryTipoDocumento.ValidarRegistro(tipoDocumento);
            }
            return resultValidacion;
        }


    }
}
