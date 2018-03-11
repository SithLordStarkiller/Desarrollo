namespace ExamenMvcEf.BusinessLogic.Securiry
{
    using Models;
    using DataAccess;
    using DataAccess.UnitOfWork;

    using System.Linq;
    using System.Data.Entity;
    using System.Collections.Generic;

    public class UsersBll
    {
        public List<Users> GetAllUsers()
        {
            var usersList = new List<Users>();

            using (var uow = new UnitOfWork(new ExamenMvcEfEntities()))
            {
                usersList = uow.Users.GetAllIncluding(x => x.CatTypeUser);
            }

            return usersList;
        }

        public Users GetUsersById(int id)
        {
            var user = new Users();

            using (var uow = new UnitOfWork(new ExamenMvcEfEntities()))
            {
                user = uow.Context.Users
                    .Include(x=>x.CatTypeUser)
                    .Where(x => x.IdUser == id).FirstOrDefault();

                var userType = uow.Context.Users.Join(uow.Context.CatTypeUser, usr => usr.IdTypeUser, type => type.IdTypeUser, (usr, type) => new { usr, type }).First();
            }
            return user;
        }

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
