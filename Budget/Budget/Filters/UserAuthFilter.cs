using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Data;
using Budget.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace Budget.Filters
{
    public class UserAuthFilter:ActionFilterAttribute
    {
        private readonly BudgetDbContext _context;
        public UserAuthFilter(BudgetDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context) 
        {
            bool hasHeader = context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues token); 

            if (!hasHeader)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            User user = _context.Users.Include("Tokens").FirstOrDefault(u => u.Tokens.Any(t => t.Token == token.ToString()));

            if (user==null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.RouteData.Values["User"] = user;

            base.OnActionExecuting(context);
        }

    }
}
