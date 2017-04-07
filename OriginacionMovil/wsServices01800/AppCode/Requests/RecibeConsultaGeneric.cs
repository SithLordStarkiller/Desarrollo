using System;

namespace wsServices01800.AppCode.Requests
{
    [Serializable]
    public class RecibeConsultaGeneric
    {
        public string Action { get; set; }
        public string ExternalId { get; set; }
        public string IdWorkOrder { get; set; }
        public string IdWorkOrderFormType { get; set; }
        public string Username { get; set; }
        public string WorkOrderType { get; set; }
    }
}