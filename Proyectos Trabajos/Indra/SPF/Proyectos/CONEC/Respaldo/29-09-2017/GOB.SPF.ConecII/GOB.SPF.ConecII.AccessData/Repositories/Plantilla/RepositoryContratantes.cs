using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositoryContratantes : Repository<Contratantes>
    {

        public RepositoryContratantes(IUnitOfWork uow) : base(uow)
        {
        }

        public override IEnumerable<Contratantes> Obtener(Paging paging)
        {
            var result = new List<Contratantes>();

            using (var cmd = UoW.CreateCommand())
            {

                cmd.CommandText = Schemas.Plantilla.ContratantesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contratantes contratantes = new Contratantes();

                        contratantes.Identificador = Convert.ToInt32(reader["IdContratante"]);
                        contratantes.IdParteDocumento = Convert.ToInt32(reader["IdParteDocumento"]);
                        contratantes.Nombre = reader["Nombre"].ToString();
                        contratantes.Cargo = reader["Cargo"].ToString();
                        contratantes.Domicilio = reader["Domicilio"].ToString();
                        contratantes.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        contratantes.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        contratantes.Activo = Convert.ToBoolean(reader["Activo"]);
                        contratantes.PartesDocumento = new PartesDocumento() { Identificador = Convert.ToInt32(reader["IdParteDocumentoEntityPartesDocumento"]), Descripcion = reader["DescripcionEntityPartesDocumento"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(contratantes);
                    }
                }
                return result;  // yield?
            }

        }

        public override Contratantes ObtenerPorId(long id)
        {
            int result = 0;
            Contratantes institucion = null;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ContratantesObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@IdContratante"
                };

                cmd.Parameters.Add(parameter);

                institucion = new Contratantes();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        institucion.Identificador = Convert.ToInt32(reader["IdContratante"]);
                        institucion.Nombre = reader["Nombre"].ToString();
                        institucion.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return institucion;
        }

        public override int CambiarEstatus(Contratantes contratantes)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ContratantesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = contratantes.Identificador, ParameterName = "@IdContratante" });
                cmd.Parameters.Add(new SqlParameter() { Value = contratantes.Activo, ParameterName = "@Activo" });


                result = (int)cmd.ExecuteScalar();



                return result;
            }
        }

        public override int Insertar(Contratantes contratantes)
        {
            int result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ContratantesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[7];

                parameters[0] = new SqlParameter() { Value = contratantes.IdParteDocumento, ParameterName = "@IdParteDocumento" };
                parameters[1] = new SqlParameter() { Value = contratantes.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = contratantes.Cargo, ParameterName = "@Cargo" };
                parameters[3] = new SqlParameter() { Value = contratantes.Domicilio, ParameterName = "@Domicilio" };
                parameters[4] = new SqlParameter() { Value = contratantes.Activo, ParameterName = "@Activo" };
                parameters[5] = new SqlParameter() { Value = contratantes.FechaInicial == DateTime.MinValue || contratantes.FechaInicial == null ? (object)DBNull.Value : contratantes.FechaInicial, ParameterName = "@FechaInicial" };
                parameters[6] = new SqlParameter() { Value = contratantes.FechaFinal == DateTime.MinValue || contratantes.FechaFinal == null ? (object)DBNull.Value : contratantes.FechaFinal, ParameterName = "@FechaFinal" };

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

        public override int Actualizar(Contratantes contratantes)
        {
            int result = 0;

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ContratantesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add(new SqlParameter() { Value = contratantes.IdParteDocumento, ParameterName = "@IdParteDocumento" });
                cmd.Parameters.Add(new SqlParameter() { Value = contratantes.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = contratantes.Cargo, ParameterName = "@Cargo" });
                cmd.Parameters.Add(new SqlParameter() { Value = contratantes.Domicilio, ParameterName = "@Domicilio" });
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", contratantes.FechaInicial == DateTime.MinValue || contratantes.FechaInicial == null ? (object)DBNull.Value : contratantes.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", contratantes.FechaFinal == DateTime.MinValue || contratantes.FechaFinal == null ? (object)DBNull.Value : contratantes.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", contratantes.Activo.Equals(null) ? (object)DBNull.Value : contratantes.Activo));
                cmd.Parameters.Add(new SqlParameter() { Value = contratantes.Identificador, ParameterName = "@IdContratante" });
                
                result = (int)cmd.ExecuteScalar();

                return result;
            }
        }

        public override IEnumerable<Contratantes> ObtenerPorCriterio(Paging paging, Contratantes busqueda)
        {
            var result = new List<Contratantes>();

            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Plantilla.ContratantesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Todos",paging.All));
                cmd.Parameters.Add(new SqlParameter("@pagina",paging.CurrentPage));
                cmd.Parameters.Add(new SqlParameter("@filas",paging.Rows));

                cmd.Parameters.Add(new SqlParameter("@IdContratante", busqueda.Identificador.Equals(null) ? 0 : busqueda.Identificador));
                cmd.Parameters.Add(new SqlParameter("@IdParteDocumento", busqueda.IdParteDocumento.Equals(null) ? 0 : busqueda.IdParteDocumento));
                cmd.Parameters.Add(new SqlParameter("@Nombre", string.IsNullOrEmpty(busqueda.Nombre) ? string.Empty : busqueda.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Cargo", string.IsNullOrEmpty(busqueda.Cargo) ? string.Empty : busqueda.Cargo));
                cmd.Parameters.Add(new SqlParameter("@Domicilio", string.IsNullOrEmpty(busqueda.Domicilio) ? string.Empty : busqueda.Domicilio));
                cmd.Parameters.Add(new SqlParameter("@FechaInicial", busqueda.FechaInicial == DateTime.MinValue || busqueda.FechaInicial == null ? (object)DBNull.Value : busqueda.FechaInicial));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", busqueda.FechaFinal == DateTime.MinValue || busqueda.FechaFinal == null ? (object)DBNull.Value : busqueda.FechaFinal));
                cmd.Parameters.Add(new SqlParameter("@Activo", busqueda.Activo.Equals(null) ? (object)DBNull.Value : busqueda.Activo));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contratantes contratantes = new Contratantes();

                        contratantes.Identificador = Convert.ToInt32(reader["IdContratante"]);
                        contratantes.IdParteDocumento = Convert.ToInt32(reader["IdParteDocumento"]);
                        contratantes.Nombre = reader["Nombre"].ToString();
                        contratantes.Cargo = reader["Cargo"].ToString();
                        contratantes.Domicilio = reader["Domicilio"].ToString();
                        contratantes.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        contratantes.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        contratantes.Activo = Convert.ToBoolean(reader["Activo"]);
                        contratantes.PartesDocumento = new PartesDocumento() { Identificador = Convert.ToInt32(reader["IdParteDocumentoEntityPartesDocumento"]), Descripcion  = reader["DescripcionEntityPartesDocumento"].ToString() };

                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(contratantes);
                    }
                }
                return result;  // yield?
            }
        }

        public override string ValidarRegistro(Contratantes contratantes)
        {
            string result = "";

            //using (var cmd = UoW.CreateCommand())
            //{
            //    cmd.CommandText = Schemas.Plantilla.ContratantesValidarRegistro;
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