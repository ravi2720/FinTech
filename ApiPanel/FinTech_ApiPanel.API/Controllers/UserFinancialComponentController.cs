using FinTech_ApiPanel.Application.Commands.UserFinancialComponents;
using FinTech_ApiPanel.Application.Queries.UserFinancialComponents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFinancialComponentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserFinancialComponentController> _logger;

        public UserFinancialComponentController(IMediator mediator, ILogger<UserFinancialComponentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateUserFinancialComponentCommand command)
        {
            _logger.LogInformation("Creating UserFinancialComponent for UserId: {UserId}", command.UserId);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Fetching UserFinancialComponent with Id: {Id}", id);
            var result = await _mediator.Send(new GetUserFinancialComponentByIdQuery(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll([FromQuery]GetAllUserFinancialComponentQuery query)
        {
            _logger.LogInformation("Fetching all UserFinancialComponents");
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] UpdateUserFinancialComponentCommand command)
        {
            _logger.LogInformation("Updating UserFinancialComponent with Id: {Id}", command.Id);
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("Deleting UserFinancialComponent with Id: {Id}", id);
            var result = await _mediator.Send(new DeleteUserFinancialComponentCommand(id));

            return StatusCode(result.StatusCode, result);
        }
    }
}
