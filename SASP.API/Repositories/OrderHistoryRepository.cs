using Microsoft.EntityFrameworkCore;
using SASP.API.Data;
using SASP.API.Entities;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Repositories
{
    public class OrderHistoryRepository : IRepository<OrderHistory>
    {
        private readonly SASPDbContext _context;

        public OrderHistoryRepository(SASPDbContext context)
        {
            _context = context;
        }
        public async Task<OrderHistory> CreateItem(OrderHistory order)
        {
            if (order != null)
            {
                var result = await _context.OrderHistories.AddAsync(order);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            return null;
        }

        public async Task<OrderHistory> DeleteItem(int id)
        {
            var orderToDelete = await _context.OrderHistories.FirstOrDefaultAsync(o => o.OrderId == id);

            if (orderToDelete == null)
            {
                return null;
            }

            _context.OrderHistories.Remove(orderToDelete);

            await _context.SaveChangesAsync();
            return orderToDelete;
        }

        public async Task<OrderHistory> GetItemById(int id)
        {
            return await _context.OrderHistories.FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<OrderHistory>> GetItems()
        {
            return await _context.OrderHistories.ToListAsync();
        }

        public async Task<OrderHistory> UpdateItem(int id, OrderHistory order)
        {
            var orderToUpdate = await _context.OrderHistories.FirstOrDefaultAsync(o => o.OrderId == id);

            if (orderToUpdate == null)
            {
                return null;
            }

            orderToUpdate.CreatedDate = order.CreatedDate;
            orderToUpdate.UserId = order.UserId;
            orderToUpdate.IssueId = order.IssueId;

            await _context.SaveChangesAsync();

            return orderToUpdate;
        }
    }
}
