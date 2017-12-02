using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using Entities.DTO;
    using System.Collections.Generic;
    using System.Linq;

    public class FactoresActividadEconomicaBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(FactorActividadEconomicaDTO entity)
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
                    var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                    //if (entity.Identificador > 0)
                    //    result = repositoryFactoresMunicipio.Actualizar(entity)>0;
                    //else
                    result = repositoryFactoresActividadEconomica.Insertar(Convert(entity)) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public IEnumerable<FactorActividadEconomicaDTO> ObtenerTodos(IPaging paging)
        {
            List<FactorActividadEconomicaDTO> result = new List<FactorActividadEconomicaDTO>();
            FactorActividadEconomicaDTO dto = new FactorActividadEconomicaDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorActividadEconomica> list = new List<FactorActividadEconomica>();
                List<Fraccion> fracciones = new List<Fraccion>();
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                var repositoryFracciones = new RepositoryFracciones(uow);
                list.AddRange(repositoryFactoresActividadEconomica.Obtener(paging));
                fracciones.AddRange(repositoryFracciones.Obtener(paging));

                foreach (var item in list)
                {
                    FactorActividadEconomicaDTO dto2 = new FactorActividadEconomicaDTO();

                    dto2.Clasificacion = item.Clasificacion;
                    dto2.Factor = item.Factor;
                    dto2.Division = item.Division;
                    dto2.Grupo = item.Grupo;
                    dto2.Fracciones.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador).Select(e => new Fraccion { Identificador = e.Fraccion.Identificador, Nombre = e.Fraccion.Nombre }));
                    dto2.Fracciones = dto2.Fracciones.Join(fracciones, a => a.Identificador, b => b.Identificador, (a, b) => new Fraccion { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Activo = item.Activo;
                    dto2.Descripcion = item.Descripcion;
                    result.Add(dto2);
                }

                return result;
            }
        }

        public IEnumerable<FactorActividadEconomica> ObtenerSinAgrupar(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                return repositoryFactoresActividadEconomica.Obtener(paging);
            }
        }

        public IEnumerable<FactorActividadEconomicaDTO> ObtenerAgrupado(IPaging paging)
        {
            List<FactorActividadEconomicaDTO> result = new List<FactorActividadEconomicaDTO>();
            FactorActividadEconomicaDTO dto = new FactorActividadEconomicaDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorActividadEconomica> list = new List<FactorActividadEconomica>();
                List<Fraccion> fracciones = new List<Fraccion>();
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                var repositoryFracciones = new RepositoryFracciones(uow);
                list.AddRange(repositoryFactoresActividadEconomica.Obtener(paging));
                fracciones.AddRange(repositoryFracciones.Obtener(paging));

                foreach (var item in list)
                {
                    FactorActividadEconomicaDTO dto2 = new FactorActividadEconomicaDTO();

                    dto2.Clasificacion = item.Clasificacion;
                    dto2.Factor = item.Factor;
                    dto2.Division = item.Division;
                    dto2.Grupo = item.Grupo;
                    dto2.Fracciones.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador).Select(e => new Fraccion { Identificador = e.Fraccion.Identificador, Nombre = e.Fraccion.Nombre }));
                    dto2.Fracciones = dto2.Fracciones.Join(fracciones, a => a.Identificador, b => b.Identificador, (a, b) => new Fraccion { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Activo = item.Activo;
                    dto2.Descripcion = item.Descripcion;
                    result.Add(dto2);
                }

                List<FactorActividadEconomicaDTO> Agrupacion = (from x in result.ToList()
                                                       group x by x.Factor.Identificador into x
                                                       select new FactorActividadEconomicaDTO
                                                       {
                                                           Identificador = x.Select(a => a.Identificador).FirstOrDefault(),
                                                           Factor = x.Select(a => a.Factor).FirstOrDefault(),
                                                           Clasificacion = x.Select(a => a.Clasificacion).FirstOrDefault(),
                                                           Grupo = x.Select(a => a.Grupo).FirstOrDefault(),
                                                           Division = x.Select(a => a.Division).FirstOrDefault(),
                                                           Fracciones = x.Select(a => a.Fracciones).FirstOrDefault(),
                                                           Descripcion = x.Select(a => a.Descripcion).FirstOrDefault(),
                                                           Activo = x.Select(a => a.Activo).FirstOrDefault()

                                                       }).ToList();

                return Agrupacion;
            }
        }

        public IEnumerable<FactorActividadEconomicaDTO> ObtenerPorCriterio(IPaging paging, FactorActividadEconomica entity)
        {
            List<FactorActividadEconomicaDTO> result = new List<FactorActividadEconomicaDTO>();
            FactorActividadEconomicaDTO dto = new FactorActividadEconomicaDTO();
            using (var uow = UnitOfWorkFactory.Create())
            {
                List<FactorActividadEconomica> list = new List<FactorActividadEconomica>();
                List<Fraccion> fracciones = new List<Fraccion>();
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                var repositoryFracciones = new RepositoryFracciones(uow);
                list.AddRange(repositoryFactoresActividadEconomica.ObtenerPorCriterio(paging, entity));
                fracciones.AddRange(repositoryFracciones.Obtener(paging));

                foreach (var item in list)
                {
                    FactorActividadEconomicaDTO dto2 = new FactorActividadEconomicaDTO();

                    dto2.Clasificacion = item.Clasificacion;
                    dto2.Factor = item.Factor;
                    dto2.Division = item.Division;
                    dto2.Grupo = item.Grupo;
                    dto2.Fracciones.AddRange(list.Where(f => f.Factor.Identificador == item.Factor.Identificador && f.Clasificacion.Identificador == item.Clasificacion.Identificador).Select(e => new Fraccion { Identificador = e.Fraccion.Identificador, Nombre = e.Fraccion.Nombre }));
                    dto2.Fracciones = dto2.Fracciones.Join(fracciones, a => a.Identificador, b => b.Identificador, (a, b) => new Fraccion { Identificador = a.Identificador, Nombre = b.Nombre }).ToList();
                    dto2.Activo = item.Activo;
                    dto2.Descripcion = item.Descripcion;
                    result.Add(dto2);
                }

                List<FactorActividadEconomicaDTO> Agrupacion = (from x in result.ToList()
                                                                group x by x.Factor.Identificador into x
                                                                select new FactorActividadEconomicaDTO
                                                                {
                                                                    Identificador = x.Select(a => a.Identificador).FirstOrDefault(),
                                                                    Factor = x.Select(a => a.Factor).FirstOrDefault(),
                                                                    Clasificacion = x.Select(a => a.Clasificacion).FirstOrDefault(),
                                                                    Grupo = x.Select(a => a.Grupo).FirstOrDefault(),
                                                                    Division = x.Select(a => a.Division).FirstOrDefault(),
                                                                    Fracciones = x.Select(a => a.Fracciones).FirstOrDefault(),
                                                                    Descripcion = x.Select(a => a.Descripcion).FirstOrDefault(),
                                                                    Activo = x.Select(a => a.Activo).FirstOrDefault()

                                                                }).ToList();

                return Agrupacion;
            }
        }

        public FactorActividadEconomica ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                return repositoryFactoresActividadEconomica.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(FactorActividadEconomica tServicio)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                result = repositoryFactoresActividadEconomica.CambiarEstatus(tServicio)>0;

                uow.SaveChanges();
            }
            return result;
        }

        public string ValidarRegistros(FactorActividadEconomicaDTO entity)
        {
            string resultValidacion = "";
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactoresActividadEconomica = new RepositoryFactoresActividadEconomica(uow);
                resultValidacion = repositoryFactoresActividadEconomica.ValidarRegistro(Convert(entity));
            }

            return resultValidacion;
        }

        private System.Data.DataTable Convert(FactorActividadEconomicaDTO dto)
        {
            IEnumerable<Entities.TablaDefinidaPorUsuario.FactoresActividadEconomica> factoresActividadEconomica = dto.Fracciones.Select(f => new Entities.TablaDefinidaPorUsuario.FactoresActividadEconomica { IdFraccion = f.Identificador, IdClasificadorFactor = dto.Clasificacion.Identificador, IdFactor = dto.Factor.Identificador, Descripcion = dto.Descripcion });
            System.Data.DataTable dataTable = ConversorEntityDatatable.TransformarADatatable(factoresActividadEconomica);
            return dataTable;
        }
    }
}
