using SASP.API.Entities;

namespace SASP.API.Repositories.Contracts
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminById(int id);
        Task<Admin> CreateAdmin(Admin admin);
        Task<Admin> UpdateAdmin(int id, Admin admin);
    }
}
