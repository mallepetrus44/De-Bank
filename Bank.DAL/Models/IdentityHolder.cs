﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DAL.Models
{
    public class IdentityHolder : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        //[Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName => MiddleName == null ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

        public virtual List<Account> Accounts { get; set; }
        public virtual List<SavedAccount> SavedAccounts { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
