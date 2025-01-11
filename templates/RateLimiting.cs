public class RateLimitMiddleware {
    private static readonly Dictionary<string, DateTime> RequestTimes = new();
    private readonly RequestDelegate _next;

    public async Task InvokeAsync(HttpContext context) {
        string clientIp = context.Connection.RemoteIpAddress?.ToString();
        if (clientIp != null && RequestTimes.TryGetValue(clientIp, out var lastRequest)) {
            if ((DateTime.Now - lastRequest).TotalSeconds < 1) {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return;
            }
        }
        RequestTimes[clientIp] = DateTime.Now;
        await _next(context);
    }
}
