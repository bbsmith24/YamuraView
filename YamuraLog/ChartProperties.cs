using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YamuraView
{
    public partial class ChartProperties : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public event AxisOffsetUpdate AxisOffsetUpdateEvent;
        public event ClearGraphicsPath ClearGraphicsPathEvent;

        public DataLogger Logger
        {
            get
            {
                return YamuraView.YamuraViewMain.dataLogger;
            }
        }

        public event ChartXAxisChange ChartXAxisChangeEvent;

        ChartControl chartOwner;
        public ChartControl ChartOwner
        {
            get { return chartOwner; }
            set { chartOwner = value; }
        }

        public YamuraView.UserControls.TriStateTreeView AxisChannelTree
        {
            get { return axisChannelTree; }
            set { axisChannelTree = value; }
        }
        public ComboBox CmbXAxis
        {
            get { return cmbXAxis; }
            set { cmbXAxis = value; }
        }
        public ComboBox CmbAlignAxis
        {
            get { return cmbAlignAxis; }
            set { cmbAlignAxis = value; }
        }
        public TextBox TxtAutoThreshold
        {
            get { return txtAutoThreshold; }
            set { txtAutoThreshold = value; }
        }
        //Dictionary<string, Axis> chartAxes;// = new Dictionary<string, Axis>();
        //public Dictionary<string, Axis> ChartAxes
        //{
        //    get { return chartAxes; }
        //    set
        //    {
        //        chartAxes = value;
        //        if (chartAxes == null)
        //        {
        //            return;
        //        }
        //        axisChannelTree.Nodes.Clear();
        //        cmbXAxis.Items.Clear();
        //        cmbAlignAxis.Items.Clear();
        //        txtAutoThreshold.Text = "0.0";
        //        foreach (KeyValuePair<String, Axis> curAxis in chartAxes)
        //        {
        //            cmbXAxis.Items.Add(curAxis.Key);
        //            cmbAlignAxis.Items.Add(curAxis.Key);

        //            bool axisFound = false;
        //            foreach (TreeNode axisItem in axisChannelTree.Nodes)
        //            {
        //                axisFound = false;
        //                if (axisItem.Name == curAxis.Key)
        //                {
        //                    axisFound = true;
        //                    break;
        //                }
        //            }
        //            if (!axisFound)
        //            {
        //                axisChannelTree.Nodes.Add(curAxis.Key, curAxis.Key, 0);
        //                axisChannelTree.Nodes[curAxis.Key].Checked = curAxis.Value.ShowAxis;
        //            }
        //            foreach (KeyValuePair<String, ChannelInfo> associatedChannel in curAxis.Value.AssociatedChannels)
        //            {
        //                if (axisChannelTree.Nodes[curAxis.Key].Nodes.ContainsKey(associatedChannel.Key))
        //                {
        //                    continue;
        //                }
        //                axisChannelTree.Nodes[curAxis.Key].Nodes.Add(associatedChannel.Key, associatedChannel.Key, 1);
        //                axisChannelTree.Nodes[curAxis.Key].Nodes[associatedChannel.Key].Checked = associatedChannel.Value.ShowChannel;

        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        public string XAxisName
        {
            get { return cmbXAxis.Text; }
            set
            {
                cmbXAxis.Text = value;
                axisOffsetsGrid.Columns["axisOffset"].Visible = (cmbXAxis.Text == "Time");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ChartProperties()
        {
            InitializeComponent();

            ChannelColorSelector embeddedColorDialog = new ChannelColorSelector();

            embeddedColorDialog.ColorSelectEvent += EmbeddedColorDialog_ColorSelectEvent;

            ToolStripControlHost colorDialogHost = new ToolStripControlHost(embeddedColorDialog as Control);
            colorDialogHost.Text = "Channel Color";
            colorDialogHost.Name = "channelColor";
            channelsContext.Items.Add(colorDialogHost);
            channelsContext.Items["channelColor"].Visible = true;
            axisOffsetsGrid.Columns["axisOffset"].Visible = false;
            axisOffsetsGrid.Columns["axisChannel"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            axisOffsetsGrid.CellEndEdit += AxisOffsetsGrid_CellEndEdit;
        }

        private void AxisOffsetsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // only handle change to offset value
            if(e.ColumnIndex != axisOffsetsGrid.Columns["axisOffset"].Index)
            {
                return;
            }
            string[] channelInfo = axisOffsetsGrid.Rows[e.RowIndex].Cells["axisChannel"].Value.ToString().Split(new char[] { '-' });
            int runIdx = Convert.ToInt32(channelInfo[0]);
            string channelName = channelInfo[1];
            float offsetValue = Convert.ToSingle(axisOffsetsGrid.Rows[e.RowIndex].Cells["axisOffset"].Value);

            AxisOffsetUpdateEventArgs updateArgs = new AxisOffsetUpdateEventArgs(channelName, runIdx, 0, offsetValue);
            AxisOffsetUpdateEvent(this, updateArgs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmbeddedColorDialog_ColorSelectEvent(object sender, ColorSelectEventArgs e)
        {
            String axisName = axisChannelTree.SelectedNode.Parent.Text;
            String channelName = axisChannelTree.SelectedNode.Text;
            ChartOwner.ChartAxes[axisName].AssociatedChannels[channelName].ChannelColor = e.SelectedColor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axisChannelTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // axis
            if (e.Node.Parent == null)
            {
                ChartOwner.ChartAxes[e.Node.Name].ShowAxis = e.Node.Checked;
            }
            // channel
            else
            {
                ChartOwner.ChartAxes[e.Node.Parent.Name].AssociatedChannels[e.Node.Text].ShowChannel = e.Node.Checked;
                ChartOwner.ChartAxes[e.Node.Parent.Name].ShowAxis = e.Node.Checked == true ? true : ChartOwner.ChartAxes[e.Node.Parent.Name].ShowAxis;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channelsContext_Opening(object sender, CancelEventArgs e)
        {
            if (axisChannelTree.SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }
            if (axisChannelTree.SelectedNode.Parent == null)
            {
                channelsContext.Items["channelColor"].Enabled = false;
                channelsContext.Items["channelColor"].Visible = false;

                channelsContext.Items["lblAxisMin"].Visible = true;
                channelsContext.Items["lblAxisMax"].Visible = true;
                channelsContext.Items["txtAxisMinValue"].Visible = true;
                channelsContext.Items["txtAxisMaxValue"].Visible = true;

                String axisName = axisChannelTree.SelectedNode.Text;

                channelsContext.Items["txtAxisMinValue"].Text = ChartOwner.ChartAxes[axisName].AxisValueRange[0].ToString();
                channelsContext.Items["txtAxisMaxValue"].Text = ChartOwner.ChartAxes[axisName].AxisValueRange[1].ToString();

            }
            else
            {
                channelsContext.Items["lblAxisMin"].Visible = false;
                channelsContext.Items["lblAxisMax"].Visible = false;
                channelsContext.Items["txtAxisMinValue"].Visible = false;
                channelsContext.Items["txtAxisMaxValue"].Visible = false;

                channelsContext.Items["channelColor"].Visible = true;
                channelsContext.Items["channelColor"].Enabled = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbXAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartControlXAxisChangeEventArgs changeEventArgs = new ChartControlXAxisChangeEventArgs();
            changeEventArgs.XAxisName = cmbXAxis.Text; 
            ChartXAxisChangeEvent(this, changeEventArgs);

            if (cmbXAxis.Text == "Time")
            {
                axisOffsetsGrid.Columns["axisOffset"].Visible = true;
                axisOffsetsGrid.Columns["axisOffset"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                cmbAlignAxis.Enabled = true;
                txtAutoThreshold.Enabled = true;
                btnDoAutoAlign.Enabled = true;
            }
            else
            {
                axisOffsetsGrid.Columns["axisOffset"].Visible = false;
                axisOffsetsGrid.Columns["axisChannel"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                cmbAlignAxis.Enabled = false;
                txtAutoThreshold.Enabled = false;
                btnDoAutoAlign.Enabled = false;
            }

            axisOffsetsGrid.Rows.Clear();
            foreach(KeyValuePair<string, ChannelInfo> kvp in ChartOwner.ChartAxes[cmbXAxis.Text].AssociatedChannels)
            {
                axisOffsetsGrid.Rows.Add();
                axisOffsetsGrid.Rows[axisOffsetsGrid.Rows.Count - 1].Cells["axisChannel"].Value = kvp.Key;
                axisOffsetsGrid.Rows[axisOffsetsGrid.Rows.Count - 1].Cells["axisStart"].Value = kvp.Value.AxisRange[0].ToString();
                axisOffsetsGrid.Rows[axisOffsetsGrid.Rows.Count - 1].Cells["axisEnd"].Value = kvp.Value.AxisRange[1].ToString();
                axisOffsetsGrid.Rows[axisOffsetsGrid.Rows.Count - 1].Cells["axisOffset"].Value = kvp.Value.AxisOffset[0].ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channelsContext_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            String nodeName = axisChannelTree.SelectedNode.Text;

            if (axisChannelTree.SelectedNode.Parent == null)
            {
                ChartOwner.ChartAxes[nodeName].AxisValueRange[0] = Convert.ToSingle(channelsContext.Items["txtAxisMinValue"].Text);
                ChartOwner.ChartAxes[nodeName].AxisValueRange[1] = Convert.ToSingle(channelsContext.Items["txtAxisMaxValue"].Text);
                ChartOwner.ChartAxes[nodeName].AxisValueRange[2] = ChartOwner.ChartAxes[nodeName].AxisValueRange[1] - ChartOwner.ChartAxes[nodeName].AxisValueRange[0];
                ChartOwner.ChartAxes[nodeName].AxisDisplayRange = ChartOwner.ChartAxes[nodeName].AxisValueRange;
                foreach (KeyValuePair<string, ChannelInfo> channelInfo in ChartOwner.ChartAxes[nodeName].AssociatedChannels)
                {
                    channelInfo.Value.AxisRange[0] = Convert.ToSingle(channelsContext.Items["txtAxisMinValue"].Text);
                    channelInfo.Value.AxisRange[1] = Convert.ToSingle(channelsContext.Items["txtAxisMaxValue"].Text);
                    channelInfo.Value.AxisRange[2] = ChartOwner.ChartAxes[nodeName].AxisValueRange[1] - ChartOwner.ChartAxes[nodeName].AxisValueRange[0];
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            channelsContext.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // selected channel
            if (axisChannelTree.SelectedNode.Parent != null)
            {
                string channelName = axisChannelTree.SelectedNode.Text;
                int runIdx = Convert.ToInt32(channelName.Substring(0, channelName.IndexOf('-')));
                channelName = channelName.Substring(channelName.IndexOf('-') + 1);
                foreach (KeyValuePair<float, DataPoint>kvp in Logger.runData[runIdx].channels[channelName].DataPoints)
                {
                    Logger.runData[runIdx].channels[channelName].DataPoints[kvp.Key].PointValue = Logger.runData[runIdx].channels[channelName].DataRange[1] - kvp.Value.PointValue + Logger.runData[runIdx].channels[channelName].DataRange[0];
                }
                ClearGraphicsPathEvent(this, new EventArgs());
            }
            // all channels
            else
            {
                string channelName = axisChannelTree.SelectedNode.Text;
                for (int runIdx = 0; runIdx < Logger.runData.Count; runIdx++)
                {
                    if(!Logger.runData[runIdx].channels.ContainsKey(channelName))
                    {
                        continue;
                    }
                    foreach (KeyValuePair<float, DataPoint> kvp in Logger.runData[runIdx].channels[channelName].DataPoints)
                    {
                        Logger.runData[runIdx].channels[channelName].DataPoints[kvp.Key].PointValue = Logger.runData[runIdx].channels[channelName].DataRange[1] - kvp.Value.PointValue + Logger.runData[runIdx].channels[channelName].DataRange[0];
                    }
                }
                ClearGraphicsPathEvent(this, new EventArgs());
            }

        }

        private void btnDoAutoAlign_Click(object sender, EventArgs e)
        {
            AutoAlign(Convert.ToSingle(txtAutoThreshold.Text), cmbAlignAxis.Text);
        }
        #region Auto align
        /// <summary>
        /// estimate launch point offset from speed data
        /// find first speed > 30, walk back to first speed = 0
        /// </summary>
        private void AutoAlign(float launchThreshold, string alignAxis)
        {
            List<float> launchPoints = new List<float>();
            int runCount = 0;
            foreach (RunData curRun in Logger.runData)
            {
                foreach (KeyValuePair<float, DataPoint> curPoint in curRun.channels[alignAxis].DataPoints)
                {
                    if (Math.Abs(curPoint.Value.PointValue) > launchThreshold)
                    {
                        launchPoints.Add(curPoint.Key);
                        break;
                    }
                }
                runCount++;
            }
            float minPoint = launchPoints.Min();
            for (int launchIdx = 0; launchIdx < launchPoints.Count; launchIdx++)
            {
                launchPoints[launchIdx] -= minPoint;
                if(axisOffsetsGrid.Rows.Count <= launchIdx)
                {
                    axisOffsetsGrid.Rows.Add();
                    axisOffsetsGrid.Rows[launchIdx].Cells["axisChannel"].Value = launchIdx.ToString() + "-" + Logger.runData[launchIdx].channels[XAxisName].ChannelName;
                    axisOffsetsGrid.Rows[launchIdx].Cells["axisStart"].Value = Logger.runData[launchIdx].channels[XAxisName].DataRange[0];
                    axisOffsetsGrid.Rows[launchIdx].Cells["axisEnd"].Value = Logger.runData[launchIdx].channels[XAxisName].DataRange[1];
                }
                axisOffsetsGrid.Rows[launchIdx].Cells["axisOffset"].Value = (-1 * launchPoints[launchIdx]).ToString();
                AxisOffsetUpdateEventArgs updateArgs = new AxisOffsetUpdateEventArgs("Time", launchIdx, 0, -1* launchPoints[launchIdx]);
                AxisOffsetUpdateEvent(this, updateArgs);
            }
        }
        #endregion

    }
    public class AxisOffsetUpdateEventArgs : EventArgs
    {
        string channelName;
        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }
        int axisIdx;
        public int AxisIdx
        {
            get { return axisIdx; }
            set { axisIdx = value; }
        }
        int runIdx;
        public int RunIdx
        {
            get { return runIdx; }
            set { runIdx = value; }
        }
        float offsetVal;
        public float OffsetVal
        {
            get { return offsetVal; }
            set { offsetVal = value; }
        }

        public AxisOffsetUpdateEventArgs(string name, int run, int axis, float offset)
        {
            ChannelName = name;
            RunIdx = run;
            AxisIdx = axis;
            OffsetVal = offset;
        }
    }
}
