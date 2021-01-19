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
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public Guid TransactionID { get; set; }

        //[Required]
        //public Guid AccountToID { get; set; }
        //[Required]
        //public Guid AccountFromID { get; set; }

        //[Required]
        //public Guid IdentityHolderToID { get; set; }
        //[Required]
        //public Guid IdentityHolderFromID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float? TransactionAmount { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }    
        [Required]
        public bool IsPeriodic { get; set; }
        public int PeriodicTransactionFrequentyDays { get; set; }
        public int Frequenty { get; set; }
        public DateTime NextPayment { get; set; }
    }
}
