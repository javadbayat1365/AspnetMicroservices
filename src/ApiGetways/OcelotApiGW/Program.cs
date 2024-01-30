using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

builder.Configuration.AddJsonFile("ocelot.local.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/", () => "Hello Ocelot");
await app.UseOcelot();

app.Run();
