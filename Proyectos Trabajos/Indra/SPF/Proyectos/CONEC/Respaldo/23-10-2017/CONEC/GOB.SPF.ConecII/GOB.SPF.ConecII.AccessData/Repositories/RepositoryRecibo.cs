using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Interfaces.Genericos;
using GOB.SPF.ConecII.Library;


namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryRecibo : IRepository<Recibo>
    {
        #region Variables privadas

        private readonly UnitOfWorkCatalog _unitOfWork;

        #endregion

        #region Constructor
        public RepositoryRecibo(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;
            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }
        #endregion

        #region Interface IRepository
        public int Actualizar(Recibo entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(Recibo entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(Recibo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recibo> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recibo> ObtenerPorCriterio(IPaging paging, Recibo entity)
        {
            throw new NotImplementedException();
        }

        public Recibo ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Methods
        public bool GuardarFirma(int identificador, byte[] firma)
        {
            return true;
        }

        #endregion



    }
}
