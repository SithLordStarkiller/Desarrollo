using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class RequestBase<T>
    {
        public string Usuario { get; set; }

        public Paging Paging { get; set; }

        public T Item { get; set; }
    }
}