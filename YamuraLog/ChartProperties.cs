using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YamuraLog
{
    public partial class ChartProperties : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public event AxisOffsetUpdate AxisOffsetUpdateEvent;

        DataLogger logger;
        public DataLogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        public event ChartXAxisChange ChartXAxisChangeEvent;

        Dictionary<string, Axis> chartAxes = new Dictionary<string, Axis>();
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
                axisChannelTree.Nodes.Clear();
                cmbXAxis.Items.Clear();
                foreach (KeyValuePair<String, Axis> curAxis in chartAxes)
                {
                    cmbXAxis.Items.Add(curAxis.Key);

                    bool axisFound = false;
                    foreach (TreeNode axisItem in axisChannelTree.Nodes)
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
                        axisChannelTree.Nodes.Add(curAxis.Key, curAxis.Key, 0);
                        axisChannelTree.Nodes[curAxis.Key].Checked = curAxis.Value.ShowAxis;
                    }
                    foreach (KeyValuePair<String, ChannelInfo> associatedChannel in curAxis.Value.AssociatedChannels)
                    {
                        if (axisChannelTree.Nodes[curAxis.Key].Nodes.ContainsKey(associatedChannel.Key))
                        {
                            continue;
                        }
                        axisChannelTree.Nodes[curAxis.Key].Nodes.Add(associatedChannel.Key, associatedChannel.Key, 1);
                        axisChannelTree.Nodes[curAxis.Key].Nodes[associatedChannel.Key].Checked = associatedChannel.Value.ShowChannel;

                    }
                }
            }
        }
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
            ChartAxes[axisName].AssociatedChannels[channelName].ChannelColor = e.SelectedColor;
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
                ChartAxes[e.Node.Name].ShowAxis = e.Node.Checked;
            }
            // channel
            else
            {
                e.Node.Parent.Checked = e.Node.Checked;
                ChartAxes[e.Node.Parent.Name].AssociatedChannels[e.Node.Text].ShowChannel = e.Node.Checked;
                ChartAxes[e.Node.Parent.Name].ShowAxis = e.Node.Checked == true ? true : ChartAxes[e.Node.Parent.Name].ShowAxis;
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

                channelsContext.Items["txtAxisMinValue"].Text = ChartAxes[axisName].AxisValueRange[0].ToString();
                channelsContext.Items["txtAxisMaxValue"].Text = ChartAxes[axisName].AxisValueRange[1].ToString();

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
            }
            else
            {
                axisOffsetsGrid.Columns["axisOffset"].Visible = false;
                axisOffsetsGrid.Columns["axisChannel"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            }

            axisOffsetsGrid.Rows.Clear();
            foreach(KeyValuePair<string, ChannelInfo> kvp in chartAxes[cmbXAxis.Text].AssociatedChannels)
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
                ChartAxes[nodeName].AxisValueRange[0] = Convert.ToSingle(channelsContext.Items["txtAxisMinValue"].Text);
                ChartAxes[nodeName].AxisValueRange[1] = Convert.ToSingle(channelsContext.Items["txtAxisMaxValue"].Text);
                ChartAxes[nodeName].AxisValueRange[2] = ChartAxes[nodeName].AxisValueRange[1] - ChartAxes[nodeName].AxisValueRange[0];
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
