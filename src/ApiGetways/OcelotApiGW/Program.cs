using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("ocelot.Development.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration).AddCacheManager(o => o.WithDictionaryHandle());

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/", () => "Hello Ocelot");
await app.UseOcelot();

app.Run();
