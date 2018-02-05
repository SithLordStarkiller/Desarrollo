namespace ExamenMvcEf.BusinessLogic.Securiry
{
    using Models;
    using DataAccess;
    using DataAccess.UnitOfWork;

    public class UsersBll
    {
        public Users AddUser(Users user)
        {
            using (var uow = new UnitOfWork(new ExamenMvcEfEntities()))
            {
                user = uow.Users.Add(user);
                uow.Commit();
            }

            return user;
        }
    }
}
