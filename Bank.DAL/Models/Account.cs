using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bank.DAL.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public bool AccountLock { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal AccountMinimum { get; set; }
        public AccountType AccountType { get;set; }
        public IdentityHolder IdentityHolder { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
