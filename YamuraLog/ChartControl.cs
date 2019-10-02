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

namespace YamuraView
{
    public delegate void ChartMouseMove(object sender, ChartControlMouseMoveEventArgs e);
    public delegate void ChartXAxisChange(object sender, ChartControlXAxisChangeEventArgs e);
    public delegate void AxisOffsetUpdate(object sender, AxisOffsetUpdateEventArgs e);
    public delegate void ClearGraphicsPath(object sender, EventArgs e);

    public partial class ChartControl :  WeifenLuo.WinFormsUI.Docking.DockContent
    {
        //DataLogger logger;
        //public DataLogger Logger
        //{
        //    get { return logger; }
        //    set
        //    {
        //        logger = value;
        //        chartPropertiesForm.Logger = logger;
        //        chartViewForm.Logger = logger;
        //    }
        //}
        public DataLogger Logger
        {
            get { return YamuraView.Form1.dataLogger; }
        }

        Dictionary<string, Axis> chartAxes;// = new Dictionary<string, Axis>();
        public Dictionary<string, Axis> ChartAxes
        {
            get { return chartAxes; }
            set
            {
                chartAxes = value;
                chartViewForm.ChartAxes = chartAxes;
                chartPropertiesForm.ChartAxes = chartAxes;
            }
        }

        string chartName = "Chart";
        public string ChartName
        {
            get { return chartName; }
            set
            {
                chartName = value;
                chartViewForm.ChartName = value;
                chartPropertiesForm.Text = value + " setup";
            }
        }

        Rectangle chartBounds = new Rectangle(0, 0, 0, 0);

        // panel forms
        public ChartView chartViewForm = new ChartView();
        public ChartProperties chartPropertiesForm = new ChartProperties();

        /// <summary>
        /// 
        /// </summary>
        public ChartControl()
        {
            InitializeComponent();
            chartViewForm.XChannelName = "Time";
            chartViewForm.CursorUpdateSource = true;

            dockPanel1.Dock = DockStyle.Fill;
            dockPanel1.BackColor = Color.Beige;
            dockPanel1.BringToFront();

            chartViewForm.ShowHint = DockState.Document;
            chartViewForm.Text = "Stripchart";
            chartViewForm.Show(dockPanel1);

            chartPropertiesForm.ShowHint = DockState.Document;
            chartPropertiesForm.Text = "Stripchart Channels Setup";
            chartPropertiesForm.Show(dockPanel1);

            chartPropertiesForm.AxisOffsetUpdateEvent += chartViewForm.OnAxisOffsetUpdate;
        }
        private void zoomAllMenuItem_Click(object sender, EventArgs e)
        {
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[0] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[0];
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[1] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[1];
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[2] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[2];

            chartViewForm.hScrollBar.Minimum = (int)chartAxes[chartViewForm.XChannelName].AxisValueRange[0];
            chartViewForm.hScrollBar.Maximum = (int)chartAxes[chartViewForm.XChannelName].AxisValueRange[1];
            chartViewForm.hScrollBar.Value = (int)chartAxes[chartViewForm.XChannelName].AxisDisplayRange[0];
            chartViewForm.hScrollBar.LargeChange = (int)chartAxes[chartViewForm.XChannelName].AxisDisplayRange[2];

            chartViewForm.chartPanel.Invalidate();
        }
    }
    public class ChartControlMouseMoveEventArgs : EventArgs
    {
        Dictionary<string, float> xAxisValues = new Dictionary<string, float>();
        public Dictionary<string, float> XAxisValues
        {
            get { return xAxisValues; }
            set { xAxisValues = value; }
        }
        Dictionary<string, float> yAxisValues = new Dictionary<string, float>();
        public Dictionary<string, float> YAxisValues
        {
            get { return yAxisValues; }
            set { yAxisValues = value; }
        }

    }
    public class ChartControlXAxisChangeEventArgs : EventArgs
    {
        string xAxisName;
        public string XAxisName
        {
            get { return xAxisName; }
            set { xAxisName = value; }
        }
    }
}
