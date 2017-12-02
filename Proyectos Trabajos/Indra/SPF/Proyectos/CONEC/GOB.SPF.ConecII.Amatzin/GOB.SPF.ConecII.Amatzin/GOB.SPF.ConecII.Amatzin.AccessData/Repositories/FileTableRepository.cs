using System.Data;
using System.Data.SqlClient;
using GOB.SPF.ConecII.Amatzin.Core.Configuration;
using GOB.SPF.ConecII.Amatzin.Core.Resources;
using Dapper;
using System.Linq;
using GOB.SPF.ConecII.Amatzin.Core.Interfaces;
using GOB.SPF.ConecII.Amatzin.Entities;
using System;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Amatzin.AccessData.Repositories
{
    public class FileTableRepository : IRepositoryFs<TableStorage>
    {
        public Result<TableStorage> GetAll()
        {
            throw new NotImplementedException();
        }

        public Result<TableStorage> GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Result<TableStorage> GetHistory(object id)
        {
            throw new NotImplementedException();
        }

        public Result<TableStorage> GetLast(object name)
        {
            throw new NotImplementedException();
        }

        public Result<TableStorage> Save(object id, byte[] file,string path)
        {
            int res;
            string msgResult = string.Empty;
            string streamIdResult = string.Empty;
            try
            {
                using (IDbConnection connection = new SqlConnection(ConnectionStringConfig.GetConnection()))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@prmNombre", id);
                    parameters.Add("@prmFile", file);
                    parameters.Add("@prmPath", path.Replace(@"/", @"\"));
                    parameters.Add("@prmOutMsg", msgResult, DbType.String, ParameterDirection.Output);
                    parameters.Add("@prmOutStreamId", streamIdResult, DbType.String, ParameterDirection.Output);
                    res = connection.Execute(
                        sql: "spInsertaFileTable",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                    msgResult = parameters.Get<string>("@prmOutMsg");
                    streamIdResult = parameters.Get<string>("@prmOutStreamId");
                }

                return new Result<TableStorage>
                {
                    Success = (string.IsNullOrEmpty(msgResult) ? true : false),
                    Message = (string.IsNullOrEmpty(msgResult) ? Messages.InsertSuccess : msgResult),
                    Entity = new TableStorage { StreamId = new Guid(streamIdResult) }
                };
            }
            catch (Exception ex)
            {

                return new Result<TableStorage>
                {
                    Success = false,
                    Errors = new List<Error> { new Error { Code = ex.GetHashCode(), Message = ex.Message } }
                };
            }
        }
    }
}
