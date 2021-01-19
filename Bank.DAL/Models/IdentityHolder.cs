using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.DAL.Models
{
    public class IdentityHolder : IdentityUser
    {
        public IdentityHolder()
        {
            Accounts = new HashSet<Account>();
            SavedAccounts = new HashSet<SavedAccount>();
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid IdentityHolderID { get; set; }

        [Required]
        public string FirstName { get; set; }

        //[Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName => MiddleName == null ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<SavedAccount> SavedAccounts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
