using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class SubscriptionRepository : IRepository<Subscription>
    {
        private readonly SASPDbContext _context;

        public SubscriptionRepository(SASPDbContext context)
        {
            _context = context;
        }
        public async Task<Subscription> CreateItem(Subscription subscription)
        {
            if (subscription != null)
            {
                var result = await _context.Subscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<Subscription> DeleteItem(int id)
        {
            var subscriptionToDelete = await _context.Subscriptions.FirstOrDefaultAsync(s => s.SubscriptionId == id);

            if (subscriptionToDelete == null)
            {
                return null;
            }

            _context.Subscriptions.Remove(subscriptionToDelete);

            await _context.SaveChangesAsync();
            return subscriptionToDelete;
        }

        public async Task<Subscription> GetItemById(int id)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.SubscriptionId == id);
        }

        public async Task<IEnumerable<Subscription>> GetItems()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> UpdateItem(int id, Subscription subscription)
        {
            var subscriptionToUpdate = await _context.Subscriptions.FirstOrDefaultAsync(s => s.SubscriptionId == id);

            if (subscriptionToUpdate == null)
            {
                return null;
            }

            subscriptionToUpdate.IssueId = subscription.IssueId;
            subscriptionToUpdate.UserId = subscription.UserId;
            subscriptionToUpdate.StartSub = subscription.StartSub;
            subscriptionToUpdate.EndSub = subscription.EndSub;
            subscriptionToUpdate.Price = subscription.Price;

            await _context.SaveChangesAsync();

            return subscriptionToUpdate;
        }
    }
}
