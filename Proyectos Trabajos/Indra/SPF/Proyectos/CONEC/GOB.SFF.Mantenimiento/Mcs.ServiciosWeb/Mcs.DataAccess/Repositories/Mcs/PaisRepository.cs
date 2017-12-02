using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcs.Core.Resources;
using Mcs.DataAccess.Enumerators;
using Mcs.Entities.Mcs;
using Mcs.DataAccess.Extensions;

namespace Mcs.DataAccess.Repositories.Mcs
{
    public class PaisRepository: Repository<BePais>
    {
        private readonly DbContext _context;
        public PaisRepository(DbContext context) : base(context)
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

        public IList<BePais> GetAllPaises()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsRepResources.SpGetPaises;
                return ToList(command).ToList();
            }
        }

        public BePais GetPaisbyId(int id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsRepResources.SpGetPaises;
                command.Parameters.Add(command.CreateParameter("@prmIdPais",id));
                return ToList(command).FirstOrDefault();
            }
        }
    }
}
