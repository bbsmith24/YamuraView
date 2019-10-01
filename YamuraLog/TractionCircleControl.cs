using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDI;
using Win32Interop.Methods;
using WeifenLuo.WinFormsUI.Docking;

namespace YamuraLog
{
    public partial class TractionCircleControl : ChartControl 
    {
        // panel forms
        new public TractionCircleView chartViewForm = new TractionCircleView();

        /// <summary>
        /// 
        /// </summary>
        public TractionCircleControl()
        {
            InitializeComponent();
            chartViewForm.XChannelName = "Time";
            chartViewForm.CursorUpdateSource = true;

            dockPanel1.Dock = DockStyle.Fill;
            dockPanel1.BackColor = Color.Beige;
            dockPanel1.BringToFront();

            chartViewForm.ShowHint = DockState.Document;
            chartViewForm.Text = "Traction Circle";
            chartViewForm.Show(dockPanel1);

            chartPropertiesForm.ShowHint = DockState.Document;
            chartPropertiesForm.Text = "Traction Circle Channels Setup";
            chartPropertiesForm.Show(dockPanel1);

            chartPropertiesForm.AxisOffsetUpdateEvent += chartViewForm.OnAxisOffsetUpdate;
            
        }
        private void zoomAllMenuItem_Click(object sender, EventArgs e)
        {
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[0] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[0];
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[1] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[1];
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[2] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[2];

            chartViewForm.hScrollBar.Minimum = (int)ChartAxes[chartViewForm.XChannelName].AxisValueRange[0];
            chartViewForm.hScrollBar.Maximum = (int)ChartAxes[chartViewForm.XChannelName].AxisValueRange[1];
            chartViewForm.hScrollBar.Value = (int)ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[0];
            chartViewForm.hScrollBar.LargeChange = (int)ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[2];

            chartViewForm.chartPanel.Invalidate();
        }
    }
}
