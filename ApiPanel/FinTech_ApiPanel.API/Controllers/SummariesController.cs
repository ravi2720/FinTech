using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Application.Queries.Summaries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummariesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SummariesController> _logger;

        public SummariesController(IMediator mediator, ILogger<SummariesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("wallet-summary")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetWalletSummary([FromQuery] WalletSummaryQuery query)
        {
            _logger.LogInformation("Received request for wallet summary with parameters: {@Query}", query);
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("aeps-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAEPSHistory([FromQuery] AEPSHistoryQuery query)
        {
            _logger.LogInformation("Received request for AEPS history with parameters: {@Query}", query);
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("payout-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetPayoutHistory([FromQuery] PayoutHistoryQuery query)
        {
            _logger.LogInformation("Received request for payout history with parameters: {@Query}", query);
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("money-transfer-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetMoneyTransferHistory([FromQuery] MoneyTransferHistoryQuery query)
        {
            _logger.LogInformation("Received request for money transfer history with parameters: {@Query}", query);
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("matm-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetMatmHistory([FromQuery] MatmHistoryQuery query)
        {
            _logger.LogInformation("Received request for MATM history with parameters: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("recharge-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetRechargeHistory([FromQuery] RechargeHistoryQuery query)
        {
            _logger.LogInformation("Received request for recharge history with parameters: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("aeps-pending-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAepsPendingHistory([FromQuery] AepsPendingHistoryQuery query)
        {
            _logger.LogInformation("Received request for AEPS pending history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("upi-transfer-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetUpiTransferHistory([FromQuery] UpiTransferHistoryQuery query)
        {
            _logger.LogInformation("Received request for UPI transfer history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("aeps-onboard-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAepsOnboardHistory([FromQuery] AepsOnboardHistoryQuery query)
        {
            _logger.LogInformation("Received request for AEPS onboard history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("pan-onboard-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetPanOnboardHistory([FromQuery] PanOnboardHistoryQuery query)
        {
            _logger.LogInformation("Received request for PAN onboard history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("nsdl-pan-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetNSDLPanHistory([FromQuery] NSDLPANHistoryQuery query)
        {
            _logger.LogInformation("Received request for NSDL PAN history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("uti-pan-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetUTIPanHistory([FromQuery] UTIPANHistoryQuery query)
        {
            _logger.LogInformation("Received request for UTI PAN history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("pan-verification-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetPanVerificationHistory([FromQuery] PANVerificationHistoryQuery query)
        {
            _logger.LogInformation("Received request for PAN verification history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("upi-cashout-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetUPICashOutHistory([FromQuery] UPICashOutHistoryQuery query)
        {
            _logger.LogInformation("Received request for UPI cashout history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("creditcard-billpayment-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetCreditCardBillPaymentHistory([FromQuery] CreditCardBillPaymentHistoryQuery query)
        {
            _logger.LogInformation("Received request for credit card bill payment history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("apiwise-service-usage")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAPIWiseServiceUse([FromQuery] APIWiseServiceUseQuery query)
        {
            _logger.LogInformation("Received request for API-wise service use report: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("operatorwise-amount")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetOperatorwiseAmount([FromQuery] OperatorwiseAmountQuery query)
        {
            _logger.LogInformation("Received request for operatorwise amount report: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("aeps-rejected-merchants")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAEPSRejectMerchant([FromQuery] AEPSRejectMerchantQuery query)
        {
            _logger.LogInformation("Received request for AEPS rejected merchant report: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("all-pending")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAllPending([FromQuery] AllPendingQuery query)
        {
            _logger.LogInformation("Received request for all pending transactions: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("dispute-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetDisputeHistory([FromQuery] DisputeHistoryQuery query)
        {
            _logger.LogInformation("Received request for dispute history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("pan-token-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetPanTokenHistory([FromQuery] PANTokenHistoryQuery query)
        {
            _logger.LogInformation("Received request for PAN token history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("aadhar-verification-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAadharVerificationHistory([FromQuery] AadharVerificationHistoryQuery query)
        {
            _logger.LogInformation("Received request for Aadhar verification history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("dmt-ppi-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetDMTPPIHistory([FromQuery] DMTPPIHistoryQuery query)
        {
            _logger.LogInformation("Received request for DMT/PPI history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("money-remitter-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetMoneyRemitterHistory([FromQuery] MoneyRemitterHistoryQuery query)
        {
            _logger.LogInformation("Received request for money remitter history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("money-wallet-load-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetMoneyWalletLoadHistory([FromQuery] MoneyWalletLoadHistoryQuery query)
        {
            _logger.LogInformation("Received request for wallet load history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("tds-gst-history")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetTdsGstHistory([FromQuery] TdsGstHistoryQuery query)
        {
            _logger.LogInformation("Received request for TDS/GST history: {@Query}", query);
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("manage-pending")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ManagePendingRequest(ManagePendingRequestCommand command)
        {
            _logger.LogInformation("Received request to manage pending transactions: {@Command}", command);
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}