using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleConnectionODP.DataAccess;

namespace OracleConnectionODP.BusinessLogic
{
    public class EntitiesBl
    {
        public string LoginUserBl()
        {
            return new ConnectionDbOracle().LoginUserDa();
        }
    }
}
