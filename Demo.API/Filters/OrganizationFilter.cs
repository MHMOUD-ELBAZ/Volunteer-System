using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.API.Filters
{
    public class OrganizationFilter : ActionFilterAttribute
    {
        public OrganizationFilter() { }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userType = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value?? "" ;
            if (userType != UserType.Organization.ToString())
                context.Result = new ObjectResult("This action is forbidden for non-organization account.") 
                { 
                    StatusCode = 403
                }; 
        }
    }
}
