using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpMap.Geometries;

namespace Ptv.Controls.Map.Interface
{
    /// <summary>
    /// interface for Printing the map
    /// </summary>
    public interface IMapPrint
    {
        /// <summary>
        /// gets the map image with the current zoom box
        /// </summary>
        /// <param name="width">the width of the image</param>
        /// <param name="height">the height of the image</param>
        /// <returns>the image</returns>
        Image GetMapImage(int width, int height);

        /// <summary>
        /// gets the map image with the current zoom box a specified dpi number
        /// note: the resulting image doesn't match the requested width an height
        /// you must use a strech-draw for your control
        /// </summary>
        /// <param name="width">the width of the image</param>
        /// <param name="height">the height of the image</param>
        /// <param name="dpi">the dpi number</param>
        /// <returns>the image</returns>
        Image GetMapImage(int width, int height, System.Drawing.Graphics gr);

        /// <summary>
        /// gets the map image for a specified bounding rectangle
        /// </summary>
        /// <param name="bbox">the bounding box the coordinates must be in the projection of the map control</param>
        /// <param name="width">the width of the image</param>
        /// <param name="height">the height of the image</param>
        /// <returns>the image</returns>
        Image GetMapImage(BoundingBox bbox, int width, int height);

        /// <summary>
        /// gets the map image for a specified bounding rectangle
        /// note: the resulting image doesn't match the requested width an height
        /// you must use a strech-draw for your control
        /// </summary>
        /// <param name="bbox">the bounding box the coordinates must be in the projection of the map control</param>
        /// <param name="width">the width of the image</param>
        /// <param name="height">the height of the image</param>
        /// <param name="dpi">the dpi number</param>
        /// <returns>the image</returns>
        Image GetMapImage(BoundingBox bbox, int width, int height, System.Drawing.Graphics gr);
    }
}
