using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.BankMasters
{
    public record DeleteBankMasterCommand(long Id) : IRequest<ApiResponse>;
}
