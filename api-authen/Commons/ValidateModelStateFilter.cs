using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Trading.Authen.Api.Commons
{
    public class ValidateModelStateFilter: ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }
            var validationErrors = context.ModelState
           .Keys
           .SelectMany(k => context.ModelState[k].Errors)
           .Select(e => e.ErrorMessage)
           .ToArray();
            JsonResponse jsonRespose = new JsonResponse
            {
                Messages = validationErrors,
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            context.Result = new BadRequestObjectResult(jsonRespose);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
