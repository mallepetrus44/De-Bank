using System;
using System.Collections.Generic;
using System.Text;

namespace De_Bank.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        
        //account 1 is altijd debet
        public Account Account1 { get; set; }
        public double AmountAccount1Before { get; set; }
        public double AmountAccount1After { get; set; }
        

        //account 2 is altijd credit
        public Account Account2 { get; set; }
        public double AmountAccount2Before { get; set; }
        public double AmountAccount2After { get; set; }


        public double TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionId { get; set; }

        public bool AutoTransaction { get; set; }
        public int AutoTransactionFrequentyDays { get; set; }
    }
}
