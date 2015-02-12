using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAsynchronous.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public int TotalRegistros { get; set; }

        public IAsyncResult BeginConsulta(int pTotalRegistros, AsyncCallback pCallback, object pState)
        {
            TotalRegistros = pTotalRegistros;
            var task = Task<List<string>>.Factory.StartNew(ObtenerResultado, pState);
            return task.ContinueWith(res => pCallback(task));
        }

        public List<string> EndConsulta(IAsyncResult pResultado)
        {
            return ((Task<List<string>>)pResultado).Result;
        }

        private List<string> ObtenerResultado(object pState)
        {
            var lista = new List<string>();

            for (var i = 0; i < TotalRegistros; i++)
            {
                lista.Add("Registro " + i);
            }
            return lista;
        }
    }
}
