using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Geometries;

namespace Ptv.Controls.Map.Interface
{
    /// <summary>
    /// interace for BusinessObject which support geometry information
    /// </summary>
    public interface IGeometryInfo
    {
        /// <summary>
        /// the SharpMap geometry, must be WGS84!
        /// </summary>
        Geometry Geometry
        {
            get;
        }
    }
}
