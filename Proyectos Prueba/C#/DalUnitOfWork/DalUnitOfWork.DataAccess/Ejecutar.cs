using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalUnitOfWork.DataAccess
{
    public class program
    {
        public static void Main(string[] args)
        {
            var context = new AdventureworksEntities();

            var lista = context.DimEmployee.OrderBy(c => c.SickLeaveHours).Take(10).ToList();

            using (var uow = new UnitOfWork(new AdventureworksEntities()))
            {
                var list = uow.Employees.GetTopSickHours();

                foreach (var item in list)
                {
                    Console.WriteLine($"-----------------------------------");
                    Console.WriteLine($"First Name: {item.FirstName} ");
                    Console.WriteLine($"Last Name: {item.LastName} ");
                    Console.WriteLine($"Sick Hours: {item.SickLeaveHours} ");
                    Console.WriteLine($"-----------------------------------");
                }

            }

            using (var uow = new UnitOfWork(new AdventureworksEntities()))
            {
                var item = uow.Employees.GetTopSickHours().FirstOrDefault();

                uow.Employees.Remove(item);

                uow.Complete();
            }
        }
    }
}
