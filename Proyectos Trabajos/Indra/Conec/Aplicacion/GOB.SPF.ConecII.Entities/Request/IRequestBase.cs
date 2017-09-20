using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Request
{
    public interface IRequestBase<T>
    {
        string Usuario { get; set; }

        Paging Paging { get; set; }

        T Item { get; set; }
    }
}
