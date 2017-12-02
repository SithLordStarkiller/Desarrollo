using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    using System.ComponentModel;

    public class RepositorySolicitudes : IRepository<Solicitudes>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositorySolicitudes(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Solicitudes> Obtener(IPaging paging)
        {
            var result = new List<Solicitudes>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudesObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Solicitudes solicitudes = new Solicitudes();

                        solicitudes.Identificador = Convert.ToInt32(reader["IdSolicitud"]);
                        solicitudes.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                        solicitudes.RazonSocial = Convert.ToString(reader["RazonSocial"]);
                        solicitudes.NombreCorto = Convert.ToString(reader["NombreCorto"]);
                        solicitudes.RFC = Convert.ToString(reader["RFC"]);
                        solicitudes.IdRegimenFiscal = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        solicitudes.RegimenFiscal.Identificador = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        solicitudes.RegimenFiscal.Nombre = reader["RegimenFiscal"].ToString();
                        solicitudes.NombreRegimenFiscal = reader["RegimenFiscal"].ToString();
                        solicitudes.IdSector = Convert.ToInt32(reader["IdSector"]);
                        solicitudes.Sector.Identificador = Convert.ToInt32(reader["IdSector"]);
                        solicitudes.Sector.Descripcion = reader["Sector"].ToString();
                        solicitudes.NombreSector = reader["Sector"].ToString();
                        solicitudes.TipoSolicitud.Identificador = Convert.ToInt32(reader["IdTipoSolicitud"]);
                        solicitudes.IdTipoSolicitud = Convert.ToInt32(reader["IdTipoSolicitud"]);
                        solicitudes.TipoSolicitud.Nombre = Convert.ToString(reader["TipoSolicitud"]);
                        solicitudes.NombreTipoSolicitud = reader["TipoSolicitud"].ToString();
                        solicitudes.DocumentoSoporte = Convert.ToInt32(reader["DocumentoSoporte"]);
                        solicitudes.Folio = reader["Folio"].ToString();
                        solicitudes.Minuta = Convert.ToInt32(reader["Minuta"]);
                        solicitudes.Cancelado = Convert.ToBoolean(reader["Cancelado"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(solicitudes);
                    }
                }
                return result;  // yield?
            }

        }

        public IEnumerable<Solicitudes> ObtenerPorId(Solicitudes entity)
        {
            var result = new List<Solicitudes>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = entity.Identificador,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Solicitudes solicitudes = new Solicitudes();

                        solicitudes.Identificador = Convert.ToInt32(reader["IdSolicitud"]);
                        solicitudes.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                        solicitudes.RazonSocial = Convert.ToString(reader["RazonSocial"]);
                        solicitudes.NombreCorto = Convert.ToString(reader["NombreCorto"]);
                        solicitudes.RFC = Convert.ToString(reader["RFC"]);
                        solicitudes.IdRegimenFiscal = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        solicitudes.RegimenFiscal.Identificador = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        solicitudes.RegimenFiscal.Nombre = reader["RegimenFiscal"].ToString();
                        solicitudes.NombreRegimenFiscal = reader["RegimenFiscal"].ToString();
                        solicitudes.IdSector = Convert.ToInt32(reader["IdSector"]);
                        solicitudes.Sector.Identificador = Convert.ToInt32(reader["IdSector"]);
                        solicitudes.Sector.Descripcion = reader["Sector"].ToString();
                        solicitudes.NombreSector = reader["Sector"].ToString();
                        solicitudes.TipoSolicitud.Identificador = Convert.ToInt32(reader["IdTipoSolicitud"]);
                        solicitudes.IdTipoSolicitud = Convert.ToInt32(reader["IdTipoSolicitud"]);
                        solicitudes.TipoSolicitud.Nombre = Convert.ToString(reader["TipoSolicitud"]);
                        solicitudes.NombreTipoSolicitud = reader["TipoSolicitud"].ToString();
                        solicitudes.DocumentoSoporte = Convert.ToInt32(reader["DocumentoSoporte"]);
                        solicitudes.Folio = reader["Folio"].ToString();
                        solicitudes.Minuta = Convert.ToInt32(reader["Minuta"]);
                        solicitudes.Cancelado = Convert.ToBoolean(reader["Cancelado"]);

                        result.Add(solicitudes);

                    }
                }
            }

            return result;
        }

        public int CambiarEstatus(Solicitudes solicitudes)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                //cmd.CommandText = Solicitud.solicitudesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = solicitudes.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = solicitudes.Cancelado, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(DataTable dataTable)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                //cmd.CommandText = Solicitud.solicitudesInsertarEstados;
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter { ParameterName = "@Tabla", SqlDbType = SqlDbType.Structured, TypeName = Solicitud.SolicitudesTipoTablaUsuario, Value = dataTable });
                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Solicitudes solicitudes)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                //cmd.CommandText = Solicitud.solicitudesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter() { Value = solicitudes.Identificador, ParameterName = "@Identificador" });
                //cmd.Parameters.Add(new SqlParameter() { Value = solicitudes.Clasificacion.Identificador, ParameterName = "@IdClasificadorFactor" });
                //cmd.Parameters.Add(new SqlParameter() { Value = solicitudes.Descripcion, ParameterName = "@Descripcion" });
                //cmd.Parameters.Add(new SqlParameter() { Value = solicitudes.Estado.Identificador, ParameterName = "@IdEstado" });
                //cmd.Parameters.Add(new SqlParameter() { Value = solicitudes.Factor.Identificador, ParameterName = "@IdFactor" });

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<Solicitudes> ObtenerPorCriterio(/*IPaging paging, */Solicitudes entity)
        {
            var result = new List<Solicitudes>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                if (entity.RazonSocial == null)
                {
                    entity.RazonSocial = "";
                }
                if (entity.NombreCorto == null)
                {
                    entity.NombreCorto = "";
                }
                if (entity.RFC == null)
                {
                    entity.RFC = "";
                }
                cmd.CommandText = Schemas.Solicitud.SolicitudesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.RazonSocial, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.NombreCorto, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.RFC, ParameterName = "@RFC" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdRegimenFiscal, ParameterName = "@IdRegimenFiscal" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdSector, ParameterName = "@IdSector" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Solicitudes solicitudes = new Solicitudes();

                        solicitudes.Identificador = Convert.ToInt32(reader["IdSolicitud"]);
                        solicitudes.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                        solicitudes.RazonSocial = Convert.ToString(reader["RazonSocial"]);
                        solicitudes.NombreCorto = Convert.ToString(reader["NombreCorto"]);
                        solicitudes.RFC = Convert.ToString(reader["RFC"]);
                        solicitudes.IdRegimenFiscal = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        solicitudes.RegimenFiscal.Identificador = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        solicitudes.RegimenFiscal.Nombre = reader["RegimenFiscal"].ToString();
                        solicitudes.NombreRegimenFiscal = reader["RegimenFiscal"].ToString();
                        solicitudes.IdSector = Convert.ToInt32(reader["IdSector"]);
                        solicitudes.Sector.Identificador = Convert.ToInt32(reader["IdSector"]);
                        solicitudes.Sector.Descripcion = reader["Sector"].ToString();
                        solicitudes.NombreSector = reader["Sector"].ToString();
                        solicitudes.TipoSolicitud.Identificador = Convert.ToInt32(reader["IdTipoSolicitud"]);
                        solicitudes.IdTipoSolicitud = Convert.ToInt32(reader["IdTipoSolicitud"]);
                        solicitudes.TipoSolicitud.Nombre = Convert.ToString(reader["TipoSolicitud"]);
                        solicitudes.NombreTipoSolicitud = reader["TipoSolicitud"].ToString();
                        solicitudes.DocumentoSoporte = Convert.ToInt32(reader["DocumentoSoporte"]);
                        solicitudes.Folio = reader["Folio"].ToString();
                        solicitudes.Minuta = Convert.ToInt32(reader["Minuta"]);
                        solicitudes.Cancelado = Convert.ToBoolean(reader["Cancelado"]);

                        result.Add(solicitudes);

                    }
                }
            }

            return result;
        }

        public int Insertar(Solicitudes entity)
        {
            throw new NotImplementedException();
        }

        public string ValidarRegistro(Solicitudes entity)
        {
            string resultado = "";
            try
            {
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    //cmd.CommandText = Solicitud.solicitudesValidarRegistros;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter();
                    //parameters[0].ParameterName = Solicitud.ParametroDataTable;
                    parameters[0].SqlDbType = SqlDbType.Structured;
                    //parameters[0].TypeName = Solicitud.SolicitudesTipoTablaUsuario;
                    parameters[0].Value = entity;
                    cmd.Parameters.Add(parameters[0]);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultado = reader["Resultado"].ToString();
                        }
                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public Solicitudes ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitudes> ObtenerPorCriterio(IPaging paging, Solicitudes entity)
        {
            throw new NotImplementedException();
        }
    }
}
