using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.UserBanks
{
    public class GetAllUserBankQuery : IRequest<ApiResponse<List<UserBank>>>
    {
        public long? UserId { get; set; }
    };
}
