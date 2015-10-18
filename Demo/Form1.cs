using Ptv.Controls.Map;
using Ptv.Controls.Map.AddressMonitor;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        private SharpMap.Map map;
        private Ptv.Controls.Map.MapMarket.MMProviderStatic districtsData;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set up sharpmap
            // Note: The process must run as 32-bit! The data source for map&market and AM use Access with OleDB.

            // create sharp map
            map = new SharpMap.Map(pictureBox1.ClientSize);

            // add background layer
            map.Layers.Add(new XMapServerLayer("Background", Properties.Settings.Default.XMapServerUrl, XMapServerMode.BackGround)
            {
                User = "xtok", Password = Properties.Settings.Default.XMapToken
            });

            // create vector layer
            SharpMap.Layers.VectorLayer districts = new SharpMap.Layers.VectorLayer("Dístricts");
            
            // initialize data source
            districtsData = new Ptv.Controls.Map.MapMarket.MMProviderStatic(
                 @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +  System.AppDomain.CurrentDomain.BaseDirectory + @"\Data\Districts.mdb",
                 "KRE", "GID", "XMIN", "YMIN", "XMAX", "YMAX", "WKB_GEOMETRY");
            districts.DataSource = districtsData;

            // set style
            districts.Theme = new SharpMap.Rendering.Thematics.CustomTheme(GetCountryStyle);

            // insert into sharp map
            map.Layers.Add(districts);

            // add label layer
            map.Layers.Add(new XMapServerLayer("Labels", Properties.Settings.Default.XMapServerUrl, XMapServerMode.Overlay)
            {
                User = "xtok",
                Password = Properties.Settings.Default.XMapToken
            });

            // insert address monitor layers
            var rootPath = System.AppDomain.CurrentDomain.BaseDirectory + "Data";
            var bitmapPath = rootPath + "\\Bitmaps";
            string[] poiFiles = System.IO.Directory.GetFiles(rootPath, "*.poi");
            foreach (string poiFile in poiFiles)
            {
                var layer = AMLayerFactory.CreateLayer(poiFile, bitmapPath);
                layer.MaxVisible = 9999999999; // override the automatic display threshold
                map.Layers.Add(layer);
            }

            // set map extends
            ZoomToBox();
        }

        protected void UpdateMap()
        {
            this.pictureBox1.Image = map.GetMap();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (map == null)
                return;

            map.Size = pictureBox1.ClientSize;

            UpdateMap();
        }

        private void ZoomToBox()
        {
            SharpMap.Geometries.BoundingBox bbox = map.GetExtents();

            if (bbox != null)
            {
                map.ZoomToBox(bbox);
                map.Zoom = map.Zoom * 1.1;
            }

            UpdateMap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            districtsData.DefinitionQuery = textBox1.Text;

            ZoomToBox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = this.comboBox1.SelectedItem.ToString();
        }

        // demonstratas the use of dynamic styles (themes) for vector layers
        private SharpMap.Styles.VectorStyle GetCountryStyle(SharpMap.Data.FeatureDataRow row)
        {
            // set a linear gradient brush for each country according to the bounding pixel rectangle
            SharpMap.Styles.VectorStyle style = new SharpMap.Styles.VectorStyle();

            Color c = SharpMap.Rendering.Thematics.ColorBlend.Rainbow7.GetColor((float)((System.Convert.ToDouble(row["KK_KAT"]) - 1.0) / 6.0));
            c = Color.FromArgb(180, c.R, c.G, c.B);

            style.Fill = new  SolidBrush(c);

            // set the border width depending on the map scale
            Pen pen = new Pen(Brushes.Black, (int)(50.0 / map.PixelSize));
            pen.LineJoin = LineJoin.Round;
            style.Outline = pen;
            style.EnableOutline = true;

            return style;
        }

        private void checkBoxBG_CheckedChanged(object sender, EventArgs e)
        {
            map.Layers["Background"].Enabled = checkBoxBG.Checked;

            UpdateMap();
        }

        private void checkBoxFG_CheckedChanged(object sender, EventArgs e)
        {
            map.Layers["Labels"].Enabled = checkBoxFG.Checked;

            UpdateMap();
        }

        private void ckechBoxMM_CheckedChanged(object sender, EventArgs e)
        {
            map.Layers["Dístricts"].Enabled = checkBoxMM.Checked;

            UpdateMap();
        }

        private void checkBoxPOI_CheckedChanged(object sender, EventArgs e)
        {
            map.Layers["p_fd7"].Enabled = checkBoxPOI.Checked;

            UpdateMap();
        }
    }
}