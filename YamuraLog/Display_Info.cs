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
        List<ChannelInfo> associatedChannels = new List<ChannelInfo>();
        public List<ChannelInfo> AssociatedChannels
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
    }
    class ChannelInfo
    {
        int runIndex = 0;
        string channelName = "unnamed";
        public ChannelInfo(int idx, string name)
        {
            runIndex = idx;
            channelName = name;
        }
    }
}
