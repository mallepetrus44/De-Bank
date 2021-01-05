using De_Bank.DAL;
using De_Bank.Interfaces;
using De_Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Bank.Logic
{
    public class BankLogic
    {

        // db is DbContext
        private readonly BankDbContext db = new BankDbContext();

        //De bank kan per account een overzicht geven van de transacties de afgelopen X seconden
        private List<Transaction> GetDataForSeconds(int seconds, Account account)
        {
            //Tijdmarkering berekenen
            DateTime referenceDate = DateTime.Now.AddSeconds(-seconds);


            //verzamel alle transacties uit de db die dit account.id bevatten
            var AllTransActions = GetAllTransactionsFromAccount(account)
                                  .Where(a => a.TransactionDate >= referenceDate);


            //return voledige 'output'
            return AllTransActions.ToList();
        }

        
        public List<Transaction> GetAllTransactionsFromAccount(Account account)
        {
            List<Transaction> AllTransactions = new List<Transaction>();

            foreach(var item in account.Transactions)
            {
                AllTransactions.Add(item);
            }

            return AllTransactions;
        }

        public List<Transaction> GetAllCreditFromAccount(Account account)
        {
            List<Transaction> AllTransactionsCredit = new List<Transaction>();

            foreach (var item in account.Transactions.Where(i => i.PlusAccount2 = true))
            {
                AllTransactionsCredit.Add(item);
            }

            return AllTransactionsCredit;
        }

        public List<Transaction> GetAllDebitFromAccount(Account account)
        {
            List<Transaction> AllTransactionsDebet = new List<Transaction>();

            foreach (var item in account.Transactions.Where(i => i.MinusAccount1 = true))
            {
                AllTransactionsDebet.Add(item);
            }

            return AllTransactionsDebet;
        }


        public List<Account> GetAllBalancesAbove(int? value)
        {
            List<Account> AllBalancesAbove = new List<Account>();

            foreach (var item in db.Accounts.Where(i => i.AccountBalance >= value))
            {
                AllBalancesAbove.Add(item);
            }

            return AllBalancesAbove;
        }


        public List<Account> GetAllBalancesBelow(int? value)
        {
            List<Account> AllBalancesBelow = new List<Account>();

            foreach (var item in db.Accounts.Where(i => i.AccountBalance <= value))
            {
                AllBalancesBelow.Add(item);
            }

            return AllBalancesBelow;
        }
    }
}
