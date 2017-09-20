using System;
using System.Data.SqlClient;
using System.Text;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using GOB.SPF.ConecII.AccessData.Repositories;

    using System.Collections.Generic;

    public class ClienteBusiness
    {

        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public IEnumerable<Cliente> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.Obtener(paging);
            }
        }

        public IEnumerable<Cliente> ObtenerPorRazonSocial(string searchText)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.ObtenerPorRazonSocial(searchText);
            }
        }

        public IEnumerable<Cliente> ObtenerPorNombreCorto(string searchText)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.ObtenerPorNombreCorto(searchText);
            }
        }

        public IEnumerable<Cliente> ObtenerPorCriterio(Paging paging, Cliente entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.ObtenerPorCriterio(paging, entity);
            }
        }

        public int Guardar(Cliente cliente)
        {
            var guardar = 0;
            var clienteInsertar = new Cliente();
            using (var tran = UnitOfWorkFactory.Create())
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryCliente = new RepositoryCliente(uow);
                    clienteInsertar = repositoryCliente.Insertar(cliente);
                    cliente.Identificador = clienteInsertar.Identificador;
                    uow.SaveChanges();
                    guardar = cliente.Identificador;

                    if (cliente.Identificador != 0)
                    {
                        GuardarDatosCliente(cliente);
                    }

                    tran.SaveChanges();
                }
                return guardar;

            }
        }

        private static void GuardarDatosCliente(Cliente cliente)
        {
            //Guarda domicilio fiscal del cliente.
            var domicilioFiscalBusiness = new DomicilioFiscalBusiness();
            domicilioFiscalBusiness.Guardar(cliente);

            //Guardar externos.
            var externosBusiness = new ExternoBusiness();
            externosBusiness.InsertarExternos(cliente);
        }
    }
}
