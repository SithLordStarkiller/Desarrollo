namespace GOB.SPF.ConecII.Business
{
    #region Librerias

    using AccessData;
    using AccessData.Repositories;
    using Entities;

    #endregion

    public class DomicilioFiscalBusiness
    {
        #region Propiedades públicas

        public int Pages => pages;

        #endregion

        #region Variables privadas

        private int pages { get; set; }

        #endregion

        #region Métodos públicos

        public DomicilioFiscal ObtenerDomicilioFiscal(Cliente entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryDomicilioFiscal(uow);
                return repositoryCliente.ObtenerPorCriterio(entity);
            }
        }

        #endregion

    }
}
