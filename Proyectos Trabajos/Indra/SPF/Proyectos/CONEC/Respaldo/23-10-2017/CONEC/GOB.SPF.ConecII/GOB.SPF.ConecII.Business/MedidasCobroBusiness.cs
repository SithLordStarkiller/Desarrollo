using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class MedidasCobroBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public bool Guardar(MedidaCobro entity)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryMedidasCobro(uow);

                if (entity.Identificador > 0)
                    result = repositoryCoutas.Actualizar(entity)>0;
                else
                    result = repositoryCoutas.Insertar(entity)>0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<MedidaCobro> ObtenerTodos(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryMedidasCobro(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<MedidaCobro> ObtenerPorCriterio(IPaging paging, MedidaCobro entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryMedidasCobro(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public MedidaCobro ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryMedidasCobro(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        public int CambiarEstatus(MedidaCobro coutas)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryMedidasCobro(uow);
                result = repositoryCoutas.CambiarEstatus(coutas);

                uow.SaveChanges();
            }
            return result;
        }
    }
}
