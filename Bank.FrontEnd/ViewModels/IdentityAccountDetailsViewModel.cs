using Bank.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.FrontEnd.ViewModels
{
    public class IdentityAccountDetailsViewModel
    {
        public IEnumerable<Account> Accounts { get; set; }
        public Account Account { get; set; }

        public IEnumerable<IdentityHolder> IdentityHolders { get; set; }

        public IdentityHolder IdentityHolder { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
