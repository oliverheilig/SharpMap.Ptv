using System;
using System.Collections.Generic;
using System.Text;
using SharpMap;
using System.Drawing.Drawing2D;
using System.Drawing;
using GisSharpBlog.NetTopologySuite.Index.Quadtree;
using GisSharpBlog.NetTopologySuite.Geometries;
using SharpMap.Layers;
using Ptv.Controls.Map.Interface;

namespace Ptv.Controls.Map
{
    public class InteractiveMap : SharpMap.Map
    {
        QuickInfoCollector m_QuickInfoCollector;
        private bool m_FirstToRender = false;
        private Quadtree m_QuadTree = new Quadtree();

        public bool FirstToRender
        {
            get
            {
                return m_FirstToRender;
            }
        }

        public InteractiveMap(QuickInfoCollector quadTree, bool firstToRender) :
            this(quadTree)
        {
            m_FirstToRender = firstToRender;
        }

        public InteractiveMap(QuickInfoCollector quadTree)
        {
            m_QuickInfoCollector = quadTree;
            quadTree.RegisterMap(this);
        }

        public void Insert(Envelope envolpe, QuickInfoObject obj)
        {
            lock (m_QuadTree)
            {
                m_QuadTree.Insert(envolpe, obj);
            }
        }

        public void ClearPopUps()
        {
            lock (m_QuadTree)
            {
                m_QuadTree = new Quadtree();
            }
        }

        public void AddObject(Region path, string layer, ToolTipInfo toolTipInfo, System.Drawing.Graphics g)
        {
            lock (m_QuadTree)
            {
                RectangleF rf = path.GetBounds(g);
                Envelope env = new Envelope(rf.Left, rf.Right, rf.Bottom, rf.Top);

                m_QuadTree.Insert(env, new QuickInfoObject(path, layer, toolTipInfo, env));
            }
        }

        public System.Collections.IList Query(System.Drawing.Point p)
        {
            Envelope env = new Envelope(p.X, p.X, p.Y, p.Y);
            System.Collections.IList tmpList;
            lock (m_QuadTree)
            {
                tmpList = m_QuadTree.Query(env);
            }

            List<QuickInfoObject> result = new List<QuickInfoObject>();

            foreach (QuickInfoObject qo in tmpList)
            {
                if (qo.Envelope.Contains(p.X, p.Y))
                    result.Add(qo);
            }

            return result;
        }
    }

    public class QuickInfoCollector
    {
        private List<InteractiveMap> m_SharpMaps = new List<InteractiveMap>();

        public void RegisterMap(InteractiveMap map)
        {
            m_SharpMaps.Add(map);
        }

        public ILayer FindLayer(string layerName)
        {
            foreach (InteractiveMap map in m_SharpMaps)
            {
                if (map.Layers[layerName] != null)
                    return map.Layers[layerName];
            }

            return null;
        }

        public List<LayerToolTips> GetQuickInfos(System.Drawing.Point p)
        {
            Dictionary<string, LayerToolTips> result = new Dictionary<string, LayerToolTips>();

            foreach (InteractiveMap map in m_SharpMaps)
            {
                System.Collections.IList quadObj = map.Query(p);

                foreach (object obj in quadObj)
                {
                    QuickInfoObject qo = obj as QuickInfoObject;

                    if (qo == null)
                        continue;

                    if (qo.Path == null || qo.Path.IsVisible(p))
                    {
                        if (!result.ContainsKey(qo.Layer))
                        {
                            LayerToolTips newTips = new LayerToolTips();

                            ILayer layer = FindLayer(qo.Layer);

                            if (layer is IDawnLayer)
                            {
                                IDawnLayer dawnLayer = layer as IDawnLayer;
                                newTips.Caption = dawnLayer.VisibleName;
                                newTips.Bitmap = dawnLayer.Icon;
                            }
                            else if (layer is Layer)
                            {
                                Layer dawnLayer = layer as Layer;
                                newTips.Caption = layer.LayerName;
                            }
                            else
                                newTips.Caption = qo.Layer;

                            result[qo.Layer] = newTips;
                        }

                        LayerToolTips tips = result[qo.Layer];

                        tips.AddToolTipInfo(qo.ToolTipInfo);
                    }
                }
            }

            List<LayerToolTips> layerToolTips = new List<LayerToolTips>();
            foreach (LayerToolTips tips in result.Values)
            {
                layerToolTips.Add(tips);
            }

            return layerToolTips;
        }
    }

    public class QuickInfoObject
    {
        private Region m_Path;
        private string m_Layer;
        private ToolTipInfo m_ToolTipInfo;
        private Envelope m_Envelope;

        public Envelope Envelope
        {
            get
            {
                return m_Envelope;
            }
        }

        public Region Path
        {
            get { return m_Path; }
        }

        public string Layer
        {
            get { return m_Layer; }
        }

        public ToolTipInfo ToolTipInfo
        {
            get { return m_ToolTipInfo; }
        }

        public QuickInfoObject(Region path, string layer, ToolTipInfo toolTipInfo, Envelope envelope)
        {
            m_Path = path;
            m_Layer = layer;
            m_ToolTipInfo = toolTipInfo;
            m_Envelope = envelope;
        }
    }
}
