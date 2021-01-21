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

        [Display(Name = "Van")]
        public string AccountFrom { get; set; }
        [Display(Name = "Naar")]
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
        [Display(Name = "Bedrag")]
        public float? TransactionAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Aanmaakdatum")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Transactiedatum")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }    
        [Required]
        [Display(Name = "Periodiek")]
        public bool IsPeriodic { get; set; }
        [Display(Name = "Termijn in dagen")]
        public int PeriodicTransactionFrequentyDays { get; set; }
        [Display(Name = "Aantal periodieke betalingen")]
        public int Frequenty { get; set; }
        [Display(Name = "Komende periodieke betaling")]
        public DateTime NextPayment { get; set; }
        public Status Status { get; set; }
    }
}
