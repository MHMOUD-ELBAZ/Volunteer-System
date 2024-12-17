using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        //[HttpGet(Name = "Get")]
        //public IActionResult Get()
        //{
        //    //var res = context.Volunteers.Include(v => v.User).ToList();
        //    var query = context.Reviews.FromSql($"SELECT * FROM Review R INNER JOIN [Application] A ON R.ApplicationId = A.Id INNER JOIN Volunteer V ON A.VolunteerId = V.VolunteerId;");

        //    //roleManager.Roles.Add(new IdentityRole("Volunteer"));

        //    return Ok(query.ToList());
        //}
    }
}
