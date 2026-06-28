using Microsoft.EntityFrameworkCore;
using SemanticSentry.Core;
using SemanticSentry.App;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure SQLite Database Connection
builder.Services.AddDbContext<SecurityDbContext>(options =>
    options.UseSqlite("Data Source=../SemanticSentry.db"));

// 2. Add YARP Reverse Proxy Services
builder.Services.AddReverseProxy()
    .LoadFromMemory(new List<Yarp.ReverseProxy.Configuration.RouteConfig>(), 
                    new List<Yarp.ReverseProxy.Configuration.ClusterConfig>());

var app = builder.Build();

// Enable serving your frontend HTML, CSS, and JS web files
app.UseDefaultFiles();
app.UseStaticFiles();

// 3. Inject our Zero-Trust Threat Analytics Middleware Layer
app.UseMiddleware<SecurityMiddleware>();

// 4. Secure API Data Endpoint for Frontend Dashboard
app.MapGet("/api/security/logs", async (SecurityDbContext db) => {
    var logs = await db.RequestLogs.ToListAsync();
    return Results.Ok(logs);
});

// 5. Run the YARP proxy pipelines
app.MapReverseProxy();

app.Run();