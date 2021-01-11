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
        //private object accountlock = new object();

        //standaard data account 1 = NL71LYMB000000001
        public string GetVar()
        {
            var prefix = "NL71" + "LYMB";
            return prefix;
        }


        //Een account aanmaken
        public async Task<Account> CreateAccountAsync(AccountHolder accountHolder)
        {
            Account NewAccount = new Account();
            NewAccount.AccountBalance = 0;
            NewAccount.AccountHolder.FirstName = accountHolder.FirstName;
            NewAccount.AccountHolder.MiddleName = accountHolder.MiddleName;
            NewAccount.AccountHolder.LastName = accountHolder.LastName;
            NewAccount.AccountNumber = await Task.Run(() => GetNextAccountNumber(NewAccount));
            //db.Accounts.Add(NewAccount);
            //db.SaveChanges();
            return NewAccount;
        }


        // een random bankrekeningnummer maken
        public bool CreateAccountHolder(string firstname, string middlename, string lastname)
        {
            if(middlename ==null)
            {
                middlename = "";
            }

            if (firstname != "" || lastname != "" || firstname != null || lastname !=null)
            {
                AccountHolder NewAccountHolder = new AccountHolder();
                NewAccountHolder.FirstName = firstname;
                NewAccountHolder.MiddleName = middlename;
                NewAccountHolder.LastName = lastname;
                //db.AccountHolders.Add(NewAccountHolder);
                //db.SaveChanges();
                return true;
            }
            return false;
        }
       

        //volgende accountnummer ophalen
        public async Task<string> GetNextAccountNumber(Account account)
        {
            var prefix = await Task.Run(() => GetVar());
            var i = account.Id;
            var NewAccountNumber = prefix + i.ToString().PadLeft(9, '0');
           
            return NewAccountNumber;
        }



        //De bank kan per account een overzicht geven van de transacties de afgelopen X seconden
        public async Task<List<Transaction>> GetDataForSeconds(int seconds, Account account)
        {
            //Tijdmarkering berekenen
            DateTime referenceDate = DateTime.Now.AddSeconds(-seconds);

            //verzamel alle transacties uit de db die dit account.id bevatten
            IEnumerable<Transaction> AllTransActions = await Task.Run(() => GetAccountTransactions(account)
                                                                 .Where(a => a.TransactionDate >= referenceDate));
            //return voledige 'output'
            return AllTransActions.ToList();
        }

        //public async Task GetAccountAsync(Account account)
        //{
        //    List<Transaction> AllTransactions = await Task.Run(() => GetAccountTransactions(account));
        //    List<Transaction> AllTransactionsDebet = await Task.Run(() => GetAllDebetFromAccount(account));
        //    List<Transaction> AllTransactionsCredit = await Task.Run(() => GetAllCreditFromAccount(account));
        //}


        // Alle transacties ophalen voor account
        public List<Transaction> GetAccountTransactions(Account account)
        {
            List<Transaction> Alltransactions = new List<Transaction>(account.transactions);
            return Alltransactions;
        }


        // Alle debit transacties ophalen van account
        public List<Transaction> GetAllDebetFromAccount(Account account)
        {
            List<Transaction> AllTransactionsDebet = new List<Transaction>(account.transactions.Where(t => t.AccountTo.Id != account.Id));
            return AllTransactionsDebet;
        }

        // Alle credit transacties ophalen van account
        public List<Transaction> GetAllCreditFromAccount(Account account)
        {
            List<Transaction> AllTransactionsCredit = new List<Transaction>(account.transactions.Where(t => t.AccountTo.Id == account.Id));
            return AllTransactionsCredit;
        }

        // Alle saldo's ophalen boven bedrag X
        //public List<Account> GetAllBalancesAbove(int value)
        //{
        //    List<Account> AllBalancesAbove = new List<Account>(db.Accounts.Where(i => i.AccountBalance >= value));
        //    return AllBalancesAbove;
        //}

        //// Alle saldo's ophalen onder bedrag X
        //public List<Account> GetAllBalancesBelow(int value)
        //{
        //    List<Account> AllBalancesBelow = new List<Account>(db.Accounts.Where(i => i.AccountBalance <= value));
        //    return AllBalancesBelow;
        //}

        // Maak een transactie aan
        public async Task<Transaction> CreateTransactionAsync(Account account, Transaction transaction)
        {           
            //blz 250 lock doornemen

            //bedrag van overboeking mag niet onder 0 zijn
            if (transaction.TransactionAmount >= 0 && account.AccountLock == false && transaction.AccountTo.AccountLock == false)
            {
                if (transaction.PeriodicPayment == true)
                {
                    var result = await Task.Run(() => CheckAutoTransaction(transaction, account));
                    if (result)
                    {

                        //accounts lock
                        account.AccountLock = true;
                        transaction.AccountTo.AccountLock = true;

                        //haal amount van account 1
                        account.AccountBalance = account.AccountBalance - transaction.TransactionAmount;
                        //wacht 2,5 seconden sync
                        Thread.Sleep(2500);
                        //stort amount op account 2
                        transaction.AccountTo.AccountBalance = transaction.AccountTo.AccountBalance + transaction.TransactionAmount;

                        //account unlock
                        account.AccountLock = false;
                        transaction.AccountTo.AccountLock = false;

                        //wacht 5 seconden async
                        await Task.Delay(5000);
                        //doorvoeren transactie

                        //db.Transactions.Add(transaction);
                        //db.SaveChanges();
                    }
                    // TODO: er bestaat al een perodieke transactie! ->> laat zien ->> vraag : toch uitvoeren?
                }

            }
            // TODO:amount = 0 of accounts zijn gelocked

            return null;
        }


        // Controleer of er al een periodieke betaling bestaat
        public bool CheckAutoTransaction(Transaction transaction, Account account)
        {
            DateTime now = DateTime.Now;

            List<Transaction> transactions = new List<Transaction>();

            int counter = 0;

            //filter uit lijst op account 1, account 2, auto = true

            foreach (var item in account.transactions.Where(f => f.PeriodicPayment).Where(a => a.AccountTo.Id == transaction.AccountTo.Id))
            {               
                    counter += 1;                  
            }
            if (counter>=0)
            {
                return false;
            }
            return true;
        }
       
    }
}
