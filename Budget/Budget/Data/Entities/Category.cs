using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Data.Entities
{
    public class Category:BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int OrderBy { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Icon { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
