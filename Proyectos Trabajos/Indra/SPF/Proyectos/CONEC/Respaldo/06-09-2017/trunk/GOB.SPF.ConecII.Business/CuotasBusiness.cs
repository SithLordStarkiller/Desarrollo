namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class CuotasBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(Cuota entity)
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
                    var repositoryCoutas = new RepositoryCuotas(uow);

                    if (entity.Identificador > 0)
                        result = repositoryCoutas.Actualizar(entity) > 0;
                    else
                        result = repositoryCoutas.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }

            return result;
        }

        public IEnumerable<Cuota> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryCuotas(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<Cuota> ObtenerPorCriterio(Paging paging, Cuota entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryCuotas(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public Cuota ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryCuotas(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(Cuota coutas)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryCuotas(uow);
                result = repositoryCoutas.CambiarEstatus(coutas) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        public string ValidacionRegistro(Cuota entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFracciones = new RepositoryCuotas(uow);
                resultValidacion = repositoryFracciones.ValidarRegistro(entity);
            }
            return resultValidacion;
        }
    }
}
