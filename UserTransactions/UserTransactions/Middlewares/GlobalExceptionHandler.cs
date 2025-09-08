
using System.Net;
using UserTransactions.Domain.Exceptions;

namespace UserTransactions.API.Middlewares
{
    internal sealed class GlobalExceptionHandler(
        RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger)
    {
        public static Dictionary<Type, HttpStatusCode> HttpCodeBasedOnExceptionType { get; } = [];
        static GlobalExceptionHandler()
        {
            HttpCodeBasedOnExceptionType[typeof(UserNotFoundException)] = HttpStatusCode.NotFound;
            HttpCodeBasedOnExceptionType[typeof(InvalidTransactionObjectException)] = HttpStatusCode.BadRequest;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // extra info can be logged here

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                if (HttpCodeBasedOnExceptionType.TryGetValue(ex.GetType(), out HttpStatusCode statusCode))
                {
                    context.Response.StatusCode = (int)statusCode;
                }

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        Type = ex.GetType().Name,
                        Title = "An error occured",
                        Details = ex.Message
                    });
            }
        }
    }
}
