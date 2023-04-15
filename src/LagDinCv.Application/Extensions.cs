using LagDinCv.Domain.Constants;
using LagDinCv.Domain.Enums;

namespace LagDinCv.Application;

public static class Extensions
{
   public static string ToFileName(this CvTemplateType cvTemplateType)
      => cvTemplateType switch
      {
         CvTemplateType.TwentySecond => TemplateFileNames.TwentySecond,
         _ => throw new ArgumentOutOfRangeException(nameof(cvTemplateType), cvTemplateType, null)
      };
}