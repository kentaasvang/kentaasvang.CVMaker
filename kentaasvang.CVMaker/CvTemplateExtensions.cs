using kentaasvang.CVMaker.Constants;
using kentaasvang.CVMaker.Enums;

namespace kentaasvang.CVMaker;

public static class CvTemplateExtensions
{
   public static string ToFileName(this CvTemplateType cvTemplateType)
      => cvTemplateType switch
      {
         CvTemplateType.TwentySecond => TemplateFileNames.TwentySecond,
         _ => throw new ArgumentOutOfRangeException(nameof(cvTemplateType), cvTemplateType, null)
      };
}