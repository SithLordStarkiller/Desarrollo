using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiResultPage<T>:UiResult<T>
    {
        public UiResultPage() {
            Paging = new UiPaging();
        }

        public T Query { get; set; }

        public UiPaging Paging { get; set; }
    }
}