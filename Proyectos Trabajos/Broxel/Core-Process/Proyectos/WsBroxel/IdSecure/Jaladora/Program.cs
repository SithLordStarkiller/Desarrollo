using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IdSecure;

namespace Jaladora
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 1000; i++)
            {
                var idUser = new Random().Next(999999);
                var t = new Thread(() => Valida(idUser));
                t.Start();
            }

        }

        private static void Valida(int idUser)
        {
            var idSecure = new IdSecureComp().GetIdSecure(idUser);
            var id = new IdSecureComp().GetIdUserValid(idSecure);
            if (id == idUser)
                Console.WriteLine("Chingonazo");
        }
    }
}
