using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GOB.SPF.ConecII.Entities.DTO;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositoryParrafos : Repository<Parrafos>
    {
        public RepositoryParrafos(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<Parrafos> Obtener(Paging paging)
        {
            var result = new List<Parrafos>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.ParrafosObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });
                cmd.Parameters.Add(new SqlParameter() { Value = 3, ParameterName = "@IdTipoDocumento" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Parrafos parrafos = new Parrafos();

                        parrafos.Identificador = Convert.ToInt32(reader["IdParrafo"]);
                        parrafos.IdParteDocumento = Convert.ToInt32(reader["IdParteDocumento"]);
                        parrafos.IdTipoSeccion = Convert.ToInt32(reader["IdTipoSeccion"]);
                        parrafos.Nombre = reader["Nombre"].ToString();
                        parrafos.Texto = reader["Texto"].ToString();
                        parrafos.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        parrafos.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        parrafos.Activo = Convert.ToBoolean(reader["Activo"]);
                        parrafos.PartesDocumento = new PartesDocumento() { Identificador = Convert.ToInt32(reader["IdParteDocumentoEntityPartesDocumento"]), Descripcion = reader["DescripcionEntityPartesDocumento"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(parrafos);
                    }
                }
                return result;  // yield?
            }

        }

        public override Parrafos ObtenerPorId(long id)
        {
            int result = 0;
            Parrafos parrafos = null;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ParrafosObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                parrafos = new Parrafos();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parrafos.Identificador = Convert.ToInt32(reader["IdTipoDocumento"]);
                        parrafos.Nombre = reader["Nombre"].ToString();
                        parrafos.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return parrafos;
        }

        public override int CambiarEstatus(Parrafos parrafos)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ParrafosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = parrafos.Identificador, ParameterName = "@IdParrafo" });
                cmd.Parameters.Add(new SqlParameter() { Value = parrafos.Activo, ParameterName = "@Activo" });


                result = (int)cmd.ExecuteScalar();



                return result;
            }
        }

        public override int Insertar(Parrafos parrafos)
        {
            int result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ParrafosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[7];

                parameters[0] = new SqlParameter() { Value = parrafos.IdParteDocumento, ParameterName = "@IdParteDocumento" };                
                parameters[1] = new SqlParameter() { Value = parrafos.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = parrafos.Texto, ParameterName = "@Texto" };
                parameters[3] = new SqlParameter() { Value = parrafos.Activo, ParameterName = "@Activo" };
                parameters[4] = new SqlParameter() { Value = parrafos.FechaInicial == DateTime.MinValue || parrafos.FechaInicial == null ? (object)DBNull.Value : parrafos.FechaInicial, ParameterName = "@FechaInicial" };
                parameters[5] = new SqlParameter() { Value = parrafos.FechaFinal == DateTime.MinValue || parrafos.FechaFinal == null ? (object)DBNull.Value : parrafos.FechaFinal, ParameterName = "@FechaFinal" };
                parameters[6] = new SqlParameter() { Value = parrafos.IdTipoSeccion, ParameterName = "@IdTipoSeccion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);
                cmd.Parameters.Add(parameters[5]);
                cmd.Parameters.Add(parameters[6]);

                result = (int)cmd.ExecuteScalar();



                return result;
            }
        }

        public override int Actualizar(Parrafos parrafos)
        {
            int result = 0;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ParrafosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter() { Value = parrafos.IdParteDocumento, ParameterName = "@IdParteDocumento" });
                cmd.Parameters.Add(new SqlParameter() { Value = parrafos.IdTipoSeccion, ParameterName = "@IdTipoSeccion" });
                cmd.Parameters.Add(new SqlParameter() { Value = parrafos.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = parrafos.Texto, ParameterName = "@Texto" });
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", parrafos.FechaInicial == DateTime.MinValue || parrafos.FechaInicial == null ? (object)DBNull.Value : parrafos.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", parrafos.FechaFinal == DateTime.MinValue || parrafos.FechaFinal == null ? (object)DBNull.Value : parrafos.FechaFinal));
                cmd.Parameters.Add(new SqlParameter() { Value = parrafos.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = parrafos.Identificador, ParameterName = "@IdParrafo" });
                

                result = (int)cmd.ExecuteScalar();

                return result;
            }
        }

        public override IEnumerable<Parrafos> ObtenerPorCriterio(Paging paging, Parrafos busqueda)
        {
            var result = new List<Parrafos>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ParrafosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Todos", paging.All));
                cmd.Parameters.Add(new SqlParameter("@pagina", paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas", paging.Rows));

                cmd.Parameters.Add(new SqlParameter("@IdParrafo", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdParteDocumento", busqueda.IdParteDocumento.Equals(null) ? 0 : busqueda.IdParteDocumento));
                cmd.Parameters.Add(new SqlParameter("@IdTipoSeccion", busqueda.IdTipoSeccion.Equals(null) ? 0 : busqueda.IdTipoSeccion));
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(busqueda.Nombre) ? string.Empty : busqueda.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Texto", string.IsNullOrEmpty(busqueda.Texto) ? string.Empty : busqueda.Texto));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Parrafos parrafos = new Parrafos();

                        parrafos.Identificador = Convert.ToInt32(reader["IdParrafo"]);
                        parrafos.IdParteDocumento = Convert.ToInt32(reader["IdParteDocumento"]);
                        parrafos.IdTipoSeccion = Convert.ToInt32(reader["IdTipoSeccion"]);
                        parrafos.Nombre = reader["Nombre"].ToString();
                        parrafos.Texto = reader["Texto"].ToString();
                        parrafos.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        parrafos.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        parrafos.Activo = Convert.ToBoolean(reader["Activo"]);
                        parrafos.PartesDocumento = new PartesDocumento() { Identificador = Convert.ToInt32(reader["IdParteDocumentoEntityPartesDocumento"]), Descripcion = reader["DescripcionEntityPartesDocumento"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(parrafos);
                    }
                }
                return result;  // yield?
            }
        }

        public override string ValidarRegistro(Parrafos parrafos)
        {
            string result = "";

            //using (var cmd = UoW.CreateCommand())
            //{
            //    cmd.CommandText = Schemas.Plantilla.ParrafosValidarRegistro;
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


        public override IEnumerable<DropDto> ObtenerDropDownList()
        {
            var result = new List<DropDto>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.ParrafosObtenerList;
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DropDto tiposSeccion = new DropDto();

                        tiposSeccion.Identificador = Convert.ToInt32(reader["IdParrafo"]);
                        tiposSeccion.Valor = reader["Valor"].ToString();

                        result.Add(tiposSeccion);
                    }
                }
                return result;  // yield?
            }

        }

        public override IEnumerable<DropDto> ObtenerDropDownListCriterio(Parrafos busqueda)
        {
            var result = new List<DropDto>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.ParrafosObtenerListCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@IdParrafo", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdParteDocumento", busqueda.IdParteDocumento.Equals(null) ? 0 : busqueda.IdParteDocumento));
                cmd.Parameters.Add(new SqlParameter("@IdTipoSeccion", busqueda.IdTipoSeccion.Equals(null) ? 0 : busqueda.IdTipoSeccion));
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(busqueda.Nombre) ? string.Empty : busqueda.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Texto", string.IsNullOrEmpty(busqueda.Texto) ? string.Empty : busqueda.Texto));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DropDto tiposSeccion = new DropDto();

                        tiposSeccion.Identificador = Convert.ToInt32(reader["IdParrafo"]);
                        tiposSeccion.Valor = reader["Valor"].ToString();

                        result.Add(tiposSeccion);
                    }
                }
                return result;  // yield?
            }

        }
    }
}