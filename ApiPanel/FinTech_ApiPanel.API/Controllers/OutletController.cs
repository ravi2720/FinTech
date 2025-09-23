using FinTech_ApiPanel.API.Middleware;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OutletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OutletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup/initiate")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> InitiateEKYC([FromBody] EKYCInitiateCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("signup/validate")]
        [IPayValidate]
        [WhitelistValidate]
        [ServiceValidate(ServiceType.AEPS)]
        public async Task<IActionResult> ValidateEKYC([FromBody] EKYCValidateCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
