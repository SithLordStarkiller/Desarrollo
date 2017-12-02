using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    #region Librerias
    using System.Collections.Generic;
    using System.Linq;
    using AccessData;
    using AccessData.Repositories;
    using Resources;
    using Entities;
    using Entities.DTO;
    using System;
    #endregion

    public class ClienteBusiness
    {
        #region Propiedades públicas

        public int Pages => pages;

        #endregion

        #region Variables privadas

        private int pages { get; set; }

        #endregion

        #region Métodos públicos

        public IEnumerable<Cliente> ObtenerPorCriterio(IPaging paging, Cliente entity)
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

        public IEnumerable<Cliente> ObtenerTodos(IPaging paging)
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
                    //Obtiene los documentos del cliente.
                    cliente.Documentos = ObtenerDocumentos(identificador, uow);
                    //Obtinene dimicilio Fiscal cliente.
                    cliente.DomicilioFiscal = ObtenerDomiclioFiscal(cliente, uow);

                    //Obtener externos.
                    var externos = ObtenerExternos(cliente, uow);
                    cliente.Solicitantes = externos.FindAll(p => p.IdTipoPersona.Equals((int)ResourceApp.TipoPersona.Solicitante));
                    cliente.Contactos = externos.FindAll(p => p.IdTipoPersona.Equals((int)ResourceApp.TipoPersona.Contacto));

                }
            }
            return cliente;
        }

        /// <summary>
        /// regresa el cliente buscando por idCotizacion
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns></returns>
        /// <remarks>
        /// Se utiliza al firmar una cotizacion, esta requiere informacion del cliente
        /// </remarks>
        public Cliente ObtenerPorCotizacion(int identificador)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.ObtenerPorCotizacion(identificador);
            }
           
        }


        public Cliente ObtenerPorIdClienteSolicitud(long identificador)
        {
            Cliente cliente;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                cliente = repositoryCliente.ObtenerPorId(identificador);

                if (cliente.Identificador != 0)
                {
                    //Obtiene los documentos del cliente.
                    cliente.Documentos = ObtenerDocumentos(identificador, uow);
                    //Obtinene dimicilio Fiscal cliente.
                    cliente.DomicilioFiscal = ObtenerDomiclioFiscal(cliente, uow);

                    //Obtener externos.
                    var externos = ObtenerExternos(cliente, uow);
                    cliente.Solicitantes = externos.FindAll(p => p.IdTipoPersona.Equals((int)ResourceApp.TipoPersona.Solicitante));
                    cliente.Contactos = externos.FindAll(p => p.IdTipoPersona.Equals((int)ResourceApp.TipoPersona.Contacto));

                    //Obtener las Solicitudes
                    cliente.Solicitud = ObtenerSolicitudes(identificador, uow).ToList();

                    cliente.Instalaciones = ObtenerInstalaciones(identificador, uow).ToList();

                }
            }
            return cliente;
        }

        private IEnumerable<Instalacion> ObtenerInstalaciones(long identificador, IUnitOfWork uow)
        {
            var repositoryInstalacion = new RepositoryInstalacion(uow);

            // Traer instalaciones y almacenarlo en una lista
            var instalaciones = repositoryInstalacion.ObtenerPorIdCliente(new Cliente { Identificador = (int)identificador });                      

            return instalaciones;
        }

        private IEnumerable<Solicitud> ObtenerSolicitudes(long identificador, IUnitOfWork uow)
        {
            var repositorySolicitud = new RepositorySolicitud(uow);

            // 1 Traer solicitudes y almacenarlo en una lista
            var solicitudes = repositorySolicitud.ObtenerSolicitudPorIdCliente(identificador);
            // 2 traer servicios de las solicitudes en una lista
            var servicios = repositorySolicitud.ObtenerServiciosPorIdCliente(identificador);
            // 3 Traer documentos de los servicios
            var documentos = repositorySolicitud.ObtenerDocumentosPorIdCliente(identificador);

            #region Agregar documentos al servicio
            servicios = servicios.Select(p => new Servicio
            {
                Identificador = p.Identificador,
                IdSolicitud = p.IdSolicitud,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                Integrantes = p.Integrantes,
                Documentos = documentos.Where(q => q.IdServicio == p.Identificador).ToList(),
                Documento = p.Documento,
                NumeroPersonas = p.NumeroPersonas,
                FechaInicial = p.FechaInicial,
                FechaFinal = p.FechaFinal,
                Observaciones = p.Observaciones,
                TieneComite = p.TieneComite,
                ObservacionesComite = p.ObservacionesComite,
                BienCustodia = p.BienCustodia,
                Viable = p.Viable,
                FechaComite = p.FechaComite,
                Cuota = p.Cuota,
                TipoServicio = p.TipoServicio,
                HorasCurso = p.HorasCurso,
                TipoInstalacionesCapacitacion = p.TipoInstalacionesCapacitacion,
                Asistentes = p.Asistentes,
                Acuerdos = p.Acuerdos,
                Instalaciones = p.Instalaciones
            }).ToList();
            #endregion

            #region Agrega Servicios a la solicitud
            solicitudes = solicitudes.Select(p => new Solicitud
            {
                Identificador = p.Identificador,
                IdTipoSolicitud = p.IdTipoSolicitud,
                IdCliente = p.IdCliente,
                Folio = p.Folio,
                FechaRegistro = p.FechaRegistro,
                DocumentoSoporte = p.DocumentoSoporte,
                Minuta = p.Minuta,
                Servicios = servicios.Where(q => q.IdSolicitud == p.Identificador).ToList(),
                Servicio = p.Servicio,
                Documento = p.Documento,
                Cliente = p.Cliente,
                Cancelado = p.Cancelado,
                TipoSolicitud = p.TipoSolicitud
            }).ToList();
            #endregion

            return solicitudes;
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

                    if (cliente.Identificador == 0)
                        throw new ConecException(error);
                    var clienteTemp = ObtenerPorId(cliente.Identificador);

                    if (cliente.Rfc != clienteTemp.Rfc)
                        throw new ConecException(error);
                }

                cliente.Identificador = cliente.Identificador != 0 ? repositoryCliente.Actualizar(cliente) : repositoryCliente.Insertar(cliente);


                if (cliente.Identificador != 0)
                {
                    result++;

                    #region Guardar datos del cliente

                    //Documentos soporte
                    if (cliente.Documentos != null && cliente.Documentos.Count > 0)
                    {
                        var repositoryDocumentos = new RepositoryDocumento(uow);
                        GuardarDocumentos(cliente, repositoryDocumentos);
                    }
                    //Datos Domicilio Fiscal
                    if (cliente.DomicilioFiscal != null)
                    {
                        var repositoryDomicilioFiscal = new RepositoryDomicilioFiscal(uow);
                        GuardarDomicilioFiscal(cliente, repositoryDomicilioFiscal);
                    }

                    //Externo
                    var repositoryExterno = new RepositoryExterno(uow);
                    var list = GuardarExternos(cliente, repositoryExterno);

                    //Correos externos
                    var repositoryCorreo = new RepositoryCorreo(uow);
                    GuardarCorreos(repositoryCorreo, list);

                    //Telefonos externos
                    var repositoryTelefono = new RepositoryTelefono(uow);
                    GuardarTelefonos(repositoryTelefono, list);

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

            if (cliente.DomicilioFiscal.Identificador != 0)
                repositoryDomicilioFiscal.Actualizar(cliente.DomicilioFiscal);
            else
                repositoryDomicilioFiscal.Insertar(cliente.DomicilioFiscal);
        }

        private static IEnumerable<Externo> GuardarExternos(Cliente cliente, RepositoryExterno repositoryExterno)
        {
            var list = ListaExternos(cliente);
            if (list.Any())
            {
                foreach (var externo in list)
                {
                    externo.Cliente.Identificador = cliente.Identificador;
                    if (externo.Identificador != 0)
                        repositoryExterno.Actualizar(externo);
                    else
                        externo.Identificador = repositoryExterno.Insertar(externo);
                }
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

        private static void GuardarDocumentos(Cliente cliente, RepositoryDocumento repositoryDocumentos)
        {
            var amatzinBusiness = new AmatzinBusiness();

            var listDocumentosClienteActualizar = new DocumentosClienteDto();
            var listDocumentosClienteInsertar = new DocumentosClienteDto();
            var clienteDocumento = new Cliente { Identificador = cliente.Identificador };

            foreach (var documento in cliente.Documentos.Where(p => p.Identificador == 0))
            {
                documento.Directorio = $"{ResourcesAmatzin.RutaCliente}{cliente.Rfc}";
                documento.Referencia = ResourcesAmatzin.Referencia;

                var documentoCliente = (ClientesDocumentos)amatzinBusiness.Insertar(documento);
                if (documentoCliente.ArchivoId != 0)
                {
                    documentoCliente.Cliente = clienteDocumento;
                    listDocumentosClienteInsertar.Add(documentoCliente);
                }
                else
                {
                    throw new ConecException("Error al guardar");
                }
            }
            if (listDocumentosClienteInsertar.Count > 0)
                repositoryDocumentos.Insertar(listDocumentosClienteInsertar);

            listDocumentosClienteActualizar.AddRange(cliente.Documentos.Where(p => p.Identificador != 0));
            if (listDocumentosClienteActualizar.Count > 0)
                repositoryDocumentos.Actualizar(listDocumentosClienteActualizar);
        }

        private static List<ClientesDocumentos> ObtenerDocumentos(long identificador, IUnitOfWork uow)
        {

            var repositoryDocumento = new RepositoryDocumento(uow);
            return repositoryDocumento.ObtenerPorId(identificador);
        }

        private static bool ValidaRfc(Cliente cliente, RepositoryCliente repositoryCliente)
        {
            return repositoryCliente.ValidarRfc(cliente.Rfc, cliente.Identificador);
        }

        private static DomicilioFiscal ObtenerDomiclioFiscal(Cliente cliente, IUnitOfWork uow)
        {
            var repositoryDomicilio = new RepositoryDomicilioFiscal(uow);
            return repositoryDomicilio.ObtenerPorCriterio(cliente);
        }

        private static List<Externo> ObtenerExternos(Cliente cliente, IUnitOfWork uow)
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
            return externos;

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