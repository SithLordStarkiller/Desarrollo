using GOB.SPF.ConecII.AccessData.Schemas;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryDomicilioFiscal : IRepository<DomicilioFiscal>
    {
        public int Pages { get; set; }

        private readonly UnitOfWorkCatalog _unitOfWork;

        public RepositoryDomicilioFiscal(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException();

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int Actualizar(DomicilioFiscal entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(DomicilioFiscal entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(Cliente entity)
        {
            var result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClientInsertarDomicilioFiscal;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.DomicilioFiscal.Asentamiento.Estado.Identificador, ParameterName = "@IdEstado" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.DomicilioFiscal.Asentamiento.Municipio.Identificador, ParameterName = "@IdMunicipio" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.DomicilioFiscal.Asentamiento.Identificador, ParameterName = "@IdAsentamiento" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.DomicilioFiscal.Calle, ParameterName = "@Calle" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.DomicilioFiscal.NoInterior, ParameterName = "@NoInterior" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.DomicilioFiscal.NoExterior, ParameterName = "@NoExterior" });

                result = cmd.ExecuteNonQuery();

                _unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<DomicilioFiscal> Obtener(Paging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomicilioFiscal> ObtenerPorCriterio(Paging paging, DomicilioFiscal entity)
        {
            throw new NotImplementedException();
        }

        public DomicilioFiscal ObtenerPorCriterio(Cliente entity)
        {
            var result = new DomicilioFiscal();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerDomicilioFiscal;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdCliente" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Identificador = Convert.ToInt32(reader["IdDomicilioFiscal"]);
                        //result.Cliente.Identificador = Convert.ToInt32(reader["IdCliente"]);
                        result.IdPais = Convert.ToInt32(reader["IdPais"]);
                        result.Asentamiento.Estado.Identificador = Convert.ToInt32(reader["IdEstado"]);
                        result.Asentamiento.Municipio.Identificador = Convert.ToInt32(reader["IdMunicipio"]);
                        result.Asentamiento.Identificador = Convert.ToInt32(reader["IdAsentamiento"]);
                        result.Calle = reader["Calle"] != DBNull.Value ? reader["Calle"].ToString() : "";
                        result.NoInterior = reader["NoInterior"] != DBNull.Value ? reader["NoInterior"].ToString() : "";
                        result.NoExterior = reader["NoExterior"] != DBNull.Value ? reader["NoExterior"].ToString() : "";
                        result.Asentamiento.CodigoPostal = reader["CodigoPostal"].ToString();
                    }
                }
                return result; 
            }
        }

        public int Insertar(DomicilioFiscal entity)
        {
            throw new NotImplementedException();
        }

        public DomicilioFiscal ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }
    }
}
