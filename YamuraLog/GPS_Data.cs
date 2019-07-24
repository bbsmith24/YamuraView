using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamuraLog
{
    class RunHeader_Data
    {
        public float[] minMaxLong = new float[] { float.MaxValue, float.MinValue };
        public float[] minMaxLat = new float[] { float.MaxValue, float.MinValue };
        public float[][] minMaxAccel = new float[][] {new float[] {float.MaxValue, float.MinValue},
                                               new float[] {float.MaxValue, float.MinValue},
                                               new float[] {float.MaxValue, float.MinValue}};
        public ulong[] minMaxTimestamp = new ulong[] { ulong.MaxValue, ulong.MinValue };
        public float[] minMaxSpeed = new float[] { float.MaxValue, float.MinValue };
        public String dateStr = "";
        public String timeStr = "";
        public String fileName = "";
    }
    class DataBlock
    {
        public ulong millis;
        public GPS_Data gps = new GPS_Data();
        public Accel_Data accel = new Accel_Data();
    }
    class GPS_Data
    {
        //public ulong millis;
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
    class Accel_Data
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
