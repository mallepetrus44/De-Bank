using De_Bank.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.FrontEnd.Models
{
    public class IdentityHolder : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => MiddleName == null ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";
        public List<Account> Accounts { get; set; }
    }
}
