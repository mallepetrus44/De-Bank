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
        public virtual List<Account> Accounts { get; set; }
    }
}
