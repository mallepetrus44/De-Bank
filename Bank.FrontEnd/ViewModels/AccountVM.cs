using Bank.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DAL.ViewModels
{
    public class AccountVM
    {
        public AccountType Accounttype { get; set; }

        public Account Account { get; set; }

        public IdentityHolder IdentityHolder { get; set; }
        
        public Transaction Transaction { get; set; }
    }
}
