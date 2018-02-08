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
                var a = uow.CommitAsync().Result;
            }

            return user;
        }

        public int GetCountUsers()
        {
            int numUsers;

            using (var uow = new UnitOfWork(new ExamenMvcEfEntities()))
            {
                numUsers = uow.Users.Count();
            }

            return numUsers;
        }

        public Users UpdateUser(Users user)
        {
            Users resultUsers;

            using (var uow = new UnitOfWork(new ExamenMvcEfEntities()))
            {
                resultUsers = uow.Users.Update(user, user.IdUser);
            }

            return resultUsers;
        }
    }
}
