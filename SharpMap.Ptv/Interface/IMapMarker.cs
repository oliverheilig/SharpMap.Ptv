using System;
using System.Collections.Generic;
using System.Text;

namespace Ptv.Controls.Map.Interface
{
    public interface IMapMarker
    {
        void SetMarker(double latitude, double longitude);

        void ResetMarker();
    }
}
