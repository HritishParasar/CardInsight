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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>().HasIndex(n => n.Name).IsUnique();
            modelBuilder.Entity<CreditCard>().Property(c => c.Category).HasMaxLength(10);
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
        }
    }
}
