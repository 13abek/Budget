using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Budget.Data.Entities;

namespace Budget.Data
{
    public class BudgetDbContext:DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext>options):base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
