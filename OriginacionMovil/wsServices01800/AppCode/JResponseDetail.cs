using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServices01800.AppCode
{
    [Serializable]
    public class JResponseDetail
    {
        public string Name { set; get; }
        public JSettings Settings { set; get; }
    }
}