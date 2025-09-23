using FinTech_ApiPanel.API.Middleware;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FiController> _logger;

        public IdentityController(IMediator mediator, ILogger<FiController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("verifyBankAccount")]
        [IPayValidate]
        [WhitelistValidate]
        public async Task<IActionResult> VerifyBankAccount([FromBody] VerifyBankAccountCommand command)
        {
            _logger.LogInformation("Processing bank account verification request");
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
