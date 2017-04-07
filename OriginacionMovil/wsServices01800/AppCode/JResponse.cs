
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServices01800.AppCode
{
    [Serializable]
    public class JResponse
    {
        public Object UpdateFieldsValues { get; set; }
        public List<Object> AfectedFields { get; set; } 
    }
}