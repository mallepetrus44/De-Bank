using System;
using System.Collections.Generic;
using System.Text;

namespace De_Bank.Models
{
    public class AccountHolder
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName); } }
        public List<Account> Accounts { get; set; }
    }
}
