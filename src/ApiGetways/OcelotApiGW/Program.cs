using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();

var app = builder.Build();

builder.Host.ConfigureLogging((hostingContext, loggingBuilder) => {
    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
   loggingBuilder.AddDebug();
    loggingBuilder.AddConsole();
});

app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();
