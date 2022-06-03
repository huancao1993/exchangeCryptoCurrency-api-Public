using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Trading.Authen.Api.Helpers;
using Trading.Services.Helpers;

namespace Trading.Authen.Api.Commons
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is AuthenException exception)
                {
                    JsonResponse jsonRespose = new JsonResponse
                    {
                        Messages = new[] { context.Exception.Message },
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    context.Result = new BadRequestObjectResult(jsonRespose);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.ExceptionHandled = true;
                }
                else
                {

                    JsonResponse jsonRespose = new JsonResponse
                    {
                        Messages = new[] { context.Exception.Message },
                        DeveloperMessage = $"{context.Exception.Message} \r\n {context.Exception.StackTrace}",
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    context.Result = new ObjectResult(jsonRespose);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.ExceptionHandled = true;
                }

            }
        }
    }
}
