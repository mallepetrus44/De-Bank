using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace De_Bank.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public double AccountMinimum { get; set; }
        public virtual AccountHolder AccountHolder { get; set; }
        public virtual List<Transaction>Transactions { get; set; }

    }
}
