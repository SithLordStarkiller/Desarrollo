namespace GOB.SPF.ConecII.AccessData.Repositories
{
    #region Librerias

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    using Entities;
    using Entities.DTO;

    #endregion

    public class RepositoryTelefono : Repository<Telefono>
    {
        #region Constructor

        public RepositoryTelefono(IUnitOfWork uow) : base(uow)
        {
        }

        #endregion

        #region Métodos públicos

        public List<Telefono> ObtenerPorIdInstalacion(long identificador)
        {
            var telefonos = new List<Telefono>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.TelefonoObtenerPorIdInstalacion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdInstalacion" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var telefono = new Telefono()
                        {
                            Identificador = Convert.ToInt32(reader["IdTelefonoInstalacion"]),
                            TipoTelefono =
                            {
                                Identificador = Convert.ToInt32(reader["IdTipoTelefono"]),
                                Nombre = reader["TipoTelefono"].ToString()
                            },
                            Numero = reader["Telefono"].ToString(),
                            Extension = reader["Extension"].ToString(),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                        telefonos.Add(telefono);
                    }
                }
                return telefonos;
            }

        }

        public List<Telefono> ObtenerPorIdExterno(long identificador)
        {
            var telefonos = new List<Telefono>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.TelefonoObtenerPorIdExterno;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdExterno" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var telefono = new Telefono()
                        {
                            Identificador = Convert.ToInt32(reader["IdTelefonos"]),
                            TipoTelefono =
                            {
                                Identificador = Convert.ToInt32(reader["IdTipoTelefono"]),
                                Nombre = reader["TipoTelefono"].ToString()
                            },
                            Numero = reader["Telefono"].ToString(),
                            Extension = reader["Extension"].ToString(),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                        telefonos.Add(telefono);
                    }
                }
                return telefonos;
            }

        }

        public int Insertar(TelefonosExternosDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.TelefonoExternoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                var value = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Telefonos",
                    Value = list
                };
                cmd.Parameters.Add(value);
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public int Actualizar(TelefonosExternosDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.TelefonoExternoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                var value = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Telefonos",
                    Value = list
                };
                cmd.Parameters.Add(value);
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }
        public int Insertar(TelefonosInstalacionDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.TelefonoInstalacionInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                var value = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Telefonos",
                    Value = list
                };
                cmd.Parameters.Add(value);
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public int Actualizar(TelefonosInstalacionDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.TelefonoInstalacionActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                var value = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Telefonos",
                    Value = list
                };
                cmd.Parameters.Add(value);
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }
        #endregion
    }
}
