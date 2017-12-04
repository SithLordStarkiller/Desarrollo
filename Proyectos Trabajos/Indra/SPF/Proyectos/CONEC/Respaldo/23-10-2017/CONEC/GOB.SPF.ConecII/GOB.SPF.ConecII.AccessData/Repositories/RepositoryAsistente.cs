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
    public class RepositoryAsistente : IRepository<Asistente>
    {
        #region Variables privadas
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public RepositoryAsistente(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;
        }
        #endregion

        #region Métodos públicos
        public int Actualizar(Asistente entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ServiciosAsistentesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.Identificador, ParameterName = "@IdServicioAsistente" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.IdPersona, ParameterName = "@IdPersona" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Activo, ParameterName = "@Activo" });
                return cmd.ExecuteNonQuery();
            }
        }

        public int CambiarEstatus(Asistente entity)
        {
            return 1;
        }

        public int Insertar(Asistente entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                var idSql = new SqlParameter { ParameterName = "@IdServicioAsistente", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                cmd.CommandText = Schemas.Solicitud.ServiciosAsistentesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.IdentificadorPadre, ParameterName = "@IdServicio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.IdPersona, ParameterName = "@IdPersona" });

                cmd.Parameters.Add(idSql);

                cmd.ExecuteNonQuery();
                return (int)idSql.Value;
            }
        }

        public IEnumerable<Asistente> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Asistente> ObtenerPorCriterio(IPaging paging, Asistente entity)
        {
            throw new NotImplementedException();
        }

        public Asistente ObtenerPorId(long Identificador)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ServiciosAsistentesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = Identificador, ParameterName = "@Identificador" });

                using (var reader = cmd.ExecuteReader())
                {
                    Asistente asistente = new Asistente();
                    while (reader.Read())
                    {
                        asistente = ConvertReaderToAsistente(reader);
                    }
                    return asistente;
                }
            }
        }
        #endregion

        #region Métodos privados
        private Asistente ConvertReaderToAsistente(IDataReader reader)
        {
            Asistente asistente = new Asistente()
            {
                IdentificadorPadre = Convert.ToInt32(reader["IdServicio"]),
                IdPersona = Guid.Parse(reader["IdPersona"].ToString()),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
            return asistente;
        }
        #endregion
    }
}
