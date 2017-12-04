using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    #region Librerias

    using System.Collections.Generic;
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Linq;
    using Entities.DTO;

    #endregion

    public class InstalacionBusiness
    {
        #region Variables privadas

        private int pages { get; set; }

        #endregion

        #region Propiedades públicas

        public int Pages => pages;

        #endregion

        #region Métodos privados
        private static void GuardarTelefonos(RepositoryTelefono repositoryTelefono, IEnumerable<Telefono> list)
        {
            var listInserta = new TelefonosInstalacionDto();

            listInserta.AddRange(list.Where(p => p.Identificador == 0 && p.Numero != string.Empty));

            var listActualizar = new TelefonosInstalacionDto();
            listActualizar.AddRange(list.Where(q => q.Identificador != 0));

            if (listInserta.Count > 0)
                repositoryTelefono.Insertar(listInserta);

            if (listActualizar.Count > 0)
                repositoryTelefono.Actualizar(listActualizar);
        }

        private static void GuardarCorreos(RepositoryCorreo repositoryCorreo, IEnumerable<Correo> list)
        {
            var listInserta = new CorreosInstalacionDto();
            listInserta.AddRange(list.Where(q => q.Identificador == 0 && q.CorreoElectronico != string.Empty));

            var listActualiza = new CorreosInstalacionDto();
            listActualiza.AddRange(list.Where(q => q.Identificador != 0));

            if (listInserta.Count > 0)
                repositoryCorreo.Insertar(listInserta);

            if (listActualiza.Count > 0)
                repositoryCorreo.Actualizar(listActualiza);
        }
        #endregion

        #region Métodos públicos

        /// <summary>
        /// Método para obtener las instalaciones de acuerdo a los parámetros establecidos.
        /// </summary>
        /// <param name="paging">Paginación</param>
        /// <param name="entity">Entidad tipo Instalacion</param>
        /// <returns>Listado de Instalaciones</returns>
        public IEnumerable<Cliente> ObtenerPorCriterio(IPaging paging, Cliente entity)
        {
            var list = new List<Instalacion>();
            List<Cliente> clientes = new List<Cliente>();
            List<InstalacionCliente> instalaciones = new List<InstalacionCliente>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                paging.All = paging.Pages < 1;
                var repositorInstalacion = new RepositoryInstalacion(uow);
                var instalacion = entity.Instalaciones.FirstOrDefault();

                instalaciones = repositorInstalacion.ObtenerPorCriterio(paging, instalacion, entity).ToList();
                clientes.AddRange(instalaciones.GroupBy(r => r.IdCliente).Select(c => new Cliente
                {
                    Identificador = c.First().IdCliente,
                    NombreCorto = c.First().NombreCorto,
                    Rfc = c.First().Rfc,
                    RazonSocial = c.First().RazonSocial

                }));
                pages = repositorInstalacion.Pages;
            }
            foreach (var cliente in clientes)
            {
                cliente.Instalaciones.AddRange(instalaciones.FindAll(p => p.IdCliente.Equals(cliente.Identificador)));
            }
            return clientes;
        }

        /// <summary>
        /// Método para obtener el listado completo de las instalaciones.
        /// </summary>
        /// <param name="paging">´Paginación</param>
        /// <returns>Listado paginado de Instalaciones</returns>
        public IEnumerable<Cliente> ObtenerTodos(IPaging paging)
        {
            var list = new List<Instalacion>();
            List<Cliente> clientes = new List<Cliente>();
            List<InstalacionCliente> instalaciones = new List<InstalacionCliente>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                paging.All = paging.Pages < 1;
                var repositorInstalacion = new RepositoryInstalacion(uow);
                instalaciones = repositorInstalacion.Obtener(paging).ToList();
                clientes.AddRange(instalaciones.GroupBy(r => r.IdCliente).Select(
                    c => new Cliente
                    {
                        Identificador = c.First().IdCliente,
                        NombreCorto = c.First().NombreCorto,
                        Rfc = c.First().Rfc,
                        RazonSocial = c.First().RazonSocial

                    }));
                pages = repositorInstalacion.Pages;
            }
            foreach (var cliente in clientes)
            {
                var instalacionesCliente = instalaciones.FindAll(p => p.IdCliente.Equals(cliente.Identificador) && p.Identificador!=0);
                if (instalacionesCliente.Count == 0)
                {
                    cliente.Instalaciones = null;
                }
                else
                {
                    cliente.Instalaciones.AddRange(instalacionesCliente);
                }
            }
            return clientes;

        }

        /// <summary>
        /// Método para cambair el estatus de Activo a la instalación
        /// </summary>
        /// <param name="entity">Instalación</param>
        /// <returns>Número de filas actualizadas.</returns>
        public int CambiarEstatus(Instalacion entity)
        {
            int result;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                result = repositoryInstalacion.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Método para obtener la instalación de acuerdo al parámetro Identififcador Instalación
        /// </summary>
        /// <param name="id">Identificador de la Instalación</param>
        /// <returns>Instalación consultada</returns>
        public Instalacion ObtenerPorId(long id)
        {
            Instalacion instalacion;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                var respositoryCorreo = new RepositoryCorreo(uow);
                var repositoryTelefono = new RepositoryTelefono(uow);
                instalacion = repositoryInstalacion.ObtenerPorId(id);
                if (instalacion != null)
                {
                    instalacion.CorreosInstalacion = respositoryCorreo.ObtenerPorIdInstalacion(id);
                    instalacion.TelefonosInstalacion = repositoryTelefono.ObtenerPorIdInstalacion(id);
                }
                else
                {
                    throw new ConecException("No se encontro el cliente");
                }
                uow.SaveChanges();
            }
            return instalacion;
        }

        public List<string> ObtenerPorNombre(string nombre)
        {
            var list = new List<string>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                list = repositoryInstalacion.ObtenerPorNombre(nombre);
                uow.SaveChanges();
            }
            return list;
        }

        /// <summary>
        /// Método para guardar o actualizar una instalación.
        /// </summary>
        /// <param name="entity">Instalación nueva o instalación actualizada</param>
        /// <returns>Identififcador de la instalación</returns>
        public int Guardar(Cliente cliente)
        {
            int result = 0;
            foreach (var instalacion in cliente.Instalaciones)
            {
                result = GuardarInstalacion(instalacion, cliente.Identificador);
            }
            return result;
        }


        private int GuardarInstalacion(Instalacion instalacion, int IdCliente)
        {
            int result;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                result = instalacion.Identificador > 0
                    ? repositoryInstalacion.Actualizar(instalacion, IdCliente)
                    : repositoryInstalacion.Insertar(instalacion, IdCliente);
                if (result > 0)
                {
                    instalacion.Identificador = result;
                    //Guarda telefonos de contacto
                    foreach (var telefonos in instalacion.TelefonosInstalacion) { telefonos.UsuarioEntity = instalacion; }
                    var repositoryTelefonos = new RepositoryTelefono(uow);
                    GuardarTelefonos(repositoryTelefonos, instalacion.TelefonosInstalacion);

                    //Guarda correos electronicos de contacto
                    foreach (var correo in instalacion.CorreosInstalacion) { correo.UsuarioEntity = instalacion; }
                    var repositoryCorreos = new RepositoryCorreo(uow);
                    GuardarCorreos(repositoryCorreos, instalacion.CorreosInstalacion);
                }
                uow.SaveChanges();
            }
            return result;
        }

        #endregion
    }
}
