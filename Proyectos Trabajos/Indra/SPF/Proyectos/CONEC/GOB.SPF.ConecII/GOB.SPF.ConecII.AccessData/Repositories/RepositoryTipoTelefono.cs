using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.AccessData.Schemas;
using System.Data;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryTipoTelefono : IRepository<TipoTelefono>
    {
        private int pages { get; set; }
        public int Pages => pages;

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryTipoTelefono(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int Insertar(TipoTelefono entity)
        {
            throw new NotImplementedException();
        }

        public TipoTelefono ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(TipoTelefono entity)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(TipoTelefono entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoTelefono> Obtener(IPaging paging)
        {
            var result = new List<TipoTelefono>();
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TiposTelefonoObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    result = reader.LeerTipoTelefono();
                }
                return result;
            }
        }

        public IEnumerable<TipoTelefono> ObtenerPorCriterio(IPaging paging, TipoTelefono entity)
        {
            throw new NotImplementedException();
        }

    }

    public static class TipoContactoExtend
    {
        public static List<TipoTelefono> LeerTipoTelefono(this IDataReader reader)
        {
            var result = new List<TipoTelefono>();
            while (reader.Read())
            {
                var tipoContacto = new TipoTelefono
                {
                    Identificador = Convert.ToInt32(reader["IdTipoTelefono"]),
                    Nombre = reader["Descripcion"].ToString()
                };
                result.Add(tipoContacto);
            }

            return result;
        }
    }
}
