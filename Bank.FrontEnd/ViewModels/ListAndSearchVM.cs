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

        public List<Account> Accounts { set; get; }
        ////public Account SelectedAccount { get; set; }



        public List<Transaction> Transactions { set; get; }
        ////public string SelectedTransaction { get; set; }



        public List<IdentityHolder> IdentityHolders { set; get; }
        ////public string SelectedIdentityHolder { get; set; }






        //// keuzes in dropdown
        public List<SelectListItem> Selection { set; get; }
        public string SelectedStatus { set; get; }
        public IdentityHolder IndentityHolder { get; set; }
        public Account Account { get; set; }
        public Transaction transaction { get; set; }
    }
}
