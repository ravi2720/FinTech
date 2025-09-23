using FinTech_ApiPanel.Domain.Entities.ServiceMasters;

namespace FinTech_ApiPanel.Domain.Interfaces.IServiceMasters
{
    public interface IServiceMasterRepository
    {
        Task<IEnumerable<ServiceMaster>> GetAllAsync();
        Task<ServiceMaster?> GetByIdAsync(long id);
        Task<long> AddAsync(ServiceMaster service);
        Task<bool> UpdateAsync(ServiceMaster service);
        Task<ServiceMaster?> GetByTitleAsync(string title);
        Task<ServiceMaster?> GetByTypeAsync(string type);
    }
}
