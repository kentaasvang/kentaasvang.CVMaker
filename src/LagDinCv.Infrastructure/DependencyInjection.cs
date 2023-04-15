using LagDinCv.Application.Interfaces;
using LagDinCv.Infrastructure.Data;
using LagDinCv.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LagDinCv.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, serverVersion));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IFileWriter, FileWriter>();
    }
}