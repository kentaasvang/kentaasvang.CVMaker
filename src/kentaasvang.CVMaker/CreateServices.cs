using LagDinCv.Application.Interfaces;
using LagDinCv.Domain;
using LagDinCv.Infrastructure.Data;
using LagDinCv.Infrastructure.Services;

namespace LagDinCv.WebUI;

public static class CreateServices
{
    public static void AddPdfBuilder(this IServiceCollection services)
    {
        services.AddScoped<IPdfBuilder, PdfBuilder>();
    }

    public static void AddWebUiComponents(this IServiceCollection services)
    {
        services.AddDefaultIdentity<UserEntity>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}