using Serilog.Context;

namespace FinanceManager.API.Middleware;

public sealed class RequestLogContextMiddleWare
{
    private readonly RequestDelegate _next;

    public RequestLogContextMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            return _next(context);
        }
    }
}