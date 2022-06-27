using Domain.Customers;
using Domain.Services;
using Domain.Services.Customers;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjectorExtension
{

    public static IServiceCollection UseDependencies(this IServiceCollection services)
    {
        
        services.AddSingleton<CommandQueryFactory>();
        services.AddSingleton<IHandler, Handler>();
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
        
        return services;
    }
    
}