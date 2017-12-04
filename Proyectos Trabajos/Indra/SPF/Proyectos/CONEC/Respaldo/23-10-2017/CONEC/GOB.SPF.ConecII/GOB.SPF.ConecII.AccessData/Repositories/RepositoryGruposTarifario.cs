using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryGruposTarifario : IRepository<VehiculoTarifario>
    {
        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryGruposTarifario(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<VehiculoTarifario> Obtener(IPaging paging)
        {
            var result = new List<VehiculoTarifario>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposTarifarioObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VehiculoTarifario gruposTarifario = new VehiculoTarifario();

                        gruposTarifario.Identificador = Convert.ToInt32(reader["IdGpoTarifario"]);
                        gruposTarifario.NombreGpoTarifario = Convert.ToString(reader["NombreGpoTarifario"]);
                        gruposTarifario.DescGpoTarifario = Convert.ToString(reader["DescGpoTarifario"]);
                        gruposTarifario.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        gruposTarifario.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        gruposTarifario.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(gruposTarifario);
                    }
                }
                return result;  // yield?
            }

        }

        public VehiculoTarifario ObtenerPorId(long id)
        {
            int result = 0;
            VehiculoTarifario gruposTarifario = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposTarifarioObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                gruposTarifario = new VehiculoTarifario();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gruposTarifario.Identificador = Convert.ToInt32(reader["IdGpoTarifario"]);
                        gruposTarifario.NombreGpoTarifario = Convert.ToString(reader["NombreGpoTarifario"]);
                        gruposTarifario.DescGpoTarifario = Convert.ToString(reader["DescGpoTarifario"]);
                        gruposTarifario.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        gruposTarifario.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        gruposTarifario.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return gruposTarifario;
        }

        public int CambiarEstatus(VehiculoTarifario gruposTarifario)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposTarifarioCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = gruposTarifario.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = gruposTarifario.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(VehiculoTarifario gruposTarifario)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposTarifarioInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter() { Value = gruposTarifario.NombreGpoTarifario, ParameterName = "@NombreGpoTarifario" };
                parameters[1] = new SqlParameter() { Value = gruposTarifario.DescGpoTarifario, ParameterName = "@DescGpoTarifario" };
                parameters[2] = new SqlParameter() { Value = gruposTarifario.FechaInicial, ParameterName = "@FechaInicial" };
                parameters[3] = new SqlParameter() { Value = gruposTarifario.FechaFinal, ParameterName = "@FechaFinal" };
                parameters[4] = new SqlParameter() { Value = gruposTarifario.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(VehiculoTarifario gruposTarifario)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposTarifarioActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = gruposTarifario.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = gruposTarifario.NombreGpoTarifario, ParameterName = "@NombreGpoTarifario" };
                parameters[2] = new SqlParameter() { Value = gruposTarifario.DescGpoTarifario, ParameterName = "@DescGpoTarifario" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<VehiculoTarifario> ObtenerPorCriterio(IPaging paging, VehiculoTarifario entity)
        {
            var result = new List<VehiculoTarifario>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposTarifarioObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NombreGpoTarifario, ParameterName = "@NombreGpoTarifario" });
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VehiculoTarifario gruposTarifario = new VehiculoTarifario();

                        gruposTarifario.Identificador = Convert.ToInt32(reader["IdGpoTarifario"]);
                        gruposTarifario.NombreGpoTarifario = Convert.ToString(reader["NombreGpoTarifario"]);
                        gruposTarifario.DescGpoTarifario = Convert.ToString(reader["DescGpoTarifario"]);
                        gruposTarifario.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        gruposTarifario.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        gruposTarifario.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(gruposTarifario);
                    }
                }
                return result;  // yield?
            }
        }
    }
}
