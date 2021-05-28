using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Budget.Data.Entities
{
    public class UserToken:BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Token { get; set; }
        public User User { get; set; }
    }
}
