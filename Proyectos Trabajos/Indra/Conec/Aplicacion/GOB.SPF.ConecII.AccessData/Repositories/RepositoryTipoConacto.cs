using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.AccessData.Schemas;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryTipoConacto : IRepository<TipoContacto>
    {
        private int pages { get; set; }
        public int Pages => pages;

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryTipoConacto(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public  IEnumerable<TipoContacto> Obtener()
        {
            var result = new List<TipoContacto>();
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.TiposContactoObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tipoContacto = LeerTipoContacto(reader);
                        result.Add(tipoContacto);
                    }
                }
                return result;
            }
        }

        private static TipoContacto LeerTipoContacto(IDataRecord reader)
        {
            return new TipoContacto
            {
                Identificador = Convert.ToInt32(reader["IdTipoContacto"]),
                Nombre = reader["Nombre"].ToString()
            };
        }

        public TipoContacto ObtenerPorId(TipoContacto tipoContacto)
        {
            TipoContacto result = null;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.TiposContactoObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = tipoContacto.Identificador, ParameterName = "@IdTipoContacto" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = LeerTipoContacto(reader);
                    }
                }
            }
            return result;

        }

        public int Insertar(TipoContacto entity)
        {
            throw new NotImplementedException();
        }

        public TipoContacto ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(TipoContacto entity)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(TipoContacto entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoContacto> Obtener(Paging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoContacto> ObtenerPorCriterio(Paging paging, TipoContacto entity)
        {
            throw new NotImplementedException();
        }
    }
}
