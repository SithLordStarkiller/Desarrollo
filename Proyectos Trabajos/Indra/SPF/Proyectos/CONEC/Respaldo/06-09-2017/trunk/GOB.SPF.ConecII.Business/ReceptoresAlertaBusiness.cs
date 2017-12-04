namespace GOB.SPF.ConecII.Business
{
    using System.Collections.Generic;

    using Entities;
    using AccessData;
    using AccessData.Repositories;

    public class ReceptoresAlertaBusiness
    {
        public int Pages { get; set; }

        public bool GuardarLista(List<ReceptorAlerta> entity, Notificaciones notificacion)
        {
            bool result;

            int idNotificacion;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryNotificacion = new RepositoryNotificacionesAlertas(uow);

                idNotificacion = repositoryNotificacion.Insertar(notificacion);

                uow.SaveChanges();
            }

            if (idNotificacion == 0)
                return false;
            
                notificacion.IdNotificacion = idNotificacion;

                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryReceptoresAlertas = new RepositoryReceptoresAlertas(uow);
                    var dataTable = ConversorEntityDatatable.TransformarADatatable(entity.ToArray());

                    result = repositoryReceptoresAlertas.InsertarListaReceptorAlerta(dataTable,
                        notificacion.IdNotificacion);

                    uow.SaveChanges();
                }
            
            return result;
        }
    }
}
