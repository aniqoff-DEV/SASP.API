using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class CountryRepository : IRepository<Country>
    {
        private readonly SASPDbContext _context;

        public CountryRepository(SASPDbContext context)
        {
            _context = context;
        }

        public async Task<Country> CreateItem(Country country)
        {
            if (country != null)
            {
                var result = await _context.Country.AddAsync(country);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Country> DeleteItem(int id)
        {
            var countryToDelete = await _context.Country.FirstOrDefaultAsync(c => c.CountryId == id);

            if (countryToDelete == null)
            {
                return null;
            }

            _context.Country.Remove(countryToDelete);

            await _context.SaveChangesAsync();
            return countryToDelete;
        }

        public async Task<Country> GetItemById(int id)
        {
            return await _context.Country.FirstOrDefaultAsync(c => c.CountryId == id);
        }

        public async Task<IEnumerable<Country>> GetItems()
        {
            return await _context.Country.ToListAsync();
        }

        public async Task<Country> UpdateItem(int id, Country country)
        {
            var countryToUpdate = await _context.Country.FirstOrDefaultAsync(c => c.CountryId == id);

            if (countryToUpdate == null)
            {
                return null;
            }
            countryToUpdate.Name = country.Name;

            await _context.SaveChangesAsync();

            return countryToUpdate;
        }
    }
}
