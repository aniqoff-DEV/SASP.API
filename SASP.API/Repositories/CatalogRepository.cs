using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class CatalogRepository : IRepository<Catalog>
    {
        private readonly SASPDbContext _context;

        public CatalogRepository(SASPDbContext context)
        {
            _context = context;
        }
        public async Task<Catalog> CreateItem(Catalog catalog)
        {
            if (catalog != null)
            {
                var result = await _context.Catalog.AddAsync(catalog);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Catalog> DeleteItem(int id)
        {
            var catalogToDelete = await _context.Catalog.FirstOrDefaultAsync(c => c.CatalogId == id);

            if (catalogToDelete == null)
            {
                return null;
            }

            _context.Catalog.Remove(catalogToDelete);

            await _context.SaveChangesAsync();
            return catalogToDelete;
        }

        public async Task<Catalog> GetItemById(int id)
        {
            return await _context.Catalog.FirstOrDefaultAsync(c => c.CatalogId == id);
        }

        public async Task<IEnumerable<Catalog>> GetItems()
        {
            return await _context.Catalog.ToListAsync();
        }

        public async Task<Catalog> UpdateItem(int id, Catalog catalog)
        {
            var catalogToUpdate = await _context.Catalog.FirstOrDefaultAsync(c => c.CatalogId == id);

            if (catalogToUpdate == null)
            {
                return null;
            }

            catalogToUpdate.CatalogName = catalog.CatalogName;

            await _context.SaveChangesAsync();

            return catalogToUpdate;
        }
    }
}
