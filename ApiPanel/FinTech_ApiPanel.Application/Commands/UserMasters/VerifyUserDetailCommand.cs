using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public class VerifyUserDetailCommand : IRequest<ApiResponse>
    {
        public long UserId { get; set; }
        public bool EmailVerified { get; set; }
        public bool PhoneVerified { get; set; }
        public bool KYCVerified { get; set; }
    }
}
