using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.Dispatcher
{
    public class PetrusCancellationResponse
    {
        public Errors[] Errors { get; set; }
        public string ReferenceNumber { get; set; }
        public DispatcherResponse Response { get; set; }
    }
}