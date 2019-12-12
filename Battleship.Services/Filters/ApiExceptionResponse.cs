using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Battleship.Services.Filters
{
    //Exception filters can be used for handling expected exceptions
    //For Expected excception, we can format the response as per the context

    public class ApiExceptionResponse : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
        }
    }
}