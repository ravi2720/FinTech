using FinTech_ApiPanel.Application.Commands.Whitelists;
using FinTech_ApiPanel.Application.Queries.Whitelists;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhitelistEntryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WhitelistEntryController> _logger;

        public WhitelistEntryController(IMediator mediator, ILogger<WhitelistEntryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Create([FromBody] CreateWhitelistCommand command)
        {
            _logger.LogInformation("Creating whitelist entry: {Value}", command.Value);
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetAll([FromQuery]GetAllWhitelistQuery query)
        {
            _logger.LogInformation("Getting all whitelist entries.");
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Getting whitelist entry ID: {Id}", id);
            var result = await _mediator.Send(new GetWhitelistByIdQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Update([FromBody] UpdateWhitelistCommand command)
        {
            _logger.LogInformation("Updating whitelist entry ID: {Id}", command.Id);
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("toggle/{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ToggleStatus(long id)
        {
            _logger.LogInformation("Toggling whitelist status ID: {Id}", id);
            var result = await _mediator.Send(new ToggleWhitelistStatusCommand(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("Deleting whitelist entry ID: {Id}", id);
            var result = await _mediator.Send(new DeleteWhitelistCommand(id));
            return StatusCode(result.StatusCode, result);
        }
    }
}
