using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class ConecWebException:Exception
    {
        public ConecWebException() : base() { }

        public ConecWebException(string message) : base(message) { }
    }
}