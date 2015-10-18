using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ptv.Controls.Map.Interface
{
    public delegate bool MapMouseEventHandler(object sender, MouseEventArgs e);
    
    public interface IMapInteractor
    {
        event MapMouseEventHandler MapMouseClick;
        event MapMouseEventHandler MapMouseDown;
        event MapMouseEventHandler MapMouseUp;
        event MapMouseEventHandler MapMouseMove;
        event MapMouseEventHandler MapMouseLeave;
    }
}
