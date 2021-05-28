using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Budget.Data.Entities
{
    public class User:BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Fullname { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(150)]
        public string Password { get; set; }
        public ICollection<UserToken> Tokens { get; set; }
        public ICollection<Account> Accounts  { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
