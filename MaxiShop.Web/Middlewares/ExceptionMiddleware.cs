using MaxiShop.Application.Exceptions;
using MaxiShop.Web.Models;
using System.Net;

namespace MaxiShop.Web.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

       public async Task InvokeAsync(HttpContext httpContext)
        {
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{

				await HandleExceptionAsync(httpContext, ex);
			}
        }

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
		{
			HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
			CustomProblemDetails problemDetails = new();

			switch(exception)
			{
				case BadRequestException badRequestException:
					statusCode = HttpStatusCode.BadRequest;
					problemDetails = new CustomProblemDetails()
					{
						Title = exception.Message,
						Status = ((int)statusCode),
						Type = nameof(exception),
						Detail = badRequestException.InnerException?.Message,
						Errors = badRequestException.ValidationErrors
					};
					break;
			}

			httpContext.Response.StatusCode = (int)statusCode;
			await httpContext.Response.WriteAsJsonAsync(problemDetails);
		}
    }
}
