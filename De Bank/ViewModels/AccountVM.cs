using De_Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace De_Bank.Models.ViewModels
{
    public class AccountVM
    {
        public AccountType accounttype { get; set; }

        public Account account {get;set;}

        public AccountHolder accountholder { get; set; }
        
        public Transaction transaction { get; set; }
    }
}
