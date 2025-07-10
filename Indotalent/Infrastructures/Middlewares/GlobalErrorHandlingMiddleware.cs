using System.Text.Json;

using Indotalent.Applications.LogErrors;

namespace Indotalent.Infrastructures.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, LogErrorService logErrorService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await CollectAndHandleErrorAsync(context, ex, logErrorService);
            }
        }

        private async Task CollectAndHandleErrorAsync(HttpContext context, Exception ex,
            LogErrorService logErrorService)
        {
            string? errorMessage = ex.Message;
            string? stackTrace = ex.StackTrace;
            string? source = ex.InnerException?.Message;

            await logErrorService.CollectErrorDataAsync(errorMessage, stackTrace, source);

            var errorResponse = new
            {
                status = 520,
                title = "Unknown Error",
                message = errorMessage,
                detail = source,
                path = context.Request.Path,
                query = context.Request.QueryString.ToString()
            };

            context.Response.StatusCode = 520;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
