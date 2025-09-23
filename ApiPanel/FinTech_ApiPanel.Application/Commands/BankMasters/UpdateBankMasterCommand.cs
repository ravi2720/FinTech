using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.BankMasters
{
    public class UpdateBankMasterCommand : IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public byte Type { get; set; }
        public string IFSCCode { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string UPIHandle { get; set; } = string.Empty;
    }
}