using Microsoft.AspNetCore.Mvc;

namespace FiapReservas.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public string? Teste()
        {
            return Environment.GetEnvironmentVariable("NOME");
        }
    }
}
