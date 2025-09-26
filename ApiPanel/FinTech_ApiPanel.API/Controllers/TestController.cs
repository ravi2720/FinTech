using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_ApiPanel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICryptoService _cryptoService;
        public TestController(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }
        [HttpGet("process")]
        //[ServiceValidate(ServiceType.DMT)]
        //[WhitelistValidate]
        //[IPayValidate]
        public async Task<IActionResult> Test()
        {

            var encryptionKey = _cryptoService.Generate32CharKey();
            var clientId = _cryptoService.GenerateClientId();
            var clientSecret = _cryptoService.GenerateClientSecret();
            return Ok(new { clientId, clientSecret, encryptionKey });
        }
    }
}
