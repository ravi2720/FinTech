using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Queries.FundManagements;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FundManagementController> _logger;

        public FundManagementController(IMediator mediator, ILogger<FundManagementController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("fund-request")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Create([FromForm] CreateFundRequestCommand command)
        {
            _logger.LogInformation("Creating fund request for user {UserId}", command.UserId);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("fund-request")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Update([FromForm] UpdateFundRequestCommand command)
        {
            _logger.LogInformation("Updating fund request {Id}", command.Id);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("fund-request/{id:long}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Getting fund request by ID {Id}", id);
            var result = await _mediator.Send(new GetFundRequestByIdQuery(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("fund-request")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all fund requests");
            var result = await _mediator.Send(new GetAllFundRequestsQuery());

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("fund-request/accept")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Accept([FromBody] AcceptFundRequestCommand command)
        {
            _logger.LogInformation("Accepting fund request {Id}", command.Id);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("fund-request/reject")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Reject([FromBody] RejectFundRequestCommand command)
        {
            _logger.LogInformation("Rejecting fund request {Id}", command.Id);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("aeps-settlement")]
        [Authorize(Roles = "admin,User")]
        public async Task<IActionResult> AEPSSettlement([FromBody] FundSettlementCommand command)
        {
            _logger.LogInformation("Processing AEPS fund settlement request.");
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("fund-add")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddFund([FromBody] AddFundCommand command)
        {
            _logger.LogInformation("Adding fund to user {UserId}, Amount: {Amount}, Ref: {ReferenceId}",
                command.UserId, command.Amount, command.ReferenceId);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("fund-hold")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> HoldFund([FromBody] HoldFundCommand command)
        {
            _logger.LogInformation("Holding fund for user {UserId}, Amount: {Amount}, Ref: {ReferenceId}",
                command.UserId, command.Amount, command.ReferenceId);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("fund-release")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ReleaseFund([FromBody] ReleaseFundCommand command)
        {
            _logger.LogInformation("Releasing fund for user {UserId}, Amount: {Amount}, Ref: {ReferenceId}",
                command.UserId, command.Amount, command.ReferenceId);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("fund-deduct")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeductFund([FromBody] DeductFundCommand command)
        {
            _logger.LogInformation("Deducting fund from user {UserId}, Amount: {Amount}, Ref: {ReferenceId}",
                command.UserId, command.Amount, command.ReferenceId);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }
    }
}
