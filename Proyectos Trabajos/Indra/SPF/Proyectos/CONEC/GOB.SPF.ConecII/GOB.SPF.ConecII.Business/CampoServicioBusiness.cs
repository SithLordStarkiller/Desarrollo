using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Business
{
    public class CampoServicioBusiness
    {
        public ConfiguracionCampoServicio ObtenerPorId(int IdTipoServicio)
        {
            var configuracion = new ConfiguracionCampoServicio();

            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryConfiguracionCampoServicio repository = new RepositoryConfiguracionCampoServicio(uow);
                configuracion = repository.ObtenerPorId(IdTipoServicio);
            }

            return configuracion;
        }
    }
}
