using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.API.Filters
{
    public class VolunteerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userType = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value ?? "";
            if (userType != UserType.Volunteer.ToString())
                context.Result = new ObjectResult("This action is forbidden for non-volunteer account.")
                {
                    StatusCode = 403
                };
        }
    }
}
