namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryActividades : IRepository<Actividad>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryActividades(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Actividad> Obtener(Paging paging)
        {
            var result = new List<Actividad>();

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.ActividadesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Actividad actividades = new Actividad();

                        actividades.Identificador = Convert.ToInt32(reader["IdActividad"]);
                        actividades.IdFase = Convert.ToInt32(reader["IdFase"]);
                        actividades.Nombre = Convert.ToString(reader["Nombre"]);
                        actividades.Descripcion = Convert.ToString(reader["Descripcion"]);
                        actividades.SePuedeAplicarPlazo = Convert.ToBoolean(reader["SePuedeAplicarPlazo"]);
                        actividades.Activo = Convert.ToBoolean(reader["Activo"]);
                        actividades.Validacion = Convert.ToBoolean(reader["Validacion"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(actividades);
                    }
                }
                return result;  // yield?
            }

        }

        public Actividad ObtenerPorId(long id)
        {
            int result = 0;
            Actividad actividades = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ActividadesObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                actividades = new Actividad();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        actividades.Identificador = Convert.ToInt32(reader["IdActividad"]);
                        actividades.IdFase = Convert.ToInt32(reader["IdFase"]);
                        actividades.Nombre = Convert.ToString(reader["Nombre"]);
                        actividades.Descripcion = Convert.ToString(reader["Descripcion"]);
                        actividades.SePuedeAplicarPlazo = Convert.ToBoolean(reader["SePuedeAplicarPlazo"]);
                        actividades.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return actividades;
        }

        public int CambiarEstatus(Actividad entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.ActividadesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Actividad entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ActividadesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@IdFase" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@SePuedeAplicarPlazo" });


                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Actividad entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ActividadesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Actividad> ObtenerPorCriterio(Paging paging, Actividad entity)
        {
            var result = new List<Actividad>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ActividadesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdTipoPago, ParameterName = "@IdTipoPago" });
                //cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                //cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                //cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Actividad actividades = new Actividad();

                        actividades.Identificador = Convert.ToInt32(reader["IdActividad"]);
                        actividades.IdFase = Convert.ToInt32(reader["IdFase"]);
                        actividades.Nombre = Convert.ToString(reader["Nombre"]);
                        actividades.Descripcion = Convert.ToString(reader["Descripcion"]);
                        actividades.SePuedeAplicarPlazo = Convert.ToBoolean(reader["SePuedeAplicarPlazo"]);
                        actividades.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            actividades.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        actividades.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(actividades);
                    }
                }
                return result;  // yield?
            }
        }
    }
}
