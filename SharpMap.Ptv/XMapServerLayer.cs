using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Layers;
using System.Net;
using System.IO;
using System.Net.Cache;
using Ptv.Controls.Map.XMap;

namespace Ptv.Controls.Map
{
    public enum XMapServerMode
    {
        All,
        BackGround,
        Overlay,
    }

    /// <summary>
    /// XMapServerLayer renders a Bitmap using PTV xMapServer
    /// </summary>
    public class XMapServerLayer : SharpMap.Layers.Layer
    {
        private string m_Url = string.Empty;
        private XMapServerMode m_xMapServerMode = XMapServerMode.All;

        public XMapServerLayer(string name, string url, XMapServerMode mode)
        {
            m_xMapServerMode = mode;
            LayerName = name;
            m_Url = url;
            SRID = 1076131; // PTV_MERCATOR
        }

        public XMapServerLayer(string name, string url) :
            this(name, url, XMapServerMode.All)
        {
        }

        public string User { get; set; }
        public string Password { get; set; }

        public override SharpMap.Geometries.BoundingBox Envelope
        {
            get
            {
                return null;
            }
        }

        public override object Clone()
        {
            return new XMapServerLayer(LayerName, m_Url, m_xMapServerMode);
        }

        public override void Render(System.Drawing.Graphics g, SharpMap.Map map)
        {
            base.Render(g, map);

            Ptv.Controls.Map.XMap.XMapWSService svcMap = new Ptv.Controls.Map.XMap.XMapWSService();
            svcMap.Url = m_Url;

            if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
                svcMap.Credentials = new NetworkCredential(User, Password);

            BoundingBox bb = new BoundingBox();
            bb.leftTop = new Point();
            bb.leftTop.point = new PlainPoint();
            bb.leftTop.point.x = map.Envelope.Min.X;
            bb.leftTop.point.y = map.Envelope.Max.Y;
            bb.rightBottom = new Point();
            bb.rightBottom.point = new PlainPoint();
            bb.rightBottom.point.x = map.Envelope.Max.X;
            bb.rightBottom.point.y = map.Envelope.Min.Y;
            
            MapParams mapParams = new MapParams();
            mapParams.showScale = true;
            mapParams.useMiles = false;
            
            ImageInfo imageInfo = new ImageInfo();
            imageInfo.format = ImageFileFormat.PNG;
            imageInfo.height = map.Size.Height;
            imageInfo.width = map.Size.Width;

            switch (m_xMapServerMode)
            {
                case XMapServerMode.All:
                    {
                        Ptv.Controls.Map.XMap.Map xmap = svcMap.renderMapBoundingBox(bb, mapParams, imageInfo, null, true, null);

                        MemoryStream imageStream = new MemoryStream(xmap.image.rawImage);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(imageStream);
                        System.Drawing.Bitmap bmp = image as System.Drawing.Bitmap;
                        bmp.MakeTransparent(System.Drawing.Color.FromArgb(255, 254, 185));
                        g.DrawImageUnscaled(image, 0, 0);

                        return;
                    }
                case XMapServerMode.BackGround:
                    {
                        // render only backgound and street 
                        StaticLayer slTown = new StaticLayer();
                        slTown.visible = false;
                        slTown.name = "town";
                        slTown.detailLevel = 0;
                        slTown.category = -1;
                        StaticLayer slStreet = new StaticLayer();
                        slStreet.visible = true;
                        slStreet.name = "street";
                        slStreet.detailLevel = 0;
                        slStreet.category = -1;
                        StaticLayer slBackground = new StaticLayer();
                        slBackground.visible = true;
                        slBackground.name = "background";
                        slBackground.detailLevel = 0;
                        slBackground.category = -1;

                        Ptv.Controls.Map.XMap.Layer[] arrLayer = new Ptv.Controls.Map.XMap.Layer[] { slBackground, slStreet, slTown };

                        Ptv.Controls.Map.XMap.Map xmap = svcMap.renderMapBoundingBox(bb, mapParams, imageInfo, arrLayer, true, null);

                        MemoryStream imageStream = new MemoryStream(xmap.image.rawImage);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(imageStream);
                        System.Drawing.Bitmap bmp = image as System.Drawing.Bitmap;
                        g.DrawImageUnscaled(image, 0, 0); 
                        
                        return;
                    }
                case XMapServerMode.Overlay:
                    {
                        // render only town
                        StaticLayer slTown = new StaticLayer();
                        slTown.visible = true;
                        slTown.name = "town";
                        slTown.detailLevel = 0;
                        slTown.category = -1;
                        StaticLayer slStreet = new StaticLayer();
                        slStreet.visible = false;
                        slStreet.name = "street";
                        slStreet.detailLevel = 0;
                        slStreet.category = -1;
                        StaticLayer slBackground = new StaticLayer();
                        slBackground.visible = false;
                        slBackground.name = "background";
                        slBackground.detailLevel = 0;
                        slBackground.category = -1;

                        Ptv.Controls.Map.XMap.Layer[] arrLayer = new Ptv.Controls.Map.XMap.Layer[] { slBackground, slStreet, slTown };

                        Ptv.Controls.Map.XMap.Map xmap = svcMap.renderMapBoundingBox(bb, mapParams, imageInfo, arrLayer, true, null);

                        MemoryStream imageStream = new MemoryStream(xmap.image.rawImage);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(imageStream);
                        System.Drawing.Bitmap bmp = image as System.Drawing.Bitmap;
                        // make map background color transparent
                        bmp.MakeTransparent(System.Drawing.Color.FromArgb(255, 254, 185));
                        g.DrawImageUnscaled(image, 0, 0); 
                        
                        return;
                    }
            }
        }
    }
}
