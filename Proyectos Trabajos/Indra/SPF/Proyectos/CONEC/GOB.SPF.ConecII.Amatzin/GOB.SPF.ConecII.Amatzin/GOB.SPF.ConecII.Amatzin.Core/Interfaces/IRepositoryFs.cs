using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Amatzin.Entities;

namespace GOB.SPF.ConecII.Amatzin.Core.Interfaces
{
    public interface IRepositoryFs<TEntity> where TEntity : class
    {
        Result<TEntity> GetAll();
        Result<TEntity> GetById(object id);
        Result<TEntity> GetLast(object name);
        Result<TEntity> GetHistory(object id);

        Result<TEntity> Save(object id,byte[] file, string path);

    }
}
