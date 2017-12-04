using GOB.SPF.ConecII.AccessData.Schemas;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.AccessData
{
    public class RepositorySector : IRepository<Sector>
    {
        public int Pages { get; set; }
        private readonly UnitOfWorkCatalog _unitOfWork;

        public RepositorySector(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException();

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int Actualizar(Sector entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(Sector entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(Sector entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sector> Obtener(Paging paging)
        {
            var result = new List<Sector>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.SectorObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sector sector = new Sector();

                        sector.Identificador = Convert.ToInt32(reader["IdSector"]);
                        sector.Descripcion = reader["Descripcion"].ToString();

                        result.Add(sector);
                    }
                }
                return result;
            }
        }

        public Sector ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sector> ObtenerPorCriterio(Paging paging, Sector entity)
        {
            throw new NotImplementedException();
        }
    }
}