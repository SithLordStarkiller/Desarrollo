using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Business
{
    public partial class AdministraReflection<TEntity> 
        where TEntity : class
    {
        public Repositorio<TEntity> MRepositorio;

        public virtual void Inicializa(Repositorio<TEntity> repositorio)
        {
            MRepositorio = repositorio;
        }

        public int InsertStored(TEntity item)
        {
            return MRepositorio.AddIdentityStored(item);
        }

        public void UpdateStored(TEntity item)
        {
            MRepositorio.ModifiyStored(item);
        }

        public void DeleteStored(TEntity item)
        {
            throw new NotImplementedException();
        }


        //public IEnumerable<TEntity> FindPagedStored<S>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<TEntity, S>> orderByExpression, Domain.Core.Specification.ISpecification<TEntity> specification, bool ascending)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual TEntity FindByIdStored(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindByIdsStored(List<int> lstIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAllStored(bool activo)
        {
            throw new NotImplementedException();
        }

        public TEntity FindByIdStored(int id, bool activo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindByIdsStored(List<int> lstIds, bool activo)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<TEntity> FindItemsStored()
        {
            return MRepositorio.GetAllStored();
        }

        public IEnumerable<DropDto> FindItemsDropStored()
        {
            return MRepositorio.GetAllDropStored();
        }

    }
}
