using Budget.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Data.Entities
{
    public class Transaction:BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [ForeignKey("ToAccount")]
        public int? ToAccountId { get; set; }
        public Account ToAccount { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [Column(TypeName ="money")]
        public decimal Amount { get; set; }
        [Required]
        public TransactionType Type { get; set; }
        [Required]
        [MaxLength(500)]
        public string Note { get; set; }

    }
}
