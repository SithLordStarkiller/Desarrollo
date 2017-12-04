using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Business
{
    public partial class Business<T> 
        where T : TEntity
    {
        public Repository<T> MRepositorio;
        internal IUnitOfWork uow;

        private int pages { get; set; }
        public int Pages { get { return pages; } }

        public Business()
        {
            MRepositorio = new Repository<T>();
            uow = MRepositorio.UoW;
        }
                

        public bool Guardar(T entity)
        {
            bool result = false;
            string messageValidation = ValidacionRegistro(entity);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                //using (var uow = UnitOfWorkFactory.Create())
                {
                    //var repositoryTiposDocumento = new RepositoryTiposDocumento(uow);

                    if (entity.Identificador > 0)
                        result = MRepositorio.Actualizar(entity) > 0;
                    else
                        result = MRepositorio.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<T> ObtenerTodos(Paging paging)
        {
            //using (var uow = UnitOfWorkFactory.Create())
            {
                //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                return MRepositorio.Obtener(paging);
            }
        }

        public IEnumerable<T> ObtenerPorCriterio(Paging paging, T entity)
        {
            //using (var uow = UnitOfWorkFactory.Create())
            {
                //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                return MRepositorio.ObtenerPorCriterio(paging, entity);
            }
        }

        public T ObtenerPorId(long id)
        {
            //using (var uow = UnitOfWorkFactory.Create())
            {
                //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                return MRepositorio.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(T tipoDocumento)
        {

            bool result = false;
            //using (var uow = UnitOfWorkFactory.Create())
            {
                //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                result = MRepositorio.CambiarEstatus(tipoDocumento) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        public string ValidacionRegistro(T tipoDocumento)
        {
            string resultValidacion = "";

            //using (var uow = UnitOfWorkFactory.Create())
            {
                //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                resultValidacion = MRepositorio.ValidarRegistro(tipoDocumento);
            }
            return resultValidacion;
        }


    }
}
