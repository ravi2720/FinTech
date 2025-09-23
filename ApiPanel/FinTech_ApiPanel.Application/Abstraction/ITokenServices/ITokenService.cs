using FinTech_ApiPanel.Domain.DTOs.Auth;
using FinTech_ApiPanel.Domain.DTOs.Locations;
using FinTech_ApiPanel.Domain.DTOs.Tokens;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Entities.Auth;
using FinTech_ApiPanel.Domain.Entities.UserMasters;

namespace FinTech_ApiPanel.Application.Abstraction.ITokenServices
{
    public interface ITokenService
    {
        TokenDto GenerateToken(UserMaster user);
        AuthTokenDto GenerateToken(AuthToken authToken);
        LoggedInUserDto GetLoggedInUserinfo();
        string? HostIpAddress();
        string? RequestIpAddress();
        Task<LocationDto?> GetLocationDataAsync(string ip);
    }
}
