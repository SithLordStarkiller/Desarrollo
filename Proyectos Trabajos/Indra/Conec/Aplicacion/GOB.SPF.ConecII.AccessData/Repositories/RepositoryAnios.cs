namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryAnios : IRepository<Anio>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryAnios(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Anio> Obtener(Paging paging)
        {
            var result = new List<Anio>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.AniosObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Anio anios = new Anio();

                        anios.Identificador = Convert.ToInt32(reader["IdAnio"]);
                        anios.Nombre = Convert.ToString(reader["Nombre"]);
                        anios.Anios = Convert.ToInt32(reader["Anio"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(anios);
                    }
                }
                return result;  // yield?
            }

        }

        public Anio ObtenerPorId(long id)
        {
            int result = 0;
            Anio anios = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.AniosObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                anios = new Anio();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        anios.Identificador = Convert.ToInt32(reader["IdAnio"]);
                        anios.Nombre = Convert.ToString(reader["Nombre"]);
                        anios.Anios = Convert.ToInt32(reader["Anio"]);
                    }
                }
            }

            return anios;
        }

        public int Insertar(Anio anios)
        {
            /*
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.AniosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = anios.Anios, ParameterName = "@Anio" };
                parameters[1] = new SqlParameter() { Value = anios.Nombre, ParameterName = "@DesAnio" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
            */
            return 0;
        }

        public int Actualizar(Anio anios)
        {
            /*
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.AniosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = anios.Identificador, ParameterName = "@Identificador" };
                parameters[2] = new SqlParameter() { Value = anios.Nombre, ParameterName = "@DesAnio" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
            */
            return 0;
        }

        public IEnumerable<Anio> ObtenerPorCriterio(Paging paging, Anio entity)
        {
            var result = new List<Anio>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.AniosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Anio" });
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Anio anios = new Anio();

                        anios.Identificador = Convert.ToInt32(reader["IdAnio"]);
                        anios.Nombre = Convert.ToString(reader["Nombre"]);
                        anios.Anios = Convert.ToInt32(reader["Anio"]);

                        result.Add(anios);
                    }
                }
                return result;  // yield?
            }
        }

        public int CambiarEstatus(Anio entity)
        {
            throw new NotImplementedException();
        }
    }
}
