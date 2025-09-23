using FinTech_ApiPanel.Application.Commands.ServiceMasters;
using FinTech_ApiPanel.Application.Queries.ServiceMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceMasterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ServiceMasterController> _logger;

        public ServiceMasterController(IMediator mediator, ILogger<ServiceMasterController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateServiceMasterCommand command)
        {
            _logger.LogInformation("Creating service: {Title}", command.Title);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all services.");
            var result = await _mediator.Send(new GetAllServiceMastersQuery());

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(long id)
        {
            _logger.LogInformation("Getting service ID: {Id}", id);
            var result = await _mediator.Send(new GetServiceMasterByIdQuery(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Update(UpdateServiceMasterCommand command)
        {
            _logger.LogInformation("Updating service ID: {Id}", command.Id);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("toggle")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> ToggleStatus(ToggleServiceStatusCommand command)
        {
            _logger.LogInformation("Toggling service status ID: {Id}", command);
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("service-types")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetServiceTypes()
        {
            var serviceTypes = Enum.GetValues(typeof(ServiceType))
                .Cast<ServiceType>()
                .Select(e => new
                {
                    Type = (byte)e,
                    Title = e.ToString()
                })
                .ToArray();

            return Ok(serviceTypes);
        }
    }
}