using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bank.DAL.Models
{
    public class SavedAccount
    {
        public int Id { get; set; }

        [Display(Name = "AccountNummer")]
        public string BankAccount { get; set; }
    }
}
