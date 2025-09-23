using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.UserBanks
{
    public class GetuserBankForDropdownQuery : IRequest<ApiResponse<List<UserBankDto>>>;
}
