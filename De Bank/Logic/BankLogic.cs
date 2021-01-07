using De_Bank.DAL;
using De_Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace De_Bank.Logic
{
    public class BankLogic
    {

        // db is DbContext
        private readonly BankDbContext db = new BankDbContext();


        //De bank kan per account een overzicht geven van de transacties de afgelopen X seconden
        private async Task<List<Transaction>> GetDataForSeconds(int seconds, Account account)
        {
            //Tijdmarkering berekenen
            DateTime referenceDate = DateTime.Now.AddSeconds(-seconds);

            //verzamel alle transacties uit de db die dit account.id bevatten
            IEnumerable<Transaction> AllTransActions = await Task.Run(() => GetAccountTransactions(account)
                                                                 .Where(a => a.TransactionDate >= referenceDate));
            //return voledige 'output'
            return AllTransActions.ToList();
        }

        private async Task GetAccountAsync(Account account)
        {
            List<Transaction> AllTransactions = await Task.Run(() => GetAccountTransactions(account));
            List<Transaction> AllTransactionsDebet = await Task.Run(() => GetAllDebetFromAccount(account));
            List<Transaction> AllTransactionsCredit = await Task.Run(() => GetAllCreditFromAccount(account));
        }

        // Alle transacties ophalen voor account
        private List<Transaction> GetAccountTransactions(Account account)
        {
            List<Transaction> AllTransactions = new List<Transaction>();

            foreach(var item in account.Transactions)
            {
                AllTransactions.Add(item);
            }
            return AllTransactions;
        }


        // Alle debit transacties ophalen van account
        private List<Transaction> GetAllDebetFromAccount(Account account)
        {
            List<Transaction> AllTransactionsDebet = new List<Transaction>();

            foreach (var item in account.Transactions.Where(i => i.Account1.Id == account.Id)) //debet
            {
                AllTransactionsDebet.Add(item);
            }

            return AllTransactionsDebet;
        }


        // Alle credit transacties ophalen van account
        private List<Transaction> GetAllCreditFromAccount(Account account)
        {
            List<Transaction> AllTransactionsCredit = new List<Transaction>();

            foreach (var item in account.Transactions.Where(i => i.Account2.Id == account.Id)) //credit
            {
                AllTransactionsCredit.Add(item);               
            }

            return AllTransactionsCredit;
        }
 

        // Alle saldo's ophalen boven bedrag X
        private List<Account> GetAllBalancesAbove(int? value)
        {
            List<Account> AllBalancesAbove = new List<Account>();

            foreach (var item in db.Accounts.Where(i => i.AccountBalance >= value))
            {
                AllBalancesAbove.Add(item);
            }

            return AllBalancesAbove;
        }


        // Alle saldo's ophalen onder bedrag X
        private List<Account> GetAllBalancesBelow(int? value)
        {
            List<Account> AllBalancesBelow = new List<Account>();

            foreach (var item in db.Accounts.Where(i => i.AccountBalance <= value))
            {
                AllBalancesBelow.Add(item);
            }

            return AllBalancesBelow;
        }

        // Maak een transactie aan
        private async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            var result = await Task.Run(() => CheckAutoTransaction(transaction));
            if (result)
            {
                //haal amount van account 1
                transaction.Account1.AccountBalance = transaction.Account1.AccountBalance - transaction.TransactionAmount;
                //wacht 2,5 seconden sync
                Thread.Sleep(2500);
                //stort amount op account 2
                transaction.Account2.AccountBalance = transaction.Account2.AccountBalance + transaction.TransactionAmount;
                //wacht 5 seconden async
                await Task.Delay(5000);
                //doorvoeren transactie
                db.Transactions.Add(transaction);
                db.SaveChanges();
            }


            // TODO: er bestaat al een perodieke transactie! ->> laat zien ->> vraag : toch uitvoeren?
            return null;


        }


        // Controleer of er al een periodieke betaling bestaat
        private bool CheckAutoTransaction(Transaction transaction)
        {
            DateTime now = DateTime.Now;

            List<Transaction> transactions = new List<Transaction>();

            int counter = 0;

            //filter uit lijst op account 1, account 2, auto = true
            foreach(var item in db.Transactions.Where(a => a.Account1 == transaction.Account1).Where(t => t.Account2 == transaction.Account2).Where(f => f.AutoTransaction))
            {
                // bepaal of er een kans bestaat op een dubbele periodieke boeking
                if(now <= item.TransactionDate.AddDays(item.AutoTransactionFrequentyDays))
                {
                    counter += 1;
                }                   
            }
            if (counter>=0)
            {
                return false;
            }
            return true;
        }
       
    }
}
