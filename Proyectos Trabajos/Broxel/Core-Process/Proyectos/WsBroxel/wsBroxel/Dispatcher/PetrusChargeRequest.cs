using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.Dispatcher
{
    public class Transaction
    {
        public int CurrencyIsoCode { get; set; }
        public int InstallmentQuantity { get; set; }
        public int SalePlan { get; set; }
        public int Amount { get; set; }
        public int TransactionType { get; set; }
        public int AutoConfirmedTrx { get; set; }
        public int PartialAuthorization { get; set; }
    }
    public class Card
    {
        public string Number { get; set; }
        public int DueMonth { get; set; }
        public int DueYear { get; set; }
        public int SecurityCode { get; set; }

    }
    public class PetrusChargeRequest
    {
        public long BranchIdentifier { get; set; }
        public Card Card { get; set; }
        public Transaction Transaction { get; set; }
    }
}