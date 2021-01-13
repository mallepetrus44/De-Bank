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
        public int AccountID { get; set; }

        public List<Transaction> Transactions { get; set; }
        public int TransactionID { get; set; }
        public List<IdentityUser> IdentityUser { get; set; }
        public int IdentityUserID { get; set; }
        public List<IdentityHolder> IdentityHolder { get; set; }
        public int IdentityHolderID { get; set; }

    }
}
