using GOB.SPF.ConecII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.AccessData.Schemas;
using System.Data.SqlClient;
using System.Data;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories.Usuarios
{
    public class RepositoryUsuarios : IRepository<IUsuario>
    {
        public RepositoryUsuarios(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private IUnitOfWork _unitOfWork;
        public int Actualizar(IUsuario entity)
        {
            var result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Id, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdPersona == null ? "" : entity.IdPersona.ToString(), ParameterName = "@IdPersona" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdExterno, ParameterName = "@IdExterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Login, ParameterName = "@Login" });

                cmd.ExecuteNonQuery();
                result = 1;
            }
            return result;
        }

        public int CambiarEstatus(IUsuario entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(IUsuario entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosInserta;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdPersona == null ? "" : entity.IdPersona.ToString(), ParameterName = "@IdPersona" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdExterno, ParameterName = "@IdExterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Login, ParameterName = "@Login" });
                cmd.ExecuteNonQuery();
            }
            
            return 1;
        }

        public IEnumerable<IUsuario> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUsuario> ObtenerPorCriterio(IPaging paging, IUsuario entity)
        {
            throw new NotImplementedException();
        }

        public IUsuario ObtenerPorId(long Identificador)
        {
            IUsuario usuario = null;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter()
                {
                    Value = Identificador,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Entities.Usuarios.Usuario(Convert.ToInt32(reader["IdUsuario"]))
                        {
                            IdPersona = reader["IdPersona"] == DBNull.Value ? null : (Guid?)reader["IdPersona"],
                            Login = (string)reader["Login"],
                            IdExterno = reader["IdExterno"] == DBNull.Value ? 0 : (int)reader["IdExterno"],
                            FechaInical = reader["FechaInicial"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["FechaInicial"],
                            FechaFinal = reader["FechaFinal"] == DBNull.Value ? null : (DateTime?)reader["FechaInicial"],
                            Activo = Convert.ToBoolean(reader["Activo"]),
                        };
                    }
                }
            }
            return usuario;
        }

        public IUsuario ObtenerPorLogin(string login)
        {
            IUsuario usuario = null;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosObtenerPorLogin;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter()
                {
                    Value = login,
                    ParameterName = "@Login"
                };

                cmd.Parameters.Add(parameter);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Entities.Usuarios.Usuario(Convert.ToInt32(reader["IdUsuario"]))
                        {
                            IdPersona = reader["IdPersona"] == DBNull.Value ? null : (Guid?)reader["IdPersona"],
                            Login = (string)reader["Login"],
                            IdExterno = reader["IdExterno"] == DBNull.Value ? 0 : (int)reader["IdExterno"],
                            FechaInical = reader["FechaInicial"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["FechaInicial"],
                            FechaFinal = reader["FechaFinal"] == DBNull.Value ? null : (DateTime?)reader["FechaInicial"],
                            Activo = Convert.ToBoolean(reader["Activo"]),
                        };
                    }
                }
            }
            return usuario;
        }
    }
}
