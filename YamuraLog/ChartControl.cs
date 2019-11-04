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
    public delegate void AxisMouseMove(object sender, YamuraView.UserControls.AxisControlMouseMoveEventArgs e);

    public partial class ChartControl :  WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public enum ChartControlType
        {
            Stripchart,     // generic X/Y plot
            TractionCirle,  // X/Y plot with 'target' drawn
            Histogram       // histogram chart for selected channel
        }
        ChartControlType chartType;
        public ChartControlType ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }
        public DataLogger Logger
        {
            get { return YamuraView.YamuraViewMain.dataLogger; }
        }

        Dictionary<string, Axis> chartAxes;// = new Dictionary<string, Axis>();
        public Dictionary<string, Axis> ChartAxes
        {
            get { return chartAxes; }
            set
            {
                chartAxes = value;
                if (chartAxes == null)
                {
                    return;
                }
                chartPropertiesForm.AxisChannelTree.Nodes.Clear();
                chartPropertiesForm.CmbXAxis.Items.Clear();
                chartPropertiesForm.CmbAlignAxis.Items.Clear();
                chartPropertiesForm.TxtAutoThreshold.Text = "0.0";
                foreach (KeyValuePair<String, Axis> curAxis in chartAxes)
                {
                    chartPropertiesForm.CmbXAxis.Items.Add(curAxis.Key);
                    chartPropertiesForm.CmbAlignAxis.Items.Add(curAxis.Key);

                    bool axisFound = false;
                    foreach (TreeNode axisItem in chartPropertiesForm.AxisChannelTree.Nodes)
                    {
                        axisFound = false;
                        if (axisItem.Name == curAxis.Key)
                        {
                            axisFound = true;
                            break;
                        }
                    }
                    if (!axisFound)
                    {
                        chartPropertiesForm.AxisChannelTree.Nodes.Add(curAxis.Key, curAxis.Key, 0);
                        chartPropertiesForm.AxisChannelTree.Nodes[curAxis.Key].Checked = curAxis.Value.ShowAxis;
                    }
                    foreach (KeyValuePair<String, ChannelInfo> associatedChannel in curAxis.Value.AssociatedChannels)
                    {
                        if (chartPropertiesForm.AxisChannelTree.Nodes[curAxis.Key].Nodes.ContainsKey(associatedChannel.Key))
                        {
                            continue;
                        }
                        chartPropertiesForm.AxisChannelTree.Nodes[curAxis.Key].Nodes.Add(associatedChannel.Key, associatedChannel.Key, 1);
                        chartPropertiesForm.AxisChannelTree.Nodes[curAxis.Key].Nodes[associatedChannel.Key].Checked = associatedChannel.Value.ShowChannel;

                    }
                }
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
        // panel forms
        public ChartView chartViewForm = new ChartView();
        public ChartProperties chartPropertiesForm = new ChartProperties();

        /// <summary>
        /// 
        /// </summary>
        public ChartControl(ChartControlType typeOfChart)
        {
            InitializeComponent();
            ChartType = typeOfChart;
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

            chartPropertiesForm.ChartOwner = this;
            chartViewForm.ChartOwner = this;
        }
        private void zoomAllMenuItem_Click(object sender, EventArgs e)
        {
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[0] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[0];
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[1] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[1];
            ChartAxes[chartViewForm.XChannelName].AxisDisplayRange[2] = ChartAxes[chartViewForm.XChannelName].AxisValueRange[2];

            chartViewForm.xAxis1.Minimum = (int)chartAxes[chartViewForm.XChannelName].AxisValueRange[0];
            chartViewForm.xAxis1.Maximum = (int)chartAxes[chartViewForm.XChannelName].AxisValueRange[1];
            chartViewForm.xAxis1.Value = (int)chartAxes[chartViewForm.XChannelName].AxisDisplayRange[0];
            chartViewForm.xAxis1.LargeChange = (int)chartAxes[chartViewForm.XChannelName].AxisDisplayRange[2];

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
