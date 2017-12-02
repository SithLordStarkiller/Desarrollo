using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.AccessData.Repositories;

namespace GOB.SPF.ConecII.Business
{
    public class ServicioInstalacionBusiness
    {
        public IEnumerable<ServicioInstalacion> ObtenerInstalacionesPorIdCliente(Solicitud solicitud, int? IdServicio)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryServicios = new RepositoryServicios(uow);
                return repositoryServicios.ObtenerInstalacionesPorIdCliente(solicitud, IdServicio);
            }
        }
    }
}
