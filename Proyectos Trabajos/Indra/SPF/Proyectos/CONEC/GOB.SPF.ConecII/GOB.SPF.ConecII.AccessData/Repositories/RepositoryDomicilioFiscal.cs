using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    #region Librerias

    using Schemas;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    #endregion

    public class RepositoryDomicilioFiscal : IRepository<DomicilioFiscal>
    {
        #region Propiedades públicas

        public int Pages { get; set; }

        #endregion

        #region Variables privadas

        private readonly UnitOfWorkCatalog _unitOfWork;

        #endregion

        #region Métodos públicos

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
            var result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ClienteActualizarDomicilioFiscal;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Asentamiento.CodigoPostal, ParameterName = "@CodigoPostal", DbType = DbType.String });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Asentamiento.Estado.Identificador, ParameterName = "@IdEstado" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Asentamiento.Municipio.Identificador, ParameterName = "@IdMunicipio" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Asentamiento.Identificador, ParameterName = "@IdAsentamiento" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Calle, ParameterName = "@Calle" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NoInterior, ParameterName = "@NoInterior" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NoExterior, ParameterName = "@NoExterior" });

                result = cmd.ExecuteNonQuery();

                return result;
            }

        }

        public int CambiarEstatus(DomicilioFiscal entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(DomicilioFiscal entity)
        {
            var result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ClientInsertarDomicilioFiscal;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Asentamiento.CodigoPostal, ParameterName = "@CodigoPostal", DbType = DbType.String });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Asentamiento.Estado.Identificador, ParameterName = "@IdEstado" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Asentamiento.Municipio.Identificador, ParameterName = "@IdMunicipio" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Asentamiento.Identificador, ParameterName = "@IdAsentamiento" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Calle, ParameterName = "@Calle" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NoInterior, ParameterName = "@NoInterior" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NoExterior, ParameterName = "@NoExterior" });

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<DomicilioFiscal> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomicilioFiscal> ObtenerPorCriterio(IPaging paging, DomicilioFiscal entity)
        {
            throw new NotImplementedException();
        }

        public DomicilioFiscal ObtenerPorCriterio(Cliente entity)
        {
            var result = new DomicilioFiscal();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ClienteObtenerDomicilioFiscal;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdCliente" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Identificador = Convert.ToInt32(reader["IdDomicilioFiscal"]);
                        result.Cliente.Identificador = Convert.ToInt32(reader["IdCliente"]);
                        result.IdPais = Convert.ToInt32(reader["IdPais"]);
                        result.Asentamiento.Estado.Identificador = Convert.ToInt32(reader["IdEstado"]);
                        result.Asentamiento.Estado.Nombre = reader["Estado"].ToString();
                        result.Asentamiento.Municipio.Identificador = Convert.ToInt32(reader["IdMunicipio"]);
                        result.Asentamiento.Municipio.Nombre = reader["Municipio"].ToString();
                        result.Asentamiento.Identificador = Convert.ToInt32(reader["IdAsentamiento"]);
                        result.Asentamiento.Nombre = reader["Asentamiento"].ToString();
                        result.Calle = reader["Calle"] != DBNull.Value ? reader["Calle"].ToString() : "";
                        result.NoInterior = reader["NoInterior"] != DBNull.Value ? reader["NoInterior"].ToString() : "";
                        result.NoExterior = reader["NoExterior"] != DBNull.Value ? reader["NoExterior"].ToString() : "";
                        result.Asentamiento.CodigoPostal = reader["CodigoPostal"].ToString();
                    }
                }
                return result;
            }
        }

        public DomicilioFiscal ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
