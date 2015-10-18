using System;
using System.Collections.Generic;
using System.Text;

namespace Ptv.Controls.Map.Interface
{
    public struct Coordinate
    {
        public Coordinate(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
    }
}
