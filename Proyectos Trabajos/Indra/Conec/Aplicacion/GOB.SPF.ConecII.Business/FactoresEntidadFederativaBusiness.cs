namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections;
    using Entities.DTO;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class FactoresEntidadFederativaBusiness
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public bool Guardar(FactorEntidadFederativaDTO entity)
        {
            bool result = false;
            string messageValidation = ValidarRegistros(entity);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                    //if (entity.Identificador > 0)
                    //    result = repositoryFactoresEntidadFederativa.Actualizar(entity)>0;
                    //else
                    result = repositoryFactoresEntidadFederativa.Insertar(Convert(entity)) > 0;


                    uow.SaveChanges();
                }
            }
            return result;
        }

        public IEnumerable<FactorEntidadFederativaDTO> ObtenerTodos(Paging paging)
        {
            List<FactorEntidadFederativaDTO> result = new List<FactorEntidadFederativaDTO>();
            FactorEntidadFederativaDTO dto = new FactorEntidadFederativaDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorEntidadFederativa> list = new List<FactorEntidadFederativa>();
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                list.AddRange(repositoryFactoresEntidadFederativa.Obtener(paging));

                foreach (var item in list)
                {
                    FactorEntidadFederativaDTO dto2 = new FactorEntidadFederativaDTO();

                    McsBusiness business = new McsBusiness();
                    List<Estado> listEstados = business.ObtenerEstados();

                    dto2.Clasificacion = item.Clasificacion;
                    dto2.Factor = item.Factor;
                    dto2.Estados.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador).Select(e => new Estado { Identificador = e.Estado.Identificador, Nombre = e.Estado.Nombre }));
                    dto2.Estados = dto2.Estados.Join(listEstados, a => a.Identificador, b => b.Identificador, (a, b) => new Estado { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Activo = item.Activo;
                    dto2.Descripcion = item.Descripcion;
                    result.Add(dto2);
                }

                List<FactorEntidadFederativaDTO> Agrupacion = (from x in result.ToList()
                                                               group x by x.Factor.Identificador into x
                                                               select new FactorEntidadFederativaDTO
                                                               {
                                                                   Identificador = x.Select(a => a.Identificador).FirstOrDefault(),
                                                                   Factor = x.Select(a => a.Factor).FirstOrDefault(),
                                                                   Clasificacion = x.Select(a => a.Clasificacion).FirstOrDefault(),
                                                                   Estados = x.Select(a => a.Estados).FirstOrDefault(),
                                                                   Descripcion = x.Select(a => a.Descripcion).FirstOrDefault(),
                                                                   Activo = x.Select(a => a.Activo).FirstOrDefault()

                                                               }).ToList();

                return Agrupacion;
            }
        }

        public IEnumerable<FactorEntidadFederativaDTO> ObtenerAgrupado(Paging paging)
        {
            List<FactorEntidadFederativaDTO> result = new List<FactorEntidadFederativaDTO>();
            FactorEntidadFederativaDTO dto = new FactorEntidadFederativaDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorEntidadFederativa> list = new List<FactorEntidadFederativa>();
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                list.AddRange(repositoryFactoresEntidadFederativa.Obtener(paging));

                foreach (var item in list)
                {
                    FactorEntidadFederativaDTO dto2 = new FactorEntidadFederativaDTO();

                    McsBusiness business = new McsBusiness();
                    List<Estado> listEstados = business.ObtenerEstados();

                    dto2.Clasificacion = item.Clasificacion;
                    dto2.Factor = item.Factor;
                    dto2.Estados.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador).Select(e => new Estado { Identificador = e.Estado.Identificador, Nombre = e.Estado.Nombre }));
                    dto2.Estados = dto2.Estados.Join(listEstados, a => a.Identificador, b => b.Identificador, (a, b) => new Estado { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Activo = item.Activo;
                    dto2.Descripcion = item.Descripcion;
                    result.Add(dto2);
                }

                List<FactorEntidadFederativaDTO> Agrupacion = (from x in result.ToList()
                                                               group x by x.Factor.Identificador into x
                                                               select new FactorEntidadFederativaDTO
                                                               {
                                                                   Identificador = x.Select(a => a.Identificador).FirstOrDefault(),
                                                                   Factor = x.Select(a => a.Factor).FirstOrDefault(),
                                                                   Clasificacion = x.Select(a => a.Clasificacion).FirstOrDefault(),
                                                                   Estados = x.Select(a => a.Estados).FirstOrDefault(),
                                                                   Descripcion = x.Select(a => a.Descripcion).FirstOrDefault(),
                                                                   Activo = x.Select(a => a.Activo).FirstOrDefault()

                                                               }).ToList();

                return Agrupacion;
            }
        }

        public IEnumerable<FactorEntidadFederativa> ObtenerSinAgrupar(Paging paging)
        {
            List<FactorEntidadFederativa> result = new List<FactorEntidadFederativa>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorEntidadFederativa> list = new List<FactorEntidadFederativa>();
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                list.AddRange(repositoryFactoresEntidadFederativa.Obtener(paging));
                
                return result;
            }
        }

        public FactorEntidadFederativaDTO Obtener(Paging paging)
        {
            FactorEntidadFederativaDTO dto = new FactorEntidadFederativaDTO();
            List<FactorEntidadFederativaDTO> listEntidadesFederativas = new List<FactorEntidadFederativaDTO>();
            listEntidadesFederativas.AddRange(this.ObtenerTodos(paging));
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryClasificacion = new RepositoryClasificacionFactor(uow);
                var repositoryFactor = new RepositoryFactores(uow);
                McsBusiness business = new McsBusiness();

                dto.Clasificaciones.AddRange(repositoryClasificacion.Obtener(paging));
                dto.Estados = business.ObtenerEstados();
                dto.Factores.AddRange(repositoryFactor.Obtener(paging));
            }
            return dto;
        }

        public IEnumerable<FactorEntidadFederativaDTO> ObtenerPorCriterio(Paging paging, FactorEntidadFederativa entity)
        {
            List<FactorEntidadFederativaDTO> result = new List<FactorEntidadFederativaDTO>();
            FactorEntidadFederativaDTO dto = new FactorEntidadFederativaDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorEntidadFederativa> list = new List<FactorEntidadFederativa>();
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                list.AddRange(repositoryFactoresEntidadFederativa.ObtenerPorCriterio(paging, entity));

                foreach (var item in list)
                {
                    FactorEntidadFederativaDTO dtos = new FactorEntidadFederativaDTO();

                    McsBusiness business = new McsBusiness();
                    List<Estado> listEstados = business.ObtenerEstados();

                    dtos.Clasificacion = item.Clasificacion;
                    dtos.Factor = item.Factor;
                    dtos.Estados.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador).Select(e => new Estado { Identificador = e.Estado.Identificador, Nombre = e.Estado.Nombre }).Distinct());
                    dtos.Estados = dtos.Estados.Join(listEstados, a => a.Identificador, b => b.Identificador, (a, b) => new Estado { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dtos.Activo = item.Activo;
                    dtos.Descripcion = item.Descripcion;
                    result.Add(dtos);
                }

                List<FactorEntidadFederativaDTO> Agrupacion = (from x in result.ToList()
                                                               group x by x.Factor.Identificador into x
                                                               select new FactorEntidadFederativaDTO
                                                               {
                                                                   Identificador = x.Select(a => a.Identificador).FirstOrDefault(),
                                                                   Factor = x.Select(a => a.Factor).FirstOrDefault(),
                                                                   Clasificacion = x.Select(a => a.Clasificacion).FirstOrDefault(),
                                                                   Estados = x.Select(a => a.Estados).FirstOrDefault(),
                                                                   Descripcion = x.Select(a => a.Descripcion).FirstOrDefault(),
                                                                   Activo = x.Select(a => a.Activo).FirstOrDefault()

                                                               }).ToList();

                return Agrupacion;
            }
        }

        public FactorEntidadFederativa ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                return repositoryFactoresEntidadFederativa.ObtenerPorId(id);
            }
        }

        public IEnumerable<FactorEntidadFederativa> ObtenerEstados(FactorEntidadFederativa entity)
        {
            List<FactorEntidadFederativaDTO> result = new List<FactorEntidadFederativaDTO>();
            FactorEntidadFederativaDTO dto = new FactorEntidadFederativaDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorEntidadFederativa> list = new List<FactorEntidadFederativa>();
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                list.AddRange(repositoryFactoresEntidadFederativa.ObtenerEstados(entity));

                return list;
            }
        }

        public bool CambiarEstatus(FactorEntidadFederativa factoresEntidadFederativa)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                result = repositoryFactoresEntidadFederativa.CambiarEstatus(factoresEntidadFederativa) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        private System.Data.DataTable Convert(FactorEntidadFederativaDTO dto)
        {
            IEnumerable<Entities.TablaDefinidaPorUsuario.FactoresEntidadFederativa> factoresEntidadFederativa = dto.Estados.Select(m => new Entities.TablaDefinidaPorUsuario.FactoresEntidadFederativa { IdClasificadorFactor = dto.Clasificacion.Identificador, IdEstado = m.Identificador, IdFactor = dto.Factor.Identificador, Descripcion = dto.Descripcion });
            System.Data.DataTable dataTable = ConversorEntityDatatable.TransformarADatatable(factoresEntidadFederativa);
            return dataTable;
        }

        public string ValidarRegistros(FactorEntidadFederativaDTO entity)
        {
            string resultValidacion = "";
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                resultValidacion = repositoryFactoresEntidadFederativa.ValidarRegistro(Convert(entity));
            }

            return resultValidacion;
        }

    }
}
