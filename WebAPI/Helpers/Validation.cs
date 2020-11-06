using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class Validation
    {
        private static string regex = @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$";

        public static bool IsGPSCoOrdinates(string coOrdinates)
        {
            if (!string.IsNullOrEmpty(coOrdinates) && coOrdinates.Contains(","))
            {
                string[] gps = new string[2];
                gps = coOrdinates.Split(",".ToCharArray());

                if (gps.Length == 2)
                {
                    double lat = Convert.ToDouble(gps[0]);
                    double lng = Convert.ToDouble(gps[1]);

                    if (lat < -90 || lat > 90)
                    {
                        throw new ArgumentOutOfRangeException("Latitude must be between -90 and 90 degrees inclusive.");
                    }
                    if (lng < -180 || lng > 180)
                    {
                        throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180 degrees inclusive.");
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsGPSCoOrdinates(string lattitude, string longitude)
        {
            var match = Regex.Match(lattitude + "," + longitude, regex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
