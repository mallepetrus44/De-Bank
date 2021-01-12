﻿using De_Bank.Models;
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
        public List<Transaction> Transactions { get; set; }
        /// <summary>
        ///                     Variabellen van het bankrekeningnummer
        /// </summary>
        /// <returns>           account met als voorloper "NL71LYMB"         </returns>
        /// 
        public string GetVar()
        {
            var prefix = "NL71" + "LYMB";
            return prefix;
        }


        /// <summary>
        ///                                 Het aanmaken van een account
        /// </summary>
        /// <param name="accountHolder">    Op basis van een aangemaakte AccountHolder     </param>
        /// <returns>                       De nieuw aangemaakte account                    </returns>
        public async Task<Account> CreateAccountAsync(AccountHolder accountHolder)
        {
            Account NewAccount = new Account();
            NewAccount.AccountBalance = 0;
            NewAccount.AccountHolder.FirstName = accountHolder.FirstName;
            NewAccount.AccountHolder.MiddleName = accountHolder.MiddleName;
            NewAccount.AccountHolder.LastName = accountHolder.LastName;
            NewAccount.AccountNumber = await Task.Run(() => GetNextAccountNumber(NewAccount));
            return NewAccount;
        }


        /// <summary>
        ///                             Een AccountHolder aanmaken
        /// </summary>
        /// <param name="firstname">    de voornaam van de nieuw aan te maken client / accountholder                    </param>
        /// <param name="middlename">   de EVENTUELE tussenvoegsels van de nieuw aan te maken client / accountholder    </param>
        /// <param name="lastname">     de achternaam van de nieuw aan te maken client / accountholder                  </param>
        /// <returns>                   een true of false  => op basis van een controle                                 </returns>
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


        /// <summary>
        ///                             volgende accountnummer ophalen
        /// </summary>
        /// <param name="account">      creëerd een uniek rekeningnummer voor een nieuwe account    </param>
        /// <returns>                   Het nieuwe unieke rekeningnummer                            </returns>
        public async Task<string> GetNextAccountNumber(Account account)
        {
            var prefix = await Task.Run(() => GetVar());
            var i = account.Id;
            var NewAccountNumber = prefix + i.ToString().PadLeft(9, '0');
           
            return NewAccountNumber;
        }



        /// <summary>
        ///                             De bank kan per account een overzicht geven van de transacties de afgelopen X seconden
        /// </summary>
        /// <param name="seconds">      Het aantal secondes waarop de bank/client wil zoeken                                    </param>
        /// <param name="account">      Het account waarop gezocht moet worden                                                  </param>
        /// <returns>                   Een lijst van transacties die voldoen aan de zoek voorwaarde(s)                         </returns>
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


        /// <summary>
        ///                                             Probeerseltje!!!
        /// </summary>
        /// <param name="account">      Het account waarop gezocht moet worden  </param>
        /// <param name="All">          Checkbox/Radiobutton in frontend        </param>
        /// <param name="AllDebet">     Checkbox/Radiobutton in frontend        </param>
        /// <param name="AllCredit">    Checkbox/Radiobutton in frontend        </param>
        /// <returns>                   Lijst van gevraagde transacties         </returns>
        public async Task GetAccountAsync(Account account, bool All, bool AllDebet, bool AllCredit) //radiobutton results
        {
          if(All)
            {
                List<Transaction> AllTransactions = await Task.Run(() => GetAccountTransactions(account));
            }
          if(AllDebet)
            {
                List<Transaction> AllTransactionsDebet = await Task.Run(() => GetAllDebetFromAccount(account));
            }
          if(AllCredit)
            {
                List<Transaction> AllTransactionsCredit = await Task.Run(() => GetAllCreditFromAccount(account));
            }      
            
        }


        /// <summary>
        ///                             Alle transacties ophalen voor account
        /// </summary>
        /// <param name="account">      Het account waarop gezocht moet worden           </param>
        /// <returns>                   Lijst ALLE van transacties van het account       </returns>
        public List<Transaction> GetAccountTransactions(Account account)
        {
            Transactions = new List<Transaction>(account.transactions);
            return Transactions;
        }


        /// <summary>
        ///                             Alle debit transacties ophalen van account
        /// </summary>
        /// <param name="account">      Het account waarop gezocht moet worden           </param>
        /// <returns>                   Lijst ALLE DEBET transacties van het account     </returns>
        public List<Transaction> GetAllDebetFromAccount(Account account)
        {
            Transactions = new List<Transaction>(account.transactions.Where(t => t.AccountTo.Id != account.Id));
            return Transactions;
        }

        /// <summary>
        ///                             Alle credit transacties ophalen van account
        /// </summary>
        /// <param name="account">      Het account waarop gezocht moet worden           </param>
        /// <returns>                   Lijst ALLE CREDIT transacties van het account    </returns>
        public List<Transaction> GetAllCreditFromAccount(Account account)
        {
            Transactions = new List<Transaction>(account.transactions.Where(t => t.AccountTo.Id == account.Id));
            return Transactions;
        }


        /// <summary>
        ///                             Alle saldo's ophalen boven bedrag X
        /// </summary>
        /// <param name="account">      List van alle accounts                                             </param>
        /// <returns>                   Lijst van alle saldo's die boven een bepaalde waarden zijn         </returns>
        public List<Account> GetAllBalancesAbove(List<Account> ListOfAllAccounts, int value)
        {
            List<Account> AllBalancesAbove = new List<Account>(ListOfAllAccounts.Where(i => i.AccountBalance >= value));
            return AllBalancesAbove;
        }

        /// <summary>
        ///                             Alle saldo's ophalen onder bedrag X
        /// </summary>
        /// <param name="account">      List van alle accounts                                             </param>
        /// <returns>                   Lijst van alle saldo's die onder een bepaalde waarden zijn         </returns>
        public List<Account> GetAllBalancesBelow(List<Account> ListOfAllAccounts, int value)
        {
            List<Account> AllBalancesBelow = new List<Account>(ListOfAllAccounts.Where(i => i.AccountBalance <= value));
            return AllBalancesBelow;
        }

        /// <summary>
        ///                             Maak een transactie aan
        /// </summary>
        /// <param name="account">      Het account waar vanuit de transactie plaats moet vinden        </param>
        /// <param name="transaction">  De transactie die uitgevoerd dient te worden                    </param>
        /// <returns>                   NOG HELEMAAL NIKS!!!!                                           </returns>
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


        /// <summary>
        ///                                 Controleer of er al een periodieke betaling bestaat
        /// </summary>
        /// <param name="transaction">      Ingevoerde transactie                                                           </param>
        /// <param name="account">          Betreffende de gebruikte account                                                </param>
        /// <returns>                       true or false => geeft aan of de transactie plaats kan vinden zonder melding 
        ///                                 voor een dubbele periodieke boeking                                             </returns>
        public bool CheckAutoTransaction(Transaction transaction, Account account)
        {
            DateTime now = DateTime.Now;

            Transactions = new List<Transaction>(account.transactions.Where(f => f.PeriodicPayment).Where(a => a.AccountTo.Id == transaction.AccountTo.Id));

            int counter = 0;

            foreach (var item in Transactions)
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
