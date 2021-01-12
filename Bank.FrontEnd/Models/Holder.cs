using De_Bank.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.FrontEnd.Models
{
    public class Holder : IdentityUser
    {
        public int HolderId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName); } }
        public List<Account> Accounts { get; set; }
    }
}
