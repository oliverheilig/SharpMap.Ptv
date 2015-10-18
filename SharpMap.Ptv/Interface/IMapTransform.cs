using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Geometries;
using SharpMap.CoordinateSystems.Transformations;
using SharpMap.CoordinateSystems;

namespace Ptv.Controls.Map.Interface
{
    public interface IMapTransform
    {
        int MapProjectionSRID
        {
            get;
        }

        Point PixelToMap(Point pixelCoordinate);

        Point MapToPixel(Point mapCoordinate);

        /// <summary>
        /// gets the transformation object for transforming to map coorindates
        /// </summary>
        /// <param name="sourceCoordinateSystem">the source coordinate system</param>
        /// <returns></returns>
        ICoordinateTransformation ToMapTransformation(ICoordinateSystem sourceCoordinateSystem);

        /// <summary>
        /// gets the transformation object for transforming from map coordinates
        /// </summary>
        /// <param name="sourceCoordinateSystem">the destination coordinate system</param>
        /// <returns></returns>
        ICoordinateTransformation FromMapTransformation(ICoordinateSystem destinationCoordinateSystem);
    }
}
