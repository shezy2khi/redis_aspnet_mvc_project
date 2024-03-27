using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace CacheRedisASP.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Column(TypeName = "nvarchar(14)")]
        [DisplayName("Account Number")]
        [Required(ErrorMessage = "Account Number must be required...")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Pls provide valid 14 digit Account no")]
        public string AccountNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Banificiary Name")]
        [Required(ErrorMessage = "This field is reuired!")]
        public string BanificiaryName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Bank Name")]
        [Required]
        public string BankName { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("SWIFT Code")]
        [Required]
        [MaxLength(11, ErrorMessage = "Maximum length is upto 11 characters")]
        public string SWIFTCode { get; set; }

        [Required]
        //[MaxLength(14)]
        public int Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd-yy}")]
        public DateTime Date { get; set; }

        


    }

}
 
