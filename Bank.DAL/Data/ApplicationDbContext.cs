using Bank.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityHolder>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<IdentityHolder>(entity =>
            {
                entity.ToTable(name: "IdentityHolder");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            builder.Entity<Transaction>()
  .HasData(
   new Transaction { Id = 1, TransactionAmount = 50, TransactionDate = DateTime.Now, CreationDate = DateTime.Now, AccountFrom = "Jaap", AccountTo = "Piet", NextPayment = DateTime.Now },
   new Transaction { Id = 2, TransactionAmount = 75, TransactionDate = DateTime.Now.AddMinutes(10), CreationDate = DateTime.Now.AddMinutes(10), AccountFrom = "Klaas", AccountTo = "Henk", NextPayment = DateTime.Now.AddMinutes(10) },
   new Transaction { Id = 3, TransactionAmount = 100, TransactionDate = DateTime.Now.AddMinutes(20), CreationDate = DateTime.Now.AddMinutes(20), AccountFrom = "Joop", AccountTo = "Henny", NextPayment = DateTime.Now.AddMinutes(20) });
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<SavedAccount> SavedAccounts { get; set; }

        public DbSet<IdentityHolder> IdentityHolders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
