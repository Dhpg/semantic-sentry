using Microsoft.AspNetCore.Http;
using SemanticSentry.Core;
using System.Threading.Tasks;

namespace SemanticSentry.App
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, SecurityDbContext dbContext)
        {
            // Extract request details for analysis
            string method = context.Request.Method;
            string path = context.Request.Path;
            string ip = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            
            // Extract Authorization header if present
            string token = context.Request.Headers["Authorization"].ToString();

            // Run the Threat Analytics Engine
            var evaluator = new ThreatEvaluator();
            RequestLog evaluationResult = evaluator.EvaluateRequest(method, path, ip, token);

            // Save the logs into our SQLite database asynchronously
            dbContext.RequestLogs.Add(evaluationResult);
            await dbContext.SaveChangesAsync();

            // If the engine flags it as blocked, halt the request immediately
            if (evaluationResult.IsBlocked)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Access Denied by SemanticSentry",
                    reason = evaluationResult.MitigationReason,
                    score = evaluationResult.FinalSuspicionScore
                });
                return; // Stop processing the request completely
            }

            // Otherwise, forward it to the next step (YARP Reverse Proxy or endpoint)
            await _next(context);
        }
    }
}