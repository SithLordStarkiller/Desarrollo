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

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

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
                FactorEntidadFederativa factor = list.FirstOrDefault();

                if (factor != null)
                {
                    McsBusiness business = new McsBusiness();

                    List<Estado> listEstados = business.ObtenerEstados();


                    dto.Estados.AddRange(list.Select(e => new Estado { Identificador = e.Estado.Identificador, Nombre = e.Estado.Nombre }));
                    dto.Estados = dto.Estados.Join(listEstados, a => a.Identificador, b => b.Identificador, (a, b) => new Estado { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto.Clasificacion = factor.Clasificacion;
                    dto.Factor = factor.Factor;
                    dto.Activo = factor.Activo;
                    dto.Descripcion = factor.Descripcion;
                    result.Add(dto);
                }

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
                FactorEntidadFederativa factor = list.FirstOrDefault();

                if (factor != null)
                {
                    McsBusiness business = new McsBusiness();

                    List<Estado> listEstados = business.ObtenerEstados();


                    dto.Estados.AddRange(list.Select(e => new Estado { Identificador = e.Estado.Identificador, Nombre = e.Estado.Nombre }));
                    dto.Estados = dto.Estados.Join(listEstados, a => a.Identificador, b => b.Identificador, (a, b) => new Estado { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto.Clasificacion = factor.Clasificacion;
                    dto.Factor = factor.Factor;
                    dto.Activo = factor.Activo;
                    dto.Descripcion = factor.Descripcion;
                    result.Add(dto);
                }

                return result;
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
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresEntidadFederativa = new RepositoryFactoresEntidadFederativa(uow);
                return repositoryFactoresEntidadFederativa.ObtenerEstados(entity);
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
            IEnumerable<Entities.TablaDefinidaPorUsuario.FactoresEntidadFederativa> factoresEntidadFederativa = dto.Estados.Select(m => new Entities.TablaDefinidaPorUsuario.FactoresEntidadFederativa { IdClasificadorFactor = dto.Clasificacion.Identificador, IdEntidFed = m.Identificador, IdFactor = dto.Factor.Identificador, Descripcion = dto.Descripcion });
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
