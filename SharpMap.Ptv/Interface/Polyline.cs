using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Ptv.Controls.Map.Interface
{
    public class Polyline : Shape
    {
        public Polyline(Coordinate[] points)
        {
            this.points = points;
        }

        public Polyline(Coordinate[] points, Color color)
        {
            this.points = points;
            this.color = color;
        }

        public Polyline(Coordinate[] points, Color color, int width)
        {
            this.points = points;
            this.color = color;
            this.width = width;
        }

        Coordinate[] points;
        public Coordinate[] Points
        {
            get { return points; }
            set { points = value; }
        }
        
        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
    }
}
