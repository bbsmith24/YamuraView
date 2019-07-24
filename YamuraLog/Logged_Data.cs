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
    class RunHeader_Data
    {
        public float[] minMaxTimestamp = new float[] { float.MaxValue, float.MinValue };

        public float[]   minMaxLong = new float[] { float.MaxValue, float.MinValue };
        public float[]   minMaxLat = new float[] { float.MaxValue, float.MinValue };
        public float[] minMaxSpeed = new float[] { float.MaxValue, float.MinValue };

        public float[][] minMaxAccel = new float[][] {new float[] {float.MaxValue, float.MinValue},
                                                      new float[] {float.MaxValue, float.MinValue},
                                                      new float[] {float.MaxValue, float.MinValue}};

        public String dateStr = "";
        public String timeStr = "";
        public String fileName = "";
        public void UpdateDataRanges(float timeStamp, GPS_Data gps, Accel_Data accel)
        {
            if (timeStamp < minMaxTimestamp[0])
            {
                minMaxTimestamp[0] = timeStamp;
            }
            if (timeStamp > minMaxTimestamp[1])
            {
                minMaxTimestamp[1] = timeStamp;
            }
            if (gps.isValid)
            {
                if (gps.latVal < minMaxLat[0])
                {
                    minMaxLat[0] = gps.latVal;
                }
                if (gps.latVal > minMaxLat[1])
                {
                    minMaxLat[1] = gps.latVal;
                }
                //
                if (gps.longVal < minMaxLong[0])
                {
                    minMaxLong[0] = gps.longVal;
                }
                if (gps.longVal > minMaxLong[1])
                {
                    minMaxLong[1] = gps.longVal;
                }
                //
                if (gps.mph < minMaxSpeed[0])
                {
                    minMaxSpeed[0] = gps.mph;
                }
                if (gps.mph > minMaxSpeed[1])
                {
                    minMaxSpeed[1] = gps.mph;
                }
            }
            if(accel.isValid)
            {
                if (accel.xAccel > minMaxAccel[0][1])
                {
                    minMaxAccel[0][1] = accel.xAccel;
                }
                if (accel.xAccel < minMaxAccel[0][0])
                {
                    minMaxAccel[0][0] = accel.xAccel;
                }
                //
                if (accel.yAccel < minMaxAccel[1][0])
                {
                    minMaxAccel[1][0] = accel.yAccel;
                }
                if (accel.yAccel > minMaxAccel[1][1])
                {
                    minMaxAccel[1][1] = accel.yAccel;
                }
                //
                if (accel.zAccel > minMaxAccel[2][1])
                {
                    minMaxAccel[2][1] = accel.zAccel;
                }
                if (accel.zAccel < minMaxAccel[2][0])
                {
                    minMaxAccel[2][0] = accel.zAccel;
                }
            }
        }
    }
    /// <summary>
    /// data block - contains sample data objects and microsecond timestamp
    /// </summary>
    class DataBlock
    {
        public float micros;
        public GPS_Data gps = new GPS_Data();
        public Accel_Data accel = new Accel_Data();
    }
    public class GPS_Data
    {
        public String dateStr;
        public String timeStr;
        public float latVal;
        public float longVal;
        public float mph;
        public float heading;
        public int satellites;
        public bool isValid = false;
        public GPS_Data()
        {
            dateStr = "";
            timeStr = "";
            latVal = 0.0F;
            longVal = 0.0F;
            mph = 0.0F;
            heading = 0.0F;
            satellites = 0;
            isValid = false;
        }
        public GPS_Data(String date, String time, float lat, float longitude, float spd, float head, int sats, float x, float y, float z)
        {
            dateStr = date;
            timeStr = time;
            latVal = lat;
            longVal = longitude;
            mph = spd;
            heading = head;
            satellites = sats;
            if ((latVal == 0.0) && (longVal == 0.0))
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }
        }
    }
    public class Accel_Data
    {
        public float xAccel;
        public float yAccel;
        public float zAccel;
        public bool isValid = false;
        public Accel_Data()
        {
            xAccel = 0.0F;
            yAccel = 0.0F;
            zAccel = 0.0F;
            isValid = false;
        }
        public Accel_Data(float x, float y, float z)
        {
            xAccel = x;
            yAccel = y;
            zAccel = z;
            if ((xAccel == 0.0) && (yAccel == 0.0) && (zAccel == 0.0))
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

        }
    }
}
