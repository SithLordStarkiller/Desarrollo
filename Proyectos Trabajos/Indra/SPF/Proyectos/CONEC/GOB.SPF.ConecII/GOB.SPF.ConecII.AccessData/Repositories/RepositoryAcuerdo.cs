using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryAcuerdo : IRepository<Acuerdo>
    {
        #region Variables privadas
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public RepositoryAcuerdo(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;
        }
        #endregion

        #region Métodos públicos
        public int Actualizar(Acuerdo entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ServicioAcuerdoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.Identificador, ParameterName = "@IdAcuerdo" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Fecha, ParameterName = "@Fecha" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Convenio, ParameterName = "@Convenio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Responsable, ParameterName = "@Responsable" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.IsActive, ParameterName = "@IsActive" });
                return cmd.ExecuteNonQuery();
            }
        }

        public int CambiarEstatus(Acuerdo entity)
        {
            return 1;
        }

        public int Insertar(Acuerdo entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                var idSql = new SqlParameter { ParameterName = "@IdServicioAcuerdo", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                cmd.CommandText = Schemas.Solicitud.ServicioAcuerdoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.IdentificadorPadre, ParameterName = "@IdServicio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Fecha, ParameterName = "@Fecha" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Convenio, ParameterName = "@Convenio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Responsable, ParameterName = "@Responsable" });

                cmd.Parameters.Add(idSql);

                cmd.ExecuteNonQuery();
                return (int)idSql.Value;
            }
        }

        public IEnumerable<Acuerdo> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Acuerdo> ObtenerPorCriterio(IPaging paging, Acuerdo entity)
        {
            throw new NotImplementedException();
        }
        
        public Acuerdo ObtenerPorId(long Identificador)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ServicioAcuerdoObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = Identificador, ParameterName = "@Identificador" });
               
                using (var reader = cmd.ExecuteReader())
                {
                    Acuerdo acuerdo = new Acuerdo();
                    while (reader.Read())
                    {
                        acuerdo = ConvertReaderToAcuerdo(reader);
                    }
                    return acuerdo;
                }
            }
        }
        #endregion

        #region Métodos privados
        private Acuerdo ConvertReaderToAcuerdo(IDataReader reader)
        {
            Acuerdo acuerdo = new Acuerdo()
            {
                IdentificadorPadre = Convert.ToInt32(reader["IdServicio"]),
                Fecha = Convert.ToDateTime(reader["FechaCumplimiento"]),
                Convenio = reader["Acuerdo"].ToString(),
                Responsable = Guid.Parse(reader["Responsable"].ToString()),
                IsActive = Convert.ToBoolean(reader["Activo"])
            };
            return acuerdo;
        }
        #endregion
    }
}
