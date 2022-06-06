using Microsoft.AspNetCore.Builder;

namespace WebApiSecurityDemo.Utils
{
    public static class ErrorHandlerMiddlewarExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder, ILoggerService logger)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>(logger);
        }
    }
}