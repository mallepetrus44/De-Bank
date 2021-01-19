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
        //public string IndentityHolderID { get; set; }
        //public string FullName { get; set; }
        //public string AccountNumber { get; set; }

        //public float? AccountBalance { get; set; }

        public virtual ICollection<Account> Accounts { set; get; }
        ////public Account SelectedAccount { get; set; }



        public virtual ICollection<Transaction> Transactions { set; get; }
        ////public string SelectedTransaction { get; set; }



        public virtual ICollection<IdentityHolder> IdentityHolders { set; get; }
        ////public string SelectedIdentityHolder { get; set; }

        //public string FullName { get; set; }
        public IdentityHolder IdentityHolder { get; set; }
        public Account Account { get; set; }
        //public Transaction Transaction { get; set; }
        //// keuzes in dropdown
        public List<SelectListItem> Selection { set; get; }
        public string SelectedStatus { set; get; }
    }
        
}
