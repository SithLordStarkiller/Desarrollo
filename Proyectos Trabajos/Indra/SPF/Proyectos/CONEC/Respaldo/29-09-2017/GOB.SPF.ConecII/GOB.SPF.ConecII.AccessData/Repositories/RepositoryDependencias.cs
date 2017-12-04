namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryDependencias : IRepository<Dependencia>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryDependencias(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Dependencia> Obtener(Paging paging)
        {
            var result = new List<Dependencia>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DependenciasObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dependencia dependencias = new Dependencia();

                        dependencias.Identificador = Convert.ToInt32(reader["IdDependencia"]);
                        dependencias.Nombre = Convert.ToString(reader["Nombre"]);
                        dependencias.Descripcion = Convert.ToString(reader["Descripcion"]);
                        dependencias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            dependencias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        dependencias.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(dependencias);
                    }
                }
                return result;  // yield?
            }

        }

        public Dependencia ObtenerPorId(long id)
        {
            int result = 0;
            Dependencia dependencias = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DependenciasObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                dependencias = new Dependencia();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dependencias.Identificador = Convert.ToInt32(reader["IdDependencia"]);
                        dependencias.Nombre = Convert.ToString(reader["Nombre"]);
                        dependencias.Descripcion = Convert.ToString(reader["Descripcion"]);
                        dependencias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            dependencias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        dependencias.Activo = Convert.ToBoolean(reader["Activo"]);  


                    }
                }
            }

            return dependencias;
        }

        public int CambiarEstatus(Dependencia dependencias)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DependenciasCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = dependencias.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = dependencias.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Dependencia dependencias)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DependenciasInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter() { Value = dependencias.Nombre, ParameterName = "@Nombre" };
                parameters[1] = new SqlParameter() { Value = dependencias.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(Dependencia dependencias)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DependenciasActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = dependencias.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = dependencias.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = dependencias.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Dependencia> ObtenerPorCriterio(Paging paging, Dependencia entity)
        {
            var result = new List<Dependencia>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DependenciasObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdDependencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dependencia dependencias = new Dependencia();


                        dependencias.Identificador = Convert.ToInt32(reader["IdDependencia"]);
                        dependencias.Nombre = reader["Nombre"].ToString();
                        dependencias.Descripcion = reader["Descripcion"].ToString();
                        dependencias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            dependencias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        dependencias.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(dependencias);

                    }
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro(Dependencia entity)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DependenciasValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" };            

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Resultado"].ToString();
                    }
                    reader.Close();
                }
                //_unitOfWork.SaveChanges();

            }

            return result;
        }


    }
}
