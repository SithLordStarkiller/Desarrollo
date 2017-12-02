using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mcs.DataAccess.Enumerators;
using Mcs.Entities;
using Mcs.Core.Resources;

namespace Mcs.DataAccess.Repositories
{
    public class EntidadesFederativasRepository : Repository<BeEntidadFederativa>
    {
        private readonly DbContext _context;

        public EntidadesFederativasRepository(DbContext context) : base(context)
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

        public IList<BeEntidadFederativa> GetEntidadesFederativas()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsMcsResources.SpGetAllEntidadesFederativasWs;

                return ToList(command).ToList();
            }
        }
    }
}
