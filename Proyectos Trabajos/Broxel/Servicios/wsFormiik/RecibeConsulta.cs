using System;

namespace PubliPayments
{
    [Serializable] 
    public class InputFields
    {
        public string Num_Tarjeta { get; set; }
        public string Monto { get; set; }
        public string Num_Seguridad { get; set; }
        public string ExternalType { get; set; }
        public string Banco { get; set; }
    }

    [Serializable]
    public class RecibeConsulta
    {
        public string Action { get; set; }
        public string ExternalId { get; set; }
        public string IdWorkOrder { get; set; }
        public string IdWorkOrderFormType { get; set; }
        public InputFields InputFields { get; set; }
        public string Username { get; set; }
        public string WorkOrderType { get; set; }
    }
}

