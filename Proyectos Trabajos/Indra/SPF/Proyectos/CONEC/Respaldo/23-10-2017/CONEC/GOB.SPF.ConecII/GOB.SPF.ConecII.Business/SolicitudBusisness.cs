using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    public class SolicitudBusisness
    {
        public int Pages { get; private set; }
        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();
        public SolicitudBusisness() { }

        #region Servicios 
        private async Task<bool> GuardarServicio(Entities.Servicio servicio, int idSolicitud, IUnitOfWork uow)
        {
            var repositoryServicios = new RepositoryServicios(uow);
            var repositoryServiciosDocumentos = new RepositoryServiciosDocumentos(uow);

            var idServicio = repositoryServicios.Insertar(servicio);
            if (idServicio == 0)
                return false;
            servicio.Documentos = servicio.Documentos.Select(x => { x.Directorio = $"Solicitudes/{idSolicitud}/{servicio.TipoServicio.Clave}"; return x; }).ToList();
            var tareasDocumentos = (from t in servicio.Documentos select RegistrarDocumento(t)).ToList(); ;
            while (tareasDocumentos.Count > 0)
            {
                Task<Entities.ServicioDocumento> tarea = await Task.WhenAny(tareasDocumentos);
                tareasDocumentos.Remove(tarea);
                var documento = await tarea;
                documento.IdServicio = idServicio;

                if (repositoryServiciosDocumentos.Insertar(documento) == 0)
                    return false;
            }
            return true;
        }
        #endregion

        private Task<Entities.ServicioDocumento> RegistrarDocumento(Entities.ServicioDocumento documento)
        {
            var amatzin = new DataAgents.ServiceAmatzin();
            documento.ArchivoId = amatzin.RegistrarArchivo(documento);
            return Task.FromResult(documento);
        }

        #region Solicitud

        public List<Solicitud> ObtenerPorIdCliente(Solicitud entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitud = new RepositorySolicitud(uow);
                return repositorySolicitud.ObtenerSolicitudPorIdCliente(entity.IdCliente);
            }
        }

        public async Task<bool> Guardar(Entities.Solicitud solicitud)
        {
            var result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitud = new RepositorySolicitud(uow);
                solicitud.Identificador = repositorySolicitud.Insertar(solicitud);

                if (solicitud.Identificador > 0)
                {
                    var amatzin = new DataAgents.ServiceAmatzin();
                    solicitud.Documento.Directorio = $"Solicitudes/{solicitud.Identificador}";
                    solicitud.Documento.ArchivoId = amatzin.RegistrarArchivo(solicitud.Documento);

                    if (repositorySolicitud.Actualizar(solicitud) != 1)
                        return false;

                    if (solicitud.Documento.ArchivoId == 0)
                        return false;

                    foreach (var servicio in solicitud.Servicios)
                    {
                        servicio.IdentificadorPadre = solicitud.Identificador;
                        var resultado = await GuardarServicio(servicio, solicitud.Identificador, uow);
                        if (!resultado)
                            return false;
                    }
                }
                uow.SaveChanges();
                result = true;
            }
            return result;
        }

        public Solicitud ObtenerSolicitudServiciosAcuerdosPorIdSolicitud(Solicitud solicitud)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                return new RepositorySolicitud(uow).ObtenerSolicitudServiciosAcuerdosPorIdSolicitud(solicitud);
            }
        }

        public bool ActualizarSolicitudAcuerdos(Solicitud solicitud)
        {
            var result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                new RepositorySolicitud(uow).Actualizar(solicitud);

                foreach (Servicio servicio in solicitud.Servicios)
                {
                    new RepositoryServicios(uow).ActualizarServicioAcuerdo(servicio);

                    #region Acuerdos
                    RepositoryAcuerdo repAcuerdo = new RepositoryAcuerdo(uow);
                    foreach (Acuerdo acuerdo in servicio.Acuerdos)
                    {
                        int id = acuerdo.Identificador != 0 ? repAcuerdo.Actualizar(acuerdo) : repAcuerdo.Insertar(acuerdo);
                    }
                    #endregion

                    #region Asistentes
                    RepositoryAsistente repAsistente = new RepositoryAsistente(uow);
                    foreach (Asistente asistente in servicio.Asistentes)
                    {
                        int id = asistente.Identificador != 0 ? repAsistente.Actualizar(asistente) : repAsistente.Insertar(asistente);
                    }
                    #endregion
                }
                uow.SaveChanges();
                result = true;
            }
            return result;
        }


        public IEnumerable<Solicitud> ObtenerPorCriterio(IPaging paging, Solicitud entity, DateTime? fechaRegistroMin, DateTime? fechaRegistroMax)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositorySolicitud repSolicitud = new RepositorySolicitud(uow);
                IEnumerable<Solicitud> solicitudes = repSolicitud.ObtenerPorCriterio(paging, entity, fechaRegistroMin, fechaRegistroMax);
                Pages = solicitudes.Count() > 0 ? solicitudes.FirstOrDefault().Paging.Pages : 0;
                return solicitudes;
            }
        }

        public Solicitud ObtenerSolicitudConDetalle(Solicitud entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                Solicitud solicitud = new RepositorySolicitud(uow).ObtenerPorId(entity.Identificador);
                solicitud.Cliente = new ClienteBusiness().ObtenerPorId(solicitud.Cliente.Identificador);
                solicitud.Servicios = new RepositoryServicios(uow).ObtenerPorIdSolicitud(solicitud).ToList();
                return solicitud;
            }
        }

        public IEnumerable<Solicitud> ObtenerSolicitudesServicios(IPaging paging)
        {
            var solicitudes = new List<Solicitud>();
          List<Solicitud> list;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitudes = new RepositorySolicitud(uow);

                list = repositorySolicitudes.ObtenerSolicitudesServicios(paging).ToList();
                uow.SaveChanges();
            }
            solicitudes.AddRange(list.GroupBy(r => r.Identificador).Select(s => new Solicitud
            {
                Cliente = s.First().Cliente,
                Identificador = s.First().Identificador,
            }));
            foreach (var solicitud in solicitudes)
            {
                solicitud.Servicios=new List<Servicio>();
                var datos = list.FindAll(p => p.Identificador.Equals(solicitud.Identificador));
                solicitud.Servicios.AddRange(datos.Select(p=> p.Servicio));
            }

            return solicitudes;
        }

        public IEnumerable<Solicitud> ObtenerSolicitudesServiciosPorCriterio(IPaging paging, Solicitud criterioBusqueda)
        {
            var solicitudes = new List<Solicitud>();
            List<Solicitud> list;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitudes = new RepositorySolicitud(uow);

                list = repositorySolicitudes.ObtenerSolicitudesServiciosPorCriterio(paging, criterioBusqueda).ToList();
                uow.SaveChanges();
            }
            solicitudes.AddRange(list.GroupBy(r => r.Identificador).Select(s => new Solicitud
            {
                Cliente = s.First().Cliente,
                Identificador = s.First().Identificador,
            }));
            foreach (var solicitud in solicitudes)
            {
                solicitud.Servicios = new List<Servicio>();
                var datos = list.FindAll(p => p.Identificador.Equals(solicitud.Identificador));
                solicitud.Servicios.AddRange(datos.Select(p => p.Servicio));
            }

            return solicitudes;
        }
        #endregion
    }
}
