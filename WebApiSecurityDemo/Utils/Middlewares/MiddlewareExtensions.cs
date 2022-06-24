using Microsoft.AspNetCore.Builder;

namespace WebApiSecurityDemo.Utils.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder, ILoggerManager loggerService)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>(loggerService);
        }

        public static IApplicationBuilder UseRateLimit(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimitingMiddleware>();
        }
    }
}