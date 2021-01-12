using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace De_Bank.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public bool AccountLock { get; set; }
        public double AccountBalance { get; set; }
        public double AccountMinimum { get; set; }
        public AccountType AccountType { get;set; }
        public AccountHolder AccountHolder { get; set; }
        public List<Transaction> transactions { get; set; }

    }
}
