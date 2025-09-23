using FinTech_ApiPanel.Domain.Entities.UserMasters;

namespace FinTech_ApiPanel.Domain.Interfaces.IUserMasters
{
    public interface IUserRepository
    {
        Task<int> AddAsync(UserMaster user);

        Task<UserMaster?> GetByIdAsync(long id);

        Task<IEnumerable<UserMaster>> GetAllAsync();

        Task<UserMaster?> GetByEmailOrUserNameAsync(string emailOrUserName);
        Task<UserMaster?> GetByPhoneNumberAsync(string phoneNumber);

        Task<bool> UpdateAsync(UserMaster user);
        Task<UserMaster?> GetAdminAsync();
    }
}
