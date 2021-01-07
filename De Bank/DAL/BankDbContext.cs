using De_Bank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace De_Bank.DAL
{
    public class BankDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AccountHolder> AccountHolders { get; set; }

        //public DbSet<Bank> Bank { get; set; }
    }
}
