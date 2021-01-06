using De_Bank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace De_Bank.Interfaces
{
   public interface IBank
    {
        //        Schrijf een applicatie die bank transacties simuleert, alle acties worden uitgevoerd door unit-testing.
        //          De applicatie zal een Class Library Applicatie worden met als target framework.NET Core 3.1.
        //          Gebruik code-first Enity-framework om je database te genereren.
        //          De unittest zal de automatische betaling zal moeten starten, 4 keer laten uitvoeren en dan weer stoppen.


        //De bank kan per account een overzicht geven van de transacties de afgelopen X seconden
        IEnumerable<Transaction> GetTransactionBySeconds(int seconds);

        //          De bank kan per account een overzicht geven van alleen de positieve/negative transacties(van X sec.)
        IEnumerable<Transaction> GetDebetCreditBySeconds(int seconds, string selectedAccount);

        //          De bank kan een overzicht opvragen van alle accounts gesorteerd op de voorkeur van de bank.
        IEnumerable<Account> SortAccounts();

        //          De bank kan een selectie maken van alle saldo’s die onder een X bedrag staan.
        Account GetAmountsByAmount(double amount);

        //          De bank kan automatische betalingen doen om de bijv. 30 sec.
        Transaction DoAutoPayment(Account accountA, Account accountB, double amount, int seconds);

        //          De bank Kan het geld van de ene user naar de andere sturen.
        Transaction SendMoney(Account accountA, Account accountB, double amount, int seconds);

        //          Deze transactie zal 5 seconden duren (gesimuleerd door een thread.sleep)
        void Sleep();

        //          De transactie moet gecanceld en terug gedraaid worden als er te weinig saldo is.
        Transaction ReTransact();
    }


}
