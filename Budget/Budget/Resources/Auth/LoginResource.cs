using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Resources.Auth
{
    public class LoginResource
    {
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
