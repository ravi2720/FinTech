using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Whitelists;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Application.Queries.Whitelists
{
    public class GetAllWhitelistQuery() : IRequest<ApiResponse<List<WhitelistDto>>>
    {
        [Required]
        public byte EntryType { get; set; }
    };
}
