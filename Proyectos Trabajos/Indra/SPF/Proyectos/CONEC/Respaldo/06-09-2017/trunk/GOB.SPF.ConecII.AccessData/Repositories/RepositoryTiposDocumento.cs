namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryTiposDocumento : IRepository<TipoDocumento>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryTiposDocumento(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<TipoDocumento> Obtener(Paging paging)
        {
            var result = new List<TipoDocumento>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoDocumento tipoDocumento = new TipoDocumento();

                        tipoDocumento.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        tipoDocumento.Nombre = reader["Nombre"].ToString();
                        tipoDocumento.Descripcion = reader["Descripcion"].ToString();
                        //tipoDocumento.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        //tipoDocumento.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tipoDocumento.Activo = Convert.ToBoolean(reader["Activo"]);
                        tipoDocumento.IdActividad = Convert.ToInt32(reader["IdActividad"]);
                        tipoDocumento.Actividad = reader["Actividad"].ToString();
                        tipoDocumento.Confidencial = Convert.ToBoolean(reader["Confidencial"]);
                        pages = Convert.ToInt32(reader["Paginas"]); 

                        result.Add(tipoDocumento);
                    }
                }
                return result;  // yield?
            }

        }

        public TipoDocumento ObtenerPorId(long id)
        {
            int result = 0;
            TipoDocumento tipoDocumento = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                tipoDocumento = new TipoDocumento();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipoDocumento.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        tipoDocumento.Nombre = reader["Nombre"].ToString();
                        tipoDocumento.Descripcion = reader["Descripcion"].ToString();
                        tipoDocumento.Activo = Convert.ToBoolean(reader["Activo"]);
                        tipoDocumento.IdActividad = Convert.ToInt32(reader["IdActividad"]);
                        tipoDocumento.Actividad = reader["Actividad"].ToString();
                        tipoDocumento.Confidencial = Convert.ToBoolean(reader["Confidencial"]);

                    }
                }
            }

            return tipoDocumento;
        }

        public int CambiarEstatus(TipoDocumento tipoDocumento)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter[] parameters = new SqlParameter[2];

                //tipoDocumento.Activo = tipoDocumento.Activo.ToString() == true.ToString() ? false : true;

                cmd.Parameters.Add(new SqlParameter() { Value = tipoDocumento.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = tipoDocumento.Activo, ParameterName = "@Activo" });

                //parameters[0] = new SqlParameter() { Value = tipoDocumento.Identificador, ParameterName = "@Identificador" };
                //parameters[1] = new SqlParameter() { Value = tipoDocumento.Activo, ParameterName = "@Activo" };

                //cmd.Parameters.Add(parameters[0]);
                //cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(TipoDocumento tipoDocumento)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" };
                parameters[1] = new SqlParameter() { Value = tipoDocumento.Descripcion, ParameterName = "@Descripcion" };
                //parameters[2] = new SqlParameter() { Value = tipoDocumento.FechaInicial, ParameterName = "@FechaInicial" };
                //parameters[3] = new SqlParameter() { Value = tipoDocumento.FechaFinal, ParameterName = "@FechaFinal" };
                //parameters[2] = new SqlParameter() { Value = tipoDocumento.Activo, ParameterName = "@Activo" };
                parameters[2] = new SqlParameter() { Value = tipoDocumento.IdActividad, ParameterName = "@IdActividad" };
                parameters[3] = new SqlParameter() { Value = tipoDocumento.Confidencial, ParameterName = "@Confidencial" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                //cmd.Parameters.Add(parameters[4]);
                //cmd.Parameters.Add(parameters[5]);
                //cmd.Parameters.Add(parameters[6]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(TipoDocumento tipoDocumento)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter() { Value = tipoDocumento.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = tipoDocumento.Descripcion, ParameterName = "@Descripcion" };
                parameters[3] = new SqlParameter() { Value = tipoDocumento.IdActividad, ParameterName = "@IdActividad" };
                parameters[4] = new SqlParameter() { Value = tipoDocumento.Confidencial, ParameterName = "@Confidencial" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<TipoDocumento> ObtenerPorCriterio(Paging paging, TipoDocumento tipoDocumento)
        {
            var result = new List<TipoDocumento>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = tipoDocumento.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                //cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdTipoDocumento" });
                //cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                //cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                //cmd.Parameters.Add(new SqlParameter() { Value = entity.IdActividad, ParameterName = "@IdActividad" });
                //cmd.Parameters.Add(new SqlParameter() { Value = entity.Confidencial, ParameterName = "@Confidencial" });


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoDocumento tipoDocumento1 = new TipoDocumento();

                        tipoDocumento1.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        tipoDocumento1.Nombre = reader["Nombre"].ToString();
                        tipoDocumento1.Descripcion = reader["Descripcion"].ToString();
                        tipoDocumento1.Activo = Convert.ToBoolean(reader["Activo"]);
                        tipoDocumento1.IdActividad = Convert.ToInt32(reader["IdActividad"]);
                        tipoDocumento1.Actividad = reader["Actividad"].ToString();
                        tipoDocumento1.Confidencial = Convert.ToBoolean(reader["Confidencial"]);

                        result.Add(tipoDocumento1);
                    }
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro(TipoDocumento tipoDocumento)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = tipoDocumento.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = tipoDocumento.IdActividad, ParameterName = "@IdActividad" };

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
                //_unitOfWork.SaveChanges();

                return result;
            }

        }
            //public IEnumerable<TipoDocumento> ObtenerPorNombre(Paging paging, TipoDocumento entity)
            //{
            //    var result = new List<TipoDocumento>();

            //    using (var cmd = _unitOfWork.CreateCommand())
            //    {
            //        cmd.CommandText = Catalogos.TipoDocumentoObtenerPorNombre;
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
            //        cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
            //        cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

            //        //cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdTipoDocumento" });
            //        //cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
            //        //cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
            //        //cmd.Parameters.Add(new SqlParameter() { Value = entity.IdActividad, ParameterName = "@IdActividad" });
            //        //cmd.Parameters.Add(new SqlParameter() { Value = entity.Confidencial, ParameterName = "@Confidencial" });


            //        using (var reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                TipoDocumento tipoDocumento = new TipoDocumento();

            //                tipoDocumento.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
            //                tipoDocumento.Nombre = reader["Nombre"].ToString();
            //                tipoDocumento.Descripcion = reader["Descripcion"].ToString();
            //                tipoDocumento.Activo = Convert.ToBoolean(reader["Activo"]);
            //                tipoDocumento.IdActividad = Convert.ToInt32(reader["IdActividad"]);
            //                tipoDocumento.Actividad = reader["Actividad"].ToString();
            //                tipoDocumento.Confidencial = Convert.ToBoolean(reader["Confidencial"]);

            //                result.Add(tipoDocumento);
            //            }
            //        }
            //        return result;  // yield?
            //    }
            //}
        }
}
