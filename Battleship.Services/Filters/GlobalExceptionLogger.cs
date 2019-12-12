using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Battleship.Services.Filters
{
    /// <summary>
    /// Logging all the unhandled exception for health monitoring and trouble shooting
    /// </summary>
    class GlobalExceptionLogger : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var exception = context.ExceptionContext.Exception;
            //TODO: Implement the proper logging solution
            //Log the exception as per the logging implementation for the application

            return Task.FromResult(0);
        }
    }
}