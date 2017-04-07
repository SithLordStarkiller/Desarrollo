using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServices01800.AppCode.Requests
{
    [Serializable]
    public class RecibeConsultaBuroCredito:RecibeConsultaGeneric
    {
        InputFieldsBuroCredito InputFields { set; get; }
    }
}