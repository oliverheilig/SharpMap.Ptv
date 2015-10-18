using System;
using System.Collections.Generic;
using System.Text;

namespace Ptv.Controls.Map
{
    public class WGS84Formatter
    {
        public static string CoordToString(double latitude)
        {
            int x = GeoDecimal_2_GeoMinSec(latitude * 100000.0);

            return string.Format("{0}° {1:00.}' {2:00.}\"",
                                x / 100000,
                                (x % 100000) / 1000,
                                (x % 1000) / 10);
        }

        public static int GeoDecimal_2_GeoMinSec(double xIn)
        {
            double xArg = (int)System.Math.Round(xIn);

            double deg, min, sec, tmp;
            int vz;
            if (xArg >= 0)
            {
                vz = 1;
            }
            else
            {
                vz = -1;
                xArg = -xArg;
            }
            deg = (int)(xArg / 100000);
            min = ((xArg / 100000) - deg) * 60;
            tmp = (int)min;
            sec = (min - tmp) * 600;
            min = tmp;
            xArg = (deg * 100000 + min * 1000 + sec) * vz;

            return (int)xArg;
        }
    }
}
