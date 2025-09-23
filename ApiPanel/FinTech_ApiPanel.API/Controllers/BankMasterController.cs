using FinTech_ApiPanel.Application.Commands.BankMasters;
using FinTech_ApiPanel.Application.Queries.BankMasters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankMasterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BankMasterController> _logger;

        public BankMasterController(IMediator mediator, ILogger<BankMasterController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateBankMasterCommand command)
        {
            _logger.LogInformation("Creating bank: {BankName}", command.BankName);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllBankMasterQuery query)
        {
            _logger.LogInformation("Getting all banks.");
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("dropdown")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAllFroDropdown([FromQuery] GetBankMasterForDropdownQuery query)
        {
            _logger.LogInformation("Getting all banks for dropdown.");
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Getting bank ID: {Id}", id);
            var result = await _mediator.Send(new GetBankMasterByIdQuery(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] UpdateBankMasterCommand command)
        {
            _logger.LogInformation("Updating bank ID: {Id}", command.Id);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("toggle/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ToggleStatus(long id)
        {
            _logger.LogInformation("Toggling bank status ID: {Id}", id);
            var result = await _mediator.Send(new ToggleBankMasterStatusCommand(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBank(long id)
        {
            _logger.LogInformation("Deleting bank ID: {Id}", id);
            var result = await _mediator.Send(new DeleteBankMasterCommand(id));

            return StatusCode(result.StatusCode, result);
        }
    }
}
