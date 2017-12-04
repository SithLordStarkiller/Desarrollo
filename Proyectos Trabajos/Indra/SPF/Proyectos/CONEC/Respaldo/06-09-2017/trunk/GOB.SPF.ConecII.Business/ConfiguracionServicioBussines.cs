using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using AccessData.Repositories;
    using System.Collections.Generic;
    using Entities.DTO;

    public class ConfiguracionServicioBussines
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public IEnumerable<ConfiguracionServicioDTO> ObtenerTodosPaginados(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorioConfig = new RepositoryConfiguracionServicio(uow);
                return repositorioConfig.ObtenerPaginado(paging);
            }
        }

        public IEnumerable<ConfiguracionServicioDTO> ObtenerTodosPorIdConfiguracionServicio(int idConfiguracionServicio)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorioConfig = new RepositoryConfiguracionServicio(uow);
                return repositorioConfig.ObtenerConfiguracionPorIdConfiguracion(idConfiguracionServicio);
            }
        }

        public IEnumerable<ConfiguracionServicioDTO> ObtenerTodosPorIdTipoServicioIdCentroCosto(int idTipoServicio, string idCentroCosto)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorioConfig = new RepositoryConfiguracionServicio(uow);
                return repositorioConfig.ObtenerConfiguracionesPorIdTipoServicioIdCentroCosto(idTipoServicio, idCentroCosto);
            }
        }

        public int Guardar(List<ConfiguracionServicio> listaConfiguracion)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorioConfig = new RepositoryConfiguracionServicio(uow);

                if (listaConfiguracion!=null && listaConfiguracion.Count() > 0)
                    result = repositorioConfig.InsertarConfiguraciones(listaConfiguracion);

                uow.SaveChanges();
            }
            return result;
        }
        
    }
}
