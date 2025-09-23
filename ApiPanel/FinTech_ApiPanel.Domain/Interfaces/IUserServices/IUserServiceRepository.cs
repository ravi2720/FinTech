using FinTech_ApiPanel.Domain.Entities.UserServices;

namespace FinTech_ApiPanel.Domain.Interfaces.IUserServices
{
    public interface IUserServiceRepository
    {
        Task<IEnumerable<UserService>> GetByUserIdAsync(long userId);
        Task AddAsync(UserService userService);
        Task UpdateAsync(UserService userService);
        Task<UserService> GetByUserIdAndServiceIdAsync(long userId, long serviceId);
    }

}
