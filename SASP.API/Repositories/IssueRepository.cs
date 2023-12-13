using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class IssueRepository : IRepository<Issue>
    {
        private readonly SASPDbContext _context;

        public IssueRepository(SASPDbContext context)
        {
            _context = context;
        }
        public async Task<Issue> CreateItem(Issue issue)
        {
            if (issue != null)
            {
                var result = await _context.Issues.AddAsync(issue);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Issue> DeleteItem(int id)
        {
            var issueToDelete = await _context.Issues.FirstOrDefaultAsync(i => i.IssueId == id);

            if (issueToDelete == null)
            {
                return null;
            }

            _context.Issues.Remove(issueToDelete);

            await _context.SaveChangesAsync();
            return issueToDelete;
        }

        public async Task<Issue> GetItemById(int id)
        {
            return await _context.Issues.FirstOrDefaultAsync(i => i.IssueId == id);
        }

        public async Task<IEnumerable<Issue>> GetItems()
        {
            return await _context.Issues.ToListAsync();
        }

        public async Task<Issue> UpdateItem(int id, Issue issue)
        {
            var issueToUpdate = await _context.Issues.FirstOrDefaultAsync(i => i.IssueId == id);

            if (issueToUpdate == null)
            {
                return null;
            }

            issueToUpdate.Title = issue.Title;
            issueToUpdate.Price = issue.Price;
            issueToUpdate.CatalogId = issue.CatalogId;
            issueToUpdate.TypeIssueId = issue.TypeIssueId;

            await _context.SaveChangesAsync();

            return issueToUpdate;
        }
    }
}
