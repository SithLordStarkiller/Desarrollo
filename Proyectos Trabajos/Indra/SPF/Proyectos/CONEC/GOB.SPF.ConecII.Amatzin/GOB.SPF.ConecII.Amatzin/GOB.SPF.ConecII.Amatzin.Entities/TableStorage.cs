using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Amatzin.Entities
{
    public class TableStorage
    {
        public Guid StreamId { get; set; }
        public byte[] FileStream { get; set; }
        public string Name { get; set; }

        public string FileType { get; set; }
    }
}
