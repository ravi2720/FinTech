using AutoMapper;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.BankMasters;
using FinTech_ApiPanel.Domain.DTOs.BankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.BankMasters
{
    public class GetAllBankMasterForDropdownHandler : IRequestHandler<GetBankMasterForDropdownQuery, ApiResponse<List<BankDto>>>
    {
        private readonly IBankMasterRepository _bankMasterRepository;
        private readonly IMapper _mapper;

        public GetAllBankMasterForDropdownHandler(IBankMasterRepository bankMasterRepository,
            IMapper mapper)
        {
            _bankMasterRepository = bankMasterRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<BankDto>>> Handle(GetBankMasterForDropdownQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bankMasters = await _bankMasterRepository.GetAllAsync();

                bankMasters= bankMasters.Where(b => b.Type == request.Type).ToList();

                List<BankDto> bankMasterDtos = new List<BankDto>();

                foreach (var bank in bankMasters)
                {
                    if (!bank.IsActive)
                        continue;

                    var bankDto = _mapper.Map<BankDto>(bank);
                    bankMasterDtos.Add(bankDto);
                }

                return ApiResponse<List<BankDto>>.SuccessResponse(bankMasterDtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<BankDto>>.BadRequestResponse($"An error occurred while retrieving the banks: {ex.Message}");
            }
        }
    }
}
