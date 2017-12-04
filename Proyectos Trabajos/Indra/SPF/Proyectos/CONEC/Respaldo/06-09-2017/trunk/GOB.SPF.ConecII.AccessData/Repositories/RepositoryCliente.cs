using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using System.Data;
using System.Data.SqlClient;
using GOB.SPF.ConecII.AccessData.Schemas;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryCliente : IRepository<Cliente>
    {
        public int Pages { get; set; }

        private readonly UnitOfWorkCatalog _unitOfWork;

        public RepositoryCliente(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException();

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

        public int Insertar(Cliente entity)
        {
            throw new NotImplementedException();
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
                        Cliente cliente = new Cliente();

                        cliente.Identificador = Convert.ToInt32(reader["IdCliente"]);
                        cliente.RazonSocial = reader["RazonSocial"].ToString();
                        cliente.NombreCorto = reader["NombreCorto"].ToString();
                        cliente.IdRegimenFiscal = reader["IdRegimenFiscal"] != DBNull.Value ? (int?)Convert.ToInt32(reader["IdRegimenFiscal"]) : null;
                        cliente.RegimenFiscal = reader["RegimenFiscal"] != DBNull.Value ? reader["RegimenFiscal"].ToString() : null;
                        cliente.IdSector = reader["IdSector"] != DBNull.Value ? (int?)Convert.ToInt32(reader["IdSector"]) : null;
                        cliente.Sector = reader["Sector"] != DBNull.Value ? reader["Sector"].ToString() : null;
                        cliente.RFC = reader["RFC"] != DBNull.Value ? reader["RFC"].ToString() : null;
                        cliente.IsActive = Convert.ToBoolean(reader["Activo"]);

                        result.Add(cliente);
                    }
                }
                return result;  // yield?
            }
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
                        Cliente cliente = new Cliente();

                        cliente.Identificador = Convert.ToInt32(reader["IdCliente"]);
                        cliente.RazonSocial = reader["RazonSocial"].ToString();
                        cliente.NombreCorto = reader["NombreCorto"].ToString();
                        cliente.IdRegimenFiscal = reader["IdRegimenFiscal"] != DBNull.Value ? (int?)Convert.ToInt32(reader["IdRegimenFiscal"]) : null;
                        cliente.RegimenFiscal = reader["RegimenFiscal"] != DBNull.Value ? reader["RegimenFiscal"].ToString() : null;
                        cliente.IdSector = reader["IdSector"] != DBNull.Value ? (int?)Convert.ToInt32(reader["IdSector"]) : null;
                        cliente.Sector = reader["Sector"] != DBNull.Value ? reader["Sector"].ToString() : null;
                        cliente.RFC = reader["RFC"] != DBNull.Value ? reader["RFC"].ToString() : null;
                        cliente.IsActive = Convert.ToBoolean(reader["Activo"]);

                        result.Add(cliente);
                    }
                }
                return result;  // yield?
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
                        Cliente cliente = new Cliente();

                        cliente.Identificador = Convert.ToInt32(reader["IdCliente"]);
                        cliente.RazonSocial = reader["RazonSocial"].ToString();
                        cliente.NombreCorto = reader["NombreCorto"].ToString();
                        cliente.IdRegimenFiscal = reader["IdRegimenFiscal"] != DBNull.Value ? (int?)Convert.ToInt32(reader["IdRegimenFiscal"]) : null;
                        cliente.RegimenFiscal = reader["RegimenFiscal"] != DBNull.Value ? reader["RegimenFiscal"].ToString() : null;
                        cliente.IdSector = reader["IdSector"] != DBNull.Value ? (int?)Convert.ToInt32(reader["IdSector"]) : null;
                        cliente.Sector = reader["Sector"] != DBNull.Value ? reader["Sector"].ToString() : null;
                        cliente.RFC = reader["RFC"] != DBNull.Value ? reader["RFC"].ToString() : null;
                        cliente.IsActive = Convert.ToBoolean(reader["Activo"]);

                        result.Add(cliente);
                    }
                }
                return result;  // yield?
            }
        }

        public IEnumerable<Cliente> ObtenerPorCriterio(Paging paging, Cliente entity)
        {
            var result = new List<Cliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.IsActive, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.RazonSocial ?? string.Empty, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NombreCorto ?? string.Empty, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();

                        cliente.Identificador = Convert.ToInt32(reader["IdCliente"]);
                        cliente.RazonSocial = reader["RazonSocial"].ToString();
                        cliente.NombreCorto = reader["NombreCorto"].ToString();
                        cliente.IdRegimenFiscal = reader["IdRegimenFiscal"] != DBNull.Value ? (int?)Convert.ToInt32(reader["IdRegimenFiscal"]) : null;
                        cliente.RegimenFiscal = reader["RegimenFiscal"] != DBNull.Value ? reader["RegimenFiscal"].ToString() : null;
                        cliente.IdSector = reader["IdSector"] != DBNull.Value ? (int?)Convert.ToInt32(reader["IdSector"]) : null;
                        cliente.Sector = reader["Sector"] != DBNull.Value ? reader["Sector"].ToString() : null;
                        cliente.RFC = reader["RFC"] != DBNull.Value ? reader["RFC"].ToString() : null;
                        cliente.IsActive = Convert.ToBoolean(reader["Activo"]);

                        result.Add(cliente);
                    }
                }
                return result;  // yield?
            }
        }

        public Cliente ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }
    }
}
