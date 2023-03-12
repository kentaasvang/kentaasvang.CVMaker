using LagDinCv.Api.Services;

namespace LagDinCv.Api;

public static class DependencyInjectionExceptions
{
   public static void AddPdfBuilder(this IServiceCollection services)
   {
      services.AddScoped<IPdfBuilderService, PdfBuilderService>();
   }
}