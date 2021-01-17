using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bank.DAL.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public IdentityHolder IdentityHolder { get; set; }

        [Required]
        public string AccountTo { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        public float? TransactionAmount { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }    
        [Required]
        public bool IsPeriodic { get; set; }
        public int PeriodicTransactionFrequentyDays { get; set; }
        public int Frequenty { get; set; }
        public DateTime NextPayment { get; set; }
    }
}
