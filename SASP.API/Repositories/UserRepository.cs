using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly SASPDbContext _context;

        public UserRepository(SASPDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateItem(User user)
        {
            if (user != null)
            {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
            }
            return null;
        }
        public async Task<IEnumerable<User>> GetItems()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetItemById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }
        public async Task<User> UpdateItem(int id, User user)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if(userToUpdate == null)
            {
                return null;
            }
            else
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.NumberPhone = user.NumberPhone;
                userToUpdate.CityId = user.CityId;
                userToUpdate.CountryId = user.CountryId;
                                
                await _context.SaveChangesAsync();

                return userToUpdate;
            }
        }

        public async Task<User> DeleteItem(int id)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (userToDelete == null)
            {
                return null;
            }

            _context.Users.Remove(userToDelete);

            await _context.SaveChangesAsync();
            return userToDelete;
        }
    }
}
