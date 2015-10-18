using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.CoordinateSystems.Transformations;
using SharpMap.CoordinateSystems;
using SharpMap.Layers;

namespace Ptv.Controls.Map.Interface
{
    /// <summary>
    /// Interface for accessing SharpMap
    /// </summary>
    public interface ISharpMapContainer
    {
        /// <summary>
        /// Gets the SharpMap instance for rendering overlays
        /// </summary>
        SharpMap.Map SharpMap
        {
            get;
        }

        /// <summary>
        /// refreshes the SharpMap overlay
        /// </summary>
        void SharpMapRefresh();

        void InsertLayer(ILayer layer, int zOrder);

        void InsertLayer(ILayer layer, int zOrder, IDawnLayer owner);

        List<LayerToolTips> GetQuickInfos(System.Drawing.Point p);

        SharpMap.Map[] SharpMaps
        {
            get;
        }

        List<DawnLayer> RootLayers
        {
            get;
        }
    }

    public class LayerToolTips
    {
        public System.Drawing.Image Bitmap;
        public string Caption;
        private System.Collections.Generic.Dictionary<object, object> _Ids = new Dictionary<object, object>();

        public void AddToolTipInfo(ToolTipInfo info)
        {
            if (info.Id != null && _Ids.ContainsKey(info.Id))
                return;

            if (info.Id != null)
                _Ids[info.Id] = info.Id;

            ToolTips.Add(info);
        }

        public List<ToolTipInfo> ToolTips = new List<ToolTipInfo>();
    }

    public class ToolTipInfo
    {
        public string Description;
        public System.Drawing.Image Bitmap;
        public object Id;
    }
}
