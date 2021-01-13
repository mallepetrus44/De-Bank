using Bank.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.FrontEnd.ViewModels
{
    public class AccountVM
    {
        public AccountType Accounttype { get; set; }

        public Account Account { get; set; }

        public IdentityUser IdentityUser { get; set; }
        
        public Transaction Transaction { get; set; }
    }
}
