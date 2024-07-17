using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AspireGeneralAvailabilityDemoHarness.Web
{
  public class CustomExceptionFilter : IExceptionFilter
  {
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
      _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
      _logger.LogError(context.Exception, "An unhandled exception occurred.");

      context.Result = new ContentResult
      {
        Content = "<h1>An error occurred while processing your request.</h1>",
        ContentType = "text/html",
        StatusCode = (int)HttpStatusCode.InternalServerError
      };

      context.ExceptionHandled = true;
    }
  }
}
