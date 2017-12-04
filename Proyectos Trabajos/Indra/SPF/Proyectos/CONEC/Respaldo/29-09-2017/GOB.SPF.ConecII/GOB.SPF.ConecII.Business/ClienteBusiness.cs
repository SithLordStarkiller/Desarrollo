namespace GOB.SPF.ConecII.Business
{
    #region Librerias

    using AccessData;
    using Entities;
    using AccessData.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.DTO;

    #endregion

    public class ClienteBusiness
    {
        #region Propiedades públicas

        public int Pages => pages;

        #endregion

        #region Variables privadas
        
        private int pages { get; set; }

        private enum TipoPersona
        {
            Solicitante = 1,
            Contacto = 2
        }

        #endregion

        #region Métodos públicos

        public IEnumerable<Cliente> ObtenerPorCriterio(Paging paging, Cliente entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.ObtenerPorCriterio(paging, entity);
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

        public IEnumerable<Cliente> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.Obtener(paging);
            }
        }

        public Cliente ObtenerPorId(long identificador)
        {
            Cliente cliente;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                cliente = repositoryCliente.ObtenerPorId(identificador);

                if (cliente.Identificador != 0)
                {
                    //Obtinene dimicilio Fiscal cliente.
                    ObtenerDomiclioFiscal(cliente, uow);

                    //Obtener externos.
                    ObtenerExternos(cliente, uow);
                }
            }
            return cliente;
        }

        public bool CambiarEstatus(Cliente cliente)
        {
            var result = false;
            if (cliente.IsActive != null)
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryCliente = new RepositoryCliente(uow);
                    result = repositoryCliente.CambiarEstatus(cliente) != 0;
                    uow.SaveChanges();
                }
            }
            return result;
        }

        public int Guardar(Cliente cliente)
        {
            var result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                if (ValidaRfc(cliente, repositoryCliente))
                {
                    var error = "El cliente ya se encuentra registrado";

                    if(cliente.Identificador == 0)
                        throw new ConecException(error);
                    else
                    {
                        var clienteTemp = ObtenerPorId(cliente.Identificador);

                        if(cliente.Rfc != clienteTemp.Rfc)
                            throw new ConecException(error);
                    }
                }

                if(cliente.Identificador != 0)
                    repositoryCliente.Actualizar(cliente);
                else
                    cliente.Identificador = repositoryCliente.Insertar(cliente);

                result++;
                if (cliente.Identificador != 0)
                {
                    #region Guardar datos del cliente

                    //Datos Domicilio Fiscal
                    var repositoryDomicilioFiscal = new RepositoryDomicilioFiscal(uow);
                    GuardarDomicilioFiscal(cliente, repositoryDomicilioFiscal);
                    result++;

                    //Externo
                    var repositoryExterno = new RepositoryExterno(uow);
                    var list = GuardarExternos(cliente, repositoryExterno);
                    result++;

                    //Correos externos
                    var repositoryCorreo = new RepositoryCorreo(uow);
                    GuardarCorreos(repositoryCorreo, list);
                    result++;

                    //Telefonos externos
                    var repositoryTelefono = new RepositoryTelefono(uow);
                    GuardarTelefonos(repositoryTelefono, list);
                    result++;

                    #endregion
                }
                uow.SaveChanges();
            }

            return result;
        }

        #endregion

        #region Métodos privados

        private static void GuardarDomicilioFiscal(Cliente cliente, RepositoryDomicilioFiscal repositoryDomicilioFiscal)
        {
            cliente.DomicilioFiscal.Cliente.Identificador = cliente.Identificador;

            if(cliente.DomicilioFiscal.Identificador != 0)
                repositoryDomicilioFiscal.Actualizar(cliente.DomicilioFiscal);
            else
                repositoryDomicilioFiscal.Insertar(cliente.DomicilioFiscal);
        }

        private static IEnumerable<Externo> GuardarExternos(Cliente cliente, RepositoryExterno repositoryExterno)
        {
            var list = ListaExternos(cliente);
            foreach (var externo in list)
            {
                externo.Cliente.Identificador = cliente.Identificador;
                if(externo.Identificador != 0)
                    repositoryExterno.Actualizar(externo);
                else
                    externo.Identificador = repositoryExterno.Insertar(externo);
            }

            return list;
        }

        private static void GuardarTelefonos(RepositoryTelefono repositoryTelefono, IEnumerable<Externo> list)
        {
            var listInserta = new TelefonosExternosDto();

            listInserta.AddRange(list.Where(p => p.Telefonos?.Count != 0 && p.Telefonos?.Count(q => q.Identificador == 0) > 0 && p.Identificador != 0));

            var listActualizar = new TelefonosExternosDto();
            listActualizar.AddRange(list.Where(p => p.Telefonos?.Count != 0 && p.Telefonos?.Count(q => q.Identificador != 0) > 0 && p.Identificador != 0));

            if (listInserta.Count > 0)
                repositoryTelefono.Insertar(listInserta);

            if (listActualizar.Count > 0)
                repositoryTelefono.Actualizar(listActualizar);
        }

        private static void GuardarCorreos(RepositoryCorreo repositoryCorreo, IEnumerable<Externo> list)
        {
            var listInserta = new CorreosExternoDto();
            listInserta.AddRange(list.Where(p => p.Correos?.Count != 0 && p.Correos?.Count(q => q.Identificador == 0) > 0 && p.Identificador != 0));

            var listActualiza = new CorreosExternoDto();
            listActualiza.AddRange(list.Where(p => p.Correos?.Count != 0 && p.Correos?.Count(q => q.Identificador != 0) > 0 && p.Identificador != 0));

            if (listInserta.Count > 0)
                repositoryCorreo.Insertar(listInserta);

            if (listActualiza.Count > 0)
                repositoryCorreo.Actualizar(listActualiza);
        }

        private static bool ValidaRfc(Cliente cliente, RepositoryCliente repositoryCliente)
        {
            return repositoryCliente.ValidarRfc(cliente.Rfc, cliente.Identificador);
        }

        private static void ObtenerDomiclioFiscal(Cliente cliente, IUnitOfWork uow)
        {
            var repositoryDomicilio = new RepositoryDomicilioFiscal(uow);
            cliente.DomicilioFiscal = repositoryDomicilio.ObtenerPorCriterio(cliente);
        }

        private static void ObtenerExternos(Cliente cliente, IUnitOfWork uow)
        {
            var repositoryExterno = new RepositoryExterno(uow);
            var externos = repositoryExterno.ObtenerPorId(cliente.Identificador);

            var repositoryCorreo = new RepositoryCorreo(uow);
            var respositoryTelefonos = new RepositoryTelefono(uow);
            foreach (var externo in externos)
            {
                externo.Correos = repositoryCorreo.ObtenerPorIdExterno(externo.Identificador);
                externo.Telefonos = respositoryTelefonos.ObtenerPorIdExterno(externo.Identificador);
            }
            cliente.Solicitantes = externos.FindAll(p => p.IdTipoPersona.Equals((int)TipoPersona.Solicitante));
            cliente.Contactos = externos.FindAll(p => p.IdTipoPersona.Equals((int)TipoPersona.Contacto));
        }

        private static IEnumerable<Externo> ListaExternos(Cliente cliente)
        {
            var externos = new List<Externo>();

            if (cliente.Solicitantes != null)
                externos.AddRange(cliente.Solicitantes);

            if (cliente.Contactos != null)
                externos.AddRange(cliente.Contactos);

            return externos;
        }

        #endregion
    }
}