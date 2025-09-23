using FinTech_ApiPanel.Application.Commands.UserBanks;
using FinTech_ApiPanel.Application.Queries.UserBanks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBankController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserBankController> _logger;

        public UserBankController(IMediator mediator, ILogger<UserBankController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Create([FromBody] CreateUserBankCommand command)
        {
            _logger.LogInformation("Creating user bank account for UserId: {UserId}", command.UserId);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAll([FromQuery]GetAllUserBankQuery query)
        {
            _logger.LogInformation("Getting all user bank records.");
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("dropdown")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAllForDropdown()
        {
            _logger.LogInformation("Getting user banks for dropdown.");
            var result = await _mediator.Send(new GetuserBankForDropdownQuery());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Getting user bank record by ID: {Id}", id);
            var result = await _mediator.Send(new GetUserBankByIdQuery(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Update([FromBody] UpdateUserBankCommand command)
        {
            _logger.LogInformation("Updating user bank record ID: {Id}", command.Id);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("toggle/{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> ToggleStatus(long id)
        {
            _logger.LogInformation("Toggling user bank status ID: {Id}", id);
            var result = await _mediator.Send(new ToggleUserBankStatusCommand(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteUserBank(long id)
        {
            _logger.LogInformation("Deleting user bank record ID: {Id}", id);
            var result = await _mediator.Send(new DeleteUserBankCommand(id));

            return StatusCode(result.StatusCode, result);
        }
    }
}
