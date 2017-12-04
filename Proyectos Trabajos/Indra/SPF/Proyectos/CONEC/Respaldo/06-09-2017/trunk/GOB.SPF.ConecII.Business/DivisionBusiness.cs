namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class DivisionBusiness
    {

        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public DivisionBusiness() { }   
        
        public int Guardar(Division entity)
        {
            int result = 0;
            string messageValidation = ValidacionRegistro(entity);

            if (!string.IsNullOrEmpty(messageValidation))
            {
                throw new ConecException(messageValidation);
            }
            else
            {
                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repositoryDivision = new RepositoryDivision(uow);

                    if (entity.Identificador > 0)
                        result = repositoryDivision.Actualizar(entity);
                    else
                        result = repositoryDivision.Insertar(entity);

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public IEnumerable<Division> ObtenerTodos(Paging paging)
        {
            //return new List<Division> { new Division { Identificador = 1, Name = "XXX" }, new Division { Identificador = 2, Name = "YYY" }, new Division { Identificador = 3, Name = "ZZZ" } };
            List<Division> listDivision = new List<Division>();
          using (var uow = UnitOfWorkFactory.Create())
          {
            var repositoryDivision = new RepositoryDivision(uow);
            listDivision.AddRange(repositoryDivision.Obtener(paging));
            this.pages = repositoryDivision.Pages;
          }
            return listDivision;
        }

        public IEnumerable<Division> ObtenerListado()
        {
            
            List<Division> listDivision = new List<Division>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryDivision = new RepositoryDivision(uow);
                listDivision.AddRange(repositoryDivision.ObtenerListado());
            }
            return listDivision;
        }

        public Division ObtenerPorId(long id)
        {
          using (var uow = UnitOfWorkFactory.Create())
          {
            var repositoryDivision = new RepositoryDivision(uow);
            return repositoryDivision.ObtenerPorId(id);
          }
        }

        public int CambiarEstatus(Division entity)  // duda...
        {
          int result = 0;
          using (var uow = UnitOfWorkFactory.Create())
          { 
            var repositoryDivision = new RepositoryDivision(uow);
            result = repositoryDivision.CambiarEstatus(entity);
            uow.SaveChanges();            
          }
          return result;
        }

        public IEnumerable<Division> ObtenerPorCriterio(Paging paging, Division entity)
        {
            List<Division> list = new List<Division>();
          using (var uow = UnitOfWorkFactory.Create())
          {
            RepositoryDivision repositoryDivision = new RepositoryDivision(uow);
            list.AddRange(repositoryDivision.ObtenerPorCriterio(paging, entity));
            this.pages = repositoryDivision.Pages;
          }
            return list;
        }

        public string ValidacionRegistro(Division entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryDivisiones = new RepositoryDivision(uow);
                resultValidacion = repositoryDivisiones.ValidarRegistro(entity);
            }
            return resultValidacion;
        }


    }
}
