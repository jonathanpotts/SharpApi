using Microsoft.AspNetCore.Mvc;

namespace SharpApi.Example
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [Route("Test")]
        public IActionResult Get()
        {
            return Ok("Hello, world!");
        }
    }
}
