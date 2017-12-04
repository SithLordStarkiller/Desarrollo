namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;

    using System.Collections.Generic;

    public class NotificacionesBusiness
    {
        public int Pages { get; set; }

        public bool Guardar(Notificaciones entity)
        {
            bool result;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryNotificacion = new RepositoryNotificacionesAlertas(uow);

                if (entity.IdNotificacion > 0)
                    result = repositoryNotificacion.Actualizar(entity) > 0;

                else
                    result = repositoryNotificacion.Insertar(entity) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        public Notificaciones ObtenerPorId(long id)
        {
            return new RepositoryNotificacionesAlertas(UnitOfWorkFactory.Create()).ObtenerPorId(id);
        }

        public IEnumerable<Notificaciones> ObtenerTodos(Paging paging)
        {
            return new RepositoryNotificacionesAlertas(UnitOfWorkFactory.Create()).Obtener(paging);
        }
    }
}
