using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GOB.SPF.ConecII.Entities.DTO;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositoryTiposSeccion : Repository<TiposSeccion>
    {
        public RepositoryTiposSeccion(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<TiposSeccion> Obtener(IPaging paging)
        {
            var result = new List<TiposSeccion>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.TiposSeccionObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@pagina",paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas",paging.Rows));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TiposSeccion tiposSeccion = new TiposSeccion();

                        tiposSeccion.Identificador = Convert.ToInt32(reader["IdTipoSeccion"]);
                        tiposSeccion.Nombre = reader["Nombre"].ToString();
                        tiposSeccion.Descripcion = reader["Descripcion"].ToString();
                        tiposSeccion.Orden = Convert.ToInt32(reader["Orden"]);
                        tiposSeccion.Numerado = Convert.ToBoolean(reader["Numerado"]);
                        tiposSeccion.Mensaje = reader["Mensaje"].ToString();
                        tiposSeccion.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        tiposSeccion.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tiposSeccion.Activo = Convert.ToBoolean(reader["Activo"]);
                        tiposSeccion.Etiqueta = Convert.ToBoolean(reader["Etiqueta"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(tiposSeccion);
                    }
                }
                return result;  // yield?
            }

        }

        public override TiposSeccion ObtenerPorId(long id)
        {
            int result = 0;
            TiposSeccion tiposSeccion = null;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.TiposSeccionObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter("@Identificador", id);

                cmd.Parameters.Add(parameter);

                tiposSeccion = new TiposSeccion();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tiposSeccion.Identificador = Convert.ToInt32(reader["IdTipoSeccion"]);
                        tiposSeccion.Nombre = reader["Nombre"].ToString();
                        tiposSeccion.Descripcion = reader["Descripcion"].ToString();
                        tiposSeccion.Orden = Convert.ToInt32(reader["Orden"]);
                        tiposSeccion.Numerado = Convert.ToBoolean(reader["Numerado"]);
                        tiposSeccion.Mensaje = reader["Mensaje"].ToString();
                        tiposSeccion.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        tiposSeccion.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tiposSeccion.Activo = Convert.ToBoolean(reader["Activo"]);
                        tiposSeccion.Etiqueta = Convert.ToBoolean(reader["Etiqueta"]);
                    }
                }
            }

            return tiposSeccion;
        }

        public override int CambiarEstatus(TiposSeccion tiposSeccion)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.TiposSeccionCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                tiposSeccion.Activo = tiposSeccion.Activo == true ? false : true;

                cmd.Parameters.Add(new SqlParameter() { Value = tiposSeccion.Identificador, ParameterName = "@IdTipoSeccion" });
                cmd.Parameters.Add(new SqlParameter() { Value = tiposSeccion.Activo, ParameterName = "@Activo" });


                result = (int)cmd.ExecuteScalar();

                

                return result;
            }
        }

        public override int Insertar(TiposSeccion tiposSeccion)
        {
            int result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.TiposSeccionInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(tiposSeccion.Nombre) ? string.Empty : tiposSeccion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", string.IsNullOrEmpty(tiposSeccion.Descripcion) ? string.Empty : tiposSeccion.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Orden", tiposSeccion.Orden.Equals(null) ? 0 : tiposSeccion.Orden));
                cmd.Parameters.Add(new SqlParameter("@Numerado", tiposSeccion.Numerado.Equals(null) ? (object)DBNull.Value : tiposSeccion.Numerado));
                cmd.Parameters.Add(new SqlParameter("@Mensaje", string.IsNullOrEmpty(tiposSeccion.Mensaje) ? string.Empty : tiposSeccion.Mensaje));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", tiposSeccion.FechaInicial == DateTime.MinValue || tiposSeccion.FechaInicial == null ? (object)DBNull.Value : tiposSeccion.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", tiposSeccion.FechaFinal == DateTime.MinValue || tiposSeccion.FechaFinal == null ? (object)DBNull.Value : tiposSeccion.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", tiposSeccion.Activo.Equals(null) ? (object)DBNull.Value : tiposSeccion.Activo));

                
                result = (int)cmd.ExecuteScalar();

                

                return result;
            }
        }

        public override int Actualizar(TiposSeccion tiposSeccion)
        {
            int result = 0;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.TiposSeccionActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@IdTipoSeccion", tiposSeccion.IdTipoSeccion.Equals(null) ? 0 : tiposSeccion.IdTipoSeccion));
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(tiposSeccion.Nombre) ? string.Empty : tiposSeccion.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", string.IsNullOrEmpty(tiposSeccion.Descripcion) ? string.Empty : tiposSeccion.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Orden", tiposSeccion.Orden.Equals(null) ? 0 : tiposSeccion.Orden));
                cmd.Parameters.Add(new SqlParameter("@Numerado", tiposSeccion.Numerado.Equals(null) ? (object)DBNull.Value : tiposSeccion.Numerado));
                cmd.Parameters.Add(new SqlParameter("@Mensaje", string.IsNullOrEmpty(tiposSeccion.Mensaje) ? string.Empty : tiposSeccion.Mensaje));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", tiposSeccion.FechaInicial == DateTime.MinValue || tiposSeccion.FechaInicial == null ? (object)DBNull.Value : tiposSeccion.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", tiposSeccion.FechaFinal == DateTime.MinValue || tiposSeccion.FechaFinal == null ? (object)DBNull.Value : tiposSeccion.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", tiposSeccion.Activo.Equals(null) ? (object)DBNull.Value : tiposSeccion.Activo));
                
                result = (int)cmd.ExecuteScalar();
                
                return result;
            }
        }

        public override IEnumerable<TiposSeccion> ObtenerPorCriterio(IPaging paging, TiposSeccion busqueda)
        {
            var result = new List<TiposSeccion>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.TiposSeccionObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Todos", paging.All));
                cmd.Parameters.Add(new SqlParameter("@pagina", paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas", paging.Rows));

                cmd.Parameters.Add(new SqlParameter("@IdTipoSeccion", busqueda.IdTipoSeccion.Equals(null) ? 0 : busqueda.IdTipoSeccion));
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(busqueda.Nombre) ? string.Empty : busqueda.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Descripcion", string.IsNullOrEmpty(busqueda.Descripcion) ? string.Empty : busqueda.Descripcion));
                cmd.Parameters.Add(new SqlParameter("@Orden", busqueda.Orden.Equals(null) ? 0 : busqueda.Orden));
                cmd.Parameters.Add(new SqlParameter("@Numerado", busqueda.Numerado.Equals(null) ? (object)DBNull.Value : busqueda.Numerado));
                cmd.Parameters.Add(new SqlParameter("@Mensaje", string.IsNullOrEmpty(busqueda.Mensaje) ? string.Empty : busqueda.Mensaje));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));



                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TiposSeccion tiposSeccion = new TiposSeccion();

                        tiposSeccion.Identificador = Convert.ToInt32(reader["IdTipoSeccion"]);
                        tiposSeccion.Nombre = reader["Nombre"].ToString();
                        tiposSeccion.Descripcion = reader["Descripcion"].ToString();
                        tiposSeccion.Orden = Convert.ToInt32(reader["Orden"]);
                        tiposSeccion.Numerado = Convert.ToBoolean(reader["Numerado"]);
                        tiposSeccion.Mensaje = reader["Mensaje"].ToString();
                        tiposSeccion.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        tiposSeccion.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tiposSeccion.Activo = Convert.ToBoolean(reader["Activo"]);
                        tiposSeccion.Etiqueta = Convert.ToBoolean(reader["Etiqueta"]);

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(tiposSeccion);
                    }
                }
                return result;  // yield?
            }
        }

        public override string ValidarRegistro(TiposSeccion tiposSeccion)
        {
            string result = "";

            //using (var cmd = UoW.CreateCommand())
            //{
            //    cmd.CommandText = Schemas.Plantilla.TiposSeccionValidarRegistro;
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

                cmd.CommandText = Schemas.Plantilla.TiposSeccionObtenerList;
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DropDto tiposSeccion = new DropDto();

                        tiposSeccion.Identificador = Convert.ToInt32(reader["IdTipoSeccion"]);
                        tiposSeccion.Valor = reader["Valor"].ToString();

                        result.Add(tiposSeccion);
                    }
                }
                return result;  // yield?
            }

        }
    }
}