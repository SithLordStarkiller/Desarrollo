using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public class RepositoryCorreosAlternos : IRepository<CorreosAltenos>
    {
        public int Insertar(CorreosAltenos entity)
        {
            throw new NotImplementedException();
        }

        public CorreosAltenos ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(CorreosAltenos entity)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(CorreosAltenos entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CorreosAltenos> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CorreosAltenos> ObtenerPorCriterio(IPaging paging, CorreosAltenos entity)
        {
            throw new NotImplementedException();
        }
    }
}
