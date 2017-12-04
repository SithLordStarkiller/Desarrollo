namespace GOB.SPF.ConecII.AccessData.Repositories
{
    #region Librerias

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    using Entities;

    #endregion

    public class RepositoryCliente : IRepository<Cliente>
    {
        #region Propiedades públicas

        public int Pages { get; set; }

        #endregion

        #region Variables privadas

        private readonly UnitOfWorkCatalog _unitOfWork;

        private Cliente _cliente = new Cliente();

        #endregion

        #region Constructor

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

        #endregion

        #region Métodos privados.

        private static Cliente LeerCliente(IDataRecord reader)
        {
            var cliente = new Cliente
            {
                Identificador = Convert.ToInt32(reader["IdCliente"]),
                RazonSocial = reader["RazonSocial"].ToString(),
                NombreCorto = reader["NombreCorto"].ToString(),
                RegimenFiscal = new RegimenFiscal
                {
                    Identificador = reader["IdRegimenFiscal"] != DBNull.Value
                        ? Convert.ToInt32(reader["IdRegimenFiscal"])
                        : 0,
                    Descripcion = reader["RegimenFiscal"]?.ToString()
                },
                Sector = new Sector
                {
                    Identificador = reader["IdSector"] != DBNull.Value ? Convert.ToInt32(reader["IdSector"]) : 0,
                    Descripcion = reader["Sector"].ToString()
                },
                Rfc = reader["RFC"] != DBNull.Value ? reader["RFC"].ToString() : null,
                IsActive = Convert.ToBoolean(reader["Activo"])
            };
            return cliente;
        }

        #endregion

        #region Métodos públicos

        public IEnumerable<Cliente> ObtenerPorCriterio(Paging paging, Cliente entity)
        {
            var result = new List<Cliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.IsActive, ParameterName = "@Activo", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = entity.RazonSocial ?? string.Empty, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.NombreCorto ?? string.Empty, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Rfc ?? string.Empty, ParameterName = "@RFC" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.RegimenFiscal.Identificador, ParameterName = "@RegimenFiscal" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Sector.Identificador, ParameterName = "@Sector" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.Rows, ParameterName = "@filas" });

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
                cmd.Parameters.Add(new SqlParameter { Value = searchText, ParameterName = "@NombreCorto" });

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

        public IEnumerable<Cliente> ObtenerPorRazonSocial(string searchText)
        {
            var result = new List<Cliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerPorRazonSocial;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = searchText, ParameterName = "@RazonSocial" });

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

        public bool ValidarRfc(string rfc, int? idCliente = null)
        {
            var result = false;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteValidar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = rfc, ParameterName = "@RFC" });
                cmd.Parameters.Add(new SqlParameter { Value = idCliente, ParameterName = "@IdCliente" });

                result = (int)cmd.ExecuteScalar() != 0;
            }
            return result;
        }

        public IEnumerable<Cliente> Obtener(Paging paging)
        {
            var result = new List<Cliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.Rows, ParameterName = "@filas" });

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

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = Identificador, ParameterName = "@IdCliente" });
                var cliente = new Cliente();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente = LeerCliente(reader);
                    }
                    return cliente;
                }
            }
        }

        public int CambiarEstatus(Cliente entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.IsActive, ParameterName = "@Activo" });
                return cmd.ExecuteNonQuery();
            }
        }

        public int Actualizar(Cliente entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.RegimenFiscal.Identificador, ParameterName = "@RegimenFiscal" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Sector.Identificador, ParameterName = "@Sector" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.RazonSocial, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.NombreCorto, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Rfc, ParameterName = "@RFC" });
                return cmd.ExecuteNonQuery();
            }
        }

        public int Insertar(Cliente entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.ClienteInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.RegimenFiscal.Identificador, ParameterName = "@RegimenFiscal" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Sector.Identificador, ParameterName = "@Sector" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.RazonSocial, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.NombreCorto, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Rfc, ParameterName = "@RFC" });
                return (int)cmd.ExecuteScalar();
            }
        }


        #endregion
    }
}
