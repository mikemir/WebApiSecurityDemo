using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebApiSecurityDemo.Utils.Middlewares
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LimitRequestsAttribute : Attribute
    {
        public int TimeWindow { get; set; }
        public int MaxRequests { get; set; }
    }

    public class ClientStatistics
    {
        public DateTime LastSuccessResponseTime { get; set; }
        public int NumberOfRequestsCompleted { get; set; }
    }

    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        private static string GenerateClientKey(HttpContext context)
            => $"{context.Request.Path}_{context.Connection.RemoteIpAddress}";

        private ClientStatistics GetClientStatisticsByKey(string key)
        {
            return _cache.Get<ClientStatistics>(key);
        }

        private void UpdateClientStatisticsStorage(string key, ClientStatistics clientStatistics)
        {
            _cache.Set<ClientStatistics>(key, clientStatistics);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var rateLimitingDecorator = endpoint?.Metadata.GetMetadata<LimitRequestsAttribute>();

            if (rateLimitingDecorator is null)
            {
                await _next(context);
                return;
            }

            var key = GenerateClientKey(context);
            var clientStatistics = GetClientStatisticsByKey(key);

            if (clientStatistics != null &&
                DateTime.UtcNow < clientStatistics.LastSuccessResponseTime.AddSeconds(rateLimitingDecorator.TimeWindow))
            {
                if (clientStatistics.NumberOfRequestsCompleted >= rateLimitingDecorator.MaxRequests)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    return;
                }

                UpdateClientStatisticsStorage(key, new ClientStatistics
                {
                    LastSuccessResponseTime = clientStatistics.LastSuccessResponseTime,
                    NumberOfRequestsCompleted = clientStatistics.NumberOfRequestsCompleted + 1
                });
            }
            else
            {
                UpdateClientStatisticsStorage(key, new ClientStatistics
                {
                    LastSuccessResponseTime = DateTime.UtcNow,
                    NumberOfRequestsCompleted = 1
                });
            }

            await _next(context);
        }
    }
}