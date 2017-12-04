using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.DTO;
using System.Collections.Generic;
using System.Linq;

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

        public T ObtenerPorId(T entity)
        {
            //using (var uow = UnitOfWorkFactory.Create())
            {
                //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                
                return MRepositorio.ObtenerPorCriterio(new Paging() {All = true }, entity).ToList().FirstOrDefault();
            }
        }
        

        public bool CambiarEstatus(T tipoDocumento)
        {
            T instancia = ObtenerPorId(tipoDocumento);
            string messageValidation = ValidacionRegistro(instancia);

            bool result = false;
            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                result = MRepositorio.CambiarEstatus(tipoDocumento) > 0;
                uow.SaveChanges();
            }
            return result;
            //bool result = false;
            ////using (var uow = UnitOfWorkFactory.Create())
            //{
            //    //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
            //    result = MRepositorio.CambiarEstatus(tipoDocumento) > 0;

            //    uow.SaveChanges();
            //}
            //return result;
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

        public IEnumerable<DropDto> ObtenerDropDownList()
        {
            //using (var uow = UnitOfWorkFactory.Create())
            {
                //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                return MRepositorio.ObtenerDropDownList();
            }
        }

        public IEnumerable<DropDto> ObtenerDropDownListCriterio(T entity)
        {
            //using (var uow = UnitOfWorkFactory.Create())
            {
                //var repositoryTipoDocumento = new RepositoryTiposDocumento(uow);
                return MRepositorio.ObtenerDropDownListCriterio(entity);
            }
        }


    }
}
