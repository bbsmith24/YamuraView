using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamuraLog
{
    /// <summary>
    /// run header - one per run, contains global data for run
    /// </summary>
    class DataLogger
    {
        public float[] minMaxTimestamp = new float[] { float.MaxValue, float.MinValue };
        public float[]   minMaxLong = new float[] { float.MaxValue, float.MinValue };
        public float[]   minMaxLat = new float[] { float.MaxValue, float.MinValue };
        public float[] minMaxSpeed = new float[] { float.MaxValue, float.MinValue };

        public float[][] minMaxAccel = new float[][] {new float[] {float.MaxValue, float.MinValue},
                                                      new float[] {float.MaxValue, float.MinValue},
                                                      new float[] {float.MaxValue, float.MinValue}};

        public List<RunData> runData = new List<RunData>();
        public Dictionary<String, float[]> channelRanges = new Dictionary<string, float[]>();
        public void UpdateChannelRange(String channelName, float curVal)
        {
            channelRanges[channelName][0] = curVal < channelRanges[channelName][0] ? curVal : channelRanges[channelName][0];
            channelRanges[channelName][1] = curVal > channelRanges[channelName][1] ? curVal : channelRanges[channelName][1];
        }
        //public void UpdateDataRanges(float timeStamp, GPS_Data gps, Accel_Data accel)
        //{
        //    if (timeStamp < minMaxTimestamp[0])
        //    {
        //        minMaxTimestamp[0] = timeStamp;
        //    }
        //    if (timeStamp > minMaxTimestamp[1])
        //    {
        //        minMaxTimestamp[1] = timeStamp;
        //    }
        //    if (gps.isValid)
        //    {
        //        if (gps.latVal < minMaxLat[0])
        //        {
        //            minMaxLat[0] = gps.latVal;
        //        }
        //        if (gps.latVal > minMaxLat[1])
        //        {
        //            minMaxLat[1] = gps.latVal;
        //        }
        //        //
        //        if (gps.longVal < minMaxLong[0])
        //        {
        //            minMaxLong[0] = gps.longVal;
        //        }
        //        if (gps.longVal > minMaxLong[1])
        //        {
        //            minMaxLong[1] = gps.longVal;
        //        }
        //        //
        //        if (gps.mph < minMaxSpeed[0])
        //        {
        //            minMaxSpeed[0] = gps.mph;
        //        }
        //        if (gps.mph > minMaxSpeed[1])
        //        {
        //            minMaxSpeed[1] = gps.mph;
        //        }
        //    }
        //    if(accel.isValid)
        //    {
        //        if (accel.xAccel > minMaxAccel[0][1])
        //        {
        //            minMaxAccel[0][1] = accel.xAccel;
        //        }
        //        if (accel.xAccel < minMaxAccel[0][0])
        //        {
        //            minMaxAccel[0][0] = accel.xAccel;
        //        }
        //        //
        //        if (accel.yAccel < minMaxAccel[1][0])
        //        {
        //            minMaxAccel[1][0] = accel.yAccel;
        //        }
        //        if (accel.yAccel > minMaxAccel[1][1])
        //        {
        //            minMaxAccel[1][1] = accel.yAccel;
        //        }
        //        //
        //        if (accel.zAccel > minMaxAccel[2][1])
        //        {
        //            minMaxAccel[2][1] = accel.zAccel;
        //        }
        //        if (accel.zAccel < minMaxAccel[2][0])
        //        {
        //            minMaxAccel[2][0] = accel.zAccel;
        //        }
        //    }
        //}
    }
    class RunData
    {
        public Dictionary<String, float[]> channelRanges = new Dictionary<string, float[]>();
        public Dictionary<String, DataChannel> channels = new Dictionary<string, DataChannel>();
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
        float timeMin = float.MaxValue;
        float timeMax = float.MinValue;
        float channelMin = float.MaxValue;
        float channelMax = float.MinValue;
        public SortedList<float, DataPoint> dataPoints = new SortedList<float, DataPoint>();
        public string ChannelName
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
        public string ChannelDescription
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
        public string ChannelSource
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
        public float TimeMin
        {
            get
            {
                return timeMin;
            }

            set
            {
                timeMin = value;
            }
        }
        public float TimeMax
        {
            get
            {
                return timeMax;
            }

            set
            {
                timeMax = value;
            }
        }
        public float ChannelMin
        {
            get
            {
                return channelMin;
            }

            set
            {
                channelMin = value;
            }
        }
        public float ChannelMax
        {
            get
            {
                return channelMax;
            }

            set
            {
                channelMax = value;
            }
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
            TimeMin = value < TimeMin ? value : TimeMin;
            TimeMax = value > TimeMax ? value : TimeMax;
            ChannelMin = value < ChannelMin ? value : ChannelMin;
            ChannelMax = value > ChannelMax ? value : ChannelMax;
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
