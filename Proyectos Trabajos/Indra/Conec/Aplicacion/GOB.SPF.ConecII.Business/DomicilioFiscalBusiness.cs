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
    public class DomicilioFiscalBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages => pages;

        public DomicilioFiscal ObtenerDomicilioFiscal(Cliente entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryDomicilioFiscal(uow);
                return repositoryCliente.ObtenerPorCriterio(entity);
            }
        }

        public int Guardar(Cliente entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryDomicilioFiscal = new RepositoryDomicilioFiscal(uow);
                return repositoryDomicilioFiscal.Insertar(entity);
            }
        }
    }
}
