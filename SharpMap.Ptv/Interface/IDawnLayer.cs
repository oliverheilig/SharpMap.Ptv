using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Layers;
using System.Drawing;

namespace Ptv.Controls.Map
{
    /// <summary>
    /// extended layer interface
    /// </summary>
    public interface IDawnLayer : ILayer
    {
        /// <summary>
        /// the visible Name of the layer
        /// </summary>
        string VisibleName
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies if layer is visible
        /// </summary>
        bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies if layer objects can be selected
        /// </summary>
        bool Selectable
        {
            get;
            set;
        }

        /// <summary>
        /// the icon of the layer
        /// </summary>
        Image Icon
        {
            get;
            set;
        }

        void AddChildLayer(ILayer layer);
    }
}
