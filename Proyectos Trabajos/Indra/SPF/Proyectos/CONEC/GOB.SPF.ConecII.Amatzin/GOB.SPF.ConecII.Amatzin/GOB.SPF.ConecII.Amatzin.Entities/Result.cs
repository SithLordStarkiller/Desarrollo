using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Amatzin.Entities
{
    public class Result<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public List<T> List { get; set; }

        public T Entity { get; set; }

        public List<Error> Errors { get; set; }
    }
}
