using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcs.Entities.Mcs;
using Mcs.Entities;
using Mcs.Core.Resources;
using Mcs.DataAccess.Enumerators;
using Mcs.DataAccess.Repositories;
namespace Mcs.DataAccess.Repositories.Mcs
{
    public class InstalacionRepository: Repository<BeInstalacion>
    {
        private readonly DbContext _context;
        public InstalacionRepository(DbContext context) : base(context)
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

        public IList<BeInstalacion> GetInstalaciones()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsMcsResources.SpInstalacionObtenerTodosWs;
                

                return ToList(command).ToList();
            }
        }
    }
}
