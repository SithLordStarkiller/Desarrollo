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
    public partial class RepositoryInstituciones : Repository<Instituciones>
    {
        public RepositoryInstituciones(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<Instituciones> Obtener(IPaging paging)
        {
            var result = new List<Instituciones>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.InstitucionesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });
                cmd.Parameters.Add(new SqlParameter() { Value = 3, ParameterName = "@IdTipoDocumento" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Instituciones institucion = new Instituciones();

                        institucion.Identificador = Convert.ToInt32(reader["IdInstitucion"]);
                        institucion.IdParteDocumento = Convert.ToInt32(reader["IdParteDocumento"]);
                        institucion.Nombre = reader["Nombre"].ToString();
                        institucion.Negrita = Convert.ToBoolean(reader["Negrita"]);
                        institucion.Orden = Convert.ToInt32(reader["Orden"]);
                        institucion.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        institucion.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        institucion.Activo = Convert.ToBoolean(reader["Activo"]);
                        institucion.PartesDocumento = new PartesDocumento() { Identificador = Convert.ToInt32(reader["IdParteDocumentoEntityPartesDocumento"]), Descripcion = reader["DescripcionEntityPartesDocumento"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(institucion);
                    }
                }
                return result;  // yield?
            }

        }

        public override Instituciones ObtenerPorId(long id)
        {
            int result = 0;
            Instituciones institucion = null;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.InstitucionesObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                institucion = new Instituciones();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        institucion.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        institucion.Nombre = reader["Nombre"].ToString();
                        institucion.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return institucion;
        }

        public override int CambiarEstatus(Instituciones institucion)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.InstitucionesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = institucion.Identificador, ParameterName = "@IdInstitucion" });
                cmd.Parameters.Add(new SqlParameter() { Value = institucion.Activo, ParameterName = "@Activo" });


                result = (int)cmd.ExecuteScalar();

                

                return result;
            }
        }

        public override int Insertar(Instituciones institucion)
        {
            int result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.InstitucionesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = institucion.IdParteDocumento, ParameterName = "@IdParteDocumento" };
                parameters[1] = new SqlParameter() { Value = institucion.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = institucion.Negrita, ParameterName = "@Negrita" };
                parameters[3] = new SqlParameter() { Value = institucion.Orden, ParameterName = "@Orden" };
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", institucion.FechaInicial == DateTime.MinValue || institucion.FechaInicial == null ? (object)DBNull.Value : institucion.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", institucion.FechaFinal == DateTime.MinValue || institucion.FechaFinal == null ? (object)DBNull.Value : institucion.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", institucion.Activo.Equals(null) ? (object)DBNull.Value : institucion.Activo));

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);

                result = (int)cmd.ExecuteScalar();

                

                return result;
            }
        }

        public override int Actualizar(Instituciones institucion)
        {
            int result = 0;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.InstitucionesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.Add(new SqlParameter() { Value = institucion.IdParteDocumento, ParameterName = "@IdParteDocumento" });
                cmd.Parameters.Add(new SqlParameter() { Value = institucion.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = institucion.Negrita, ParameterName = "@Negrita" });
                cmd.Parameters.Add(new SqlParameter() { Value = institucion.Orden, ParameterName = "@Orden" });
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", institucion.FechaInicial == DateTime.MinValue || institucion.FechaInicial == null ? (object)DBNull.Value : institucion.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", institucion.FechaFinal == DateTime.MinValue || institucion.FechaFinal == null ? (object)DBNull.Value : institucion.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", institucion.Activo.Equals(null) ? (object)DBNull.Value : institucion.Activo));
                cmd.Parameters.Add(new SqlParameter() { Value = institucion.Identificador, ParameterName = "@IdInstitucion" });
                //cmd.Parameters.Add(new SqlParameter() { Value = institucion.FechaInicial == null ? (object)DBNull.Value : institucion.FechaInicial, ParameterName = "@FechaInicial" });
                //cmd.Parameters.Add(new SqlParameter() { Value = institucion.FechaFinal == null ? (object)DBNull.Value : institucion.FechaFinal, ParameterName = "@FechaFinal" });

                result = (int)cmd.ExecuteScalar();
                
                return result;
            }
        }

        public override IEnumerable<Instituciones> ObtenerPorCriterio(IPaging paging, Instituciones busqueda)
        {
            var result = new List<Instituciones>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.InstitucionesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Todos", paging.All));
                cmd.Parameters.Add(new SqlParameter("@pagina", paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas", paging.Rows));

                cmd.Parameters.Add(new SqlParameter("@IdInstitucion", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdParteDocumento", busqueda.IdParteDocumento.Equals(null) ? 0 : busqueda.IdParteDocumento));
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(busqueda.Nombre) ? string.Empty : busqueda.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Negrita", busqueda.Negrita.Equals(null) ? (object)DBNull.Value : busqueda.Negrita));
                cmd.Parameters.Add(new SqlParameter("@Orden", busqueda.Orden.Equals(null) ? 0 : busqueda.Orden));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Instituciones institucion = new Instituciones();

                        institucion.Identificador = Convert.ToInt32(reader["IdInstitucion"]);
                        institucion.IdParteDocumento = Convert.ToInt32(reader["IdParteDocumento"]);
                        institucion.Nombre = reader["Nombre"].ToString();
                        institucion.Negrita = Convert.ToBoolean(reader["Negrita"]);
                        institucion.Orden = Convert.ToInt32(reader["Orden"]);
                        institucion.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        institucion.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        institucion.Activo = Convert.ToBoolean(reader["Activo"]);
                        institucion.PartesDocumento = new PartesDocumento() { Identificador = Convert.ToInt32(reader["IdParteDocumentoEntityPartesDocumento"]), Descripcion = reader["DescripcionEntityPartesDocumento"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(institucion);
                    }
                }
                return result;  // yield?
            }
        }

        public override string ValidarRegistro(Instituciones institucion)
        {
            string result = "";

            //using (var cmd = UoW.CreateCommand())
            //{
            //    cmd.CommandText = Schemas.Plantilla.InstitucionesValidarRegistro;
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