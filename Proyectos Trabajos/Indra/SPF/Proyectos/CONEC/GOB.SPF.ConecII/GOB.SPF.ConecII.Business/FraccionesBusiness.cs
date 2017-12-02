using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class FraccionesBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(Fraccion entity)
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

                    var repositoryFracciones = new RepositoryFracciones(uow);

                    if (entity.Identificador > 0)
                        result = repositoryFracciones.Actualizar(entity) > 0;
                    else
                        result = repositoryFracciones.Insertar(entity) > 0;

                    uow.SaveChanges();

                }
            }

            return result;
        }

        public IEnumerable<Fraccion> ObtenerTodos(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFracciones = new RepositoryFracciones(uow);
                return repositoryFracciones.Obtener(paging);
            }
        }

        public IEnumerable<Fraccion> ObtenerPorCriterio(IPaging paging, Fraccion entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFracciones = new RepositoryFracciones(uow);
                return repositoryFracciones.ObtenerPorCriterio(paging, entity);
            }
        }

        public Fraccion ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFracciones = new RepositoryFracciones(uow);
                return repositoryFracciones.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(Fraccion fracciones)
        {
            bool result = false;
            Fraccion fraccion = ObtenerPorId(fracciones.Identificador);
            string messageValidation = ValidacionRegistro(fraccion);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryFracciones = new RepositoryFracciones(uow);
                    result = repositoryFracciones.CambiarEstatus(fracciones) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public string ValidacionRegistro (Fraccion entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFracciones = new RepositoryFracciones(uow);
                resultValidacion = repositoryFracciones.ValidarRegistro(entity);
            }
            return resultValidacion;
        }
    }
}
