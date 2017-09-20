using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Business
{
    public class SectoresBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public IEnumerable<Sector> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySector = new RepositorySector(uow);
                return repositorySector.Obtener(paging);
            }
        }
    }
}
