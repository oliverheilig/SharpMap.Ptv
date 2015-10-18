using System;
using System.Collections.Generic;
using System.Text;
using EMapRequest;
using System.IO;

namespace Ptv.Controls.Map
{
    /// <summary>
    /// EMapServerLayer renders a Bitmap using PTV eMapServer
    /// </summary>
    public class EMapServerLayer : SharpMap.Layers.Layer
    {
        private string m_Url = string.Empty;
        private string m_MapName = string.Empty;
        private string m_ProfileName = string.Empty;
        private string m_User = string.Empty;
        private string m_Password = string.Empty;

        public EMapServerLayer(string name, string url, string mapName)
        {
            LayerName = name;
            m_Url = url;
            m_MapName = mapName;
            SRID = 1076131; // PTV_MERCATOR
        }

        public string User
        {
            get { return m_User; }
            set { m_User = value; }
        }

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        public string ProfileName
        {
            get { return m_ProfileName; }
            set { m_ProfileName = value; }
        }
        
        public override void Render(System.Drawing.Graphics g, SharpMap.Map map)
        {
            base.Render(g, map);

            Request request = new Request();
            request.Map = new EMapRequest.Map();
            request.Map.Name = m_MapName;
            request.User = m_User;
            request.Pwd = m_Password;
            request.Version = "2";
            request.Type = "eMap.Map";
            request.Map.ImageFile = new ImageFile();
            request.Map.ImageFile.IncludeInResponse = false;
            request.Map.ImageFile.IncludeInResponseSpecified = true;
            request.Map.ImageFile.SizeXSpecified = true;
            request.Map.ImageFile.SizeX = (ulong)map.Size.Width;
            request.Map.ImageFile.SizeYSpecified = true;
            request.Map.ImageFile.SizeY = (ulong)map.Size.Height;
            request.Map.ImageFile.Format = MapFileFormatEnum.GIF;

            request.Map.VisibleSection = new VisibleSection();
            request.Map.VisibleSection.bottomSpecified = true;
            request.Map.VisibleSection.bottom = (long)map.Envelope.Min.Y;
            request.Map.VisibleSection.leftSpecified = true;
            request.Map.VisibleSection.left = (long)map.Envelope.Min.X;
            request.Map.VisibleSection.topSpecified = true;
            request.Map.VisibleSection.top = (long)map.Envelope.Max.Y;
            request.Map.VisibleSection.rightSpecified = true;
            request.Map.VisibleSection.right = (long)map.Envelope.Max.X;

            MapRequest mr = new MapRequest();
            mr.Url = m_Url;
            Response res = mr.ExecuteRequest(request);

            MemoryStream imageStream = new MemoryStream(res.Image);
            System.Drawing.Image image = System.Drawing.Image.FromStream(imageStream);
            g.DrawImageUnscaled(image, 0, 0); 
        }

        public override SharpMap.Geometries.BoundingBox Envelope
        {
            get { return null; }
        }

        public override object Clone()
        {
            // ToDo: implement
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
