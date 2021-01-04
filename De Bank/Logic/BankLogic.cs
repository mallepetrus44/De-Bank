using De_Bank.DAL;
using De_Bank.Interfaces;
using De_Bank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace De_Bank.Logic
{
    public class BankLogic : IBank
    {
        private readonly BankDbContext db;

        //          De bank kan automatische betalingen doen om de bijv. 30 sec.
        public void DoAutoPayment()
        {
            
            throw new NotImplementedException();
        }


        //          De bank kan een selectie maken van alle saldo’s die onder een X bedrag staan.
        public void GetAmountsByAmount(double amount)
        {
            List<Account> Accounts = new List<Account>();

            foreach(var saldo in db.Accounts)
            {
               if (saldo.AccountBalance<=amount)
                {
                    Accounts.Add(saldo);
                }
            }
        }


        //          De bank kan per account een overzicht geven van alleen de positieve/negative transacties(van X sec.)
        public void GetDebetCreditBySeconds(int seconds, string account)
        {
            throw new NotImplementedException();
        }


        //          De bank kan per account een overzicht geven van de transacties de afgelopen X seconden
        public void GetTransactionBySeconds(int seconds, string account)
        {
            throw new NotImplementedException();
        }


        //          Er mogen meerdere users tegelijk van de zelfde rekening afschrijven. (Multi-Threading ).
        public void MultiUserSending()
        {
            throw new NotImplementedException();
        }


        //          De transactie moet gecanceld en terug gedraaid worden als er te weinig saldo is.
        public void ReTransact()
        {
            throw new NotImplementedException();
        }


        //          De bank Kan het geld van de ene user naar de andere sturen.
        public void SendMoney(double amount, string AccountA, string AccountB)
        {
            throw new NotImplementedException();
        }


        //          Deze transactie zal 5 seconden duren (gesimuleerd door een thread.sleep)
        public void Sleep()
        {
            throw new NotImplementedException();
        }


        //          De bank kan een overzicht opvragen van alle accounts gesorteerd op de voorkeur van de bank.
        public void SortAccounts(string searchstring)
        {
            throw new NotImplementedException();
        }
    }
}
