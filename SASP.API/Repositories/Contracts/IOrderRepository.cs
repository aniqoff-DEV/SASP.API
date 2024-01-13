using SASP.API.Entities;

namespace SASP.API.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderHistory>> GetItems();
        Task<OrderHistory> GetItemById(int id);
        Task<OrderHistory> CreateItem(OrderHistory record);
        Task<OrderHistory> UpdateItem(int id, string status);
        Task<OrderHistory> DeleteItem(int id);
    }
}
