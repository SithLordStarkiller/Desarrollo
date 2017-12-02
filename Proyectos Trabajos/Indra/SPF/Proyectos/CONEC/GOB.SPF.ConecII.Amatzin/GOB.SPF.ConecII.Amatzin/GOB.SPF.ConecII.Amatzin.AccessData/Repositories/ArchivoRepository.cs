using System.Data;
using System.Data.SqlClient;
using GOB.SPF.ConecII.Amatzin.Core.Configuration;
using GOB.SPF.ConecII.Amatzin.Core.Resources;
using Dapper;
using System.Linq;
using GOB.SPF.ConecII.Amatzin.Core.Interfaces;
using GOB.SPF.ConecII.Amatzin.Entities.Request;

namespace GOB.SPF.ConecII.Amatzin.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;

    public class ArchivoRepository : IRepository<Archivo>
    {

        public Result<Archivo> ObtenerPorId(object id)
        {

            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionStringConfig.GetConnection()))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@prmArchivoId", (long) id);
                    var result = connection.Query<Archivo, TableStorage, Archivo>("spConsultaArchivo", (Archivo, TableStorage) => { Archivo.Stream = TableStorage; return Archivo; }, commandType: CommandType.StoredProcedure, param: parameters, splitOn: "StreamId");


                    return new Result<Archivo>
                    {
                        Success = true,
                        List = result.ToList()
                    };
                }

            }
            catch (Exception ex)
            {
                return new Result<Archivo>
                {
                    Success = false,
                    Errors = new List<Error> { new Error { Code = ex.GetHashCode(),Message=ex.Message} }
                };
            }
        }

        public Result<Archivo> Actualizar(Archivo entity)
        {
            throw new NotImplementedException();
        }

        public Result<Archivo> Insertar(Archivo entity)
        {
            int res;
            string msgResult = string.Empty;
            long IdResult = 0;
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionStringConfig.GetConnection()))
                {
                    
                    var parameters = new DynamicParameters();
                    parameters.Add("prmArchivoId", entity.ArchivoId);
                    parameters.Add("prmNombre", entity.Nombre);
                    parameters.Add("prmNombreSistema", entity.NombreSistema);
                    parameters.Add("prmExtension", entity.Extension);
                    parameters.Add("prmFechaRegistro", entity.FechaRegistro);
                    parameters.Add("prmDirectorio", entity.Directorio);
                    parameters.Add("prmReferencia", entity.Referencia);
                    parameters.Add("prmStreamId", entity.Stream.StreamId);
                    parameters.Add("prmArchivoIdParent", entity.ArchivoIdParent);
                    parameters.Add("prmErrorMsg", msgResult, DbType.String,ParameterDirection.Output);
                    parameters.Add("prmArchivoIdOut", IdResult, DbType.Int64, ParameterDirection.Output);
                    res = connection.Execute(
                        sql: "spInsertaArchivo",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                    msgResult = parameters.Get<string>("prmErrorMsg");
                    IdResult = parameters.Get<long>("prmArchivoIdOut");
                }


                return new Result<Archivo>
                {
                    Success = (string.IsNullOrEmpty(msgResult) ? true:false),
                    Message = (string.IsNullOrEmpty(msgResult) ? Messages.InsertSuccess : msgResult),
                    Entity = new Archivo { ArchivoId = IdResult }
                };
            }
            catch (Exception ex)
            {

                return new Result<Archivo>
                {
                    Success = false,
                    Errors = new List<Error> { new Error { Code = ex.GetHashCode(), Message = ex.Message } }
                };
            }
        }

        public Result<IEnumerable<Archivo>> Obtener(Archivo archivo)
        {

            throw new NotImplementedException();


        }

        public Result<Archivo> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Result<Archivo> ConsultarHistoricoPorId(object id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionStringConfig.GetConnection()))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@prmArchivoId", (long)id);
                    var result = connection.Query<Archivo, TableStorage, Archivo>("spConsultaArchivoHistorico", (Archivo, TableStorage) => { Archivo.Stream = TableStorage; return Archivo; }, commandType: CommandType.StoredProcedure, param: parameters, splitOn: "StreamId");

                    return new Result<Archivo>
                    {
                        Success = true,
                        List = result.ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Archivo>
                {
                    Success = false,
                    Errors = new List<Error> { new Error { Code = ex.GetHashCode(), Message = ex.Message } }
                };
            }
        }
    }
}
