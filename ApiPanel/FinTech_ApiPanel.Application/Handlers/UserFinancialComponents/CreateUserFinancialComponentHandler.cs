using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.UserFinancialComponents;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using FinTech_ApiPanel.Domain.Entities.UserFinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using System.Linq;
using System.Transactions;

namespace FinTech_ApiPanel.Application.Handlers.UserFinancialComponents
{
    public class CreateUserFinancialComponentHandler : IRequestHandler<CreateUserFinancialComponentCommand, ApiResponse>
    {
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public CreateUserFinancialComponentHandler(IUserFinancialComponentRepository userFinancialComponentRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userFinancialComponentRepository = userFinancialComponentRepository;
        }

        public async Task<ApiResponse> Handle(CreateUserFinancialComponentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse.UnauthorizedResponse("Access denied.");

                var overlapes = HasOverlappingRanges(request);
                if (overlapes)
                    return ApiResponse.BadRequestResponse("Components have overlapping ranges.");

                var financialComponentList = await _userFinancialComponentRepository.GetAllAsync();

                var userFinancialComponents = financialComponentList
                    .Where(x => x.UserId == request.UserId && x.Type == request.Type && x.ServiceType == request.ServiceType)
                    .ToList();

                var txOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, txOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Delete existing components for the user
                    foreach (var component in userFinancialComponents)
                    {
                        await _userFinancialComponentRepository.DeleteAsync(component.Id);
                    }

                    // Add new components
                    foreach (var component in request.Components)
                    {
                        var financialComponent = _mapper.Map<UserFinancialComponent>(component);
                        financialComponent.UserId = request.UserId;
                        financialComponent.Type = request.Type;
                        financialComponent.ServiceType = request.ServiceType;
                        financialComponent.ServiceSubType = component.ServiceSubType;
                        financialComponent.OperatorId = component.OperatorId;

                        await _userFinancialComponentRepository.AddAsync(financialComponent);
                    }

                    scope.Complete();
                    return ApiResponse.SuccessResponse();
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while creating: {ex.Message}");
            }
        }

        private bool HasOverlappingRanges(CreateUserFinancialComponentCommand request)
        {
            // Skip validation for these types
            if (request.ServiceType == (byte)FinancialComponenService.Other_Commission ||
                request.ServiceType == (byte)FinancialComponenService.Other_Surcharge)
                return false;

            // Invalid range check
            if (request.Components.Any(x => x.Start_Value.HasValue && x.End_Value.HasValue && x.Start_Value > x.End_Value))
                return true;

            if (request.ServiceType == (byte)FinancialComponenService.Recharge)
            {
                // Group by OperatorId + ServiceSubType
                var grouped = request.Components
                    .GroupBy(x => new { x.OperatorId, x.ServiceSubType });

                foreach (var group in grouped)
                {
                    var sorted = group
                        .OrderBy(x => x.Start_Value ?? 0) 
                        .ToList();

                    for (int i = 1; i < sorted.Count; i++)
                    {
                        var prev = sorted[i - 1];
                        var current = sorted[i];

                        if ((current.Start_Value ?? 0) < (prev.End_Value ?? decimal.MaxValue))
                        {
                            return true; // Overlap detected
                        }
                    }
                }
            }
            else
            {
                // For other types, check globally
                var sorted = request.Components
                    .OrderBy(x => x.Start_Value ?? 0)
                    .ToList();

                for (int i = 1; i < sorted.Count; i++)
                {
                    var prev = sorted[i - 1];
                    var current = sorted[i];

                    if ((current.Start_Value ?? 0) < (prev.End_Value ?? decimal.MaxValue))
                    {
                        return true; // Overlap detected
                    }
                }
            }

            return false; // No overlaps found
        }
    }
}
