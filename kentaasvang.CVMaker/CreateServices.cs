using kentaasvang.CVMaker.Data;
using kentaasvang.CVMaker.Services;
using Microsoft.EntityFrameworkCore;

namespace kentaasvang.CVMaker;

public static class CreateServices
{
    public static void AddPdfBuilder(this IServiceCollection services)
    {
        services.AddScoped<PdfBuilder>();
    }

    public static void AddWebUiComponents(this IServiceCollection services)
    {
        // services.AddDefaultIdentity<UserEntity>()
        //     .AddEntityFrameworkStores<ApplicationDbContext>();
    }

    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, serverVersion));
    }

    public static void AddFileWriter(this IServiceCollection services)
    {
        services.AddScoped<FileWriter>();
    }
}