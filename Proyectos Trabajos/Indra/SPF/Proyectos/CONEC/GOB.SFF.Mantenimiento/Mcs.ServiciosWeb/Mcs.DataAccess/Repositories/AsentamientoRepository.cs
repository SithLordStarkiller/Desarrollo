using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mcs.DataAccess.Extensions;
using Mcs.DataAccess.Enumerators;
using Mcs.Entities;
using Mcs.Core.Resources;

namespace Mcs.DataAccess.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class AsentamientoRepository:Repository<BeAsentamiento>
    {
        private readonly DbContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AsentamientoRepository(DbContext context) : base(context)
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
        public IList<BeAsentamiento> GetAsentamientos()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsMcsResources.SpGetAsentamientosWs;

                return ToList(command).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IList<BeAsentamiento> GetAsentamientos(BeAsentamiento request)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsMcsResources.SpGetAsentamientosWs;

                command.Parameters.Add(command.CreateParameter("@IDESTADO", request.IdEstado));
                command.Parameters.Add(command.CreateParameter("@IDMUNICIPIO", request.IdMunicipio));
                command.Parameters.Add(command.CreateParameter("@ASECODIGOPOSTAL", request.CodigoPostal));

                return ToList(command).ToList();


            }
        }

    }
}
