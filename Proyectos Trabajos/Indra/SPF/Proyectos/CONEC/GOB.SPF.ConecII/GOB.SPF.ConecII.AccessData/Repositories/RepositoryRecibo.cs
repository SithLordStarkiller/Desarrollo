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
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_ReciboObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = Identificador, ParameterName = "@IdRecibo" });
                var recibo = new Recibo();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        recibo = LeerRecibo(reader);
                    }

                    return recibo;
                }
            }
        }

        #endregion

        #region Public Methods
        public bool GuardarFirma(int identificador, byte[] firma)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_ReciboFirmaActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdRecibo" });
                cmd.Parameters.Add(new SqlParameter() { Value = firma, ParameterName = "@Firma" });
                var result = cmd.ExecuteNonQuery();
            }

            return true;
        }

        #endregion

        private Recibo LeerRecibo(IDataRecord reader)
        {
            var recibo = new Recibo();
            var repositoryCliente = new RepositoryCliente(_unitOfWork);            
            recibo.Identificador = reader["IdRecibo"].To<int>();          
            recibo.FechaEmision = reader["FechaEmision"].To<DateTime>();
            recibo.Folio = reader["Folio"].To<string>();
            recibo.FechaInicio = reader["FechaInicio"].To<DateTime>();
            recibo.FechaFin = reader["FechaFin"].To<DateTime>();
            recibo.IdCliente = reader["IdCliente"].To<int>();
            recibo.Cliente = repositoryCliente.ObtenerPorId(reader["IdCliente"].To<int>());
            return recibo;
        }


    }
}
