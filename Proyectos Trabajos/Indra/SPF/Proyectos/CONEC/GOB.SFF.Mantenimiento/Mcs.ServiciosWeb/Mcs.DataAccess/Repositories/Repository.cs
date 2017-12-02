
using System;
using System.Collections.Generic;
using System.Data;
using Mcs.DataAccess.Extensions;
namespace Mcs.DataAccess.Repositories
{
    public abstract class Repository<TEntity> where TEntity : new()
    {
        protected Repository(DbContext context)
        {
            Context = context;
        }

        protected DbContext Context { get; }

        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            using (var record = command.ExecuteReader())
            {
                List<TEntity> items = new List<TEntity>();
                while (record.Read())
                {
                    
                    items.Add(Map<TEntity>(record));
                }
                return items;
            }
        }


        
            
        
        protected TEntity Map<TEntity>(IDataRecord record)
        {
            var objT = Activator.CreateInstance<TEntity>();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                if (record.HasColumn(property.Name) && !record.IsDBNull(record.GetOrdinal(property.Name)))
                    property.SetValue(objT, record[property.Name]);


            }
            return objT;
        }


        //protected abstract TEntity Map<TEntity>(IDataRecord record);
    }
}
