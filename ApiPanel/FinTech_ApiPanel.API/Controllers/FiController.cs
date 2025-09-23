using FinTech_ApiPanel.API.Middleware;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Application.Queries.InstantPay;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FiController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FiController> _logger;

        public FiController(IMediator mediator, ILogger<FiController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #region AEPS

        [HttpPost("aeps/outletLoginStatus")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> OutletLoginStatus([FromQuery] GetOutletLoginStatusCommand command)
        {
            _logger.LogInformation("Getting outlet login status with command: {Command}", command);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("aeps/outletLogin")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> OutletLogin([FromBody] OutletLoginCommand command)
        {
            _logger.LogInformation("Outlet login with command: {Command}", command);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("aeps/banks")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> GetBanks([FromQuery] GetAEPSBankQuery query)
        {
            _logger.LogInformation("Getting AEPS banks with query: {Query}", query);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("aeps/cashWithdrawal")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> CashWithdrawal([FromBody] AEPSCashWithdrawalCommand command)
        {
            _logger.LogInformation("Processing AEPS cash withdrawal for Mobile: {Mobile}, Amount: {Amount}", command.Mobile, command.Amount);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("aeps/cashDeposit")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> CashDeposit([FromBody] AEPSCashDepositCommand command)
        {
            _logger.LogInformation("Processing AEPS cash deposit for Mobile: {Mobile}, Amount: {Amount}", command.Mobile, command.Amount);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("aeps/balanceInquiry")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> BalanceInquiry([FromBody] AEPSBalanceInquiryCommand command)
        {
            _logger.LogInformation("Processing AEPS balance inquiry for Mobile: {Mobile}", command.Mobile);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("aeps/miniStatement")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> MiniStatement([FromBody] AEPSMiniStatementCommand command)
        {
            _logger.LogInformation("Processing AEPS mini statement for Mobile: {Mobile}", command.Mobile);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

        #region DMT

        [HttpPost("remit/out/domestic/v2/banks")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> RemitBank()
        {
            _logger.LogInformation("Processing remit bank fetch request");
            var result = await _mediator.Send(new GetRemitBankCommand());
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/remitterProfile")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> GetRemitterProfile([FromBody] GetRemitterProfileCommand command)
        {
            _logger.LogInformation("Fetching Remitter Profile for Mobile: {Mobile}", command.MobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/remitterRegistration")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> RegisterRemitter([FromBody] RemitterRegistrationCommand command)
        {
            _logger.LogInformation("Registering remitter with mobile: {Mobile}", command.MobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/remitterRegistrationVerify")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> VerifyRemitterOtp([FromBody] RemitterRegistrationVerifyCommand command)
        {
            _logger.LogInformation("Verifying remitter OTP for mobile: {Mobile}", command.MobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/remitterKyc")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> RemitterBiometricKyc([FromBody] RemitterKycCommand command)
        {
            _logger.LogInformation("Performing biometric KYC for mobile: {Mobile}", command.MobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/generateTransactionOtp")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> GenerateTransactionOtp([FromBody] GenerateTransactionOtpCommand command)
        {
            _logger.LogInformation("Generating transaction OTP for mobile: {Mobile}", command.RemitterMobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/bioAuthTransaction")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> RemitterBioAuthTransaction([FromBody] RemitterBioAuthTransactionCommand command)
        {
            _logger.LogInformation("Initiating BioAuth Transaction for Mobile: {Mobile}", command.RemitterMobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/transaction")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> RemitTransaction([FromBody] RemitterTransactionCommand command)
        {
            _logger.LogInformation("Initiating Remitter Transaction for Mobile: {Mobile}", command.RemitterMobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/beneficiaryRegistration")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> RegisterBeneficiary([FromBody] RegisterBeneficiaryCommand command)
        {
            _logger.LogInformation("Registering Beneficiary for Remitter: {Mobile}", command.RemitterMobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/beneficiaryRegistrationVerify")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> BeneficiaryRegistrationVerify([FromBody] BeneficiaryRegistrationVerifyCommand command)
        {
            _logger.LogInformation("Verifying beneficiary for mobile: {Mobile}", command.RemitterMobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/beneficiaryDelete")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> DeleteBeneficiary([FromBody] DeleteBeneficiaryCommand command)
        {
            _logger.LogInformation("Initiating deletion for beneficiary {BeneficiaryId} by Mobile {Mobile}",
                command.BeneficiaryId, command.RemitterMobileNumber);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remit/out/domestic/v2/beneficiaryDeleteVerify")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.DMT)]
        public async Task<IActionResult> BeneficiaryDeleteVerify([FromBody] DeleteBeneficiaryVerifyCommand command)
        {
            _logger.LogInformation("Verifying beneficiary delete for mobile: {Mobile}", command.RemitterMobileNumber);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion
    }
}
