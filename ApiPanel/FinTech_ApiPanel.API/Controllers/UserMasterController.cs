using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Queries.Dashboard;
using FinTech_ApiPanel.Application.Queries.UserMaster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserMasterController> _logger;

        public UserMasterController(IMediator mediator, ILogger<UserMasterController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            _logger.LogInformation("Creating user with email: {Email}", command.Email);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand command)
        {
            _logger.LogInformation("Updating user with ID: {Id}", command.Id);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query)
        {
            _logger.LogInformation("Getting all users.");
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("me")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            _logger.LogInformation("Getting logged-in user details.");
            var result = await _mediator.Send(new GetLoggedInUserQuery());

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("toggle/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ToggleStatus(long id)
        {
            _logger.LogInformation("Toggling user status for ID: {Id}", id);
            var result = await _mediator.Send(new ToggleUserStatusCommand(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            _logger.LogInformation("User login attempt for: {UsernameOrEmail}", command.UserNameOrEmail);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("reset-password/send-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> SendOTP(ResetPassword_SendOTPCommand command)
        {
            _logger.LogInformation("Sending OTP to: {Email}", command.Email);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("reset-password/verify-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyOTP(ResetPassword_VerifyOTPCommand command)
        {
            _logger.LogInformation("Verifying OTP.");
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            _logger.LogInformation("Resetting password.");
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("change-password")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            _logger.LogInformation("Changing password for user.");
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("verify-user")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> VerifyUserDetail(VerifyUserDetailCommand command)
        {
            _logger.LogInformation("Verifying user detail for command: {@Command}", command);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Dashboard([FromQuery] DashboardQuery query)
        {
            _logger.LogInformation("Fetching dashboard data for user.");
            var result = await _mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("change-ipin/send-otp")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> IpinSendOTP()
        {
            _logger.LogInformation("Sending OTP to change Ipin");
            var result = await _mediator.Send(new ChangeIpin_SendOTPCommand());

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("change-ipin/verify-otp")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> IpinVerifyOTP(ChangeIpin_VerifyOTPCommand command)
        {
            _logger.LogInformation("Verifying OTP.");
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("change-ipin")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> IpinResetPassword(ChangeIpinCommand command)
        {
            _logger.LogInformation("Resetting Ipin.");
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("client-secret")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetSecret()
        {
            _logger.LogInformation("Fetching client secret for user.");
            var result = await _mediator.Send(new GetClientSecretQuery());

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("client-config")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateClientCredentials([FromBody] CreateClientCredentialCommand command)
        {
            _logger.LogInformation("Creating client credentials.");
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("validate-ipin")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> ValidateIpin([FromQuery] ValidateIpinQuery query)
        {
            _logger.LogInformation("Validating Ipin.");
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }
    }
}
