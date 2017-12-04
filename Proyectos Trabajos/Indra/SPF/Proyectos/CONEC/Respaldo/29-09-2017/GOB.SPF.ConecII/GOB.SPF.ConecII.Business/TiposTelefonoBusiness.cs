using GOB.SPF.ConecII.AccessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.AccessData.Repositories;

namespace GOB.SPF.ConecII.Business
{
    public class TiposTelefonoBusiness
    {
        private int pages { get; set; }
        private readonly UnitOfWorkFactory _unitOfWork = new UnitOfWorkFactory();

        public int Pages => pages;

        public IEnumerable<TipoTelefono> ObtenerTodos()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoTelefono = new RepositoryTipoTelefono(uow);
                return repositoryTipoTelefono.Obtener(new Paging());
            }
        }
    }
}
