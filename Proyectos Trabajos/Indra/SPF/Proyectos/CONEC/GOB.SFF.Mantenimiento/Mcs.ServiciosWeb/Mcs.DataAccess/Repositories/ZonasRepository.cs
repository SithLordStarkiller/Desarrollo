using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mcs.DataAccess.Enumerators;
using Mcs.Entities;
using Mcs.Core.Resources;

namespace Mcs.DataAccess.Repositories
{
    public class ZonasRepository : Repository<BeZona>
    {
        private readonly DbContext _context;
        public ZonasRepository(DbContext context) : base(context)
        {
            if (context == null)
            {
                var connectionFactory = ConnectionHelper.GetConnection(DataBaseType.Sicogua);
                _context = new DbContext(connectionFactory);
            }
            else
            {
                _context = context;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<BeZona> GetZonas()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsMcsResources.SpGetAllZonasWs;

                return ToList(command).ToList();
            }
        }
    }
}
