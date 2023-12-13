using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class TypeIssueRepository : IRepository<TypeIssue>
    {
        private readonly SASPDbContext _context;

        public TypeIssueRepository(SASPDbContext context)
        {
            _context = context;
        }
        public async Task<TypeIssue> CreateItem(TypeIssue typeIssue)
        {
            if (typeIssue != null)
            {
                var result = await _context.TypeIssues.AddAsync(typeIssue);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<TypeIssue> DeleteItem(int id)
        {
            var typeIssueToDelete = await _context.TypeIssues.FirstOrDefaultAsync(t => t.TypeIssueId == id);

            if (typeIssueToDelete == null)
            {
                return null;
            }

            _context.TypeIssues.Remove(typeIssueToDelete);

            await _context.SaveChangesAsync();
            return typeIssueToDelete;
        }

        public async Task<TypeIssue> GetItemById(int id)
        {
            return await _context.TypeIssues.FirstOrDefaultAsync(t => t.TypeIssueId == id);
        }

        public async Task<IEnumerable<TypeIssue>> GetItems()
        {
            return await _context.TypeIssues.ToListAsync();
        }

        public async Task<TypeIssue> UpdateItem(int id, TypeIssue typeIssue)
        {
            var typeIssueToUpdate = await _context.TypeIssues.FirstOrDefaultAsync(t => t.TypeIssueId == id);

            if (typeIssueToUpdate == null)
            {
                return null;
            }

            typeIssueToUpdate.TypeName = typeIssue.TypeName;

            await _context.SaveChangesAsync();

            return typeIssueToUpdate;
        }
    }
}
