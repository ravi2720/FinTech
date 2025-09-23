using FinTech_ApiPanel.API.Middleware;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FiController> _logger;

        public ReportsController(IMediator mediator,
            ILogger<FiController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("txnStatus")]
        [IPayValidate]
        public async Task<IActionResult> GetTxnStatus([FromBody] TxnStatusCommand command)
        {
            _logger.LogInformation("Checking transaction status for ExternalRef: {ExternalRef} on Date: {Date}", command.ExternalRef, command.TransactionDate);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
