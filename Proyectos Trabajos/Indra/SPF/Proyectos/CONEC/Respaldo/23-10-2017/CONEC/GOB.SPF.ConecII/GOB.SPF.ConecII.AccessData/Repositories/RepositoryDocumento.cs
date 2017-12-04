using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Amatzin;
using GOB.SPF.ConecII.Entities.DTO;
using Solicitud = GOB.SPF.ConecII.AccessData.Schemas.Solicitud;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryDocumento : Repository<Documento>
    {
        #region Constructor
        public RepositoryDocumento(IUnitOfWork uow) : base(uow)
        {
        }
        #endregion

        /// <summary>
        /// Método que guarda un listado de documentos en la tabla Solicitud.ClientesDocumentos
        /// </summary>
        /// <param name="listDocumentosCliente">Lista de documentos</param>
        /// <returns>Número de registros</returns>
        public int Insertar(DocumentosClienteDto listDocumentosCliente)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.DocumentoClienteInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Structured, ParameterName = "@Documentos", Value = listDocumentosCliente });
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        /// <summary>
        /// Método que actualiza un listado de documentos en la tabla Solicitud.ClientesDocumentos
        /// </summary>
        /// <param name="listDocumentosClienteDto">Lista de documentos</param>
        /// <returns>Registros afectados</returns>
        public int Actualizar(DocumentosClienteDto listDocumentosClienteDto)
        {
            var result = 0;
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.DocumentoClienteActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Structured, ParameterName = "@Documentos", Value = listDocumentosClienteDto });
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        /// <summary>
        /// Obtiene un listado de documentos procedientes al identificador del cliente
        /// </summary>
        /// <param name="identificador">Identificador del cliente</param>
        /// <returns>listado tabla  documentos</returns>
        public List<ClientesDocumentos> ObtenerPorId(long identificador)
        {
            var result = 0;
            List<ClientesDocumentos> list = new List<ClientesDocumentos>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.DocumentosClienteObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdCliente", Value = identificador });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var element = new ClientesDocumentos
                        {
                            Nombre = reader["Nombre"].ToString(),
                            Identificador = reader["IdClienteDocumento"] as int? ?? 0,
                            Cliente = new Cliente { Identificador = reader["IdCliente"] as int? ?? 0 },
                            TipoDocumento = new TipoDocumento { Identificador = reader["IdTipoDocumento"] as int? ?? 0 },
                            ArchivoId = reader["DocumentoSoporte"] as int? ?? 0,
                            FechaRegistro = reader["FechaRegistro"] as DateTime? ?? DateTime.Now,
                            Activo = reader["Activo"] as bool? ?? false

                        };
                        list.Add(element);
                    }

                }
            }
            return list;
        }
    }
}
