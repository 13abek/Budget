using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Budget.Data.Enums;

namespace Budget.Data.Entities
{
    public class Account:BaseEntity
    {
        [Required]
       
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public AccountType Type { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public Decimal Balance { get; set; }
    }
}
