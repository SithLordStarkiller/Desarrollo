using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using GOB.SPF.ConecII.Interfaces.Genericos;
using GOB.SPF.ConecII.Library;

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
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_NotaDeCreditoObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = Identificador, ParameterName = "@IdNotaDeCredito" });
                var entity = new NotaDeCredito();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entity = LeerNotaDeCredito(reader);
                    }

                    return entity;
                }
            }
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
            using (var cmd = _unitOfWork.CreateCommand())
            {
                //cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_NotaDeCreditoFirmaActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdNotaCredito" });
                cmd.Parameters.Add(new SqlParameter() { Value = firma, ParameterName = "@Firma" });
                var result = cmd.ExecuteNonQuery();
            }

            return true;
        }

        private NotaDeCredito LeerNotaDeCredito(IDataRecord reader)
        {
            var entity = new NotaDeCredito();
            var reositoryRecibo = new RepositoryRecibo(_unitOfWork);
            entity.Identificador = reader["IdNotaDeCredito"].To<int>();
            entity.Folio = reader["Folio"].To<string>();
            entity.IdRecibo= reader["IdRecibo"].To<int>();
            entity.Recibo = reositoryRecibo.ObtenerPorId(reader["IdRecibo"].To<int>());
            return entity;
        }
        #endregion


    }
}
