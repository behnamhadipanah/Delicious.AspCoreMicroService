using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddDbContext<OrderContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
        });
        service.AddTransient<IOrderRepository, OrderRepository>();
        service.AddTransient(typeof(IAsyncRepository<>),typeof(RepositoryBase<>));
        service.AddTransient<IEmailService, EmailService>();
        return service;
    }
}