using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mcs.DataAccess.Extensions;
using Mcs.DataAccess.Enumerators;
using Mcs.Entities;
using Mcs.Core.Resources;

namespace Mcs.DataAccess.Repositories
{
    public class MunicipiosRepository : Repository<BeMunicipio>
    {
        private readonly DbContext _context;

        public MunicipiosRepository(DbContext context) : base(context)
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

        public IList<BeMunicipio> GetMunicipios(int id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsMcsResources.SpGetMunicipiosByIdEstadoWs;
                command.Parameters.Add(command.CreateParameter("@idEstado", id));
                return ToList(command).ToList();
            }
        }
    }
}
