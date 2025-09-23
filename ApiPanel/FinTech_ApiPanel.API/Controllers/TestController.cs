using FinTech_ApiPanel.API.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("process")]
        //[ServiceValidate(ServiceType.DMT)]
        //[WhitelistValidate]
        //[IPayValidate]
        public async Task<IActionResult> Test()
        {

            return Ok();
        }

    }
}
