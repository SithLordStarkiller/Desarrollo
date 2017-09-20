namespace GOB.SPF.ConecII.Business
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Entities;
    using AccessData;
    using AccessData.Repositories;

    public class ReceptoresAlertaBusiness
    {
        public int Pages { get; set; }

        public bool GuardarLista(List<ReceptorAlerta> entity, Notificaciones notificacion)
        {
            bool result;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryReceptoresAlertas = new RepositoryReceptoresAlertas(uow);
                var dataTable = ConversorEntityDatatable.TransformarADatatable(entity.ToArray());

                result = repositoryReceptoresAlertas.InsertarListaReceptorAlerta(dataTable, notificacion);

                uow.SaveChanges();
            }

            return result;
        }

        public List<ReceptorAlerta> ListaReceptorAlertaObtenerTodos(Notificaciones notificaciones)
        {
            List<ReceptorAlerta> result;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryReceptoresAlertas = new RepositoryReceptoresAlertas(uow);

                result = repositoryReceptoresAlertas.ListaReceptorAlertaObtenerTodos(notificaciones);

                uow.SaveChanges();
            }

            return result;
        }
    }
}
