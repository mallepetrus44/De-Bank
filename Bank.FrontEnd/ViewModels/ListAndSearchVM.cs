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
        public string IndentityHolderID { get; set; }



        public IEnumerable<Account> Accounts { set; get; }
        public Account SelectedAccount { get; set; }



        public IEnumerable<Transaction> Transactions { set; get; }
        public string SelectedTransaction { get; set; }



        public IEnumerable<IdentityHolder> IdentityHolders { set; get; }
        public string SelectedIdentityHolder { get; set; }


        // keuzes in dropdown
        public IEnumerable<SelectListItem> Selection { set; get; }
        public string SelectedStatus { set; get; }
    }
}
