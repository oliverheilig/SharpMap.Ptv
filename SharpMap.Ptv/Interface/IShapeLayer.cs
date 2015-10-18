using System;
using System.Collections.Generic;
using System.Text;

namespace Ptv.Controls.Map.Interface
{
    public interface IShapeLayer
    {
        /// <summary>
        /// Adds a shape object
        /// </summary>
        /// <param name="shape"></param>
        void AddShape(Shape shape);

        /// <summary>
        /// Removes a shpe object
        /// </summary>
        /// <param name="shape"></param>
        void DeleteShape(Shape shape);

        /// <summary>
        /// Delete all shapes
        /// </summary>
        void ClearShapes();
    }
}
