using FinTech_ApiPanel.Application.Commands.FinancialComponentMasters;
using FinTech_ApiPanel.Application.Queries.FinancialComponentMasters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialComponentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FinancialComponentController> _logger;

        public FinancialComponentController(IMediator mediator, ILogger<FinancialComponentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateFinancialComponentCommand command)
        {
            _logger.LogInformation("Creating FinancialComponent of Type: {Type}", command.Type);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Fetching FinancialComponent with Id: {Id}", id);
            var result = await _mediator.Send(new GetFinancialComponentByIdQuery(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFinancialComponentQuery query)
        {
            _logger.LogInformation("Fetching all FinancialComponents");
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] UpdateFinancialComponentCommand command)
        {
            _logger.LogInformation("Updating FinancialComponent with Id: {Id}", command.Id);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("Deleting FinancialComponent with Id: {Id}", id);
            var result = await _mediator.Send(new DeleteFinancialComponentCommand(id));

            return StatusCode(result.StatusCode, result);
        }
    }
}
