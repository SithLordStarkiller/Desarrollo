using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.AccessData.Schemas;
using System.Data;
using System.Data.SqlClient;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryClaves : IRepository<string, string>
    {
        public RepositoryClaves(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private IUnitOfWork _unitOfWork;
        public IUnitOfWork unitOfWork { get { return _unitOfWork; } }
        public void Actualizar(string id, string valor)
        {
            using (var cmd = unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ClavesUsuariosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = id, ParameterName = "@Login" });
                cmd.Parameters.Add(new SqlParameter() { Value = valor, ParameterName = "@Clave" });

                cmd.ExecuteNonQuery();
            }
        }
        public void Insertar(string id, string valor)
        {
            using (var cmd = unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ClavesUsuariosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = id, ParameterName = "@Login" });
                cmd.Parameters.Add(new SqlParameter() { Value = valor, ParameterName = "@Clave" });

                cmd.ExecuteNonQuery();
            }
        }
        public string ObtenerPorId(string id)
        {
            var clave = string.Empty;
            using (var cmd = unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ClavesUsuariosObtenerPorLogin;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = id, ParameterName = "@Login" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clave = (string)reader["Clave"];
                    }
                }
            }
            return clave;
        }
    }
}
