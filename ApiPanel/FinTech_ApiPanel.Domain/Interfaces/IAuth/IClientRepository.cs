using FinTech_ApiPanel.Domain.Entities.Auth;

namespace FinTech_ApiPanel.Domain.Interfaces.IAuth
{
    public interface IClientRepository
    {
        Task<Client> GetByUserIdAsync(long userId);
        Task<int> AddAsync(Client client);
        Task<Client> ValidateClient(string clientId, string clientSecret);
        Task<int> UpdateAsync(Client client);
        Task<Client?> GetAdminClientAsync();
    }
}
