using CardInsight.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CardInsight.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<CreditCard> CreditCards { get; set; } 
        public DbSet<User> Users { get; set; }
    }
}
