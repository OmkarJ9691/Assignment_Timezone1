using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Application;

public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}

