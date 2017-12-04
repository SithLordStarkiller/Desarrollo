using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public partial class Repository<T> : IRepository<T>
        where T : class
    {

        internal int pages { get; set; }
        public int Pages { get { return pages; } }
                
        private readonly UnitOfWorkCatalog _unitOfWork;

        public UnitOfWorkCatalog UoW
        {
            get { return _unitOfWork; }
        }

        public Repository()
        {
            IUnitOfWork uow = UnitOfWorkFactory.Create();
            _unitOfWork = uow as UnitOfWorkCatalog;            
        }

        public Repository(IUnitOfWork uow)
        {            
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public virtual IEnumerable<T> Obtener(Paging paging)
        {
            throw new NotImplementedException();
        }

        public virtual T ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public virtual int CambiarEstatus(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual int Insertar(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual int Actualizar(T entity)
        {
            throw new NotImplementedException();
        }
        
        public virtual IEnumerable<T> ObtenerPorCriterio(Paging paging, T entity)
        {
            throw new NotImplementedException();
        }

        public string ValidarRegistro(T tipoDocumento)
        {
            throw new NotImplementedException();
        }
    }
}
