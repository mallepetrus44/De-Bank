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

        public List<Transaction> GetAllDebitFromAccount(Account account)
        {
            List<Transaction> AllTransactionsDebet = new List<Transaction>();

            foreach (var item in account.Transactions.Where(i => i.Account1.Id == account.Id)) //debet
            {
                AllTransactionsDebet.Add(item);
            }

            return AllTransactionsDebet;
        }


        public List<Transaction> GetAllCreditFromAccount(Account account)
        {
            List<Transaction> AllTransactionsCredit = new List<Transaction>();

            foreach (var item in account.Transactions.Where(i => i.Account2.Id == account.Id)) //credit
            {
                AllTransactionsCredit.Add(item);               
            }

            return AllTransactionsCredit;
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

        public Task<Transaction> CreateTransaction(Account account1, Account account2,bool auto, int frequenty, 
                                                                        double amount)
        {
            Transaction transaction = new Transaction();
            {
                transaction.Account1 = account1;
                transaction.AmountAccount1Before = account1.AccountBalance;
                transaction.AmountAccount1After = account1.AccountBalance - amount;

                transaction.Account2 = account2;
                transaction.AmountAccount2Before = account2.AccountBalance;
                transaction.AmountAccount2After = account2.AccountBalance + amount;

                transaction.TransactionAmount = amount;
                transaction.TransactionDate = DateTime.Now;

                transaction.AutoTransaction = auto; //bool
                transaction.AutoTransactionFrequentyDays = frequenty;
                
            }
            var result = CheckAutoTransaction(transaction);
            if(result)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();               
            }
            return null;


        }

        private bool CheckAutoTransaction(Transaction transaction)
        {
            DateTime now = DateTime.Now;

            List<Transaction> transactions = new List<Transaction>();

            foreach(var item in db.Transactions.Where(a => a.Account1 == transaction.Account1).Where(t => t.Account2 == transaction.Account2).Where(f => f.AutoTransaction))
            {
                if(item.TransactionDate >= now.AddDays(-item.AutoTransactionFrequentyDays))
                {
                    transactions.Add(item);;
                }
            }
            if(transactions.Count >=0)
            {
                //er bestaat al een periodieke betaling
                return false;
            }
            return true;
        }

        public Task<Transaction> TransactionModel()
        {
            return null;
        }
    }
}
