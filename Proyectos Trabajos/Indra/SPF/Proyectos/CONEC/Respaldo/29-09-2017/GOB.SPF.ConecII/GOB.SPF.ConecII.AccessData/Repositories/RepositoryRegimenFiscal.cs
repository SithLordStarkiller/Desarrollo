namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using GOB.SPF.ConecII.AccessData.Schemas;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    public class RepositoryRegimenFiscal : IRepository<RegimenFiscal>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryRegimenFiscal(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<RegimenFiscal> Obtener(Paging paging)
        {
            var result = new List<RegimenFiscal>();

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.RegimenFiscalObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                //cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RegimenFiscal regimenFiscal = new RegimenFiscal();

                        regimenFiscal.Identificador = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        regimenFiscal.Nombre = Convert.ToString(reader["Nombre"]);
                        //regimenFiscal.Descripcion = Convert.ToString(reader["Descripcion"]);
                        //regimenFiscal.Activo = Convert.ToBoolean(reader["Activo"]);
                        //pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(regimenFiscal);
                    }
                }
                return result;  // yield?
            }

        }

        public RegimenFiscal ObtenerPorId(long id)
        {
            int result = 0;
            RegimenFiscal regimenFiscal = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.RegimenFiscalObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                regimenFiscal = new RegimenFiscal();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        regimenFiscal.Identificador = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        regimenFiscal.Nombre = Convert.ToString(reader["Nombre"]);
                        regimenFiscal.Descripcion = Convert.ToString(reader["Descripcion"]);
                        regimenFiscal.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        regimenFiscal.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        regimenFiscal.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return regimenFiscal;
        }

        public int CambiarEstatus(RegimenFiscal entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.RegimenFiscalCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(RegimenFiscal entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.RegimenFiscalInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(RegimenFiscal entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.RegimenFiscalActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<RegimenFiscal> ObtenerPorCriterio(Paging paging, RegimenFiscal entity)
        {
            var result = new List<RegimenFiscal>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.RegimenFiscalObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RegimenFiscal regimenFiscal = new RegimenFiscal();

                        regimenFiscal.Identificador = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        regimenFiscal.Nombre = Convert.ToString(reader["Nombre"]);
                        regimenFiscal.Descripcion = reader["Descripcion"].ToString();
                        regimenFiscal.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            regimenFiscal.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        regimenFiscal.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(regimenFiscal);
                    }
                }
                return result;  // yield?
            }
        }


    }
}
