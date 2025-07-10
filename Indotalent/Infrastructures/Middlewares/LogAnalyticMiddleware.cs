using Indotalent.Applications.LogAnalytics;

namespace Indotalent.Infrastructures.Middlewares
{
    public class LogAnalyticMiddleware
    {
        private readonly RequestDelegate _next;

        public LogAnalyticMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, LogAnalyticService logAnalyticService)
        {
            await logAnalyticService.CollectAnalyticDataAsync();
            await _next(context);
        }
    }
}
