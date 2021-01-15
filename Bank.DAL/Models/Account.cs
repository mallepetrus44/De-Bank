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

        [Column(TypeName = "decimal(18, 2)")] //max = 999999999999999999,99
        public decimal AccountBalance { get; set; }

        [Column(TypeName = "decimal(18, 2)")] //max = 999999999999999999,99
        public decimal AccountMinimum { get; set; }
        [Required]
        public Choice AccountLimiet { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        public IdentityHolder IdentityHolder { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
