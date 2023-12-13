using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SASPDbContext _context;

        public AdminRepository(SASPDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> CreateAdmin(Admin admin)
        {
            if (admin != null)
            {
                var result = await _context.Admins.AddAsync(admin);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Admin> GetAdminById(int id)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Admin> UpdateAdmin(int id, Admin admin)
        {
            var adminToUpdate = await _context.Admins.FirstOrDefaultAsync(a => a.Id == id);

            if (adminToUpdate == null)
            {
                return null;
            }

            adminToUpdate.Name = admin.Name;
            adminToUpdate.Password = admin.Password;

            await _context.SaveChangesAsync();

            return adminToUpdate;
        }
    }
}
