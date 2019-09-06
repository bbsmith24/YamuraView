using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamuraLog
{
    public class Axis
    {
        string axisName = "unnamed";
        public string AxisName
        {
            get { return axisName; }
            set { axisName = value; }
        }

        float[] axisRange = new float[] { float.MaxValue, float.MinValue, float.MaxValue };
        public float[] AxisRange
        {
            get { return axisRange; }
            set { axisRange = value; }
        }

        Dictionary<string, ChannelInfo> associatedChannels = new Dictionary<string, ChannelInfo>();
        public Dictionary<string, ChannelInfo> AssociatedChannels
        {
            get { return associatedChannels; }
            set { associatedChannels = value; }
        }

        bool showAxis = false;
        public bool ShowAxis
        {
            get { return showAxis; }
            set { showAxis = value; }
        }
    }
    public class ChannelInfo
    {
        int runIndex = 0;
        public int RunIndex
        {
            get { return runIndex; }
            set { runIndex = value; }
        }

        String channelName = "unnamed";
        public String ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }

        bool showChannel = false;
        public bool ShowChannel
        {
            get { return showChannel; }
            set { showChannel = value; }
        }

        Color channelColor = Color.Black;
        public Color ChannelColor
        {
            get { return channelColor; }
            set { channelColor = value; }
        }

        float[] axisRange = new float[] { 0.0F, 0.0F, 0.0F };
        public float[] AxisRange
        {
            get { return axisRange; }
            set { axisRange = value; }
        }

        float[] axisOffset = new float[] { 0.0F, 0.0F};
        /// <summary>
        /// axis offset value
        /// </summary>
        public float[] AxisOffset
        {
            get { return axisOffset; }
            set { axisOffset = value; }
        }

        public ChannelInfo(int idx, String name)
        {
            runIndex = idx;
            channelName = name;
        }
    }
}
