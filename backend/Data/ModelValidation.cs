using System.Text.RegularExpressions;
using backend.Models;

namespace backend.Data
{
  public class ModelValidation
  {
    public static bool HasValidFeedingData(Feeding feeding)
    {
      if (feeding.eatenpercentage < 0 || feeding.eatenpercentage > 100)
      {
        return false;
      }
      return true;
    }

    public static  bool HasValidCatname(Feeding feeding)
    {
      string pattern = "^[A-Z][a-z]+$";
      Regex rg = new Regex(pattern);
      if (rg.IsMatch(feeding.catname))
      {
        return true;
      }
      return false;
    }

  }
}