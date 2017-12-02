using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class ReferenciasBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();
        public int Pages { get { return pages; } }
        
        public bool Guardar(Referencia entity)
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
                    var repositoryCoutas = new RepositoryReferencias(uow);

                    if (entity.Identificador > 0)
                        result = repositoryCoutas.Actualizar(entity) > 0;
                    else
                        result = repositoryCoutas.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<Referencia> ObtenerTodos(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryReferencias(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<Referencia> ObtenerPorCriterio(IPaging paging, Referencia entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryReferencias(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public Referencia ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryReferencias(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(Referencia entity)
        {
            bool result = false;
            Referencia referencia = ObtenerPorId(entity.Identificador);
            string messageValidation = ValidacionRegistro(referencia);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryCoutas = new RepositoryReferencias(uow);
                    result = repositoryCoutas.CambiarEstatus(entity) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public string ValidacionRegistro(Referencia entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryReferencias(uow);
                resultValidacion = repositoryCoutas.ValidarRegistro(entity);
            }
            return resultValidacion;
        }
    }
}
