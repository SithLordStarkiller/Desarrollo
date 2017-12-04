using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using GOB.SPF.ConecII.AccessData.Schemas;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    public class RepositoryFase : IRepository<Fase>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryFase(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Fase> Obtener(IPaging paging)
        {
            var result = new List<Fase>();

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Configuracion.FasesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Fase fases = new Fase();

                        fases.Identificador = Convert.ToInt32(reader["IdFase"]);
                        fases.Nombre = Convert.ToString(reader["Nombre"]);
                        fases.Descripcion = Convert.ToString(reader["Descripcion"]);
                        //Fases.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        //Fases.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        fases.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(fases);
                    }
                }
                return result;  // yield?
            }

        }

        public Fase ObtenerPorId(long id)
        {
            int result = 0;
            Fase fases = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.FasesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                fases = new Fase();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fases.Identificador = Convert.ToInt32(reader["IdFase"]);
                        fases.Nombre = Convert.ToString(reader["Nombre"]);
                        fases.Descripcion = Convert.ToString(reader["Descripcion"]);
                        fases.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        fases.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        fases.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return fases;
        }

        public int CambiarEstatus(Fase entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Configuracion.FasesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Fase entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.FasesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Fase entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.FasesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Fase> ObtenerPorCriterio(IPaging paging, Fase entity)
        {
            var result = new List<Fase>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.FasesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Fase fases = new Fase();

                        fases.Identificador = Convert.ToInt32(reader["IdFase"]);
                        fases.Nombre = Convert.ToString(reader["Nombre"]);
                        fases.Descripcion = reader["Descripcion"].ToString();
                        fases.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            fases.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        fases.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(fases);
                    }
                }
                return result;  // yield?
            }
        }


    }
}
