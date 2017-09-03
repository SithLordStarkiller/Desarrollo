namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryJerarquias : IRepository<Jerarquia>
    {
        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryJerarquias(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Jerarquia> Obtener(Paging paging)
        {
            var result = new List<Jerarquia>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.JerarquiasObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Jerarquia jerarquias = new Jerarquia();

                        jerarquias.Identificador = Convert.ToInt32(reader["IdJerarquia"]);
                        jerarquias.Descripcion = Convert.ToString(reader["DescJerarquia"]);
                        jerarquias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        jerarquias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        jerarquias.IsActive = Convert.ToBoolean(reader["IsActive"]);

                        result.Add(jerarquias);
                    }
                }
                return result;  // yield?
            }

        }

        public Jerarquia ObtenerPorId(long id)
        {
            int result = 0;
            Jerarquia jerarquias = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.JerarquiasObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                jerarquias = new Jerarquia();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jerarquias.Identificador = Convert.ToInt32(reader["IdJerarquia"]);
                        jerarquias.Descripcion = Convert.ToString(reader["DescJerarquia"]);
                        jerarquias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        jerarquias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        jerarquias.IsActive = Convert.ToBoolean(reader["IsActive"]);


                    }
                }
            }

            return jerarquias;
        }

        public int CambiarEstatus(Jerarquia jerarquias)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.JerarquiasCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = jerarquias.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = jerarquias.IsActive, ParameterName = "@IsActive" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Jerarquia jerarquias)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.JerarquiasInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = jerarquias.Descripcion, ParameterName = "@DescJerarquia" };
                parameters[1] = new SqlParameter() { Value = jerarquias.FechaInicial, ParameterName = "@FechaInicial" };
                parameters[2] = new SqlParameter() { Value = jerarquias.FechaFinal, ParameterName = "@FechaFinal" };
                parameters[3] = new SqlParameter() { Value = jerarquias.IsActive, ParameterName = "@IsActive" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(Jerarquia jerarquias)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.JerarquiasActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter() { Value = jerarquias.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = jerarquias.Descripcion, ParameterName = "@DescJerarquia" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Jerarquia> ObtenerPorCriterio(Paging paging, Jerarquia entity)
        {
            var result = new List<Jerarquia>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.JerarquiasObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@DescJerarquia" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Jerarquia jerarquias = new Jerarquia();

                        jerarquias.Identificador = Convert.ToInt32(reader["IdJerarquia"]);
                        jerarquias.Descripcion = Convert.ToString(reader["DescJerarquia"]);
                        jerarquias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        jerarquias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        jerarquias.IsActive = Convert.ToBoolean(reader["IsActive"]);

                        result.Add(jerarquias);
                    }
                }
                return result;  // yield?
            }
        }
    }
}
