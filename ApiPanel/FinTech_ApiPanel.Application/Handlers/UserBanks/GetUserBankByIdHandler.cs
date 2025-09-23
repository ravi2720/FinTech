using AutoMapper;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.BankMasters;
using FinTech_ApiPanel.Application.Queries.UserBanks;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserBanks
{
    public class GetUserBankByIdHandler : IRequestHandler<GetUserBankByIdQuery, ApiResponse<UserBank>>
    {
        private readonly IUserBankRepository _userBankRepository;
        private readonly IMapper _mapper;

        public GetUserBankByIdHandler(IUserBankRepository userBankRepository,
            IMapper mapper)
        {
            _userBankRepository = userBankRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UserBank>> Handle(GetUserBankByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bankMaster = await _userBankRepository.GetByIdAsync(request.Id);
                if (bankMaster == null)
                    return ApiResponse<UserBank>.NotFoundResponse("Bank not found");

                return ApiResponse<UserBank>.SuccessResponse(bankMaster);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserBank>.BadRequestResponse($"An error occurred while retrieving the bank: {ex.Message}");
            }
        }
    }
}
