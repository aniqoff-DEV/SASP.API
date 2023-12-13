using Microsoft.EntityFrameworkCore;
using SASP.API.Entities;

namespace SASP.API.Data
{
    public class SASPDbContext : DbContext
    {
        public DbSet<Issue> Issues {  get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<OrderHistory> OrderHistories { get; set; }

        public DbSet<Catalog> Catalog { get; set; }

        public DbSet<TypeIssue> TypeIssues { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Country { get; set; }

        public SASPDbContext(DbContextOptions<SASPDbContext> options) : base(options)
        {
            
        }
    }
}
