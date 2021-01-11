using System;
using System.Collections.Generic;
using System.Text;

namespace De_Bank.Models
{
    public class AccountHolder
    {
        public int Id { get; set; }
        public string AccountHolderName { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
