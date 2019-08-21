using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamuraLog
{
    class Axis
    {
        String axisName = "unnamed";
        public String AxisName
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

        Dictionary<String, ChannelInfo> associatedChannels = new Dictionary<String, ChannelInfo>();
        public Dictionary<String, ChannelInfo> AssociatedChannels
        {
            get { return associatedChannels; }
            set { associatedChannels = value; }
        }

        float[] displayScale = new float[] { 1.0F, 1.0F };
        public float[] DisplayScale
        {
            get { return displayScale; }
            set { displayScale = value; }
        }

        float displayOffset = 0.0F;
        public float DisplayOffset
        {
            get { return displayOffset; }
            set { displayOffset = value; }
        }

        bool showAxis = false;
        public bool ShowAxis
        {
            get { return showAxis; }
            set { showAxis = value; }
        }
    }
    class ChannelInfo
    {
        int runIndex = 0;
        public int RunIndex
        {
            get { return runIndex; }
            set { runIndex = value; }
        }

        string channelName = "unnamed";
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
        public ChannelInfo(int idx, string name)
        {
            runIndex = idx;
            channelName = name;
        }
    }
}
