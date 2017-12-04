using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositoryEtiquetas : Repository<Etiquetas>
    {

        public RepositoryEtiquetas(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<Etiquetas> Obtener(Paging paging)
        {
            var result = new List<Etiquetas>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.EtiquetasObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });
                cmd.Parameters.Add(new SqlParameter() { Value = 3, ParameterName = "@IdTipoDocumento" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Etiquetas etiquetas = new Etiquetas();

                        etiquetas.Identificador = Convert.ToInt32(reader["IdEtiqueta"]);
                        etiquetas.IdParteDocumento = Convert.ToInt32(reader["IdParteDocumento"]);
                        etiquetas.Etiqueta = reader["Etiqueta"].ToString();
                        etiquetas.Contenido = reader["Contenido"].ToString();
                        etiquetas.Negrita = Convert.ToBoolean(reader["Negrita"]);
                        etiquetas.Orden = Convert.ToInt32(reader["Orden"]);
                        etiquetas.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        etiquetas.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        etiquetas.Activo = Convert.ToBoolean(reader["Activo"]);
                        etiquetas.PartesDocumento = new PartesDocumento() { Identificador = Convert.ToInt32(reader["IdParteDocumentoEntityPartesDocumento"]), Descripcion = reader["DescripcionEntityPartesDocumento"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(etiquetas);
                    }
                }
                return result;  // yield?
            }

        }

        public override Etiquetas ObtenerPorId(long id)
        {
            int result = 0;
            Etiquetas etiquetas = null;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                etiquetas = new Etiquetas();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        etiquetas.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        etiquetas.Etiqueta = reader["Etiqueta"].ToString();
                        etiquetas.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return etiquetas;
        }

        public override int CambiarEstatus(Etiquetas etiquetas)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.Identificador, ParameterName = "@IdEtiqueta" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.Activo, ParameterName = "@Activo" });


                result = (int)cmd.ExecuteScalar();



                return result;
            }
        }

        public override int Insertar(Etiquetas etiquetas)
        {
            int result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[8];

                parameters[0] = new SqlParameter() { Value = etiquetas.IdParteDocumento, ParameterName = "@IdParteDocumento" };
                parameters[1] = new SqlParameter() { Value = etiquetas.Etiqueta, ParameterName = "@Etiqueta" };
                parameters[2] = new SqlParameter() { Value = etiquetas.Contenido, ParameterName = "@Contenido" };
                parameters[3] = new SqlParameter() { Value = etiquetas.Negrita, ParameterName = "@Negrita" };
                parameters[4] = new SqlParameter() { Value = etiquetas.Orden, ParameterName = "@Orden" };
                parameters[5] = new SqlParameter() { Value = etiquetas.Activo, ParameterName = "@Activo" };
                parameters[6] = new SqlParameter() { Value = etiquetas.FechaInicial == DateTime.MinValue || etiquetas.FechaInicial == null ? (object)DBNull.Value : etiquetas.FechaInicial, ParameterName = "@FechaInicial" };
                parameters[7] = new SqlParameter() { Value = etiquetas.FechaFinal == DateTime.MinValue || etiquetas.FechaFinal == null ? (object)DBNull.Value : etiquetas.FechaFinal, ParameterName = "@FechaFinal" };

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

        public override int Actualizar(Etiquetas etiquetas)
        {
            int result = 0;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.Identificador, ParameterName = "@IdEtiqueta" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.IdParteDocumento, ParameterName = "@IdParteDocumento" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.Etiqueta, ParameterName = "@Etiqueta" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.Contenido, ParameterName = "@Contenido" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.Negrita, ParameterName = "@Negrita" });
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.Orden, ParameterName = "@Orden" });
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", etiquetas.FechaInicial == DateTime.MinValue || etiquetas.FechaInicial == null ? (object)DBNull.Value : etiquetas.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", etiquetas.FechaFinal == DateTime.MinValue || etiquetas.FechaFinal == null ? (object)DBNull.Value : etiquetas.FechaFinal));
                cmd.Parameters.Add(new SqlParameter() { Value = etiquetas.Activo, ParameterName = "@Activo" });
                

                result = (int)cmd.ExecuteScalar();

                return result;
            }
        }

        public override IEnumerable<Etiquetas> ObtenerPorCriterio(Paging paging, Etiquetas busqueda)
        {
            var result = new List<Etiquetas>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.EtiquetasObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Todos", paging.All));
                cmd.Parameters.Add(new SqlParameter("@pagina", paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas", paging.Rows));

                cmd.Parameters.Add(new SqlParameter("@IdEtiqueta", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdParteDocumento", busqueda.IdParteDocumento.Equals(null) ? 0 : busqueda.IdParteDocumento));
                cmd.Parameters.Add(new SqlParameter("@Etiqueta", string.IsNullOrEmpty(busqueda.Etiqueta) ? string.Empty : busqueda.Etiqueta));
                cmd.Parameters.Add(new SqlParameter("@Contenido", string.IsNullOrEmpty(busqueda.Contenido) ? string.Empty : busqueda.Contenido));
                cmd.Parameters.Add(new SqlParameter("@Negrita", busqueda.Negrita.Equals(null) ? (object)DBNull.Value : busqueda.Negrita));
                cmd.Parameters.Add(new SqlParameter("@Orden", busqueda.Orden.Equals(null) ? 0 : busqueda.Orden));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));



                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Etiquetas etiquetas = new Etiquetas();

                        etiquetas.Identificador = Convert.ToInt32(reader["IdEtiqueta"]);
                        etiquetas.IdParteDocumento = Convert.ToInt32(reader["IdParteDocumento"]);
                        etiquetas.Etiqueta = reader["Etiqueta"].ToString();
                        etiquetas.Contenido = reader["Contenido"].ToString();
                        etiquetas.Negrita = Convert.ToBoolean(reader["Negrita"]);
                        etiquetas.Orden = Convert.ToInt32(reader["Orden"]);
                        etiquetas.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        etiquetas.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        etiquetas.Activo = Convert.ToBoolean(reader["Activo"]);
                        etiquetas.PartesDocumento = new PartesDocumento() { Identificador = Convert.ToInt32(reader["IdParteDocumentoEntityPartesDocumento"]), Descripcion = reader["DescripcionEntityPartesDocumento"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(etiquetas);
                    }
                }
                return result;  // yield?
            }
        }

        public override string ValidarRegistro(Etiquetas etiquetas)
        {
            string result = "";

            //using (var cmd = UoW.CreateCommand())
            //{
            //    cmd.CommandText = Schemas.Plantilla.EtiquetasValidarRegistro;
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

    }
}