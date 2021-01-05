using De_Bank.DAL;
using De_Bank.Interfaces;
using De_Bank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace De_Bank.Logic
{
    public class BankLogic 
    {
        private readonly BankDbContext db;

        //De bank kan per account een overzicht geven van de transacties de afgelopen X seconden
        public static Task<List<Transaction>> GetTransaction(Account account, int seconds)
        {

            DateTime referenceDate = DateTime.Now.AddSeconds(-seconds);
            List<Transaction> transactions = new List<Transaction>();

            foreach(var trans in transactions)
            {
                if (trans.TransactionDate >= referenceDate)
                {
                    
                    return null;
                }
            }
            return null;
        }
    }
}
