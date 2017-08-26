using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiExample.DataAccess;

namespace WepApiExmaple.Server
{
    public class EmployeeSecure
    {
        public static bool Login(string userName, string password)
        {
            using (var context = new AdventureWorksEntities())
            {
                return context.Users.FirstOrDefault(x => x.UserName.Equals(userName) && x.Password.Equals(password)) != null;
            }
        }
    }
}