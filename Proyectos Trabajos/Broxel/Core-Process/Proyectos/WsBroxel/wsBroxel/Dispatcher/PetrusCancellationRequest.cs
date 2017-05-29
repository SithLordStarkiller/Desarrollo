using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.Dispatcher
{
    public class TransactionCancellation
    {
        public int CurrencyIsoCode { get; set; }
        public int InstallmentQuantity { get; set; }
        public int SalePlan { get; set; }
        public int Amount { get; set; }
        public int TransactionType { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal ReplacementAmount { get; set; }
    }
    public class PetrusCancellationRequest
    {
        public long BranchIdentifier { get; set; }
        public Card Card { get; set; }
        public TransactionCancellation Transaction { get; set; }
    }
}