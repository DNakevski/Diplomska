using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Helpers
{
    public class ColorHelper
    {
        public static bool IsColor(string input)
        {
            List<string> colors = new List<string>() { 
                "darkBlueLighter",
                "darkBlueDarker",
                "lightBlueLighter",
                "lightBlueDarker",
                "greenLighter",
                "greenDarker",
                "orangeLighter",
                "orangeDarker",
                "redLighter",
                "redDarker",
                "darkGreyLighter",
                "darkGreyDarker",
                "lightGreyLighter",
                "lightGreyDarker",
                "white",
                "black"
            };
            //dodadi gi svite boe...

            return colors.Contains(input);
        }
    }
}