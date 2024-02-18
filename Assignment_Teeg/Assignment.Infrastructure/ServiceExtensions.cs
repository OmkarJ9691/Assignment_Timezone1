using Assignment.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Assignment.Application.Interface;

namespace Assignment.Infrastructure;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
        //    configuration.GetConnectionString("DefaultConnection")
        //    ));

        services.AddDbContext<ApplicationDbContext>();

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }
}

