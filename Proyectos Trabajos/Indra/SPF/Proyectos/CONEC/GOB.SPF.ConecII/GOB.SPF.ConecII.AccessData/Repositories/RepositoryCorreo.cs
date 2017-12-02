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

    public class RepositoryCorreo : Repository<Correo>
    {
        #region Constructor

        public RepositoryCorreo(IUnitOfWork uow) : base(uow)
        {
        }

        #endregion

        #region Métodos públicos

        #region Instalacion

        public List<Correo> ObtenerPorIdInstalacion(long identificador)
        {
            var correos = new List<Correo>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.CorreoObtenerPorIdInstalacion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdInstalacion" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var correo = new Correo()
                        {
                            Identificador = Convert.ToInt32(reader["IdCorreoInstalacion"]),
                            CorreoElectronico = reader["Correo"].ToString()
                        };
                        correos.Add(correo);
                    }
                }
                return correos;
            }

        }

        public int Actualizar(CorreosInstalacionDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.CorreoInstalacionActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                var value = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Correos",
                    Value = list
                };
                cmd.Parameters.Add(value);
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public int Insertar(CorreosInstalacionDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.CorreoInstalacionInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                var value = new SqlParameter { SqlDbType = SqlDbType.Structured, ParameterName = "@Correos", Value = list };
                cmd.Parameters.Add(value);
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        #endregion

        #region Externos

        public List<Correo> ObtenerPorIdExterno(long identificador)
        {
            var correos = new List<Correo>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.CorreoObtenerPorIdExterno;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdExterno" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var correo = new Correo()
                        {
                            Identificador = Convert.ToInt32(reader["IdCorreos"]),
                            CorreoElectronico = reader["Correo"].ToString(),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                        correos.Add(correo);
                    }
                }
                return correos;
            }

        }

        public int Actualizar(CorreosExternoDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.CorreoExternoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                var value = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Correos",
                    Value = list
                };
                cmd.Parameters.Add(value);
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public int Insertar(CorreosExternoDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.CorreoExternoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                var value = new SqlParameter { SqlDbType = SqlDbType.Structured, ParameterName = "@Correos", Value = list };
                cmd.Parameters.Add(value);
                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        #endregion


        #endregion

    }
}
