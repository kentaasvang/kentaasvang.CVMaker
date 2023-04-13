using LagDinCv.Api.Services;
using LagDinCv.Domain.Interfaces;

namespace LagDinCv.WebGui;

public static class DependencyInjectionExtensions
{
   public static void AddPdfBuilder(this IServiceCollection services)
   {
      services.AddScoped<IPdfBuilder, PdfBuilder>();
   }
}