using Microsoft.EntityFrameworkCore;
using SASP.API.Entities;

namespace SASP.API.Data
{
    public class AdminSASPDbContext : DbContext
    {
        public DbSet<Admin> Admins {  get; set; }

        public AdminSASPDbContext(DbContextOptions<AdminSASPDbContext> options) : base(options)
        {

        }
    }
}
