using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryNotaDeCredito : IRepository<NotaDeCredito>
    {

        #region Variables privadas

        private readonly UnitOfWorkCatalog _unitOfWork;

        #endregion

        #region Constructor

        public RepositoryNotaDeCredito(IUnitOfWork uow)
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

        public int Actualizar(NotaDeCredito entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(NotaDeCredito entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(NotaDeCredito entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotaDeCredito> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotaDeCredito> ObtenerPorCriterio(IPaging paging, NotaDeCredito entity)
        {
            throw new NotImplementedException();
        }

        public NotaDeCredito ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Guarda la fima digital en la cotizacion
        /// </summary>
        /// <param name="idCotiazcion"></param>
        /// <param name="firma"></param>
        /// <returns></returns>
        public bool GuardarFirma(int identificador, byte[] firma)
        {   
            return true;
        }

        #endregion


    }
}
