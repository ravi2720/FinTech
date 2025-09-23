using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserBanks
{
    public class CreateUserBankCommand : IRequest<ApiResponse>
    {
        public long UserId { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string UPIHandle { get; set; } = string.Empty;
    }
}
