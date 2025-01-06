using Demo.Business.Exceptions;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Demo.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ForbiddenAccessException)
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
            else if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else
            {
                var method = context.HttpContext.Request.Method;
                
                StringBuilder resultMessage =
                    new StringBuilder("An unexpected error occurred, call \"Mahmoud_Elbaz\" to investigate in it.");

                if (method == HttpMethods.Post || method == HttpMethods.Put)
                    resultMessage.Append("\nMake sure that all IDs already exists and try again.");
                
                context.Result = new ObjectResult(resultMessage.ToString())
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            
            context.ExceptionHandled = true;
        }
    }
}
