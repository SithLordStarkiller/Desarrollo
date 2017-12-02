using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcs.Core.Resources;
using Mcs.DataAccess.Enumerators;
using Mcs.Entities.Sicer;
using Mcs.DataAccess.Extensions;


namespace Mcs.DataAccess.Repositories
{
    public class PersonaFisicaRepository:Repository<BePersonaFisica>
    {
        private readonly DbContext _context;
        public PersonaFisicaRepository(DbContext context) : base(context)
        {
            if (context == null)
            {
                var connectionFactory = ConnectionHelper.GetConnection(DataBaseType.Sicer);
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
        /// <param name="request"></param>
        /// <returns></returns>
        public IList<BePersonaFisica> PostRegistraPersonaFisica(BePersonaFisica request)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = SpsSicerResources.SpInsertPersonaFisicaWs;

                command.Parameters.Add(command.CreateParameter("@IDESTADO", request.IdEstado));
                command.Parameters.Add(command.CreateParameter("@IDMUNICIPIO", request.IdMunicipio));
                command.Parameters.Add(command.CreateParameter("@ASECODIGOPOSTAL", request.CodigoPostal));

                return ToList(command).ToList();


            }

        }
    }
}
