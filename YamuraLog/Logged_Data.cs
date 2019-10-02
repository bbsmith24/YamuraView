using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamuraView
{
    /// <summary>
    /// run header - one per run, contains global data for run
    /// </summary>
    public class DataLogger
    {
        public float[] minMaxTimestamp = new float[] { float.MaxValue, float.MinValue };
        public float[]   minMaxLong = new float[] { float.MaxValue, float.MinValue };
        public float[]   minMaxLat = new float[] { float.MaxValue, float.MinValue };
        public float[] minMaxSpeed = new float[] { float.MaxValue, float.MinValue };

        public float[][] minMaxAccel = new float[][] {new float[] {float.MaxValue, float.MinValue},
                                                      new float[] {float.MaxValue, float.MinValue},
                                                      new float[] {float.MaxValue, float.MinValue}};

        public List<RunData> runData = new List<RunData>();
        public Dictionary<String, float[]> channelRanges = new Dictionary<String, float[]>();
        public void UpdateChannelRange(String channelName, float curVal)
        {
            channelRanges[channelName][0] = curVal < channelRanges[channelName][0] ? curVal : channelRanges[channelName][0];
            channelRanges[channelName][1] = curVal > channelRanges[channelName][1] ? curVal : channelRanges[channelName][1];
        }
        public void Reset()
        {
            runData.Clear();
            channelRanges.Clear();
            minMaxTimestamp = new float[] { float.MaxValue, float.MinValue };
            minMaxLong = new float[] { float.MaxValue, float.MinValue };
            minMaxLat = new float[] { float.MaxValue, float.MinValue };
            minMaxSpeed = new float[] { float.MaxValue, float.MinValue };

            minMaxAccel = new float[][] {new float[] {float.MaxValue, float.MinValue},
                                                          new float[] {float.MaxValue, float.MinValue},
                                                          new float[] {float.MaxValue, float.MinValue}};
    }
}
    public class RunData
    {
        public Dictionary<String, float[]> channelRanges = new Dictionary<String, float[]>();
        public Dictionary<String, DataChannel> channels = new Dictionary<String, DataChannel>();
        public String dateStr = "";
        public String timeStr = "";
        public String fileName = "";
        public float[] minMaxTimestamp = new float[] { float.MaxValue, float.MinValue };
        public void AddChannel(String name, String desc, String src, float scl)
        {
            if(channels.ContainsKey(name))
            {
                return;
            }
            channelRanges.Add(name, new float[2] { float.MaxValue, float.MinValue });
            channels.Add(name, new DataChannel(name, desc, src, scl));
        }
        public void UpdateChannelRange(String channelName, float time,  float curVal)
        {
            channelRanges[channelName][0] = curVal < channelRanges[channelName][0] ? curVal : channelRanges[channelName][0];
            channelRanges[channelName][1] = curVal > channelRanges[channelName][1] ? curVal : channelRanges[channelName][1];
            channelRanges[channelName][0] = curVal < channelRanges[channelName][0] ? curVal : channelRanges[channelName][0];
            channelRanges[channelName][1] = curVal > channelRanges[channelName][1] ? curVal : channelRanges[channelName][1];
        }
        public void AddChannelData(String channelName, float time, float value)
        {
            UpdateChannelRange(channelName, time, value);
            channels[channelName].DataPoints.Add(time, new DataPoint(value));
        }
    }
    ///// <summary>
    ///// data block - contains sample data objects and microsecond timestamp
    ///// </summary>
    //class DataBlock
    //{
    //    public float micros;
    //    public GPS_Data gps = new GPS_Data();
    //    public Accel_Data accel = new Accel_Data();
    //}
    //public class GPS_Data
    //{
    //    public String dateStr;
    //    public String timeStr;
    //    public float latVal;
    //    public float longVal;
    //    public float mph;
    //    public float heading;
    //    public int satellites;
    //    public bool isValid = false;
    //    public GPS_Data()
    //    {
    //        dateStr = "";
    //        timeStr = "";
    //        latVal = 0.0F;
    //        longVal = 0.0F;
    //        mph = 0.0F;
    //        heading = 0.0F;
    //        satellites = 0;
    //        isValid = false;
    //    }
    //    public GPS_Data(String date, String time, float lat, float longitude, float spd, float head, int sats, float x, float y, float z)
    //    {
    //        dateStr = date;
    //        timeStr = time;
    //        latVal = lat;
    //        longVal = longitude;
    //        mph = spd;
    //        heading = head;
    //        satellites = sats;
    //        if ((latVal == 0.0) && (longVal == 0.0))
    //        {
    //            isValid = false;
    //        }
    //        else
    //        {
    //            isValid = true;
    //        }
    //    }
    //}
    //public class Accel_Data
    //{
    //    public float xAccel;
    //    public float yAccel;
    //    public float zAccel;
    //    public bool isValid = false;
    //    public Accel_Data()
    //    {
    //        xAccel = 0.0F;
    //        yAccel = 0.0F;
    //        zAccel = 0.0F;
    //        isValid = false;
    //    }
    //    public Accel_Data(float x, float y, float z)
    //    {
    //        xAccel = x;
    //        yAccel = y;
    //        zAccel = z;
    //        if ((xAccel == 0.0) && (yAccel == 0.0) && (zAccel == 0.0))
    //        {
    //            isValid = false;
    //        }
    //        else
    //        {
    //            isValid = true;
    //        }

    //    }
    //}
    // single channel classes
    public class DataChannel
    {
        String channelName;
        String channelDescription;
        String channelSource;
        float channelScale;
        float[] timeRange = new float[] { float.MaxValue, float.MinValue };
        float[] dataRange = new float[] { float.MaxValue, float.MinValue };
        public SortedList<float, DataPoint> dataPoints = new SortedList<float, DataPoint>();
        public String ChannelName
        {
            get
            {
                return channelName;
            }

            set
            {
                channelName = value;
            }
        }
        public String ChannelDescription
        {
            get
            {
                return channelDescription;
            }

            set
            {
                channelDescription = value;
            }
        }
        public String ChannelSource
        {
            get
            {
                return channelSource;
            }

            set
            {
                channelSource = value;
            }
        }
        public float ChannelScale
        {
            get
            {
                return channelScale;
            }

            set
            {
                channelScale = value;
            }
        }
        public float[] TimeRange
        {
            get { return timeRange; }
            set { timeRange = value; }
        }
        public float[] DataRange
        {
            get { return dataRange; }
            set { dataRange = value; }
        }
        public DataChannel(String name, String desc, String src, float scale)
        {
            channelName = name;
            channelDescription = desc;
            channelSource = src;
            channelScale = scale;
            dataPoints = new SortedList<float, DataPoint>();
        }
        public SortedList<float, DataPoint> DataPoints
        {
            get
            {
                return dataPoints;
            }

            set
            {
                dataPoints = value;
            }
        }
        public void AddPoint(float timeStamp, float value)
        {
            if(DataPoints.ContainsKey(timeStamp))
            {
                DataPoints[timeStamp].PointValue = value;
            }
            else
            {
                DataPoints.Add(timeStamp, new DataPoint(value));
            }
            timeRange[0] = value < timeRange[0] ? value : timeRange[0];
            timeRange[1] = value > timeRange[1] ? value : timeRange[1];
            dataRange[0] = value < dataRange[0] ? value : dataRange[0];
            dataRange[1] = value > dataRange[1] ? value : dataRange[1];
        }
        public bool FindPointAtTime(float timeStamp, ref DataPoint foundPoint)
        {
            float priorTime = dataPoints.LastOrDefault(i => i.Key <= timeStamp).Key;
            float nextTime = dataPoints.FirstOrDefault(i => i.Key >= timeStamp).Key;
            // exact match
            if (priorTime == timeStamp)
            {
                foundPoint = dataPoints[priorTime];
            }
            // check for window size?
            // prior time is nearest
            else if ((timeStamp - priorTime) < (nextTime - timeStamp))
            {
                foundPoint = dataPoints[priorTime];
            }
            // next time is nearest
            else
            {
                foundPoint = dataPoints[nextTime];
            }
            return true;
        }
    }
    public class DataPoint
    {
        float pointValue = 0.0F;
        public float PointValue
        {
            get
            {
                return pointValue;
            }

            set
            {
                pointValue = value;
            }
        }
        public DataPoint(float dataValue)
        {
            PointValue = dataValue;
        }
    }
}
