using Bank.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace De_Bank.Logic
{
    public class BankLogic
    {
        private readonly object balanceLock = new object();
        //private readonly ClaimsPrincipal User;

        public List<Transaction> Transactions { get; set; }
        public ClaimsPrincipal User { get; private set; }


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

        //OVERBODIG

        //public async Task<Account> CreateAccountAsync(IdentityHolder user, AccountType accountType, int limiet, int id)
        //{
        //    Account NewAccount = new Account
        //    {
        //        AccountMinimum = limiet,
        //        AccountType = accountType,
        //        AccountBalance = 0,
        //        AccountLock = false,
        //        IdentityHolder = user,
        //    };                    
        //    NewAccount.AccountNumber = await Task.Run(() => GetNextAccountNumber(id));
        //    return NewAccount;
        //}


        /// <summary>
        ///                             Een AccountHolder aanmaken
        /// </summary>
        /// <param name="firstname">    de voornaam van de nieuw aan te maken client / accountholder                    </param>
        /// <param name="middlename">   de EVENTUELE tussenvoegsels van de nieuw aan te maken client / accountholder    </param>
        /// <param name="lastname">     de achternaam van de nieuw aan te maken client / accountholder                  </param>
        /// <returns>                   een true of false  => op basis van een controle                                 </returns>
        //        public bool CreateAccountHolder(string firstname, string middlename, string lastname) /// TODO checken nog niet goed! en niet meer nodig
        //        {
        //            if (!string.IsNullOrEmpty(middlename)
        //)
        //            {
        //                if (firstname != "" || lastname != "" || firstname != null || lastname != null)
        //                {
        //                    IdentityHolder NewAccountHolder = new IdentityHolder
        //                    {
        //                        FirstName = firstname,
        //                        MiddleName = middlename,
        //                        LastName = lastname
        //                    };
        //                    return true;
        //                }             
        //            }
        //            return false;
        //        }


        /// <summary>
        ///                             volgende accountnummer ophalen
        /// </summary>
        /// <param name="account">      creëerd een uniek rekeningnummer voor een nieuwe account    </param>
        /// <returns>                   Het nieuwe unieke rekeningnummer                            </returns>
        public async Task<string> GetNextAccountNumber(int id)
        {
            var prefix = await Task.Run(() => GetVar());
            var i = id +1;
            var NewAccountNumber = prefix + i.ToString().PadLeft(9, '0');
           
            return NewAccountNumber;
        }


        /// <summary>
        ///                             De bank kan per account een overzicht geven van de transacties de afgelopen X seconden
        /// </summary>
        /// <param name="seconds">      Het aantal secondes waarop de bank/client wil zoeken                                    </param>
        /// <param name="account">      Het account waarop gezocht moet worden                                                  </param>
        /// <returns>                   Een lijst van transacties die voldoen aan de zoek voorwaarde(s)                         </returns>
        public async Task<List<Transaction>> GetDataForSeconds(int seconds, IdentityHolder identityHolder)
        {
            //Tijdmarkering berekenen
            DateTime referenceDate = DateTime.Now.AddSeconds(-seconds);

            //verzamel alle transacties uit de db die dit account.id bevatten
            IEnumerable<Transaction> AllTransActions = await Task.Run(() => GetAccountTransactions(identityHolder)
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
        public async Task<List<Transaction>> GetAccountAsync(IdentityHolder identityHolder/*, bool All, bool AllDebet, bool AllCredit*/) //radiobutton results => of checkboxes
        {
            //List<Transaction> AllTransactions = await Task.Run(() => GetAccountTransactions(identityHolder));
            return await Task.Run(() => GetAccountTransactions(identityHolder));

          //  if (All)
          //  {
          //      List<Transaction> AllTransactions = await Task.Run(() => GetAccountTransactions(identityHolder));
          //  }
          //if(AllDebet)
          //  {
          //      List<Transaction> AllTransactionsDebet = await Task.Run(() => GetAllDebetFromAccount(identityHolder));
          //  }
          //if(AllCredit)
          //  {
          //      List<Transaction> AllTransactionsCredit = await Task.Run(() => GetAllCreditFromAccount(identityHolder));
          //  }      
            
        }


        /// <summary>
        ///                             Alle transacties ophalen voor account
        /// </summary>
        /// <param name="account">      Het account waarop gezocht moet worden           </param>
        /// <returns>                   Lijst ALLE van transacties van het account       </returns>
        public List<Transaction> GetAccountTransactions(IdentityHolder identityHolder)
        {
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //IdentityHolder identityHolder = new IdentityHolder
            //{
            //    Id = currentUserID
            //};
            Transactions = new List<Transaction>(identityHolder.Transactions);
            return Transactions;
        }



       
        /// <summary>
        ///                             Alle debit transacties ophalen van account
        /// </summary>
        /// <param name="account">      Het account waarop gezocht moet worden           </param>
        /// <returns>                   Lijst ALLE DEBET transacties van het account     </returns>
        public List<Transaction> GetAllDebetFromAccount(IdentityHolder identityHolder)
        {
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            Transactions = new List<Transaction>(identityHolder.Transactions);
            return Transactions;
        }


        /// <summary>
        ///                             Alle credit transacties ophalen van account
        /// </summary>
        /// <param name="account">      Het account waarop gezocht moet worden           </param>
        /// <returns>                   Lijst ALLE CREDIT transacties van het account    </returns>
        //public List<Transaction> GetAllCreditFromAccount(IdentityHolder identityHolder)
        //{
        //    ClaimsPrincipal currentUser = this.User;
        //    var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    Transactions = new List<Transaction>(identityHolder.Transactions.Where(t => t.AccountTo.Id == identityHolder.Id));
        //    return Transactions;
        //}


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
        //public async Task<Transaction> CreateTransactionAsync(Account account, Transaction transaction)
        //{

        //        //var result = await Task.Run(() => CheckAutoTransaction(transaction, account));
        //        if (result)
        //        {

        //            //haal amount van account 1
        //            if (transaction.TransactionAmount < 0)
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(transaction.TransactionAmount), "Het bedrag dient hoger dan 0 te zijn.");
        //            }

        //            float? appliedAmount = 0;

        //            lock (balanceLock)
        //            {
        //                if (account.AccountBalance >= transaction.TransactionAmount)
        //                {
        //                    account.AccountBalance -= transaction.TransactionAmount;
        //                    appliedAmount = transaction.TransactionAmount;
        //                }
        //            }
                    
        //            //stort amount op account 2
        //            if (transaction.TransactionAmount < 0)
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(transaction.TransactionAmount), "Het bedrag dient hoger dan 0 te zijn.");
        //            }

        //            lock (balanceLock)
        //            {
        //                transaction.AccountTo.Transactions.AccountBalance += transaction.TransactionAmount;
        //            }

        //            return transaction;
        //        }

        //        // er bestaat al een perodieke transactie! ->> laat zien ->> vraag : toch uitvoeren?
        //        return transaction; // <======== nog niet goed          
        //}          


        /// <summary>
        ///                                 Controleer of er al een periodieke betaling bestaat
        /// </summary>
        /// <param name="transaction">      Ingevoerde transactie                                                           </param>
        /// <param name="account">          Betreffende het account                                                         </param>
        /// <returns>                       true or false => geeft aan of de transactie plaats kan vinden zonder melding 
        ///                                 voor een dubbele periodieke boeking                                             </returns>
        //public bool CheckAutoTransaction(Transaction transaction, Account account)
        //{
        //    DateTime now = DateTime.Now;

        //    Transactions = new List<Transaction>(account.Transactions.Where(f => f.IsPeriodic).Where(a => a.AccountTo.Id == transaction.AccountTo.Id));

        //    int counter = 0;

        //    foreach (var item in Transactions)
        //    {               
        //            counter += 1;                  
        //    }
        //    if (counter>=0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}


       
    }
}
