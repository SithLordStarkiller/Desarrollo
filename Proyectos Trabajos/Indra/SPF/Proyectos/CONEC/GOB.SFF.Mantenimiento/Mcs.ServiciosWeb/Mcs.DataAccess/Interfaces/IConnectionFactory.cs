using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.DataAccess.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
