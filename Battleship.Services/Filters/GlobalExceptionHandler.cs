using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Battleship.Services.Filters
{
    /// <summary>
    /// this is exception handler to capture all the unhandle excceptions
    /// within the web api pipeline
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public virtual Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            Handle(context);
            return Task.FromResult(0);
        }

        private static void Handle(ExceptionHandlerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var exceptioncontext = context.ExceptionContext;
            var exception = exceptioncontext.Exception;

            if (exception == null)
            {
                //If exception is null, just return
                return;
            }

            //TODO: This is the point to manage all custom code for unhandled exception here 

            //Reference for below codoe is taken from - https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/web-api-global-error-handling
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "Oops! Sorry! Something went wrong." +
                     "Please contact us at anknivyas@outlook.com so we can try to fix it."
            };
        }
        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                                 new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(Content);
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}