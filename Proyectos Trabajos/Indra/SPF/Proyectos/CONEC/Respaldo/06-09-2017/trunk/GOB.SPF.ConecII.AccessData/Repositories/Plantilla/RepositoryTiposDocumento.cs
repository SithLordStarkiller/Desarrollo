using GOB.SPF.ConecII.Entities.Plantilla;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.AccessData.Schemas;
using System.Data;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositoryTiposDocumento : Repository<TiposDocumento>, IRepository<TiposDocumento>
    {
        public RepositoryTiposDocumento(IUnitOfWork uow):base(uow)
        {
        }

        public override IEnumerable<TiposDocumento> Obtener(Paging paging)
        {
            var result = new List<TiposDocumento>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TiposDocumento tipoDocumento = new TiposDocumento();

                        tipoDocumento.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        tipoDocumento.Nombre = reader["Nombre"].ToString();
                        tipoDocumento.Descripcion = reader["Descripcion"].ToString();
                        //tipoDocumento.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        //tipoDocumento.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tipoDocumento.Activo = Convert.ToBoolean(reader["Activo"]);
                        
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(tipoDocumento);
                    }
                }
                return result;  // yield?
            }

        }

        public override TiposDocumento ObtenerPorId(long id)
        {
            int result = 0;
            TiposDocumento tipoDocumento = null;

            using (var cmd = UoW.CreateCommand())
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

                tipoDocumento = new TiposDocumento();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipoDocumento.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        tipoDocumento.Nombre = reader["Nombre"].ToString();
                        tipoDocumento.Descripcion = reader["Descripcion"].ToString();
                        tipoDocumento.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return tipoDocumento;
        }

        public override int CambiarEstatus(TiposDocumento tipoDocumento)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter() { Value = tipoDocumento.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = tipoDocumento.Activo, ParameterName = "@Activo" });

                
                result = cmd.ExecuteNonQuery();

                UoW.SaveChanges();

                return result;
            }
        }

        public override int Insertar(TiposDocumento tipoDocumento)
        {
            int result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" };
                parameters[1] = new SqlParameter() { Value = tipoDocumento.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                UoW.SaveChanges();

                return result;
            }
        }

        public override int Actualizar(TiposDocumento tipoDocumento)
        {
            int result = 0;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter() { Value = tipoDocumento.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = tipoDocumento.Descripcion, ParameterName = "@Descripcion" };
               

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                UoW.SaveChanges();

                return result;
            }
        }

        public override IEnumerable<TiposDocumento> ObtenerPorCriterio(Paging paging, TiposDocumento tipoDocumento)
        {
            var result = new List<TiposDocumento>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = tipoDocumento.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });
                                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TiposDocumento tipoDocumento1 = new TiposDocumento();

                        tipoDocumento1.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        tipoDocumento1.Nombre = reader["Nombre"].ToString();
                        tipoDocumento1.Descripcion = reader["Descripcion"].ToString();
                        tipoDocumento1.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(tipoDocumento1);
                    }
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro(TiposDocumento tipoDocumento)
        {
            string result = "";

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoDocumentoValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = tipoDocumento.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Resultado"].ToString();
                    }
                }
                UoW.SaveChanges();

                return result;
            }

        }
    }
}