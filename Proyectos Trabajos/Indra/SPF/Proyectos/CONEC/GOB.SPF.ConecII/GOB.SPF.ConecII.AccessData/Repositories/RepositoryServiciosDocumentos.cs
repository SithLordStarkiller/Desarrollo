using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Amatzin;
using System.Data.SqlClient;
using System.Data;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryServiciosDocumentos : IRepository<Entities.ServicioDocumento>
    {
        private IUnitOfWork _unitOfWork;
        public RepositoryServiciosDocumentos(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        public int Actualizar(ServicioDocumento entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(ServicioDocumento entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(ServicioDocumento documento)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                var idSql = new SqlParameter {ParameterName = "@IdServicioDocumento", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }; ; 

                cmd.CommandText = Schemas.Solicitud.ServicioDocumentoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = documento.IdServicio, ParameterName = "@IdServicio" });
                cmd.Parameters.Add(new SqlParameter { Value = documento.TipoDocumento.Identificador, ParameterName = "@idTipoDocumento" });
                cmd.Parameters.Add(new SqlParameter { Value = documento.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter { Value = documento.ArchivoId, ParameterName = "@DocumentoSoporte" });
                cmd.Parameters.Add(new SqlParameter { Value = documento.Observaciones, ParameterName = "@Observaciones" });
                cmd.Parameters.Add(idSql);

                cmd.ExecuteNonQuery();
                return (int)idSql.Value;
            }
        }

        public IEnumerable<ServicioDocumento> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServicioDocumento> ObtenerPorCriterio(IPaging paging, ServicioDocumento entity)
        {
            throw new NotImplementedException();
        }

        public ServicioDocumento ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }
    }
}
