using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mcs.DataAccess.Enumerators;
using Mcs.Entities;
using Mcs.Core.Resources;

namespace Mcs.DataAccess.Repositories
{
    public class GrupoTarifarioRepository: Repository<BeGrupoTarifario>
    {
        private readonly DbContext _context;

        public GrupoTarifarioRepository(DbContext context) : base(context)
        {
            if (context == null)
            {
                var connectionFactory = ConnectionHelper.GetConnection(DataBaseType.Cove);
                _context = new DbContext(connectionFactory);
            }
            else
            {
                _context = context;
            }
        }

        public IList<BeGrupoTarifario> GetGrupoTarifarios()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsCoveResources.SpGetAllGrupoTarifariosWs;

                return ToList(command).ToList();
            }
        }

    }
}
