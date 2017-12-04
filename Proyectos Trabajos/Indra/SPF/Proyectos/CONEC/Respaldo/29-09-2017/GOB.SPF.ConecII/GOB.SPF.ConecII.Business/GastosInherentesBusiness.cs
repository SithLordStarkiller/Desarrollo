namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class GastosInherentesBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public bool Guardar(GastoInherente entity)
        {
            bool result = false;
            string messageValidation = ValidacionRegistro(entity);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryGastosInherentes = new RepositoryGastosInherentes(uow);

                    if (entity.Identificador > 0)
                        result = repositoryGastosInherentes.Actualizar(entity) > 0;
                    else
                        result = repositoryGastosInherentes.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<GastoInherente> ObtenerTodos(Paging paging)
        {
            //return new List<GastosInherentes> { new Division { Identificador = 1, Name = "XXX" }, new GastosInherentes { Identificador = 2, Name = "YYY" }, new GastosInherentes { Identificador = 3, Name = "ZZZ" } };

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGastosInherentes = new RepositoryGastosInherentes(uow);
                return repositoryGastosInherentes.Obtener(paging);
            }
        }

        public IEnumerable<GastoInherente> ObtenerPorCriterio(Paging paging, GastoInherente entity)
        {
            //return new List<GastosInherentes> { new Division { Identificador = 1, Name = "XXX" }, new GastosInherentes { Identificador = 2, Name = "YYY" }, new GastosInherentes { Identificador = 3, Name = "ZZZ" } };

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGastosInherentes = new RepositoryGastosInherentes(uow);
                return repositoryGastosInherentes.ObtenerPorCriterio(paging, entity);
            }
        }

        public GastoInherente ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGastosInherentes = new RepositoryGastosInherentes(uow);
                return repositoryGastosInherentes.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(GastoInherente gastosInherentes)
        {
            GastoInherente instancia = ObtenerPorId(gastosInherentes.Identificador);
            string messageValidation = ValidacionRegistro(instancia);

            bool result = false;
            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryGastosInherentes = new RepositoryGastosInherentes(uow);
                    result = repositoryGastosInherentes.CambiarEstatus(gastosInherentes) > 0;

                    uow.SaveChanges();
                }
            }
            return result;

        }

        public string ValidacionRegistro(GastoInherente entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGastosInherentes = new RepositoryGastosInherentes(uow);
                resultValidacion = repositoryGastosInherentes.ValidarRegistro(entity);
            }
            return resultValidacion;
        }
    }
}
