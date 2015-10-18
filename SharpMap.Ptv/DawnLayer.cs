using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Layers;
using SharpMap;
using SharpMap.Geometries;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using Ptv.Controls.Map.Interface;

namespace Ptv.Controls.Map
{
    public class DawnLayer : InteractiveLayer, IDawnLayer
    {
        /// <summary>
        /// Initializes a new layer
        /// </summary>
        /// <param name="layername">Name of layer</param>
        public DawnLayer(string layername)
            : base(layername)
        {
        }
        /// <summary>
        /// Initializes a new layer with a specified datasource
        /// </summary>
        /// <param name="layername">Name of layer</param>
        /// <param name="dataSource">Data source</param>
        public DawnLayer(string layername, SharpMap.Data.Providers.IProvider dataSource)
            : base(layername, dataSource)
        {
        }
    
        #region IDawnLayer Members

        private string m_VisibleName;
        public string VisibleName
        {
            get
            {
                if (string.IsNullOrEmpty(m_VisibleName))
                    return LayerName;
                else
                    return m_VisibleName;
            }
            set
            {
                m_VisibleName = value;
            }
        }

        public bool Visible
        {
            get
            {
                return this.Enabled;
            }
            set
            {
                this.Enabled = value;
                foreach (ILayer layer in m_childLayers)
                {
                    layer.Enabled = value;
                }
            }
        }        
        
        private bool m_Selectable;
        public bool Selectable
        {
            get
            {
                return m_Selectable;
            }
            set
            {
                m_Selectable = value;
            }
        }

        private string m_Category = LayerCategory.Undefined;
        public string Category
        {
            get
            {
                return m_Category;
            }
            set
            {
                m_Category = value;
            }
        }

        private System.Drawing.Image m_Icon;
        public System.Drawing.Image Icon
        {
            get
            {
                return m_Icon;
            }
            set
            {
                m_Icon = value;
            }
        }

        private List<ILayer> m_childLayers = new List<ILayer>();
        public void AddChildLayer(ILayer layer)
        {
            m_childLayers.Add(layer);

            if (layer is DawnLayer)
                ((DawnLayer)layer).ParentLayer = this;
        }

        private ILayer m_ParentLayer;
        public ILayer ParentLayer
        {
            get
            {
                return m_ParentLayer;
            }
            set
            {
                m_ParentLayer = value;
            }
        }

        #endregion
    }

    public class LayerCategory
    {
        /// <summary>
        /// The extension site for the main menu.
        /// </summary>
        public const string Undefined = "Undefined";

        public const string POI = "POI";

        public const string CustomAddresses = "CustomAddresses";

        public const string TrafficInfo = "TrafficInfo";

        public const string AdminRegion = "AdminRegion";
    }
}
