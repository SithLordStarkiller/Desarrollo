namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using Entities.DTO;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class FactoresMunicipioBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(FactorMunicipioDTO entity)
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
                    var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                    //if (entity.Identificador > 0)
                    //    result = repositoryFactoresMunicipio.Actualizar(entity)>0;
                    //else
                    result = repositoryFactoresMunicipio.Insertar(Convert(entity)) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public string ValidarRegistros(FactorMunicipioDTO entity)
        {
            string resultValidacion = "";
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                resultValidacion = repositoryFactoresMunicipio.ValidarRegistro(Convert(entity));
            }

            return resultValidacion;
        }

        public IEnumerable<FactorMunicipioDTO> ObtenerTodos(Paging paging)
        {
            List<FactorMunicipioDTO> result = new List<FactorMunicipioDTO>();
            FactorMunicipioDTO dto = new FactorMunicipioDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorMunicipio> list = new List<FactorMunicipio>();
                List<ClasificacionFactor> listCla = new List<ClasificacionFactor>();
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                var repositoryClasificacionFactor = new RepositoryClasificacionFactor(uow);
                var repositoryFactores = new RepositoryFactores(uow);
                list.AddRange(repositoryFactoresMunicipio.Obtener(paging));

                foreach (var item in list)
                {
                    FactorMunicipioDTO dto2 = new FactorMunicipioDTO();

                    McsBusiness business = new McsBusiness();
                    List<Estado> listEstados = business.ObtenerEstados();
                    List<Municipio> listMunicipios = business.ObtenerMunipios(item.Estado);
                    dto2.Clasificacion = item.Clasificacion;
                    dto2.Factor = item.Factor;
                    dto2.Estado = item.Estado;
                    dto2.Estados.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador && f.Estado.Identificador == item.Estado.Identificador).Select(e => new Estado { Identificador = e.Estado.Identificador, Nombre = e.Estado.Nombre }));
                    dto2.Estados = dto2.Estados.Join(listEstados, a => a.Identificador, b => b.Identificador, (a, b) => new Estado { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Municipios.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador && f.Estado.Identificador == item.Estado.Identificador).Select(e => new Municipio { Identificador = e.Municipio.Identificador, Nombre = e.Municipio.Nombre }));
                    dto2.Municipios = dto2.Municipios.Join(listMunicipios, a => a.Identificador, b => b.Identificador, (a, b) => new Municipio { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Activo = item.Activo;
                    dto2.Descripcion = item.Descripcion;
                    result.Add(dto2);
                }

                return result;
            }
        }

        public IEnumerable<FactorMunicipioDTO> ObtenerPorCriterio(Paging paging, FactorMunicipio entity)
        {
            List<FactorMunicipioDTO> result = new List<FactorMunicipioDTO>();
            FactorMunicipioDTO dto = new FactorMunicipioDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorMunicipio> list = new List<FactorMunicipio>();
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                list.AddRange(repositoryFactoresMunicipio.ObtenerPorCriterio(paging, entity));

                foreach (var item in list)
                {
                    FactorMunicipioDTO dto2 = new FactorMunicipioDTO();

                    McsBusiness business = new McsBusiness();
                    List<Municipio> listMunicipios = business.ObtenerMunipios(item.Estado);
                    List<Estado> listEstados = business.ObtenerEstados();

                    dto2.Clasificacion = item.Clasificacion;
                    dto2.Factor = item.Factor;
                    dto2.Estado = item.Estado;
                    dto2.Estados.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador && f.Estado.Identificador == item.Estado.Identificador).Select(e => new Estado { Identificador = e.Estado.Identificador, Nombre = e.Estado.Nombre }));
                    dto2.Estados = dto2.Estados.Join(listEstados, a => a.Identificador, b => b.Identificador, (a, b) => new Estado { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Municipios.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador && f.Estado.Identificador == item.Estado.Identificador).Select(e => new Municipio { Identificador = e.Municipio.Identificador, Nombre = e.Municipio.Nombre }));
                    dto2.Municipios = dto2.Municipios.Join(listMunicipios, a => a.Identificador, b => b.Identificador, (a, b) => new Municipio { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Activo = item.Activo;
                    dto2.Descripcion = item.Descripcion;
                    result.Add(dto2);
                }

                List<FactorMunicipioDTO> Agrupacion = (from x in result.ToList()
                                                       group x by x.Factor.Identificador into x
                                                       select new FactorMunicipioDTO
                                                       {
                                                           Identificador = x.Select(a => a.Identificador).FirstOrDefault(),
                                                           Factor = x.Select(a => a.Factor).FirstOrDefault(),
                                                           Clasificacion = x.Select(a => a.Clasificacion).FirstOrDefault(),
                                                           Estado = x.Select(a => a.Estado).FirstOrDefault(),
                                                           Estados = x.Select(a => a.Estados).FirstOrDefault(),
                                                           Municipios = x.Select(a => a.Municipios).FirstOrDefault(),
                                                           Descripcion = x.Select(a => a.Descripcion).FirstOrDefault(),
                                                           Activo = x.Select(a => a.Activo).FirstOrDefault()

                                                       }).ToList();

                return Agrupacion;
            }
        }

        public FactorMunicipio ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                return repositoryFactoresMunicipio.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(FactorMunicipio tServicio)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                result = repositoryFactoresMunicipio.CambiarEstatus(tServicio) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<FactorMunicipio> ObtenerMunicipios(FactorMunicipio entity)
        {
            List<FactorMunicipioDTO> result = new List<FactorMunicipioDTO>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorMunicipio> list = new List<FactorMunicipio>();
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                list.AddRange(repositoryFactoresMunicipio.ObtenerMunicipios(entity));

                return list;
            }
        }
        public FactorMunicipioDTO Obtener(Paging paging)
        {
            FactorMunicipioDTO dto = new FactorMunicipioDTO();
            List<FactorMunicipioDTO> listEntidadesFederativas = new List<FactorMunicipioDTO>();
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

        public IEnumerable<FactorMunicipioDTO> ObtenerAgrupado(Paging paging)
        {
            List<FactorMunicipioDTO> result = new List<FactorMunicipioDTO>();
            FactorMunicipioDTO dto = new FactorMunicipioDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorMunicipio> list = new List<FactorMunicipio>();
                var repositoryFactoresMunicipio = new RepositoryFactoresMunicipio(uow);
                list.AddRange(repositoryFactoresMunicipio.Obtener(paging));

                foreach (var item in list)
                {
                    FactorMunicipioDTO dto2 = new FactorMunicipioDTO();

                    McsBusiness business = new McsBusiness();
                    List<Municipio> listMunicipios = business.ObtenerMunipios(item.Estado);
                    List<Estado> listEstados = business.ObtenerEstados();

                    dto2.Clasificacion = item.Clasificacion;
                    dto2.Factor = item.Factor;
                    dto2.Estado = item.Estado;
                    dto2.Estados.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador && f.Estado.Identificador == item.Estado.Identificador).Select(e => new Estado { Identificador = e.Estado.Identificador, Nombre = e.Estado.Nombre }));
                    dto2.Estados = dto2.Estados.Join(listEstados, a => a.Identificador, b => b.Identificador, (a, b) => new Estado { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Municipios.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador && f.Estado.Identificador == item.Estado.Identificador).Select(e => new Municipio { Identificador = e.Municipio.Identificador, Nombre = e.Municipio.Nombre }));
                    dto2.Municipios = dto2.Municipios.Join(listMunicipios, a => a.Identificador, b => b.Identificador, (a, b) => new Municipio { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Activo = item.Activo;
                    dto2.Descripcion = item.Descripcion;
                    result.Add(dto2);
                }

                List<FactorMunicipioDTO> Agrupacion = (from x in result.ToList()
                                                       group x by x.Factor.Identificador into x
                                                       select new FactorMunicipioDTO
                                                       {
                                                           Identificador = x.Select(a => a.Identificador).FirstOrDefault(),
                                                           Factor = x.Select(a => a.Factor).FirstOrDefault(),
                                                           Clasificacion = x.Select(a => a.Clasificacion).FirstOrDefault(),
                                                           Estado = x.Select(a => a.Estado).FirstOrDefault(),
                                                           Estados = x.Select(a => a.Estados).FirstOrDefault(),
                                                           Municipios = x.Select(a => a.Municipios).FirstOrDefault(),
                                                           Descripcion = x.Select(a => a.Descripcion).FirstOrDefault(),
                                                           Activo = x.Select(a => a.Activo).FirstOrDefault()

                                                       }).ToList();

                return Agrupacion;
            }
        }

        private System.Data.DataTable Convert(FactorMunicipioDTO dto)
        {

            IEnumerable<Entities.TablaDefinidaPorUsuario.FactoresMunicipios> factoresMunicipios = dto.Municipios.Select(m => new Entities.TablaDefinidaPorUsuario.FactoresMunicipios { IdMunicipio = m.Identificador, IdClasificacionFactor = dto.Clasificacion.Identificador, IdEstado = dto.Estado.Identificador, IdFactor = dto.Factor.Identificador, Descripcion = dto.Descripcion });
            System.Data.DataTable dataTable = ConversorEntityDatatable.TransformarADatatable(factoresMunicipios);
            return dataTable;
        }
    }
}

