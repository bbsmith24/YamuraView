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
    public partial class ChartControlAxesSelect : Form
    {
        Dictionary<string, Axis> chartAxes = new Dictionary<string, Axis>();
        public Dictionary<string, Axis> ChartAxes
        {
            get { return chartAxes; }
            set
            {
                chartAxes = value;

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

        public string XAxisName
        {
            get { return cmbXAxis.Text; }
            set { cmbXAxis.Text = value; }
        }

        public ChartControlAxesSelect()
        {
            InitializeComponent();
        }

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

        private void channelsContext_Opening(object sender, CancelEventArgs e)
        {
            if (axisChannelTree.SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }
            if (axisChannelTree.SelectedNode.Parent == null)
            {
                channelsContext.Items["axisExtents"].Enabled = true;
                channelsContext.Items["channelInfo"].Enabled = false;
            }
            else
            {
                channelsContext.Items["axisExtents"].Enabled = false;
                channelsContext.Items["channelInfo"].Enabled = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axisExtents_Click(object sender, EventArgs e)
        {
            String axisName = axisChannelTree.SelectedNode.Text;

            AxisInfo axisDlg = new AxisInfo();
            axisDlg.Text = axisName + " Info";
            axisDlg.RangeMinimum = ChartAxes[axisName].AxisRange[0];
            axisDlg.RangeMaximum = ChartAxes[axisName].AxisRange[1];
            if (axisDlg.ShowDialog() == DialogResult.OK)
            {
                ChartAxes[axisName].AxisRange[0] = axisDlg.RangeMinimum;
                ChartAxes[axisName].AxisRange[1] = axisDlg.RangeMaximum;
                ChartAxes[axisName].AxisRange[2] = ChartAxes[axisName].AxisRange[1] - ChartAxes[axisName].AxisRange[0];
                ChartAxes[axisName].DisplayOffset = -1 * ChartAxes[axisName].AxisRange[0];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channelInfo_Click(object sender, EventArgs e)
        {
            String axisName = axisChannelTree.SelectedNode.Parent.Text;
            String channelName = axisChannelTree.SelectedNode.Text;
            ///String channelFullName = 
            //int runIdx = Convert.ToInt32(channelFullName.Substring(0, channelFullName.IndexOf('-')));
            //String channelName = channelFullName.Substring(channelFullName.IndexOf('-') + 1);

            ChannelInfoForm channelDlg = new ChannelInfoForm();
            channelDlg.Text = channelName + " Info";

            //channelDlg.RangeMinimum = ChartAxes[axisName].AssociatedChannels[channelName]. dataLogger.runData[runIdx].channels[channelName].DataRange[0];
            //channelDlg.RangeMaximum = dataLogger.runData[runIdx].channels[channelName].DataRange[1];
            channelDlg.ChannelColor = ChartAxes[axisName].AssociatedChannels[channelName].ChannelColor;

            if (channelDlg.ShowDialog() == DialogResult.OK)
            {
                ChartAxes[axisName].AssociatedChannels[channelName].ChannelColor = channelDlg.ChannelColor;
            }

        }
    }
}
