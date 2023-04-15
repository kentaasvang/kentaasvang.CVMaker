using LagDinvCv.Application.Services;
using LagDinCv.Domain.Interfaces;

namespace LagDinCv.WebUI;

public static class DependencyInjectionExtensions
{
   public static void AddPdfBuilder(this IServiceCollection services)
   {
      services.AddScoped<IPdfBuilder, PdfBuilder>();
   }
}