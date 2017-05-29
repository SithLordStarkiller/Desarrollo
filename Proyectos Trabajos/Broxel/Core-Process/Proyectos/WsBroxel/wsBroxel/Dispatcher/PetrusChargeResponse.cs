using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.Dispatcher
{
    public class DispatcherResponse
    {
        public string Code { get; set; }
        public string Authorization { get; set; }
        public decimal AuthorizedAmount { get; set; } /*Este valor que regresa si se hacen cargos sobre el saldo?*/
    }
    public class Errors
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
    public class PetrusChargeResponse
    {
        public Errors[] Errors { get; set; }
        public string ReferenceNumber { get; set; }
        public DispatcherResponse Response { get; set; }
        public decimal Ammount { get; set; }
    }
}