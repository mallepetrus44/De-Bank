using System;
using System.Collections.Generic;
using System.Text;

namespace De_Bank.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double TransactionAmount { get; set; } 
        public DateTime TransactionDate { get; set; }
        public bool AutoTransaction { get; set; }
        public int AutoTransactionFrequentyDays { get; set; }
        public Account Account1 { get; set; }
        public bool MinusAccount1 { get; set; }
        public Account Account2 { get; set; }
        public bool PlusAccount2 { get; set; }
    }
}
