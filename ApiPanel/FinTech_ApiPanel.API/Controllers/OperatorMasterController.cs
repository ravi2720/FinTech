using FinTech_ApiPanel.Application.Commands.BankMasters;
using FinTech_ApiPanel.Application.Queries.BankMasters;
using FinTech_ApiPanel.Application.Queries.OperatorMasters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorMasterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OperatorMasterController> _logger;

        public OperatorMasterController(IMediator mediator, ILogger<OperatorMasterController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllOperatorMasterQuery query)
        {
            _logger.LogInformation("Getting all operators.");
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }
    }
}
