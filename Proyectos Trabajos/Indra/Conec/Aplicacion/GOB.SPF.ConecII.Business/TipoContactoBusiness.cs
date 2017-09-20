using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.Business
{
    public class TipoContactoBusiness
    {
        private int pages { get; set; }
        private readonly UnitOfWorkFactory _unitOfWork = new UnitOfWorkFactory();

        public int Pages => pages;

        public IEnumerable<TipoContacto> ObtenerTodos()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTipoContacto = new  RepositoryTipoConacto(uow);
                return repositoryTipoContacto.Obtener();
            }
        }

    }
}
