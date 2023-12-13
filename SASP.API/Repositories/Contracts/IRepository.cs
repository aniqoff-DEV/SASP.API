using SASP.API.Entities;

namespace SASP.API.Repositories.Contracts
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetItems();
        Task<T> GetItemById(int id);
        Task<T> CreateItem(T record);
        Task<T> UpdateItem(int id, T record);
        Task<T> DeleteItem(int id);        
    }
}
