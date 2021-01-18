using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bank.DAL.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public bool AccountLock { get; set; }

        [DataType(DataType.Currency)]
        public float? AccountBalance { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name ="Account Limiet")]
        public float? AccountMinimum { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        public IdentityHolder IdentityHolder { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
