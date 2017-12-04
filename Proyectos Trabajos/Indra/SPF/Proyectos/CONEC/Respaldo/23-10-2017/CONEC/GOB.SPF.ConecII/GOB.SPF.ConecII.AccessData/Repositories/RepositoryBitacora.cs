using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using System.Data.SqlClient;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryBitacora : IRepository<Bitacora>
    {
        private IUnitOfWork _unitOfWork;
        public RepositoryBitacora(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        public int Actualizar(Bitacora entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(Bitacora entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(Bitacora entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = Schemas.Auditoria.BitacoraInsertar;
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdUsuario", Value = entity.IdUsuario });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@IP", Value = entity.IP });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@MAC", Value = entity.MAC });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@FechaRegistro", Value = entity.FechaRegistro });
                
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@Path", Value = entity.Path });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@Request", Value = entity.Request });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@Response", Value = entity.Response });

                cmd.ExecuteNonQuery();
                return 1;
            }
        }

        public IEnumerable<Bitacora> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bitacora> ObtenerPorCriterio(IPaging paging, Bitacora entity)
        {
            throw new NotImplementedException();
        }

        public Bitacora ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }
    }
}
