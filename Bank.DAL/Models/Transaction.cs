using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bank.DAL.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public Account AccountTo { get; set; }


        [Required]
        [Column(TypeName = "decimal(18, 2)")] //max = 999999999999999999,99
        public decimal TransactionAmount { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }


        public bool PeriodicPayment { get; set; }
        public int PeriodicTransactionFrequentyDays { get; set; }
    }
}
