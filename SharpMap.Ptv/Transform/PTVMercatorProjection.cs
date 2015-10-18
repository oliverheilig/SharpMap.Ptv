using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.CoordinateSystems.Transformations;
using SharpMap.CoordinateSystems;

namespace Ptv.Controls.Map
{
    /// <summary>
    /// SharpMap Parmeters for PTV_Mercator
    /// Warning: http://www.iter.dk/post/2006/07/Applying-on-the-fly-transformation-in-SharpMap.aspx
    /// "Unfortunately datum-shifts and transformations between two projections are still down the pipeline"
    /// </summary>
    public static class PTVMercatorProjection
    {
        /// <summary>
        /// PTV_MERCATOR as IProjectedCoordinateSystem
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IProjectedCoordinateSystem GetProjectedCoordinateSystem(ICoordinateSystem source)
        {
            CoordinateSystemFactory cFac = new SharpMap.CoordinateSystems.CoordinateSystemFactory();

            System.Collections.Generic.List<ProjectionParameter> parameters = new System.Collections.Generic.List<ProjectionParameter>(5);
            parameters.Add(new ProjectionParameter("latitude_of_origin", 0));
            parameters.Add(new ProjectionParameter("central_meridian", 0));
            parameters.Add(new ProjectionParameter("false_easting", 0));
            parameters.Add(new ProjectionParameter("false_northing", 0));
            parameters.Add(new ProjectionParameter("semi_major", 6371000));
            parameters.Add(new ProjectionParameter("semi_minor", 6371000));
            IProjection projection = cFac.CreateProjection("Mercator", "Mercator_2SP", parameters);

            return new SharpMap.CoordinateSystems.CoordinateSystemFactory().CreateProjectedCoordinateSystem(
                      "Mercator", source as IGeographicCoordinateSystem, projection, LinearUnit.Metre,
                      new AxisInfo("East", AxisOrientationEnum.East), new AxisInfo("North", AxisOrientationEnum.North));
        }

        /// <summary>
        /// PTV_Mercator as ICoordinateSystem
        /// </summary>
        /// <returns>the coordinate system</returns>
        public static ICoordinateSystem GetMercatorCoordinateSystem()
        {
            return (SharpMap.CoordinateSystems.ICoordinateSystem)SharpMap.Converters.WellKnownText.CoordinateSystemWktReader.Parse(
                @"PROJCS[""Mercator Spheric"", GEOGCS[""WGS84basedSpheric_GCS"", DATUM[""WGS84basedSpheric_Datum"", SPHEROID[""WGS84based_Sphere"", 6371000, 0], TOWGS84[0, 0, 0, 0, 0, 0, 0]], PRIMEM[""Greenwich"", 0, AUTHORITY[""EPSG"", ""8901""]], UNIT[""degree"", 0.0174532925199433, AUTHORITY[""EPSG"", ""9102""]], AXIS[""E"", EAST], AXIS[""N"", NORTH]], PROJECTION[""Mercator""], PARAMETER[""False_Easting"", 0], PARAMETER[""False_Northing"", 0], PARAMETER[""Central_Meridian"", 0], PARAMETER[""Latitude_of_origin"", 0], UNIT[""metre"", 1, AUTHORITY[""EPSG"", ""9001""]], AXIS[""East"", EAST], AXIS[""North"", NORTH]]");
        }

        /// <summary>
        /// Transformation from PTV_Mercator to destination
        /// </summary>
        /// <param name="source">destionation coordinate system (no projection!)</param>
        /// <returns>the transformation</returns>
        public static ICoordinateTransformation TransformFromMercator(ICoordinateSystem destination)
        {
            return new CoordinateTransformationFactory().CreateFromCoordinateSystems(
                GetProjectedCoordinateSystem(destination), destination);
        }

        /// <summary>
        /// Transformation from source to PTV_Mercator
        /// </summary>
        /// <param name="source">destionation coordinate system (no projection!)</param>
        /// <returns>the transformation</returns>
        public static ICoordinateTransformation TransformToMercator(ICoordinateSystem source)
        {
            return new CoordinateTransformationFactory().CreateFromCoordinateSystems(
                source, GetProjectedCoordinateSystem(source));
        }
    }
}
