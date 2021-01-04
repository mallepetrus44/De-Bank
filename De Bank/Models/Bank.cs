using De_Bank.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace De_Bank.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public virtual List<Account>Accounts { get; set; }
        public virtual List<AccountHolder> AccountHolders { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
 
    }
}
