using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructures;
using Ordering.Application.Contracts.Persistances;
using Ordering.Application.Models;
using Ordering.Infrastructure.Email;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection  services,IConfiguration configuration)
    {
        services.AddDbContext<OrderContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
        });

        services.AddScoped<IOrderRepository,OrderRepository>();
        services.AddScoped(typeof(IAsyncRepository<>),typeof(RepositoryBase<>));

        services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
