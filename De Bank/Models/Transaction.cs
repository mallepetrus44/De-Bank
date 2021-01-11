using System;
using System.Collections.Generic;
using System.Text;

namespace De_Bank.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Account AccountTo { get; set; }
        public double TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool PeriodicPayment { get; set; }
        public int PeriodicTransactionFrequentyDays { get; set; }
    }
}
