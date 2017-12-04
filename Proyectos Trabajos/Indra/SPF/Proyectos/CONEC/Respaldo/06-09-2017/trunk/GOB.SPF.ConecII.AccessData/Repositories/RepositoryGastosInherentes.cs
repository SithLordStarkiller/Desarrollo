namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryGastosInherentes : IRepository<GastoInherente>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryGastosInherentes(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<GastoInherente> Obtener(Paging paging)
        {
            var result = new List<GastoInherente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Factores.GastosInerentesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GastoInherente gastosInherentes = new GastoInherente();

                        gastosInherentes.Identificador = Convert.ToInt32(reader["IdGastoInherente"]);
                        gastosInherentes.Nombre = Convert.ToString(reader["Nombre"]);
                        gastosInherentes.Descripcion = Convert.ToString(reader["Descripcion"]);
                        gastosInherentes.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(gastosInherentes);
                    }
                }
                return result;  // yield?
            }

        }

        public GastoInherente ObtenerPorId(long id)
        {
            int result = 0;
            GastoInherente gastosInherentes = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.GastosInerentesObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                gastosInherentes = new GastoInherente();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gastosInherentes.Identificador = Convert.ToInt32(reader["IdGastoInherente"]);
                        gastosInherentes.Nombre = Convert.ToString(reader["Nombre"]);
                        gastosInherentes.Descripcion = Convert.ToString(reader["Descripcion"]);
                        gastosInherentes.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        gastosInherentes.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        gastosInherentes.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return gastosInherentes;
        }

        public int CambiarEstatus(GastoInherente entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Factores.GastosInerentesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(GastoInherente entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.GastosInerentesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(GastoInherente entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.GastosInerentesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<GastoInherente> ObtenerPorCriterio(Paging paging, GastoInherente entity)
        {
            var result = new List<GastoInherente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.GastosInerentesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GastoInherente gastosInherentes = new GastoInherente();

                        gastosInherentes.Identificador = Convert.ToInt32(reader["IdGastoInherente"]);
                        gastosInherentes.Nombre = Convert.ToString(reader["Nombre"]);
                        gastosInherentes.Descripcion = reader["Descripcion"].ToString();
                        gastosInherentes.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            gastosInherentes.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        gastosInherentes.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(gastosInherentes);
                    }
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro(GastoInherente entity)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.GastosInherenteValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Resultado"].ToString();
                    }
                    reader.Close();
                }

                //_unitOfWork.SaveChanges();

                return result;
            }
        }
    }
}
