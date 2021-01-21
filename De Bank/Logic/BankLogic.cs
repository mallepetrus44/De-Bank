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
        ///                             Alle transacties ophalen voor account
        /// </summary>
        /// <param name="account">      Het account waarop gezocht moet worden           </param>
        /// <returns>                   Lijst ALLE van transacties van het account       </returns>
        public List<Transaction> GetAccountTransactions(IdentityHolder identityHolder)
        {
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
            Transactions = new List<Transaction>(identityHolder.Transactions);
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
    }
}
