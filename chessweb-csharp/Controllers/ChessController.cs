using Microsoft.AspNetCore.Mvc;

namespace ChessWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Chess Web API!");
        }
    }
}