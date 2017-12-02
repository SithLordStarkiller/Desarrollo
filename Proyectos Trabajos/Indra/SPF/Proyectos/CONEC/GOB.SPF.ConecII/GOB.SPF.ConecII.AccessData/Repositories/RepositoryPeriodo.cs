using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using GOB.SPF.ConecII.AccessData.Schemas;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    public class RepositoryPeriodo : IRepository<Periodo>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryPeriodo(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Periodo> Obtener(IPaging paging)
        {
            var result = new List<Periodo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.PeriodosObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Periodo periodos = new Periodo();

                        periodos.Identificador = Convert.ToInt32(reader["IdPeriodo"]);
                        periodos.Nombre = Convert.ToString(reader["Periodo"]);
                        periodos.Descripcion = Convert.ToString(reader["Descripcion"]);
                        periodos.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(periodos);
                    }
                }
                return result;  // yield?
            }

        }

        public Periodo ObtenerPorId(long id)
        {
            int result = 0;
            Periodo periodos = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.PeriodosObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                periodos = new Periodo();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        periodos.Identificador = Convert.ToInt32(reader["IdPeriodo"]);
                        periodos.Nombre = Convert.ToString(reader["Periodo"]);
                        periodos.Descripcion = Convert.ToString(reader["Descripcion"]);
                        periodos.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return periodos;
        }

        public int CambiarEstatus(Periodo entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.PeriodosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public int Insertar(Periodo entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.PeriodosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Periodo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Periodo entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.PeriodosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Periodo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<Periodo> ObtenerPorCriterio(IPaging paging, Periodo entity)
        {
            var result = new List<Periodo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.PeriodosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Periodo periodos = new Periodo();

                        periodos.Identificador = Convert.ToInt32(reader["IdPeriodo"]);
                        periodos.Nombre = Convert.ToString(reader["Periodo"]);
                        periodos.Descripcion = reader["Descripcion"].ToString();
                        periodos.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(periodos);
                    }
                }
                return result; 
            }
        }

        public string ValidarRegistro(Periodo entity)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.PeriodosValidarRegistro;
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
            }
            return result;
        }
    }
}
