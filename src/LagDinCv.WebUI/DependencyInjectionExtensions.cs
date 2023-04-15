using LagDinCv.Application.Interfaces;
using LagDinCv.Infrastructure.Services;

namespace LagDinCv.WebUI;

public static class DependencyInjectionExtensions
{
   public static void AddPdfBuilder(this IServiceCollection services)
   {
      services.AddScoped<IPdfBuilder, PdfBuilder>();
   }
}