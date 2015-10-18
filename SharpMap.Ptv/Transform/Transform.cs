using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Geometries;

namespace Ptv.Controls.Map.Transform
{
    public class Transform
    {
        private const double SMARTFACTOR = 4.809543;
        private const int SMART_OFFSET = 20015087;    // 1/2 Earth-circumference(PI*MERCATOR_RADIUS);

        public static Point Trans(CoordinateFormat inFormat, CoordinateFormat outFormat, double x, double y)
        {
            return Trans(inFormat, outFormat, new Point(x, y));
        }

        public static Point Trans(CoordinateFormat inFormat, CoordinateFormat outFormat, Point point)
        {
            // int == out -> return
            if (inFormat == outFormat)
                return point;

            // direct transformations
            if (inFormat == CoordinateFormat.PTV_Geodecimal && outFormat == CoordinateFormat.WGS84)
                return Geodecimal_2_WGS84(point);

            if (inFormat == CoordinateFormat.WGS84 && outFormat == CoordinateFormat.PTV_Geodecimal)
                return WGS84_2_Geodecimal(point);

            if (inFormat == CoordinateFormat.PTV_Mercator && outFormat == CoordinateFormat.PTV_SmartUnits)
                return Mercator_2_SmartUnits(point);

            if (inFormat == CoordinateFormat.PTV_SmartUnits && outFormat == CoordinateFormat.PTV_Mercator)
                return SmartUnits_2_Mercator(point);

            if (inFormat == CoordinateFormat.PTV_Mercator && outFormat == CoordinateFormat.PTV_Geodecimal)
                return Mercator_2_GeoDecimal(point);

            if (inFormat == CoordinateFormat.PTV_Geodecimal && outFormat == CoordinateFormat.PTV_Mercator)
                return GeoDecimal_2_Mercator(point);

            // transitive transformations
            if (inFormat == CoordinateFormat.PTV_SmartUnits)
            {
                return Trans(CoordinateFormat.PTV_Mercator, outFormat,
                    SmartUnits_2_Mercator(point));
            }

            if (outFormat == CoordinateFormat.PTV_SmartUnits)
            {
                return Mercator_2_SmartUnits(
                    Trans(inFormat, CoordinateFormat.PTV_Mercator, point));
            }

            if (inFormat == CoordinateFormat.WGS84)
            {
                return Trans(CoordinateFormat.PTV_Geodecimal, outFormat,
                    WGS84_2_Geodecimal(point));
            }

            if (outFormat == CoordinateFormat.WGS84)
            {
                return Geodecimal_2_WGS84(
                    Trans(inFormat, CoordinateFormat.PTV_Geodecimal, point));
            }

            if (inFormat == CoordinateFormat.PTV_Geodecimal && outFormat == CoordinateFormat.PTV_Mercator)
            {
                return GeoDecimal_2_Mercator(point);
            }

            if (inFormat == CoordinateFormat.PTV_Mercator && outFormat == CoordinateFormat.PTV_Geodecimal)
            {
                return Mercator_2_GeoDecimal(point);
            }

            throw new NotImplementedException(string.Format("transformation not implemented for {0} to {1}",
                inFormat.ToString(), outFormat.ToString()));
        }

        public static Point Geodecimal_2_WGS84(Point point)
        {
            return new Point(point.X / 100000.0, point.Y / 100000.0);
        }

        public static Point WGS84_2_Geodecimal(Point point)
        {
            return new Point(point.X * 100000.0, point.Y * 100000.0);
        }

        public static Point SmartUnits_2_Mercator(Point point)
        {
            return new Point(
                (point.X * SMARTFACTOR) - SMART_OFFSET,
                (point.Y * SMARTFACTOR) - SMART_OFFSET);
        }

        public static Point Mercator_2_SmartUnits(Point point)
        {
            return new Point(
                (point.X + SMART_OFFSET) / SMARTFACTOR,
                (point.Y + SMART_OFFSET) / SMARTFACTOR);
        }

        public static Point GeoDecimal_2_Mercator(Point point)
        {
            double xArg = point.X, yArg = point.Y;

            xArg = xArg / 100000;
            yArg = yArg / 100000;

            double lambda, phi;
            lambda = xArg;
            phi = yArg;
            xArg = 6371000.0 * ((Math.PI / 180) * ((lambda) - 0.0));
            yArg = 6371000.0 * Math.Log(Math.Tan((Math.PI / 4) + (Math.PI / 180) * phi * 0.5));

            return new Point(xArg, yArg);
        }

        public static Point Mercator_2_GeoDecimal(Point point)
        {
            double xArg = point.X, yArg = point.Y;

            double lambda, phi;
            lambda = (180 / Math.PI) * (xArg / 6371000.0 + 0.0);
            phi = (180 / Math.PI) * (Math.Atan(Math.Exp(yArg / 6371000.0)) - (Math.PI / 4)) / 0.5;
            xArg = lambda;
            yArg = phi;
            xArg = xArg * 100000;
            yArg = yArg * 100000;

            return new Point(xArg, yArg);
        }
    }
}