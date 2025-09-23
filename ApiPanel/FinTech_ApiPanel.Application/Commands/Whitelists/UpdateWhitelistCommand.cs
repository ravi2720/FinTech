using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.Whitelists
{
    public class UpdateWhitelistCommand : IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public byte EntryType { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
