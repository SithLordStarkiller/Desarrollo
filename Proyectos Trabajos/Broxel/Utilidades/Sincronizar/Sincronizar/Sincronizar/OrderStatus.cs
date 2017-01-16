﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sincronizar
{
    [DataContract]
    internal class OrdenStatus
    {
        [DataMember]
        public string ExternalId { get; set; }

        [DataMember]
        public string FormiikId { get; set; }

        [DataMember]
        public string FormatType { get; set; }

        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public string OrderStatus { get; set; }

        [DataMember]
        public string OrderStatusDate { get; set; } 



    }
}
