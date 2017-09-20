using GOB.SPF.ConecII.Entities;

using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositoryPartesDocumento : Repository<PartesDocumento>
    {
        //public IEnumerable<PartesDocumento> ObtenerPorIdTipoDocumento(long idTipoDocumento)
        //{
            
        //}

        public RepositoryPartesDocumento(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<PartesDocumento> Obtener(Paging paging)
        {
            var result = new List<PartesDocumento>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.PartesDocumentoObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });
                cmd.Parameters.Add(new SqlParameter() { Value = 3, ParameterName = "@IdTipoDocumento" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PartesDocumento partesDocumento = new PartesDocumento();

                        partesDocumento.Identificador = Convert.ToInt32(reader["IdInstitucion"]);
                        partesDocumento.IdTipoDocumento = Convert.ToInt32(reader["IdTipoDocumento"]);
                        partesDocumento.Nombre = reader["Nombre"].ToString();
                        
                        partesDocumento.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        partesDocumento.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        partesDocumento.Activo = Convert.ToBoolean(reader["Activo"]);
                        
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(partesDocumento);
                    }
                }
                return result;  // yield?
            }

        }

        public override PartesDocumento ObtenerPorId(long id)
        {
            int result = 0;
            PartesDocumento partesDocumento = null;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.PartesDocumentoObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                partesDocumento = new PartesDocumento();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        partesDocumento.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        partesDocumento.Nombre = reader["Nombre"].ToString();
                        partesDocumento.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return partesDocumento;
        }

        public override int CambiarEstatus(PartesDocumento partesDocumento)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.PartesDocumentoCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = partesDocumento.Identificador, ParameterName = "@IdInstitucion" });
                cmd.Parameters.Add(new SqlParameter() { Value = partesDocumento.Activo, ParameterName = "@Activo" });


                result = (int)cmd.ExecuteScalar();



                return result;
            }
        }

        public override int Insertar(PartesDocumento partesDocumento)
        {
            int result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.PartesDocumentoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter[] parameters = new SqlParameter[7];

                //parameters[0] = new SqlParameter() { Value = partesDocumento.IdTipoDocumento, ParameterName = "@IdTipoDocumento" };
                //parameters[1] = new SqlParameter() { Value = partesDocumento.Nombre, ParameterName = "@Nombre" };                
                //parameters[4] = new SqlParameter() { Value = partesDocumento.Activo, ParameterName = "@Activo" };
                //parameters[5] = new SqlParameter() { Value = partesDocumento.FechaInicial == DateTime.MinValue ? (object)DBNull.Value : partesDocumento.FechaInicial, ParameterName = "@FechaInicial" };
                //parameters[6] = new SqlParameter() { Value = partesDocumento.FechaFinal == DateTime.MinValue ? (object)DBNull.Value : partesDocumento.FechaFinal, ParameterName = "@FechaFinal" };

                //cmd.Parameters.Add(parameters[0]);
                //cmd.Parameters.Add(parameters[1]);
                //cmd.Parameters.Add(parameters[2]);
                //cmd.Parameters.Add(parameters[3]);
                //cmd.Parameters.Add(parameters[4]);
                //cmd.Parameters.Add(parameters[5]);
                //cmd.Parameters.Add(parameters[6]);

                //cmd.Parameters.Add(new SqlParameter("@IdParteDocumento", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdTipoDocumento", partesDocumento.IdTipoDocumento.Equals(null) ? 0 : partesDocumento.IdTipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", string.IsNullOrEmpty(partesDocumento.Descripcion) ? string.Empty : partesDocumento.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@RutaLogo", string.IsNullOrEmpty(partesDocumento.RutaLogo) ? string.Empty : partesDocumento.RutaLogo));
                cmd.Parameters.Add(new SqlParameter("@Folio", string.IsNullOrEmpty(partesDocumento.Folio) ? string.Empty : partesDocumento.Folio));
                cmd.Parameters.Add(new SqlParameter("@Asunto", string.IsNullOrEmpty(partesDocumento.Asunto) ? string.Empty : partesDocumento.Asunto));
                cmd.Parameters.Add(new SqlParameter("@LugarFecha", string.IsNullOrEmpty(partesDocumento.LugarFecha) ? string.Empty : partesDocumento.LugarFecha));
                cmd.Parameters.Add(new SqlParameter("@Paginado", partesDocumento.Paginado.Equals(null) ? (object)DBNull.Value : partesDocumento.Paginado));
                cmd.Parameters.Add(new SqlParameter("@Ccp", string.IsNullOrEmpty(partesDocumento.Ccp) ? string.Empty : partesDocumento.Ccp));
                cmd.Parameters.Add(new SqlParameter("@Puesto", string.IsNullOrEmpty(partesDocumento.Puesto) ? string.Empty : partesDocumento.Puesto));
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(partesDocumento.Nombre) ? string.Empty : partesDocumento.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Direccion", string.IsNullOrEmpty(partesDocumento.Direccion) ? string.Empty : partesDocumento.Direccion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", partesDocumento.FechaInicial == DateTime.MinValue || partesDocumento.FechaInicial == null ? (object)DBNull.Value : partesDocumento.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", partesDocumento.FechaFinal == DateTime.MinValue || partesDocumento.FechaFinal == null ? (object)DBNull.Value : partesDocumento.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", partesDocumento.Activo.Equals(null) ? (object)DBNull.Value : partesDocumento.Activo));
                //cmd.Transaction;

                result = (int)cmd.ExecuteScalar();



                return result;
            }
        }

        public override int Actualizar(PartesDocumento partesDocumento)
        {
            int result = 0;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.PartesDocumentoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter() { Value = partesDocumento.IdTipoDocumento, ParameterName = "@IdTipoDocumento" });
                cmd.Parameters.Add(new SqlParameter() { Value = partesDocumento.Nombre, ParameterName = "@Nombre" });
                
                cmd.Parameters.Add(new SqlParameter() { Value = partesDocumento.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = partesDocumento.Identificador, ParameterName = "@IdInstitucion" });
                
                result = (int)cmd.ExecuteScalar();

                return result;
            }
        }

        public override IEnumerable<PartesDocumento> ObtenerPorCriterio(Paging paging, PartesDocumento busqueda)
        {
            var result = new List<PartesDocumento>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.PartesDocumentoObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Todos", paging.All));
                cmd.Parameters.Add(new SqlParameter("@pagina", paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas", paging.Rows));

                cmd.Parameters.Add(new SqlParameter("@IdParteDocumento", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdTipoDocumento", busqueda.IdTipoDocumento.Equals(null) ? 0 : busqueda.IdTipoDocumento));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", string.IsNullOrEmpty(busqueda.Descripcion) ? string.Empty : busqueda.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@RutaLogo", string.IsNullOrEmpty(busqueda.RutaLogo) ? string.Empty : busqueda.RutaLogo));
                cmd.Parameters.Add(new SqlParameter("@Folio", string.IsNullOrEmpty(busqueda.Folio) ? string.Empty : busqueda.Folio));
                cmd.Parameters.Add(new SqlParameter("@Asunto", string.IsNullOrEmpty(busqueda.Asunto) ? string.Empty : busqueda.Asunto));
                cmd.Parameters.Add(new SqlParameter("@LugarFecha", string.IsNullOrEmpty(busqueda.LugarFecha) ? string.Empty : busqueda.LugarFecha));
                cmd.Parameters.Add(new SqlParameter("@Paginado", busqueda.Paginado.Equals(null) ? (object)DBNull.Value : busqueda.Paginado));
                cmd.Parameters.Add(new SqlParameter("@Ccp", string.IsNullOrEmpty(busqueda.Ccp) ? string.Empty : busqueda.Ccp));
                cmd.Parameters.Add(new SqlParameter("@Puesto", string.IsNullOrEmpty(busqueda.Puesto) ? string.Empty : busqueda.Puesto));
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(busqueda.Nombre) ? string.Empty : busqueda.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Direccion", string.IsNullOrEmpty(busqueda.Direccion) ? string.Empty : busqueda.Direccion));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PartesDocumento partesDocumento = new PartesDocumento();

                        partesDocumento.Identificador = Convert.ToInt32(reader["IdInstitucion"]);
                        partesDocumento.IdTipoDocumento = Convert.ToInt32(reader["IdTipoDocumento"]);
                        partesDocumento.Nombre = reader["Nombre"].ToString();

                        partesDocumento.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        partesDocumento.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        partesDocumento.Activo = Convert.ToBoolean(reader["Activo"]);

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(partesDocumento);
                    }
                }
                return result;  // yield?
            }
        }

        public override string ValidarRegistro(PartesDocumento partesDocumento)
        {
            string result = "";

            //using (var cmd = UoW.CreateCommand())
            //{
            //    cmd.CommandText = Schemas.Plantilla.PartesDocumentoValidarRegistro;
            //    cmd.CommandType = CommandType.StoredProcedure;


            //    SqlParameter[] parameters = new SqlParameter[3];

            //    parameters[0] = new SqlParameter() { Value = tipoDocumento.Identificador, ParameterName = "@Identificador" };
            //    parameters[1] = new SqlParameter() { Value = tipoDocumento.Nombre, ParameterName = "@Nombre" };

            //    cmd.Parameters.Add(parameters[0]);
            //    cmd.Parameters.Add(parameters[1]);

            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            result = reader["Resultado"].ToString();
            //        }
            //    }


            return result;
        }

        public PartesDocumento ObtenerPorIdTipoDocumento(PartesDocumento busqueda)
        {
            PartesDocumento partesDocumento = new PartesDocumento();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.PartesDocumentoObtenerPorIdTipoDocumento;
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@IdTipoDocumento", busqueda.IdTipoDocumento.Equals(null) ? 0 : busqueda.IdTipoDocumento));

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        
                        partesDocumento.Identificador = Convert.ToInt32(reader["IdParteDocumento"]);
                        partesDocumento.IdTipoDocumento = Convert.ToInt32(reader["IdTipoDocumento"]);
                        partesDocumento.Descripcion = reader["Descripcion"].ToString();
                        partesDocumento.RutaLogo = reader["RutaLogo"].ToString();
                        partesDocumento.Folio = reader["Folio"].ToString();
                        partesDocumento.Asunto = reader["Asunto"].ToString();
                        partesDocumento.LugarFecha = reader["LugarFecha"].ToString();
                        partesDocumento.Paginado = Convert.ToBoolean(reader["Paginado"]);
                        partesDocumento.Ccp = reader["Ccp"].ToString();
                        partesDocumento.Puesto = reader["Puesto"].ToString();
                        partesDocumento.Nombre = reader["Nombre"].ToString();
                        partesDocumento.Direccion = reader["Direccion"].ToString();                        
                        partesDocumento.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        partesDocumento.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        partesDocumento.Activo = Convert.ToBoolean(reader["Activo"]);
                        
                    }
                }
                return partesDocumento;  // yield?
            }
        }
    }
}