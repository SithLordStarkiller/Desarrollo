using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Business
{
    public class ClavesBusiness
    {
        public void ActualizarClave(string loginUsuario, string nuevaClave)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var clavesRepository = new RepositoryClaves(uow);
                if (string.IsNullOrEmpty(clavesRepository.ObtenerPorId(loginUsuario)))
                {
                    clavesRepository.Insertar(loginUsuario, nuevaClave);
                }
                else
                {
                    clavesRepository.Actualizar(loginUsuario, nuevaClave);
                }
                uow.SaveChanges();
            }
        }
        public string ObtenerClave(string loginUsuario)
        {
            var clave = string.Empty;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var clavesRepository = new RepositoryClaves(uow);
                clave = clavesRepository.ObtenerPorId(loginUsuario);
            }
            return clave;
        }
    }
}
