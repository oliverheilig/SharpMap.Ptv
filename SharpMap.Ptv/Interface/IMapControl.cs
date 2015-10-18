using System;
using System.Collections.Generic;
using System.Text;

namespace Ptv.Controls.Map.Interface
{
    /// <summary>
    /// a generic inteface for map controls
    /// </summary>
    public interface IMapControl
    {
        /// <summary>
        /// sets the map focus so all objects are visible
        /// </summary>
        void SetFocus(Coordinate[] coords);

        /// <summary>
        /// enables the embedded map navigator
        /// </summary>
        bool UseEmbeddedNavigator
        {
            get;
            set;
        }

        Coordinate[] Envelope
        {
            get;
            set;
        }

        bool ToolbarActionSupported(ToolbarAction action);

        bool ToolbarActionEnabled(ToolbarAction action);
        
        void ToolbarActionInvoke(ToolbarAction action);

        event UpdateToolbarHandler UpdateToolbar;
    }

    public delegate void UpdateToolbarHandler(object sender, EventArgs e);

    public enum ToolbarAction
    {
        ZoomIn,
        ZoomOut,
        HistoryForward,
        HistoryBack,
        ZoomMode,
        MoveMode,
        ToggleOverView,
        ViewMap,
        ViewAerial,
        ViewHybrid,
    }
}
