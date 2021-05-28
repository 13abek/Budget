using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Data;
using Budget.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace Budget.Libs
{
    public interface IUserAuth
    {
        User user { get; }
    }

    public class UserAuth : IUserAuth
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly BudgetDbContext _context;
        public UserAuth(BudgetDbContext context,IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _context = context;
        }
        public User user
        {
            get{
                bool hasHeader = this._accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues token);
                if (!hasHeader)
                {
                    return null;
                }

                var user = _context.Users.Include("Tokens").FirstOrDefault(u => u.Tokens.Any(t => t.Token == token.ToString()));

                if (user!=null)
                {
                    return user;
                }
                return null;
            }
        }
    }
}
