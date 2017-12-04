namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryDivision : IRepository<Division>
    {

        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryDivision(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Division> Obtener(Paging paging)
        {
            var result = new List<Division>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DivisionesObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = paging.All, ParameterName = "@todos" });
                cmd.Parameters.Add(new SqlParameter {Value = paging.CurrentPage, ParameterName = "@pagina"  });
                cmd.Parameters.Add(new SqlParameter { Value = paging.Rows, ParameterName = "@filas" });
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Division Division = new Division();

                        Division.Identificador = Convert.ToInt32(reader["IdDivision"]);
                        Division.NombreDivision = reader["Nombre"].ToString();
                        Division.DescDivision = reader["Descripcion"].ToString();
                        Division.Activo = Convert.ToBoolean(reader["Activo"]);

                        if (reader["Paginas"]!=null)
                            pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(Division);
                    }
                }
                return result;  // yield?
            }

        }

        public IEnumerable<Division> ObtenerListado()
        {
            var result = new List<Division>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DivisionesObtenerListado;
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Division Division = new Division();

                        Division.Identificador = Convert.ToInt32(reader["IdDivision"]);
                        Division.NombreDivision = reader["Nombre"].ToString();
                        Division.DescDivision = reader["Descripcion"].ToString();
                        Division.Activo = Convert.ToBoolean(reader["Activo"]);
                        result.Add(Division);
                    }
                }
                return result;  // yield?
            }

        }

        public Division ObtenerPorId(long id)
        {
            int result = 0;
            Division Division = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DivisionesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                Division = new Division();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Division.Identificador = Convert.ToInt32(reader["IdDivision"]);
                        Division.NombreDivision = reader["Nombre"].ToString();
                        Division.DescDivision = reader["Descripcion"].ToString();
                        Division.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return Division;
        }

        public int CambiarEstatus(Division Division)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DivisionesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = Division.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Division.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Division Division)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DivisionInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = Division.NombreDivision, ParameterName = "@NombreDivision" });
                cmd.Parameters.Add(new SqlParameter() { Value = Division.DescDivision, ParameterName = "@DescDivision" });
                
                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Division Division)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DivisionActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = Division.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Division.NombreDivision, ParameterName = "@NombreDivision" };
                parameters[2] = new SqlParameter() { Value = Division.DescDivision, ParameterName = "@DescDivision" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Division> ObtenerPorCriterio(Paging paging, Division entity)
        {
            var result = new List<Division>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DivisionesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdDivision" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Division Division = new Division();

                        Division.Identificador = Convert.ToInt32(reader["IdDivision"]);
                        Division.NombreDivision = reader["Nombre"].ToString();
                        Division.DescDivision = reader["Descripcion"].ToString();
                        Division.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if(!(reader["FechaFinal"] == DBNull.Value))
                            Division.FechaFinal =  Convert.ToDateTime(reader["FechaFinal"]);
                        Division.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(Division);
                    }
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro(Division division)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.DivisionValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = division.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = division.NombreDivision, ParameterName = "@NombreDivision" };
                parameters[2] = new SqlParameter() { Value = division.DescDivision, ParameterName = "@DescDivision" };

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
