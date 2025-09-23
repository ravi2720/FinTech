using AutoMapper;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.UserMaster;
using FinTech_ApiPanel.Domain.DTOs.BankMasters;
using FinTech_ApiPanel.Domain.DTOs.Services;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserServices;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using FinTech_ApiPanel.Domain.Shared.SortHelper;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.Clients
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<PagedResponse<UserMasterListDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IWalletRepository _walletRepository;
        private readonly IUserBankRepository _userBankRepository;
        private readonly IUserServiceRepository _userServiceRepository;
        private readonly IServiceMasterRepository _serviceMasterRepository;

        public GetAllUsersHandler(IUserRepository userRepository,
            IMapper mapper,
            IUserBankRepository userBankRepository,
            IUserServiceRepository userServiceRepository,
            IServiceMasterRepository serviceMasterRepository,
            IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _walletRepository = walletRepository;
            _userBankRepository = userBankRepository;
            _userServiceRepository = userServiceRepository;
            _serviceMasterRepository = serviceMasterRepository;
        }

        public async Task<ApiResponse<PagedResponse<UserMasterListDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserMasterListDto> userDtos = new List<UserMasterListDto>();

                var users = await _userRepository.GetAllAsync();
                var serviceList = await _serviceMasterRepository.GetAllAsync();
                foreach (var user in users)
                {
                    if (user.IsAdmin)
                        continue;

                    if (!string.IsNullOrEmpty(request.Title) && !user.FullName.Contains(request.Title, StringComparison.OrdinalIgnoreCase))
                        continue;


                    var userInfo = _mapper.Map<UserMasterListDto>(user);

                    var waletInfo = await _walletRepository.GetByUserIdAsync(user.Id);

                    if (waletInfo == null)
                        return ApiResponse<PagedResponse<UserMasterListDto>>.NoContentResponse($"Wallet not found for user: {user.FullName}");

                    userInfo.Role = "user";
                    userInfo.WalletBalance = waletInfo.TotalBalance;
                    userInfo.HoldAmount = waletInfo.HeldAmount;


                    //map user bank details
                    var userBank = await _userBankRepository.GetByUserIdAsync(user.Id);

                    foreach (var bank in userBank)
                    {
                        var bankDto = _mapper.Map<BankDto>(bank);
                        userInfo.UserBanks.Add(bankDto);
                    }

                    //map user services
                    var userServices = await _userServiceRepository.GetByUserIdAsync(user.Id);
                    if (userServices != null && userServices.Any())
                    {
                        userInfo.Services = userServices.Select(s => new ServiceDto
                        {
                            Id = s.ServiceId,
                            Title = serviceList.Where(x=>x.Id==s.ServiceId).FirstOrDefault().Title,
                            IsActive = s.IsActive
                        }).ToList();
                    }

                    userDtos.Add(userInfo);
                }

                var sortedResponse = SortHelper.ApplySort<UserMasterListDto>(userDtos.AsQueryable(), request.OrderBy);

                var pagedResponse = PagedResponse<UserMasterListDto>.Create(sortedResponse, request.PageNumber, request.PageSize);

                return ApiResponse<PagedResponse<UserMasterListDto>>.SuccessResponse(pagedResponse);
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResponse<UserMasterListDto>>.BadRequestResponse($"An error occurred while retrieving users: {ex.Message}");
            }
        }
    }

}
