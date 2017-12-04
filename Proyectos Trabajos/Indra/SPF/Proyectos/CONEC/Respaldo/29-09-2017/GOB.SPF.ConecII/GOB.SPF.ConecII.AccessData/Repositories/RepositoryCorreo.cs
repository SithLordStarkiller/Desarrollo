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

        public int GuardarCorreosExternos(CorreosExternoDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.CorreoExternoInsertar;
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

        public List<Correo> ObtenerPorIdExterno(long identificador)
        {
            var correos = new List<Correo>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.CorreoObtenerPorIdExterno;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdExterno" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var correo = new Correo()
                        {
                            Identificador = Convert.ToInt32(reader["IdCorreos"]),
                            CorreoElectronico = reader["Correo"].ToString()
                        };
                        correos.Add(correo);
                    }
                }
                return correos;
            }

        }

        public List<Correo> ObtenerPorIdInstalacion(long identificador)
        {
            var correos = new List<Correo>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.CorreoObtenerPorIdInstalacion;
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

        public int Insertar(CorreosExternoDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.CorreoExternoInsertar;
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

        public int Actualizar(CorreosExternoDto list)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.CorreoExternoActualizar;
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
        #endregion

    }
}
