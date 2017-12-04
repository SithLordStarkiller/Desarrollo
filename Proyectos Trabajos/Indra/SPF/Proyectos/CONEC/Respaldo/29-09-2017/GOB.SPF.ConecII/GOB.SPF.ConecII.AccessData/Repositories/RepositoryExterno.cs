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

    public class RepositoryExterno : Repository<Externo>
    {
        #region Constructor

        public RepositoryExterno(IUnitOfWork uow) : base(uow)
        {
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Inserta o actualiza en la Bd un nuevo externo (contacto o solicitante)
        /// </summary>
        /// <param name="entity">contacto o soliciante a guardar.</param>
        /// <returns>Identificador externo</returns>
        public override int Insertar(Externo entity)
        {
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.ExternoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdExterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdTipoPersona, ParameterName = "@IdTipoPersona" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.TipoContacto.Identificador, ParameterName = "@IdTipoContacto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ApellidoPaterno, ParameterName = "@ApellidoPaterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ApellidoMaterno, ParameterName = "@ApellidoMaterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cargo, ParameterName = "@Cargo" });
                var result = (int)cmd.ExecuteScalar();
                return result;
            }
        }

        public override int Actualizar(Externo entity)
        {
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.ExternoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdExterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdTipoPersona, ParameterName = "@IdTipoPersona" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.TipoContacto.Identificador, ParameterName = "@IdTipoContacto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ApellidoPaterno, ParameterName = "@ApellidoPaterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ApellidoMaterno, ParameterName = "@ApellidoMaterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cargo, ParameterName = "@Cargo" });
                var result = (int)cmd.ExecuteScalar();
                return result;
            }
        }

        public List<Externo> ObtenerPorId(long Identificador)
        {
            var externos = new List<Externo>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.ExternoObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = Identificador, ParameterName = "@IdCliente" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var externo = new Externo()
                        {
                            Identificador = Convert.ToInt32(reader["IdExterno"]),
                            Cliente = { Identificador = Convert.ToInt32(reader["IdCliente"]) },
                            IdTipoPersona = Convert.ToInt32(reader["IdTipoPersona"]),
                            TipoContacto = {
                                Identificador = reader["IdTipoContacto"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdTipoContacto"]),
                                Nombre = reader["TipoContacto"] == DBNull.Value ? null : reader["TipoContacto"].ToString()
                                },
                            Nombre = reader["Nombre"].ToString(),
                            ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                            Cargo = reader["Cargo"].ToString(),
                            Activo = (bool)reader["Activo"]
                        };
                        externos.Add(externo);
                    }
                }
                return externos;
            }
        }

        #endregion
    }
}
