using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositoryEtiquetasParrafo : Repository<EtiquetasParrafo>
    {
        public RepositoryEtiquetasParrafo(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<EtiquetasParrafo> Obtener(IPaging paging)
        {
            var result = new List<EtiquetasParrafo>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.EtiquetasParrafoObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });
                cmd.Parameters.Add(new SqlParameter() { Value = 3, ParameterName = "@IdTipoDocumento" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EtiquetasParrafo etiquetasParrafo = new EtiquetasParrafo();

                        etiquetasParrafo.Identificador = Convert.ToInt32(reader["IdEtiquetaParrafo"]);
                        etiquetasParrafo.IdParrafo = Convert.ToInt32(reader["IdParrafo"]);
                        etiquetasParrafo.Etiqueta = reader["Etiqueta"].ToString();
                        etiquetasParrafo.Contenido = reader["Contenido"].ToString();
                        etiquetasParrafo.Negrita = Convert.ToBoolean(reader["Negrita"]);
                        etiquetasParrafo.Orden = Convert.ToInt32(reader["Orden"]);
                        etiquetasParrafo.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        etiquetasParrafo.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        etiquetasParrafo.Activo = Convert.ToBoolean(reader["Activo"]);
                        etiquetasParrafo.Parrafos = new Entities.Plantilla.Parrafos() { Identificador = Convert.ToInt32(reader["IdParrafoEntityParrafos"]), Nombre = reader["nombreEntityParrafos"].ToString(), Texto = reader["TextoEntityParrafos"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(etiquetasParrafo);
                    }
                }
                return result;  // yield?
            }

        }

        public override EtiquetasParrafo ObtenerPorId(long id)
        {
            int result = 0;
            EtiquetasParrafo etiquetasParrafo = null;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasParrafoObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                etiquetasParrafo = new EtiquetasParrafo();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        etiquetasParrafo.Identificador = Convert.ToInt32(reader["IdEtiquetasParrafo"]);
                        etiquetasParrafo.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return etiquetasParrafo;
        }

        public override int CambiarEstatus(EtiquetasParrafo etiquetasParrafo)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasParrafoCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.Identificador, ParameterName = "@IdEtiquetaParrafo" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.Activo, ParameterName = "@Activo" });


                result = (int)cmd.ExecuteScalar();



                return result;
            }
        }

        public override int Insertar(EtiquetasParrafo etiquetasParrafo)
        {
            int result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasParrafoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[8];

                parameters[0] = new SqlParameter() { Value = etiquetasParrafo.IdParrafo, ParameterName = "@IdParrafo" };
                parameters[1] = new SqlParameter() { Value = etiquetasParrafo.Etiqueta, ParameterName = "@Etiqueta" };
                parameters[2] = new SqlParameter() { Value = etiquetasParrafo.Contenido, ParameterName = "@Contenido" };
                parameters[3] = new SqlParameter() { Value = etiquetasParrafo.Negrita, ParameterName = "@Negrita" };
                parameters[4] = new SqlParameter() { Value = etiquetasParrafo.Orden, ParameterName = "@Orden" };
                parameters[5] = new SqlParameter() { Value = etiquetasParrafo.Activo, ParameterName = "@Activo" };
                parameters[6] = new SqlParameter() { Value = etiquetasParrafo.FechaInicial == DateTime.MinValue || etiquetasParrafo.FechaInicial == null ? (object)DBNull.Value : etiquetasParrafo.FechaInicial, ParameterName = "@FechaInicial" };
                parameters[7] = new SqlParameter() { Value = etiquetasParrafo.FechaFinal == DateTime.MinValue || etiquetasParrafo.FechaInicial == null ? (object)DBNull.Value : etiquetasParrafo.FechaFinal, ParameterName = "@FechaFinal" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);
                cmd.Parameters.Add(parameters[5]);
                cmd.Parameters.Add(parameters[6]);
                cmd.Parameters.Add(parameters[7]);

                result = (int)cmd.ExecuteScalar();



                return result;
            }
        }

        public override int Actualizar(EtiquetasParrafo etiquetasParrafo)
        {
            int result = 0;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasParrafoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.IdParrafo, ParameterName = "@IdParrafo" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.Etiqueta, ParameterName = "@Etiqueta" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.Contenido, ParameterName = "@Contenido" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.Negrita, ParameterName = "@Negrita" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.Orden, ParameterName = "@Orden" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetasParrafo.Identificador, ParameterName = "@IdEtiquetaParrafo" });

                result = (int)cmd.ExecuteScalar();

                return result;
            }
        }

        public override IEnumerable<EtiquetasParrafo> ObtenerPorCriterio(IPaging paging, EtiquetasParrafo busqueda)
        {
            var result = new List<EtiquetasParrafo>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasParrafoObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Todos", paging.All));
                cmd.Parameters.Add(new SqlParameter("@pagina", paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas", paging.Rows));

                cmd.Parameters.Add(new SqlParameter("@IdEtiquetaParrafo", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdParrafo", busqueda.IdParrafo.Equals(null) ? 0 : busqueda.IdParrafo));
                cmd.Parameters.Add(new SqlParameter("@Etiqueta", string.IsNullOrEmpty(busqueda.Etiqueta) ? string.Empty : busqueda.Etiqueta));
                cmd.Parameters.Add(new SqlParameter("@Contenido", string.IsNullOrEmpty(busqueda.Contenido) ? string.Empty : busqueda.Contenido));
                cmd.Parameters.Add(new SqlParameter("@Negrita", busqueda.Negrita.Equals(null) ? (object)DBNull.Value : busqueda.Negrita));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EtiquetasParrafo etiquetasParrafo = new EtiquetasParrafo();

                        etiquetasParrafo.Identificador = Convert.ToInt32(reader["IdEtiquetaParrafo"]);
                        etiquetasParrafo.IdParrafo = Convert.ToInt32(reader["IdParrafo"]);
                        etiquetasParrafo.Etiqueta = reader["Etiqueta"].ToString();
                        etiquetasParrafo.Contenido = reader["Contenido"].ToString();
                        etiquetasParrafo.Negrita = Convert.ToBoolean(reader["Negrita"]);
                        etiquetasParrafo.Orden = Convert.ToInt32(reader["Orden"]);
                        etiquetasParrafo.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        etiquetasParrafo.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        etiquetasParrafo.Activo = Convert.ToBoolean(reader["Activo"]);
                        etiquetasParrafo.Parrafos = new Entities.Plantilla.Parrafos() { Identificador = Convert.ToInt32(reader["IdParrafoEntityParrafos"]), Nombre = reader["nombreEntityParrafos"].ToString(), Texto = reader["TextoEntityParrafos"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(etiquetasParrafo);
                    }
                }
                return result;  // yield?
            }
        }

        public override string ValidarRegistro(EtiquetasParrafo etiquetasParrafo)
        {
            string result = "";

            //using (var cmd = UoW.CreateCommand())
            //{
            //    cmd.CommandText = Schemas.Plantilla.EtiquetasParrafoValidarRegistro;
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

        public List<EtiquetasParrafo> ObtenerPorIdParteDocumento(IPaging paging, EtiquetasParrafo busqueda)
        {
            var result = new List<EtiquetasParrafo>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasParrafooObtenerPorParteDocumento;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Todos", paging.All));
                cmd.Parameters.Add(new SqlParameter("@pagina", paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas", paging.Rows));

                cmd.Parameters.Add(new SqlParameter("@IdEtiquetaParrafo", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdParteDocumento", busqueda.IdParteDocumento.Equals(null) ? 0 : busqueda.IdParteDocumento));
                cmd.Parameters.Add(new SqlParameter("@IdParrafo", busqueda.IdParrafo.Equals(null) ? 0 : busqueda.IdParrafo));
                cmd.Parameters.Add(new SqlParameter("@Etiqueta", string.IsNullOrEmpty(busqueda.Etiqueta) ? string.Empty : busqueda.Etiqueta));
                cmd.Parameters.Add(new SqlParameter("@Contenido", string.IsNullOrEmpty(busqueda.Contenido) ? string.Empty : busqueda.Contenido));
                cmd.Parameters.Add(new SqlParameter("@Negrita", busqueda.Negrita.Equals(null) ? (object)DBNull.Value : busqueda.Negrita));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EtiquetasParrafo etiquetasParrafo = new EtiquetasParrafo();

                        etiquetasParrafo.Identificador = Convert.ToInt32(reader["IdEtiquetaParrafo"]);
                        etiquetasParrafo.IdParrafo = Convert.ToInt32(reader["IdParrafo"]);
                        etiquetasParrafo.Etiqueta = reader["Etiqueta"].ToString();
                        etiquetasParrafo.Contenido = reader["Contenido"].ToString();
                        etiquetasParrafo.Negrita = Convert.ToBoolean(reader["Negrita"]);
                        etiquetasParrafo.Orden = Convert.ToInt32(reader["Orden"]);
                        etiquetasParrafo.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        etiquetasParrafo.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        etiquetasParrafo.Activo = Convert.ToBoolean(reader["Activo"]);
                        etiquetasParrafo.Parrafos = new Entities.Plantilla.Parrafos() { Identificador = Convert.ToInt32(reader["IdParrafoEntityParrafos"]), Nombre = reader["nombreEntityParrafos"].ToString(), Texto = reader["TextoEntityParrafos"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(etiquetasParrafo);
                    }
                }
                return result;  // yield?
            }
        }
    }
}