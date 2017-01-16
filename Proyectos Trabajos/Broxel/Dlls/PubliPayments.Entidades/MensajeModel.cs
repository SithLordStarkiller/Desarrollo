namespace PubliPayments.Entidades
{
    public class MensajeModel
    {
        public string ClientId { get; set; }
        public string ProductId { get; set; }
        public string[] UserName { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public bool IsImportant { get; set; }

        public MensajeModel(string clientid, string productid, string[] userName, string sender, string message, bool isImportant)
        {
            ClientId = clientid;
            ProductId = productid;
            UserName = userName;
            Sender = sender;
            Message = message;
            IsImportant = isImportant;
        }

        public MensajeModel()
        {
            ClientId = "";
            ProductId = "";
            UserName = new []{""};
            Sender = "";
            Message = "";
            IsImportant = false;
        }
    }
}
