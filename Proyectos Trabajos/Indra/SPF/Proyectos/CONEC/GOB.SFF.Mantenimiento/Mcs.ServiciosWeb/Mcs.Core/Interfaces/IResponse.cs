using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Core.Interfaces
{
    internal interface IResponse<TResponse>
    {
        TResponse Value { get; set; }
        int TotalRows { get; set; }
        bool Success { get; set; }
        //ErrorListDto ErrorList { get; }
    }
}
