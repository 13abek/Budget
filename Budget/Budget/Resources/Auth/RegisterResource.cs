using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Budget.Resources.Auth
{
    public class RegisterResource
    {
        [Required]
        [MaxLength(50)]
        public string Fullname { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(150)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
