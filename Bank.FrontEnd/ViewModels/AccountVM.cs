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
        public Transaction Transaction { get; set; }
        
        public List<Account> Accounts => new List<Account>();
        public int AccountID { get; set; }

        public List<Transaction> Transactions => new List<Transaction>();
        public int TransactionID { get; set; }

        public List<IdentityHolder> IdentityHolders => new List<IdentityHolder>();
        public IdentityHolder IdentityHolder { get; set; }
        public int IdentityHolderID { get; set; }

    }
}
