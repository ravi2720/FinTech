using FinTech_ApiPanel.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FinTech_ApiPanel.Application.Commands.FundManagements
{
    public class UpdateFundRequestCommand : IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public long BankId { get; set; }

        public decimal Amount { get; set; }
        public string? Remark { get; set; }

        public string ReferenceId { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public byte ModeOfPayment { get; set; }
        public IFormFile? AttachmentFile { get; set; }
    }
}
