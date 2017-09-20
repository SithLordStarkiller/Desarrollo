using GOB.SPF.ConecII.AccessData.Schemas;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryCliente : IRepository<Cliente>
    {
        public int Pages { get; set; }
        private Cliente _cliente = new Cliente();

        private readonly UnitOfWorkCatalog _unitOfWork;

        public RepositoryCliente(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;
            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int Actualizar(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public Cliente Insertar(Cliente entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.RegimenFiscal.Identificador, ParameterName = "@RegimenFiscal" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Sector.Identificador, ParameterName = "@Sector" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.RazonSocial, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NombreCorto, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Rfc, ParameterName = "@RFC" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _cliente = LeerCliente(reader);
                    }
                }
            }
            return _cliente;
        }

        public IEnumerable<Cliente> Obtener(Paging paging)
        {
            var result = new List<Cliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _cliente = LeerCliente(reader);
                        result.Add(_cliente);
                    }
                }
                return result;
            }
        }

        private static Cliente LeerCliente(IDataRecord reader)
        {
            var cliente = new Cliente();
            cliente.Identificador = Convert.ToInt32(reader["IdCliente"]);
            cliente.RazonSocial = reader["RazonSocial"].ToString();
            cliente.NombreCorto = reader["NombreCorto"].ToString();
            cliente.RegimenFiscal = new RegimenFiscal();
            cliente.RegimenFiscal.Identificador = reader["IdRegimenFiscal"] != DBNull.Value? Convert.ToInt32(reader["IdRegimenFiscal"]): 0;
            cliente.Sector = new Sector();
            cliente.Sector.Identificador = reader["IdSector"] != DBNull.Value ? Convert.ToInt32(reader["IdSector"]) : 0;
            cliente.Rfc = reader["RFC"] != DBNull.Value ? reader["RFC"].ToString() : null;
            cliente.IsActive = Convert.ToBoolean(reader["Activo"]);
            return cliente;
        }

        public IEnumerable<Cliente> ObtenerPorRazonSocial(string searchText)
        {
            var result = new List<Cliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerPorRazonSocial;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = searchText, ParameterName = "@RazonSocial" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _cliente = LeerCliente(reader);
                        result.Add(_cliente);
                    }
                }
                return result;
            }
        }

        public IEnumerable<Cliente> ObtenerPorNombreCorto(string searchText)
        {
            var result = new List<Cliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerPorNombreCorto;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = searchText, ParameterName = "@NombreCorto" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _cliente = LeerCliente(reader);
                        result.Add(_cliente);
                    }
                }
                return result;
            }
        }

        public IEnumerable<Cliente> ObtenerPorCriterio(Paging paging, Cliente entity)
        {
            var result = new List<Cliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IsActive, ParameterName = "@Activo", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.RazonSocial ?? string.Empty, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NombreCorto ?? string.Empty, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.RegimenFiscal.Identificador, ParameterName = "@RegimenFiscal" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Sector.Identificador, ParameterName = "@Sector" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _cliente = LeerCliente(reader);
                        result.Add(_cliente);
                    }
                }
                return result;
            }
        }

        public Cliente ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        int IRepository<Cliente>.Insertar(Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}
