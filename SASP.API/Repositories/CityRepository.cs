using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class CityRepository : IRepository<City>
    {
        private readonly SASPDbContext _context;

        public CityRepository(SASPDbContext context)
        {
            _context = context;
        }
        public async Task<City> CreateItem(City city)
        {
            if (city != null)
            {
                var result = await _context.Cities.AddAsync(city);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<City> DeleteItem(int id)
        {
            var cityToDelete = await _context.Cities.FirstOrDefaultAsync(c => c.CityId == id);

            if (cityToDelete == null)
            {
                return null;
            }

            _context.Cities.Remove(cityToDelete);

            await _context.SaveChangesAsync();
            return cityToDelete;
        }

        public async Task<City> GetItemById(int id)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.CityId == id);
        }

        public async Task<IEnumerable<City>> GetItems()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> UpdateItem(int id, City city)
        {
            var cityToUpdate = await _context.Cities.FirstOrDefaultAsync(c => c.CityId == id);

            if(cityToUpdate == null)
            {
                return null;
            }

            cityToUpdate.Name = city.Name;

            await _context.SaveChangesAsync();

            return cityToUpdate;
        }
    }
}
