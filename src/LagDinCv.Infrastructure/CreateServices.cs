using LagDinCv.Application.Interfaces;
using LagDinCv.Infrastructure.Data;
using LagDinCv.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LagDinCv.Infrastructure;

public static class CreateServices
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, serverVersion));

        services.AddScoped<IFileWriter, FileWriter>();
    }
}