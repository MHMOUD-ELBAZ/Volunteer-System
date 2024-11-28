using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestingController : ControllerBase
    {
        private readonly AppDbContext context;

        public TestingController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "Get")]
        public IActionResult Get()
        {
            //var res = context.Volunteers.Include(v => v.User).ToList();
            return Ok($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}");
        }
    }
}
