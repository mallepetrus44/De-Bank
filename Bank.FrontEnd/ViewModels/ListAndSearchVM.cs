using Bank.DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.FrontEnd.ViewModels
{
    public class ListAndSearchVM
    {

        public virtual ICollection<Account> Accounts { set; get; }
        public virtual ICollection<Transaction> Transactions { set; get; }
        public virtual ICollection<IdentityHolder> IdentityHolders { set; get; }
        public IdentityHolder IdentityHolder { get; set; }
        public Account Account { get; set; }
        public Transaction Transaction { get; set; }
        public List<SelectListItem> Selection { set; get; }
        public string SelectedStatus { set; get; }
    }
        
}
