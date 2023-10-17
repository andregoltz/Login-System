using Login_System.Core.Contexts.AccountContext.Entities;
using Login_System.Infra.Contexts.AccountContext.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Login_System.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
