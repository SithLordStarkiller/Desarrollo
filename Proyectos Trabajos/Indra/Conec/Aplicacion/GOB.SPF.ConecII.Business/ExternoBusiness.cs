using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.Business
{
    public partial class ExternoBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages => pages;


        public int Guardar(Externo externo, int identificadorCliente)
        {
            var result = 0;
            var clienteInsertar = new Cliente();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryExterno = new RepositoryExterno(uow);
                result = repositoryExterno.Insertar(externo, identificadorCliente);
                uow.SaveChanges();
            }
            return result;
        }

        private static IEnumerable<Externo> ListaExternos(Cliente cliente)
        {
            var externos = new List<Externo>();
            externos.AddRange(cliente.Solicitantes);
            externos.AddRange(cliente.Contactos);
            return externos;
        }

        public void InsertarExternos(Cliente cliente)
        {
            foreach (var externo in ListaExternos(cliente))
            {
                var result = Guardar(externo,cliente.Identificador);
                if (result != 0)
                {
                    //Guardar listado de telefonos y correos

                }
            }
        }
    }
}
