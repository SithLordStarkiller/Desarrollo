using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryMeses : IRepository<Meses>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryMeses(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Meses> Obtener(IPaging paging)
        {
            var result = new List<Meses>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MesesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Meses meses = new Meses();

                        meses.Identificador = Convert.ToInt32(reader["IdMes"]);
                        meses.Nombre = reader["Nombre"].ToString();
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(meses);
                    }
                }
                return result;  // yield?
            }

        }

        public Meses ObtenerPorId(long id)
        {
            int result = 0;
            Meses meses = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MesesObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                meses = new Meses();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        meses.Identificador = Convert.ToInt32(reader["IdMes"]);
                        meses.Nombre = reader["Nombre"].ToString();
                    }
                }
            }

            return meses;
        }


        public int Insertar(Meses meses)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MesesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter() { Value = meses.Nombre, ParameterName = "@Mes" };


                cmd.Parameters.Add(parameters[0]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(Meses meses)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MesesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = meses.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = meses.Nombre, ParameterName = "@Mes" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int CambiarEstatus(Meses entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meses> ObtenerPorCriterio(IPaging paging, Meses entity)
        {
            throw new NotImplementedException();
        }
    }
}
