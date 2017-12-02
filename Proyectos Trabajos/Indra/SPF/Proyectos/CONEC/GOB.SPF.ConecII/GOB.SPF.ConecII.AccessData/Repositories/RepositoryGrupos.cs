using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryGrupos : IRepository<Grupo>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryGrupos(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Grupo> Obtener(IPaging paging)
        {
            var result = new List<Grupo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Grupo grupos = new Grupo();

                        grupos.Identificador = Convert.ToInt32(reader["IdGrupo"]);
                        grupos.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        grupos.Division = reader["Division"].ToString();
                        grupos.Nombre = reader["Nombre"].ToString();
                        grupos.Descripcion = reader["Descripcion"].ToString();
                        grupos.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            grupos.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        grupos.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(grupos);
                    }
                }
                return result;  // yield?
            }

        }

        public Grupo ObtenerPorId(long id)
        {
            int result = 0;
            Grupo grupos = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                grupos = new Grupo();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        grupos.Identificador = Convert.ToInt32(reader["IdGrupo"]);
                        grupos.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        grupos.Division = reader["Division"].ToString();
                        grupos.Nombre = reader["Nombre"].ToString();
                        grupos.Descripcion = reader["Descripcion"].ToString();
                        grupos.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            grupos.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        grupos.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return grupos;
        }

        public int CambiarEstatus(Grupo grupos)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = grupos.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = grupos.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Grupo grupos)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = grupos.IdDivision, ParameterName = "@IdDivision" };
                parameters[1] = new SqlParameter() { Value = grupos.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = grupos.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(Grupo grupos)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = grupos.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = grupos.IdDivision, ParameterName = "@IdDivision" };
                parameters[2] = new SqlParameter() { Value = grupos.Nombre, ParameterName = "@Nombre" };
                parameters[3] = new SqlParameter() { Value = grupos.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Grupo> ObtenerPorCriterio(IPaging paging, Grupo entity)
        {
            var result = new List<Grupo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdGrupo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdDivision, ParameterName = "@IdDivision" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Grupo grupos = new Grupo();

                        grupos.Identificador = Convert.ToInt32(reader["IdGrupo"]);
                        grupos.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        grupos.Division = reader["Division"].ToString();
                        grupos.Nombre = reader["Nombre"].ToString();
                        grupos.Descripcion = reader["Descripcion"].ToString();
                        grupos.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            grupos.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        grupos.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(grupos);
                    }
                }
                return result;  // yield?
            }
        }

        public IEnumerable<Grupo> ObtenerPorIdDivision(int idDivision)
        {
            var result = new List<Grupo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposObtenerPorIdDivision;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = idDivision,
                    ParameterName = "@IdDivision"
                };

                cmd.Parameters.Add(parameter);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Grupo grupos = new Grupo();

                        grupos.Identificador = Convert.ToInt32(reader["IdGrupo"]);
                        grupos.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        grupos.Nombre = reader["Nombre"].ToString();
                        grupos.Descripcion = reader["Descripcion"].ToString();
                        grupos.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        grupos.FechaFinal = reader["FechaFinal"] != DBNull.Value ? Convert.ToDateTime(reader["FechaFinal"]): DateTime.Today;
                        grupos.Activo = Convert.ToBoolean(reader["Activo"]);
                        grupos.Division = reader["Division"].ToString(); 
                        result.Add(grupos);
                    }
                }
            }

            return result;
        }

        public string ValidarRegistro(Grupo grupo)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.GruposValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = grupo.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = grupo.IdDivision, ParameterName = "@IdDivision" };
                parameters[2] = new SqlParameter() { Value = grupo.Nombre, ParameterName = "@Nombre" };
                
                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

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
