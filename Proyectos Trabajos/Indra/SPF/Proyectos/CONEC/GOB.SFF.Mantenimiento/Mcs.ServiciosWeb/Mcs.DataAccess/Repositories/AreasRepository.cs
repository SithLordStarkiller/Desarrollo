using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mcs.DataAccess.Enumerators;
using Mcs.Entities;
using Mcs.Core.Resources;


namespace Mcs.DataAccess.Repositories
{
    public class AreasRepository : Repository<BeArea>
    {
        private readonly DbContext _context;

        public AreasRepository(DbContext context) : base(context)
        {
            if (context == null)
            {
                var connectionFactory = ConnectionHelper.GetConnection(DataBaseType.Rep);
                _context = new DbContext(connectionFactory);
            }
            else
            {
                _context = context;
            }
        }

        public IList<BeArea> GetAreas()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsRepResources.SpGetAllAreasWs;
                
                return ToList(command).ToList();
            }
        }
    }
}
