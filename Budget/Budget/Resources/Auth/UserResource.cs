using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Resources.Auth
{
    public class UserResource
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
