using FinTech_ApiPanel.API.Middleware;
using FinTech_ApiPanel.Application.Commands.GoterPay;
using FinTech_ApiPanel.Application.Queries.GoterPay;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RechargeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RechargeController> _logger;

        public RechargeController(IMediator mediator, ILogger<RechargeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("offer")]
        [GoterValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.RECHARGE)]
        public async Task<IActionResult> GetOffers([FromQuery] string operatorCode, [FromQuery] string number)
        {
            _logger.LogInformation("ROffer API called with OperatorCode: {OperatorCode}, Number: {Number}", operatorCode, number);

            if (string.IsNullOrWhiteSpace(operatorCode) || string.IsNullOrWhiteSpace(number))
            {
                _logger.LogWarning("ROffer API validation failed: operatorCode or number missing.");
                return BadRequest(new { message = "operatorCode and number are required" });
            }

            try
            {
                var query = new ROfferQuery
                {
                    OperatorCode = operatorCode,
                    Number = number
                };

                var result = await _mediator.Send(query);
                _logger.LogInformation("ROffer API response: {Response}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching ROffer");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpPost]
        [GoterValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.RECHARGE)]
        public async Task<IActionResult> Recharge([FromBody] RechargeCommand command)
        {
            _logger.LogInformation("Recharge API called with Number: {Number}, Amount: {Amount}, Operator: {Operator}, Circle: {Circle}, TxnId: {TxnId}",
                command?.Number, command?.Amount, command?.OperatorCode, command?.CircleCode, command?.ExternalRef);

            if (command == null)
            {
                _logger.LogWarning("Recharge API validation failed: request body missing.");
                return BadRequest(new { message = "Request body is required" });
            }

            try
            {
                var result = await _mediator.Send(command);
                _logger.LogInformation("Recharge API response: {Response}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing recharge");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpGet("plan")]
        [GoterValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.RECHARGE)]
        public async Task<IActionResult> GetRechargePlans([FromQuery] string operatorCode, [FromQuery] string circle)
        {
            _logger.LogInformation("RechargePlan API called with OperatorCode: {OperatorCode}, Circle: {Circle}", operatorCode, circle);

            if (string.IsNullOrWhiteSpace(operatorCode) || string.IsNullOrWhiteSpace(circle))
            {
                _logger.LogWarning("RechargePlan API validation failed: operatorCode or circle missing.");
                return BadRequest(new { message = "operatorCode and circle are required" });
            }

            try
            {
                var query = new RechargePlanQuery
                {
                    OperatorCode = operatorCode,
                    Circle = circle
                };

                var result = await _mediator.Send(query);
                _logger.LogInformation("RechargePlan API response: {Response}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching recharge plans");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpGet("mobile-info")]
        [GoterValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.RECHARGE)]
        public async Task<IActionResult> GetMobileInfo([FromQuery] string mobile)
        {
            _logger.LogInformation("MobileInfo API called with Mobile: {Mobile}", mobile);

            if (string.IsNullOrWhiteSpace(mobile))
            {
                _logger.LogWarning("MobileInfo API validation failed: mobile missing.");
                return BadRequest(new { message = "mobile is required" });
            }

            try
            {
                var query = new MobileInfoQuery
                {
                    Mobile = mobile
                };

                var result = await _mediator.Send(query);
                _logger.LogInformation("MobileInfo API response: {Response}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching mobile info");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpGet("dth-info")]
        [GoterValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.RECHARGE)]
        public async Task<IActionResult> GetDthInfo([FromQuery] string operatorCode, [FromQuery] string number)
        {
            _logger.LogInformation("DTHInfo API called with OperatorCode: {OperatorCode}, Number: {Number}", operatorCode, number);

            if (string.IsNullOrWhiteSpace(operatorCode) || string.IsNullOrWhiteSpace(number))
            {
                _logger.LogWarning("DTHInfo API validation failed: operatorCode or number missing.");
                return BadRequest(new { message = "operatorCode and number are required" });
            }

            try
            {
                var query = new DthInfoQuery
                {
                    OperatorCode = operatorCode,
                    Number = number
                };

                var result = await _mediator.Send(query);
                _logger.LogInformation("DTHInfo API response: {Response}", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching DTH info");
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpPost("bill")]
        [GoterValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.RECHARGE)]
        public async Task<IActionResult> PayBill([FromBody] BillPayCommand command)
        {
            if (command == null)
            {
                _logger.LogWarning("BillPay request received with null payload.");
                return BadRequest(new { status = "ERROR", message = "Invalid request payload." });
            }

            try
            {
                _logger.LogInformation("Processing BillPay request for Number: {Number}, TxnId: {TxnId}",
                    command.Number, command.TxnId);

                var result = await _mediator.Send(command);

                _logger.LogInformation("BillPay request processed successfully for TxnId: {TxnId}", command.TxnId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing BillPay request for TxnId: {TxnId}", command?.TxnId);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { status = "ERROR", message = ex.Message });
            }
        }
    }
}
