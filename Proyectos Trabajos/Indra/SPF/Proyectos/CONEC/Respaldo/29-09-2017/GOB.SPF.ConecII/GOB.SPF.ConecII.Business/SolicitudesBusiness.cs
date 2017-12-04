namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections;
    using Entities.DTO;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class SolicitudesBusiness
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public bool Guardar(Solicitudes entity)
        {
            bool result = false;
            //string messageValidation = ValidarRegistros(entity);

            //if (!string.IsNullOrEmpty(messageValidation))
            //{
            //    throw new ConecException(messageValidation);
            //}
            //else
            //{
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositorySolicitudes = new RepositorySolicitudes(uow);
                    if (entity.Identificador > 0)
                        result = repositorySolicitudes.Actualizar(entity) > 0;
                    else
                        result = repositorySolicitudes.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            //}
            return result;
        }

        public IEnumerable<Solicitudes> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitudes = new RepositorySolicitudes(uow);
                return repositorySolicitudes.Obtener(paging);
            }
        }

        public IEnumerable<Solicitudes> Obtener(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitudes = new RepositorySolicitudes(uow);
                return repositorySolicitudes.Obtener(paging);
            }
        }

        public IEnumerable<Solicitudes> ObtenerPorCriterio(/*Paging paging, */Solicitudes entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitudes = new RepositorySolicitudes(uow);
                return repositorySolicitudes.ObtenerPorCriterio(/*paging, */entity);
            }
        }

        public IEnumerable<Solicitudes> ObtenerPorId(Solicitudes entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitudes = new RepositorySolicitudes(uow);
                return repositorySolicitudes.ObtenerPorId(entity);
            }
        }

        public bool CambiarEstatus(Solicitudes Solicitudes)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitudes = new RepositorySolicitudes(uow);
                result = repositorySolicitudes.CambiarEstatus(Solicitudes) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        public string ValidarRegistros(Solicitudes entity)
        {
            string resultValidacion = "";
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorySolicitudes = new RepositorySolicitudes(uow);
                resultValidacion = repositorySolicitudes.ValidarRegistro(entity);
            }

            return resultValidacion;
        }

    }
}
