using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controller
{
    [Route("/api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> TestInboundPostAsync()
        {
            return Ok("Testing TestInboundPostAsync.");
        }


    }
}