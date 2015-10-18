using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Layers;
using System.Drawing;
using System.Data.OleDb;
using Ptv.Controls.Map;

namespace Ptv.Controls.Map.AddressMonitor
{
    public class AMLayerFactory
    {
        public static ILayer CreateLayer(string fileName, string bitmapBase)
        {
            // create layer
            DawnLayer layer = new DawnLayer(System.IO.Path.GetFileNameWithoutExtension(fileName));

            layer.Category = LayerCategory.POI;

            string iconFile = fileName.Substring(0, fileName.LastIndexOf(".poi")) + ".bmp";
            if (System.IO.File.Exists(iconFile))
            {
                Bitmap bmp = new Bitmap(iconFile);
                bmp.MakeTransparent(System.Drawing.Color.Magenta);
                layer.Icon = bmp;
            }
            //else
            //layer.Icon = new Bitmap(new System.Drawing.Bitmap(typeof(AMLayerFactory), "Resources.DefaultPOIIcon.bmp"));

            // initialize data source
            AMProvider pointProv = new AMProvider(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName,
                "_IndexData", "AUTOINC", "X", "Y");
            layer.DataSource = pointProv;

            // initialize style
            AMStyle amStyle = new AMStyle(fileName, bitmapBase);
            if(amStyle.MaxVisible > 0)
                layer.MaxVisible = amStyle.MaxVisible;
            layer.Theme = amStyle;
            
            return layer;
        }
    }
}
