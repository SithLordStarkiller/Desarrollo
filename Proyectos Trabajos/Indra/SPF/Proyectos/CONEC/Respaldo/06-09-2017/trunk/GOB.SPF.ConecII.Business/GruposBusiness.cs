namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class GruposBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();
        public int Pages { get { return pages; } }
        
        public bool Guardar(Grupo entity)
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
                    var repositoryGrupos = new RepositoryGrupos(uow);

                    if (entity.Identificador > 0)
                        result = repositoryGrupos.Actualizar(entity) > 0;
                    else
                        result = repositoryGrupos.Insertar(entity) > 0;

                    uow.SaveChanges();
                }
            }
            return result;
        }

        public IEnumerable<Grupo> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGrupos = new RepositoryGrupos(uow);
                return repositoryGrupos.Obtener(paging);
            }
        }

        public IEnumerable<Grupo> ObtenerPorCriterio(Paging paging, Grupo entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGrupos = new RepositoryGrupos(uow);
                return repositoryGrupos.ObtenerPorCriterio(paging, entity);
            }
        }

        public Grupo ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGrupos = new RepositoryGrupos(uow);
                return repositoryGrupos.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(Grupo tServicio)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGrupos = new RepositoryGrupos(uow);
                result = repositoryGrupos.CambiarEstatus(tServicio)>0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Grupo> ObtenerPorIdDivision(int IdDivision)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGrupos = new RepositoryGrupos(uow);
                return repositoryGrupos.ObtenerPorIdDivision(IdDivision);
            }
        }

        public string ValidacionRegistro(Grupo entity)
        {
            string resultValidacion = "";

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryGrupoes = new RepositoryGrupos(uow);
                resultValidacion = repositoryGrupoes.ValidarRegistro(entity);
            }
            return resultValidacion;
        }
    }
}
