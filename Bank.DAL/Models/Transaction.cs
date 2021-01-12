using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.DAL.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Account AccountTo { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool PeriodicPayment { get; set; }
        public int PeriodicTransactionFrequentyDays { get; set; }
    }
}
