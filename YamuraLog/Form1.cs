using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDI;
using Win32Interop.Methods;

namespace YamuraLog
{
    public partial class Form1 : Form
    {
        // GDI draw modes for eraseable cursors
        public enum DrawMode
        {
            R2_BLACK = 1,  // Pixel is always black.
            R2_NOTMERGEPEN = 2,  // Pixel is the inverse of the R2_MERGEPEN color (final pixel = NOT(pen OR screen pixel)).
            R2_MASKNOTPEN = 3,  // Pixel is a combination of the colors common to both the screen and the inverse of the pen (final pixel = (NOT pen) AND screen pixel).
            R2_NOTCOPYPEN = 4,  // Pixel is the inverse of the pen color.
            R2_MASKPENNOT = 5,  // Pixel is a combination of the colors common to both the pen and the inverse of the screen (final pixel = (NOT screen pixel) AND pen).
            R2_NOT = 6,  // Pixel is the inverse of the screen color.
            R2_XORPEN = 7,  // Pixel is a combination of the colors that are in the pen or in the screen, but not in both (final pixel = pen XOR screen pixel).
            R2_NOTMASKPEN = 8,  // Pixel is the inverse of the R2_MASKPEN color (final pixel = NOT(pen AND screen pixel)).
            R2_MASKPEN = 9,  // Pixel is a combination of the colors common to both the pen and the screen (final pixel = pen AND screen pixel).
            R2_NOTXORPEN = 10,  // Pixel is the inverse of the R2_XORPEN color (final pixel = NOT(pen XOR screen pixel)).
            R2_NOP = 11,  // Pixel remains unchanged.
            R2_MERGENOTPEN = 12,  // Pixel is a combination of the screen color and the inverse of the pen color (final pixel = (NOT pen) OR screen pixel).
            R2_COPYPEN = 13,  // Pixel is the pen color.
            R2_MERGEPENNOT = 14,  // Pixel is a combination of the pen color and the inverse of the screen color (final pixel = (NOT screen pixel) OR pen).
            R2_MERGEPEN = 15,  // Pixel is a combination of the pen color and the screen color (final pixel = pen OR screen pixel).
            R2_WHITE = 16,  // Pixel is always white.
            R2_LAST = 16
        }

        private Button colorButton = new Button();
        List<Color> penColors = new List<Color>();
        private List<Task> tasks = new List<Task>();

        // DataLogger contains runs, which contain channels which contain data points
        DataLogger dataLogger = new DataLogger();
        // global display data
        GlobalDisplay_Data globalDisplay = new GlobalDisplay_Data();
        // run for display data
        List<RunDisplay_Data> runDisplay = new List<RunDisplay_Data>();

        float gpsDist = 0.0F;

        #region track map
        #region mouse move points
        Point trackMapStartPos = new Point(0, 0);
        bool trackMapStartPosValid = false;
        Point trackMapLastPos = new Point(0, 0);
        #endregion
        #region cursor
        List<Point> trackMapLastCursorPos = new List<Point>();
        List<bool> trackMapLastCursorPosValid = new List<bool>();
        float trackMapCursorSize = 10.0F;
        #endregion
        #region scaling
        float trackMapScale = 1.0F;
        float trackMapScaleFactor = 1.0F;
        float[] trackMapOffset = new float[] { 0.0F, 0.0F };
        #endregion
        #region window
        Rectangle trackMapBounds = new Rectangle(0, 0, 0, 0);
        int trackMapBorder = 10;
        #endregion
        #endregion
        #region traction circle
        #region mouse move points
        Point tractionCircleStartPos = new Point(0, 0);
        bool tractionCircleStartPosValid = false;
        Point tractionCircleLastPos = new Point(0, 0);
        #endregion
        #region cursor
        List<Point> tractionCircleLastCursorPos = new List<Point>();
        List<bool> tractionCircleLastCursorPosValid = new List<bool>();
        float tractionCircleCursorSize = 10.0F;
        #endregion
        #region scaling
        float tractionCircleScale = 1.0F;
        float tractionCircleScaleFactor = 1.0F;
        float[] tractionCircleOffset = new float[] { 0.0F, 0.0F };
        #endregion
        #region window
        int tractionCircleBorder = 10;
        Rectangle tractionCircleBounds = new Rectangle(0, 0, 0, 0);
        #endregion
        #endregion
        #region strip chart
        #region mouse move points
        Point stripChartStartPos = new Point(0, 0);
        bool stripChartStartPosValid = false;
        Point stripChartLastPos = new Point(0, 0);
        #endregion
        #region cursor
        PointF stripChartStartCursorPos = new Point(0, 0);
        PointF stripChartLastCursorPos = new Point(0, 0);
        Point stripChartStartCursorPosInt = new Point(0, 0);
        Point stripChartLastCursorPosInt = new Point(0, 0);
        bool stripChartLastCursorPosValid = false;
        #endregion
        #region scaling
        float stripChartScaleX = 0.0F;
        Dictionary<String, float> stripChartScaleY = new Dictionary<String, float>();
        float stripChartScaleFactorX = 1.0F;
        float stripChartScaleFactorY = 1.0F;
        #endregion
        #region window
        int stripChartPanelBorder = 10;
        Rectangle stripChartPanelBounds = new Rectangle(0, 0, 0, 0);
        #endregion
        #region X axis range
        float[] stripChartOffset = new float[] { 0.0F, 0.0F };
        float[] stripChartExtentsX = new float[] { 0.0F, 0.0F };
        #endregion
        string xChannelName = "none";
        string yChannelName = "none";
        #endregion

        int dragZoomPenWidth = 1;
        public Form1()
        {
            InitializeComponent();
            (mapPanel as Control).KeyPress += new KeyPressEventHandler(trackMap_KeyDown);
            (tractionCirclePanel as Control).KeyPress += new KeyPressEventHandler(tractionCircle_KeyDown);
            (stripChartPanel as Control).KeyPress += new KeyPressEventHandler(stripChart_KeyDown);
            penColors.Add(Color.Red);
            penColors.Add(Color.Green);
            penColors.Add(Color.Blue);
            penColors.Add(Color.Cyan);
            penColors.Add(Color.Magenta);
            penColors.Add(Color.Orange);
            penColors.Add(Color.DarkRed);
            penColors.Add(Color.DarkGreen);
            penColors.Add(Color.DarkBlue);
            penColors.Add(Color.DarkCyan);
            penColors.Add(Color.DarkMagenta);
            penColors.Add(Color.DarkOrange);
            penColors.Add(Color.Black);

            colorButton.Text = "Generate Report";
            colorButton.Dock = DockStyle.Top;

            // Add a CellClick handler to handle clicks in the button column.
            runDataGrid.CellClick += new DataGridViewCellEventHandler(RunDataGrid_CellClick);
            channelDataGrid.CellClick += new DataGridViewCellEventHandler(YAxisChannelDataGrid_CellValueChanged);
            runDataGrid.CellEndEdit += new DataGridViewCellEventHandler(RunDataGrid_CellEndEdit);
            runDataGrid.CellValueChanged += new DataGridViewCellEventHandler(RunDataGrid_CellValueChanged);
            runDataGrid.CellMouseUp += new DataGridViewCellMouseEventHandler(RunDataGrid_CellMouseUp);
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            runDataGrid.Rows.Clear();
            channelDataGrid.Rows.Clear();
            dataLogger.Reset();
            globalDisplay.Reset();
            runDisplay.Clear();

            stripChartPanel.Invalidate();
            mapPanel.Invalidate();
            tractionCirclePanel.Invalidate();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openLogFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (openLogFile.FileName.EndsWith("TXT", StringComparison.CurrentCultureIgnoreCase))
            {
                ReadTXTFile(openLogFile.FileName);
            }
            else if (openLogFile.FileName.EndsWith("YLG", StringComparison.CurrentCultureIgnoreCase))
            {
                ReadYLGFile(openLogFile.FileName);
            }
            DrawMap();
            DrawTraction();
            DrawSpeed();
        }
        private void btnAutoAlign_Click(object sender, EventArgs e)
        {
            float autoThreshold = 0.0F;
            try
            {
                autoThreshold = Convert.ToSingle(txtAutoAlignThreshold.Text);
            }
            catch
            {
                MessageBox.Show("Can't convert auto align value " + txtAutoAlignThreshold.Text + " to a number");
                return;
            }
            AutoAlign(autoThreshold);
        }
        private void cmbXAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            xChannelName = cmbXAxis.Text;
            stripChartOffset[0] = -1.0F * globalDisplay.channelRanges[xChannelName][0];
            stripChartExtentsX[0] = globalDisplay.channelRanges[xChannelName][0];
            stripChartExtentsX[1] = globalDisplay.channelRanges[xChannelName][1];


            stripChartScaleX = 0.0F;
            stripChartPanel.Invalidate();
        }
        #region read log file
        private void ReadTXTFile(String fileName)
        {
            #region create a temp file to write cleaned up data stream
            String tempLogFile = fileName.Replace(".txt", ".tmp");
            tempLogFile = tempLogFile.Replace(".TXT", ".TMP");
            StreamReader readLog = new StreamReader(fileName, true);
            StreamWriter writeLog = new StreamWriter(tempLogFile, false);
            String tmp_text = readLog.ReadToEnd();// readFile.ReadToEnd();
            StringBuilder gpx_text = new StringBuilder();
            foreach (char c in tmp_text)
            {
                if ((c != 0x01) && (c != 0x11) && (c != 0x0C))
                {
                    writeLog.Write(c);
                    gpx_text.Append(c);
                }
            }
            readLog.Close();
            writeLog.Close();
            #endregion

            StreamReader readTemp = new StreamReader(tempLogFile, true);
            String inputStr;
            String[] splitStr;
            int logRunsIdx = 0;
            float priorLatVal = 0.0F;
            float priorLongVal = 0.0F;
            float latVal = 0.0F;
            float longVal = 0.0F;
            float gX = 0.0F;
            float gY = 0.0F;
            float gZ = 0.0F;
            ulong timestamp = 0;
            ulong timestampOffset = 0;
            float timestampSeconds = 0.0F;
            float mph = 0;
            float heading = 0;
            bool timestampOffsetValid = false;
            bool gpsDistanceValid = false;
            StringBuilder strRunsList = new StringBuilder();
            while (!readTemp.EndOfStream)
            {
                #region skip blanks
                inputStr = readTemp.ReadLine();
                if (inputStr.Length == 0)
                {
                    continue;
                }
                #endregion
                #region run start, add a new run to logger
                // found run start, create new data list in log events
                //                  new run data header
                //                  new display header
                if (String.Compare(inputStr, "Start", true) == 0)
                {
                    gpsDistanceValid = false;
                    gpsDist = 0.0F;
                    timestampOffsetValid = false;
                    timestampOffset = 0;
                    dataLogger.runData.Add(new RunData());
                    logRunsIdx = dataLogger.runData.Count - 1;
                    // set run file name in run data
                    dataLogger.runData[logRunsIdx].fileName = System.IO.Path.GetFileName(fileName);
                    // add timestamp here, since it is always present
                    dataLogger.runData[logRunsIdx].AddChannel("Time", "Timestamp", "Internal", 1.0F);
                    continue;
                }
                #endregion
                #region run end, skip just a marker
                else if ((String.Compare(inputStr, "Stop", true) == 0) ||
                         inputStr.StartsWith("GPS") ||
                         inputStr.StartsWith("Accel") ||
                         inputStr.StartsWith("Team Yamura"))
                {
                    continue;
                }
                #endregion
                #region break up the input string
                splitStr = inputStr.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                #endregion
                #region add timestamp
                timestamp = (ulong)BitConverter.ToUInt32(BitConverter.GetBytes(Convert.ToInt32(splitStr[0])), 0);
                if (!timestampOffsetValid)
                {
                    timestampOffset = timestamp;
                    timestampOffsetValid = true;
                }
                timestamp -= timestampOffset;
                timestampSeconds = Convert.ToSingle(timestamp) / 1000000.0F;
                dataLogger.runData[logRunsIdx].channels["Time"].AddPoint(timestampSeconds, timestampSeconds);
                #endregion
                #region GPS data
                // gps+accel form - 11 fields
                // 35990532 07/11/2019 12:44:39.00 42.446449 -83.456070  0.70    183.920000  7   0.306   0.005   0.947
                //
                // gps only - 8 fields
                // 35990532 07/11/2019 12:44:39.00 42.446449 -83.456070  0.70    183.920000  7
                //
                // accel only - 4 fields
                //36050376 0.41   0.00    0.94
                // timestamp is always first

                // gps only, gps+accelerometer - get gps portion
                if ((splitStr.Count() == 8) || (splitStr.Count() == 11))
                {
                    latVal = Convert.ToSingle(splitStr[3]);
                    longVal = Convert.ToSingle(splitStr[4]);
                    mph = Convert.ToSingle(splitStr[5]);
                    heading = Convert.ToSingle(splitStr[6]);
                    if (dataLogger.runData[logRunsIdx].dateStr.Length == 0)
                    {
                        dataLogger.runData[logRunsIdx].dateStr = splitStr[1];
                        dataLogger.runData[logRunsIdx].timeStr = splitStr[2];
                    }
                    // add channel (only if needed)
                    dataLogger.runData[logRunsIdx].AddChannel("Latitude", "GPS Latitude", "GPS", 1.0F);
                    dataLogger.runData[logRunsIdx].AddChannel("Longitude", "GPS Longitude", "GPS", 1.0F);
                    dataLogger.runData[logRunsIdx].AddChannel("Speed-GPS", "GPS Speed", "GPS", 1.0F);
                    dataLogger.runData[logRunsIdx].AddChannel("Heading-GPS", "GPS Heading", "GPS", 1.0F);
                    dataLogger.runData[logRunsIdx].AddChannel("Distance-GPS", "GPS Distance", "GPS", 1.0F);
                    // add data to channel
                    dataLogger.runData[logRunsIdx].channels["Latitude"].AddPoint(timestampSeconds, latVal);
                    dataLogger.runData[logRunsIdx].channels["Longitude"].AddPoint(timestampSeconds, longVal);
                    dataLogger.runData[logRunsIdx].channels["Speed-GPS"].AddPoint(timestampSeconds, mph);
                    dataLogger.runData[logRunsIdx].channels["Heading-GPS"].AddPoint(timestampSeconds, heading);
                    if (!gpsDistanceValid)
                    {
                        dataLogger.runData[logRunsIdx].channels["Distance-GPS"].AddPoint(timestampSeconds, 0.0F);
                        priorLatVal = latVal;
                        priorLongVal = longVal;
                        gpsDistanceValid = true;
                    }
                    else
                    {
                        gpsDist += GPSDistance(priorLatVal, priorLongVal, latVal, longVal);
                        dataLogger.runData[logRunsIdx].channels["Distance-GPS"].AddPoint(timestampSeconds, gpsDist);
                    }
                }
                #endregion
                #region accelerometer data
                // accelerometer only, or gps+accelerometer - get accelerometer portion
                if ((splitStr.Count() == 4) || (splitStr.Count() == 11))
                {
                    latVal = 0;
                    longVal = 0;
                    int xValIdx = splitStr.Count() == 4 ? 1 : 8;
                    int yValIdx = splitStr.Count() == 4 ? 2 : 9;
                    int zValIdx = splitStr.Count() == 4 ? 3 : 10;

                    gX = Convert.ToSingle(splitStr[xValIdx]);
                    gY = Convert.ToSingle(splitStr[yValIdx]);
                    gZ = Convert.ToSingle(splitStr[zValIdx]);
                    // add channel (only if needed)
                    dataLogger.runData[logRunsIdx].AddChannel("gX", "X Axis Acceleration", "Accelerometer", 1.0F);
                    dataLogger.runData[logRunsIdx].AddChannel("gY", "Y Axis Acceleration", "Accelerometer", 1.0F);
                    dataLogger.runData[logRunsIdx].AddChannel("gZ", "Z Axis Acceleration", "Accelerometer", 1.0F);
                    // add data to channel
                    dataLogger.runData[logRunsIdx].channels["gX"].AddPoint(timestampSeconds, gX);
                    dataLogger.runData[logRunsIdx].channels["gY"].AddPoint(timestampSeconds, gY);
                    dataLogger.runData[logRunsIdx].channels["gZ"].AddPoint(timestampSeconds, gZ);

                }
                #endregion
            }
            #region close and delete temp file
            // close and delete temp file
            readTemp.Close();
            System.IO.File.Delete(tempLogFile);
            #endregion
            UpdateData();
        }
        private void ReadYLGFile(String fileName)
        {
            int logRunsIdx = 0;
            char[] b = new char[3];
            uint timeStamp = 0;
            float timestampSeconds = 0;
            float priorLatVal = 0.0F;
            float priorLongVal = 0.0F;
            bool gpsDistanceValid = false;

            dataLogger.runData.Add(new RunData());

            logRunsIdx = dataLogger.runData.Count - 1;
            dataLogger.runData[logRunsIdx].AddChannel("Time", "Timestamp", "Internal", 1.0F);

            dataLogger.runData[logRunsIdx].fileName = System.IO.Path.GetFileName(fileName);

            using (BinaryReader inFile = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                // check for EOF
                while (inFile.BaseStream.Position != inFile.BaseStream.Length)
                {
                    #region readf 1 char, exception on EOF
                    try
                    {
                        b[0] = (char)inFile.ReadByte();// inFile.ReadChar();
                    }
                    catch
                    {
                        continue;
                    }
                    #endregion
                    #region timestamp
                    // 'T', next 4 bytes are a unsigned long int
                    if ((char)b[0] == 'T')
                    {
                        timeStamp = inFile.ReadUInt32();
                        timestampSeconds = (float)timeStamp / 1000000.0F;
                        dataLogger.runData[logRunsIdx].channels["Time"].AddPoint(timestampSeconds, timestampSeconds);
                        continue;
                    }
                    #endregion
                    #region get channel type chars
                    try
                    {
                        b[1] = (char)inFile.ReadByte(); //inFile.ReadChar();
                        b[2] = (char)inFile.ReadByte(); //inFile.ReadChar();
                    }
                    catch
                    {
                        break;
                    }
                    #endregion
                    #region GPS
                    // GPS (gps device) returns NMEA strings
                    // 4 byte channel number followed by NMEA string
                    if ((b[0] == 'G') && (b[1] == 'P') && (b[2] == 'S'))
                    {
                        inFile.ReadUInt32();

                        float lat = 0.0F;
                        string ns = "";
                        float lng = 0.0F;
                        string ew = "";
                        float hd = 0.0F;
                        float speed = 0.0F;
                        int sat = 0;
                        string date = "";
                        int utcHr = 0;
                        int utcMin = 0;
                        Single utcSec = 0.0F;
                        if (ParseGPS_NMEA(inFile, out date, out utcHr, out utcMin, out utcSec, out lat, out ns, out lng, out ew, out hd, out speed, out sat))
                        {
                            dataLogger.runData[logRunsIdx].AddChannel("Latitude", "GPS Latitude", "GPS", 1.0F);
                            dataLogger.runData[logRunsIdx].AddChannel("Longitude", "GPS Longitude", "GPS", 1.0F);
                            dataLogger.runData[logRunsIdx].AddChannel("Speed-GPS", "GPS Speed", "GPS", 1.0F);
                            dataLogger.runData[logRunsIdx].AddChannel("Heading-GPS", "GPS Heading", "GPS", 1.0F);
                            dataLogger.runData[logRunsIdx].AddChannel("Distance-GPS", "GPS Distance", "GPS", 1.0F);
                            // add data to channel
                            dataLogger.runData[logRunsIdx].channels["Latitude"].AddPoint(timestampSeconds, lat);
                            dataLogger.runData[logRunsIdx].channels["Longitude"].AddPoint(timestampSeconds, lng);
                            dataLogger.runData[logRunsIdx].channels["Speed-GPS"].AddPoint(timestampSeconds, speed);
                            dataLogger.runData[logRunsIdx].channels["Heading-GPS"].AddPoint(timestampSeconds, hd);
                            if (!gpsDistanceValid)
                            {
                                dataLogger.runData[logRunsIdx].channels["Distance-GPS"].AddPoint(timestampSeconds, 0.0F);
                                priorLatVal = lat;
                                priorLongVal = lng;
                                gpsDistanceValid = true;
                            }
                            else
                            {
                                gpsDist += GPSDistance(priorLatVal, priorLongVal, lat, lng);
                                dataLogger.runData[logRunsIdx].channels["Distance-GPS"].AddPoint(timestampSeconds, gpsDist);
                            }
                        }
                    }
                    #endregion
                    #region ACC
                    //
                    // accel channel
                    // ACC (3 axis accelerometer) returns byte channel number followed by 3 float values
                    //
                    else if ((b[0] == 'A') && (b[1] == 'C') && (b[2] == 'C'))
                    {
                        inFile.ReadUInt32();
                        // add channel (only if needed)
                        dataLogger.runData[logRunsIdx].AddChannel("gX", "X Axis Acceleration", "Accelerometer", 1.0F);
                        dataLogger.runData[logRunsIdx].AddChannel("gY", "Y Axis Acceleration", "Accelerometer", 1.0F);
                        dataLogger.runData[logRunsIdx].AddChannel("gZ", "Z Axis Acceleration", "Accelerometer", 1.0F);
                        Single accelVal = 0.0F;
                        for (int valIdx = 0; valIdx < 3; valIdx++)
                        {
                            accelVal = inFile.ReadSingle();
                            // add data to channel
                            if (valIdx == 0)
                            {
                                dataLogger.runData[logRunsIdx].channels["gX"].AddPoint(timestampSeconds, accelVal);
                            }
                            else if (valIdx == 1)
                            {
                                dataLogger.runData[logRunsIdx].channels["gY"].AddPoint(timestampSeconds, accelVal);
                            }
                            else if (valIdx == 2)
                            {
                                dataLogger.runData[logRunsIdx].channels["gZ"].AddPoint(timestampSeconds, accelVal);
                            }
                        }
                    }
                    #endregion
                    #region IMU
                    // not implemented
                    #endregion
                    #region A2D/DIG/CNT/RPM
                    // 
                    // analog channel
                    // 4 byte channel number followed by 1 float (value)
                    //
                    else if ((b[0] == 'A') && (b[1] == '2') && (b[2] == 'D'))
                    {
                        uint channelNum = inFile.ReadUInt32();
                        UInt32 channelVal = inFile.ReadUInt32();
                        float channelValF = (float)channelVal;
                        String channelName = "A2D" + channelNum.ToString();
System.Diagnostics.Debug.WriteLine(timestampSeconds + " " + channelName + " " + channelVal.ToString() + " " + channelValF.ToString());
                        dataLogger.runData[logRunsIdx].AddChannel(channelName, "Analog to Digital channel " + channelName, "A2D", 1.0F);
                        dataLogger.runData[logRunsIdx].channels[channelName].AddPoint(timestampSeconds, channelValF);

                    }
                    #endregion
                }
                inFile.Close();
            }
            UpdateData();
        }
        /// <summary>
        /// very specific NMEA parser for the output from Sparkfun QWIIC GPS breakout
        /// see the Titan datasheet for more info
        /// 
        /// GGA - Time, position and fix type data.
        /// $GPGGA -
        /// $GNGGA -
        ///
        /// GSA - GNSS receiver operating mode, active satellites used in the position solution and DOP values.
        /// $GPGSA
        /// $GLGSA
        ///
        /// GSV - The number of GPS satellites in view satellite ID numbers, elevation, azimuth, and SNR values.
        /// $GPGSV
        /// $GLGSV
        ///
        /// RMC - Time, date, position, course and speed data. The recommended minimum navigation information.
        /// $GPRMC
        /// $GNRMC
        /// 
        /// Course and speed information relative to the ground.
        /// $GPVTG
        /// $GNVTG
        /// 
        /// </summary>
        /// <param name="inFile"></param>
        public bool ParseGPS_NMEA(BinaryReader inFile, out string date, out int hr, out int min, out float sec, out float lat, out string ns, out float lng, out string ew, out float hd, out float speed, out int sat)
        {
            bool rVal = false;
            int utcHour = -1;
            int utcMin = -1;
            int utcSec = -1;
            int utcmSec = -1;
            int latDeg = -1;
            int latMin = -1;
            int latMinDecimal = -1;
            int longDeg = -1;
            int longMin = -1;
            int longMinDecimal = -1;
            int fixType = -1;
            int satellites = -1;
            Single speedKnotsPH = 0.0F;
            Single speedKmPH = 0.0F;
            Single heading = 0.0F;
            String dateStr = "";
            String nsIndication = "";
            String ewIndication = "";
            String dataValid = "";
            lat = 0.0F;
            ns = "X";
            lng = 0.0F;
            ew = "X";
            hd = 0.0F;
            speed = 0.0F;
            sat = 0;
            date = "xx/xx/xxxx";
            hr = 0;
            min = 0;
            sec = 0F;


            char c;
            String dataSentence;
            // sentence always begins with '$', ends with 0x0D
            // except when it doesn't - sometimes the '$' gets dropped....
            while ((inFile.PeekChar() == '$') || (inFile.PeekChar() == 'G'))
            {
                #region read sentence
                dataSentence = "";
                c = inFile.ReadChar();
                while (c != 0x0D)
                {
                    dataSentence += c;
                    c = (char)inFile.ReadByte();
                }
                String[] words = dataSentence.Split(new char[] { ',' });
                #endregion
                try
                {
                    #region PARSE GGA - Time, position and fix type data.
                    if ((dataSentence.StartsWith("$GPGGA")) || // GPS
                        (dataSentence.StartsWith("$GNGGA")))   // 
                    {
                        utcHour = Convert.ToInt32(words[1].Substring(0, 2));
                        utcMin = Convert.ToInt32(words[1].Substring(2, 2));
                        utcSec = Convert.ToInt32(words[1].Substring(4, 2));
                        utcmSec = Convert.ToInt32(words[1].Substring(7, 3));

                        latDeg = Convert.ToInt32(words[2].Substring(0, 2));
                        latMin = Convert.ToInt32(words[2].Substring(2, 2));
                        latMinDecimal = Convert.ToInt32(words[2].Substring(5, 4));

                        nsIndication = words[3];

                        longDeg = Convert.ToInt32(words[4].Substring(0, 3));
                        longMin = Convert.ToInt32(words[4].Substring(3, 2));
                        longMinDecimal = Convert.ToInt32(words[4].Substring(6, 4));

                        ewIndication = words[5];

                        fixType = Convert.ToInt32(words[6]);

                        satellites = Convert.ToInt32(words[7]);
                    }
                    #endregion
                    #region PARSE RMC - Time, date, position, course and speed data. The recommended minimum navigation information.
                    else if ((dataSentence.StartsWith("$GPRMC")) || // GPS
                             (dataSentence.StartsWith("$GNRMC"))) // GNSS
                    {
                        utcHour = Convert.ToInt32(words[1].Substring(0, 2));
                        utcMin = Convert.ToInt32(words[1].Substring(2, 2));
                        utcSec = Convert.ToInt32(words[1].Substring(4, 2));
                        utcmSec = Convert.ToInt32(words[1].Substring(7, 3));

                        dataValid = words[2];

                        latDeg = Convert.ToInt32(words[3].Substring(0, 2));
                        latMin = Convert.ToInt32(words[3].Substring(2, 2));
                        latMinDecimal = Convert.ToInt32(words[3].Substring(5, 4));

                        nsIndication = words[4];

                        longDeg = Convert.ToInt32(words[5].Substring(0, 3));
                        longMin = Convert.ToInt32(words[5].Substring(3, 2));
                        longMinDecimal = Convert.ToInt32(words[5].Substring(6, 4));

                        ewIndication = words[6];

                        speedKnotsPH = Convert.ToSingle(words[7]);
                        heading = Convert.ToSingle(words[8]);
                        dateStr = words[9];
                    }
                    #endregion
                    #region PARSE VTG - Course and speed information relative to the ground.
                    else if ((dataSentence.StartsWith("$GPVTG")) || // GPS
                             (dataSentence.StartsWith("$GNVTG"))) // GNSS
                    {
                        heading = Convert.ToSingle(words[1]);
                        speedKnotsPH = Convert.ToSingle(words[5]);
                        speedKmPH = Convert.ToSingle(words[7]);
                    }
                    #endregion
                    #region SKIP: GSA - GNSS receiver operating mode, active satellites used in the position solution and DOP values.
                    else if ((dataSentence.StartsWith("$GPGSA")) || // GPS, GNSS
                             (dataSentence.StartsWith("$GLGSA"))) // GPS+GLONASS
                    {

                    }
                    #endregion
                    #region SKIP: GSV - The number of GPS satellites in view satellite ID numbers, elevation, azimuth, and SNR values.
                    else if ((dataSentence.StartsWith("$GPGSV")) || // GPS, GNSS
                             (dataSentence.StartsWith("$GLGSV"))) // GPS + GLONASS
                    {

                    }
                    #endregion
                    #region SKIP unknown/deformed sentances - ignore
                    else
                    { }
                    #endregion
                }
                catch
                { }
            }
            if(latDeg == -1)
            {
                rVal = false;
            }
            else
            { 
                rVal = true;
                if (dateStr.Length < 6)
                {
                    date = "xx/xx/xxxx";
                }
                else
                {
                    date = dateStr.Substring(2, 2) + "/" + dateStr.Substring(0, 2) + "/20" + dateStr.Substring(4, 2);
                }
                hr = utcHour;
                min = utcMin;
                sec= (Single)utcSec + (Single)utcmSec/1000.0F;

                lat = (Single)latDeg + ((Single)latMin + ((Single)latMinDecimal / 10000.0F)) / 60.0F;
                ns = nsIndication;
                lng = (Single)longDeg + ((Single)longMin + ((Single)longMinDecimal / 10000.0F)) / 60.0F;
                ew =ewIndication;
                hd = heading;
                if((speedKmPH == -1.0F) && (speedKnotsPH != -1.0F))
                {
                    speedKmPH = speedKnotsPH * 1.852F;
                }
                speed = speedKmPH;
                sat = satellites;
            }
            return rVal;
        }
        public void UpdateData()
        {
            int channelIdx = 0;
            int runIdx = 0;
            #region update display info
            foreach (RunData curRun in dataLogger.runData)
            {
                runDisplay.Add(new RunDisplay_Data());
                runIdx = runDisplay.Count - 1;
                runDisplay[runIdx].runColor = penColors[runIdx % penColors.Count()];
                runDisplay[runIdx].stipchart_Offset[0] = 0.0F;
                runDisplay[runIdx].stipchart_Offset[1] = 0.0F;

                trackMapLastCursorPos.Add(new Point(0, 0));
                trackMapLastCursorPosValid.Add(false);
                tractionCircleLastCursorPos.Add(new Point(0, 0));
                tractionCircleLastCursorPosValid.Add(false);

                channelIdx = 0;
                foreach (KeyValuePair<String, DataChannel> curChannel in curRun.channels)
                {
                    globalDisplay.AddChannel(curChannel.Key);
                    globalDisplay.channelRanges[curChannel.Key][0] = curChannel.Value.ChannelMin<globalDisplay.channelRanges[curChannel.Key][0] ? curChannel.Value.ChannelMin : globalDisplay.channelRanges[curChannel.Key][0];
                    globalDisplay.channelRanges[curChannel.Key][1] = curChannel.Value.ChannelMax > globalDisplay.channelRanges[curChannel.Key][1] ? curChannel.Value.ChannelMax : globalDisplay.channelRanges[curChannel.Key][1];
                    runDisplay[runIdx].channelDisplay.Add(curChannel.Key, false);
                    runDisplay[runIdx].channelColor.Add(curChannel.Key, penColors[channelIdx % penColors.Count()]);
                    channelIdx++;
                }
            }
            foreach (KeyValuePair<String, float[]> kvp in globalDisplay.channelRanges)
            {

                if(!cmbXAxis.Items.Contains(kvp.Key))
                {
                    cmbXAxis.Items.Add(kvp.Key);
                }
            }
            cmbXAxis.SelectedIndex = 0;
            #endregion


            // auto align launches
            AutoAlign(0.10F);

            channelDataGrid.Rows.Clear();
            for (int runGridIdx = 0; runGridIdx < dataLogger.runData.Count; runGridIdx++)
            {
                foreach (KeyValuePair<String, DataChannel> kvp in dataLogger.runData[runGridIdx].channels)
                {
                    channelDataGrid.Rows.Add();
                    channelDataGrid.Rows[channelDataGrid.Rows.Count - 1].Cells["displayChannel"].Value = runDisplay[runGridIdx].channelDisplay[kvp.Key];
                    channelDataGrid.Rows[channelDataGrid.Rows.Count - 1].Cells["channelColor"].Style.BackColor = runDisplay[runGridIdx].channelColor[kvp.Key];
                    channelDataGrid.Rows[channelDataGrid.Rows.Count - 1].Cells["runName"].Value = (runGridIdx + 1).ToString();
                    channelDataGrid.Rows[channelDataGrid.Rows.Count - 1].Cells["channelName"].Value = kvp.Key.ToString();
                }
            }
            runDataGrid.Rows.Clear();
            for (int runGridIdx = 0; runGridIdx < dataLogger.runData.Count(); runGridIdx++)
            {
                runDataGrid.Rows.Add();
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colRunNumber"].Value = (runGridIdx + 1).ToString();
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colShowRun"].Value = runDisplay[runGridIdx].showRun;
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colDate"].Value = dataLogger.runData[runGridIdx].dateStr + " " + dataLogger.runData[runGridIdx].timeStr;
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colMinTime"].Value = dataLogger.runData[runGridIdx].channels["Time"].ChannelMin;
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colMaxTime"].Value = dataLogger.runData[runGridIdx].channels["Time"].ChannelMax;
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colOffsetTime"].Value = runDisplay[runGridIdx].stipchart_Offset[0];
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colSourceFile"].Value = dataLogger.runData[runGridIdx].fileName.ToString();
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colTraceColor"].Style.BackColor = runDisplay[runDataGrid.Rows.Count - 1].runColor;
            }
            foreach (RunData curRun in dataLogger.runData)
            {
                foreach (KeyValuePair<String, DataChannel> curChan in curRun.channels)
                {
                    System.Diagnostics.Debug.WriteLine(curChan.Key + " Range " +
                                                       curChan.Value.ChannelMin + " - " + curChan.Value.ChannelMax + " Scale " +
                                                       curChan.Value.ChannelScale + " Points" +
                                                       curChan.Value.DataPoints.Count);
                }
            }
        }

        #endregion
        /// <summary>
        /// estimate launch point offset from speed data
        /// find first speed > 30, walk back to first speed = 0
        /// </summary>
        private void AutoAlign(float launchThreshold)
        {
            //int runCount = 0;
            //float baseLaunch = 0.0F;
            //int launchIdx = 0;
            //foreach (RunData curRun in dataLogger.runData)
            //{
            //    if(dataLogger[runCount].minMaxSpeed[1] < 20.0F)
            //    {
            //        runDisplay[runCount].showRun = false;
            //        runCount++;
            //        continue;
            //    }
            //    foreach (KeyValuePair<float, DataBlock> curBlock in curPath)
            //    {
            //        if (!curBlock.Value.gps.isValid)
            //        {
            //            continue;
            //        }
            //        if(curBlock.Value.gps.mph < 30.0F)
            //        {
            //            continue;
            //        }
            //        // found point after launch, walk back to speed 0
            //        launchIdx = curPath.IndexOfKey(curBlock.Value.micros);
            //        while(launchIdx >= 0)
            //        {
            //            // not valid gps, no speed - continue
            //            if(!curPath.ElementAt(launchIdx).Value.gps.isValid)
            //            {
            //                launchIdx--;
            //                continue;
            //            }
            //            // speed - 0 = this is last point prior to launch
            //            if(curPath.ElementAt(launchIdx).Value.gps.mph <= launchThreshold)
            //            {
            //                if(runCount == 0)
            //                {
            //                    baseLaunch = curPath.ElementAt(launchIdx).Value.micros;
            //                }
            //                runDisplay[runCount].stipchart_Offset[0] = baseLaunch - curPath.ElementAt(launchIdx).Value.micros;
            //                break;
            //            }
            //            launchIdx--;
            //        }
            //    }
            //    runCount++;
            //}
        }
        public void DrawMap()
        {
            mapPanel.Invalidate();
        }
        public void DrawTraction()
        {
            tractionCirclePanel.Invalidate();
        }
        public void DrawSpeed()
        {
            stripChartPanel.Invalidate();
        }
        #region track map events
        private void trackMap_Paint(object sender, PaintEventArgs e)
        {
            for (int validIdx = 0; validIdx < trackMapLastCursorPosValid.Count(); validIdx++)
            {
                trackMapLastCursorPosValid[validIdx] = false;
            }

            // nothing to display yet, or not acceleration data
            if ((globalDisplay.channelRanges.Count == 0) ||
                (!globalDisplay.channelRanges.ContainsKey("Latitude")) ||
                (!globalDisplay.channelRanges.ContainsKey("Longitude")))
            {
                return;
            }
            Pen drawPen = new Pen(Color.Black, 1);
            PointF startPt = new PointF(0.0F, 0.0F);
            PointF endPt = new PointF(0.0F, 0.0F);
            bool initialValue = false;
            float scaleX = (float)trackMapBounds.Width / (float)Math.Abs(globalDisplay.channelRanges["Longitude"][1] - globalDisplay.channelRanges["Longitude"][0]);
            float scaleY = (float)trackMapBounds.Height / (float)Math.Abs(globalDisplay.channelRanges["Latitude"][1] - globalDisplay.channelRanges["Latitude"][0]);
            trackMapScale = scaleX < scaleY ? scaleX : scaleY;
            float longVal = 0.0F;
            float startTime = 0.0F;
            float endTime = 0.0F;
            int runCount = 0;

            using (Graphics mapGraphics = mapPanel.CreateGraphics())
            {

                foreach (RunData curRun in dataLogger.runData)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    drawPen = new Pen(runDisplay[runCount].runColor);
                    initialValue = false;
                    foreach (KeyValuePair<float, DataPoint> curData in curRun.channels["Longitude"].DataPoints)
                    {
                        endPt.X = curData.Value.PointValue - globalDisplay.channelRanges["Longitude"][0];
                        longVal = curRun.channels["Latitude"].dataPoints[curData.Key].PointValue;
                        endPt.Y = longVal - globalDisplay.channelRanges["Latitude"][0];

                        endPt = globalDisplay.ScaleDataToDisplay(endPt,
                                                                 trackMapScale * trackMapScaleFactor,
                                                                 trackMapScale * trackMapScaleFactor,
                                                                 trackMapOffset[0],
                                                                 trackMapOffset[1],
                                                                 trackMapBounds);


                        endTime = curData.Key;

                        if (stripChartExtentsX[0] != stripChartExtentsX[1])
                        {
                            if (endTime < stripChartExtentsX[0])
                            {
                                startPt.X = endPt.X;
                                startPt.Y = endPt.Y;
                                startTime = endTime;
                                continue;
                            }
                            if (startTime > stripChartExtentsX[1])
                            {
                                break;
                            }
                        }
                        if (initialValue)
                        {
                            mapGraphics.DrawLine(drawPen, startPt, endPt);
                        }
                        startPt.X = endPt.X;
                        startPt.Y = endPt.Y;
                        startTime = endTime;
                        initialValue = true;
                    }
                    runCount++;
                }
            }
        }
        private void trackMap_MouseMove(object sender, MouseEventArgs e)
        {
            mapPanel.Focus();
            if (e.Button == MouseButtons.Left)
            {
                if (!trackMapStartPosValid)
                {
                    trackMapStartPosValid = true;
                    trackMapStartPos = e.Location;
                }
            }
            trackMapLastPos = e.Location;
        }
        private void trackMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (trackMapStartPosValid)
            {
                trackMapOffset[0] += ((float)(e.Location.X - trackMapStartPos.X) / trackMapScale);
                trackMapOffset[1] += ((float)(e.Location.Y - trackMapStartPos.Y) / trackMapScale);
            }
            trackMapStartPosValid = false;
            mapPanel.Invalidate();
        }
        private void trackMap_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '+')
            {
                trackMapScaleFactor *= 1.05F;
                mapPanel.Invalidate();
            }
            else if (e.KeyChar == '-')
            {
                trackMapScaleFactor *= 0.95F;
                mapPanel.Invalidate();
            }
            else if ((e.KeyChar == '1') || (e.KeyChar == 'R') || (e.KeyChar == 'r'))
            {
                trackMapScaleFactor = 1.0F;
                trackMapOffset[0] = 0;
                trackMapOffset[1] = 0;
                mapPanel.Invalidate();
            }
        }
        private void trackMap_Resize(object sender, EventArgs e)
        {
            trackMapBounds.X = 5;
            trackMapBounds.Y = 5;
            trackMapBounds.Width = mapPanel.Width - 10;
            trackMapBounds.Height = mapPanel.Height - 10;
            mapPanel.Invalidate();
        }
        private void trackMap_Layout(object sender, LayoutEventArgs e)
        {
            trackMapBounds.X = trackMapBorder;
            trackMapBounds.Y = trackMapBorder;
            trackMapBounds.Width = mapPanel.Width - (2 * trackMapBorder);
            trackMapBounds.Height = mapPanel.Height - (2 * trackMapBorder);
        }
        private void TrackMapUpdateCursor(float xAxisValue)
        {
            Pen drawPen = new Pen(Color.Black, 1);
            PointF locationPt = new PointF();
            Rectangle locationBox = new Rectangle();
            int runCount = 0;
            float runTimeStamp = 0.0F;
            using (Graphics mapGraphics = mapPanel.CreateGraphics())
            {
                foreach (RunData curRun in dataLogger.runData)
                {
                    if(!curRun.channels.ContainsKey("Longitude"))
                    {
                        continue;
                    }
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    drawPen = new Pen(runDisplay[runCount].runColor);
                    runTimeStamp = xAxisValue - runDisplay[runCount].stipchart_Offset[0];

                    KeyValuePair<float, DataPoint> kvp = curRun.channels["Longitude"].DataPoints.FirstOrDefault(i => i.Key > runTimeStamp);
                    DataPoint curPoint = kvp.Value;
                    runTimeStamp = kvp.Key;
                    if (curPoint == null)
                    {
                        continue;
                    }
                    locationPt.X = curPoint.PointValue - globalDisplay.channelRanges["Longitude"][0];
                    locationPt.Y = curRun.channels["Latitude"].DataPoints[runTimeStamp].PointValue - globalDisplay.channelRanges["Latitude"][0];

                    locationPt = globalDisplay.ScaleDataToDisplay(locationPt,
                                                                  trackMapScale * trackMapScaleFactor,
                                                                  trackMapScale * trackMapScaleFactor,
                                                                  trackMapOffset[0],
                                                                  trackMapOffset[1],
                                                                  trackMapBounds);

                    #region position cursor
                    IntPtr hDC = mapGraphics.GetHdc();

                    IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                    dragZoomPenWidth,
                                                    NotRGB(runDataGrid.Rows[runCount].Cells["colTraceColor"].Style.BackColor));
                    IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.NULL_BRUSH);
                    IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                    IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                    Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);

                    IntPtr lpPoint = new IntPtr();
                    lpPoint = IntPtr.Zero;

                    if (trackMapLastCursorPosValid[runCount])
                    {
                        locationBox = new Rectangle(trackMapLastCursorPos[runCount].X, trackMapLastCursorPos[runCount].Y, (int)trackMapCursorSize, (int)trackMapCursorSize);
                        Gdi32.Rectangle(hDC, locationBox.Left, locationBox.Top, locationBox.Left + (int)trackMapCursorSize, locationBox.Top + (int)trackMapCursorSize);
                    }

                    locationBox = new Rectangle((int)(locationPt.X),
                                                (int)(locationPt.Y),
                                                (int)(trackMapCursorSize),
                                                (int)(trackMapCursorSize));


                    Gdi32.Rectangle(hDC, locationBox.Left, locationBox.Top, locationBox.Left + (int)trackMapCursorSize, locationBox.Top + (int)trackMapCursorSize);
                    trackMapLastCursorPos[runCount] = new Point(locationBox.Left, locationBox.Top);
                    trackMapLastCursorPosValid[runCount] = true;

                    Gdi32.SelectObject(hDC, oldPen);
                    Gdi32.SelectObject(hDC, oldBrush);
                    Gdi32.DeleteObject(newPen);
                    Gdi32.DeleteObject(newBrush);
                    mapGraphics.ReleaseHdc();
                    #endregion
                    runCount++;
                }
            }
        }
        private void TrackMapClearCursor()
        {
            using (Graphics mapGraphics = mapPanel.CreateGraphics())
            {
                int runCount = 0;
                Rectangle locationBox = new Rectangle(0, 0, 0, 0);
                foreach (RunData curRun in dataLogger.runData)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    #region position cursor
                    IntPtr hDC = mapGraphics.GetHdc();

                    IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                    dragZoomPenWidth,
                                                    NotRGB(runDataGrid.Rows[runCount].Cells["colTraceColor"].Style.BackColor));
                    IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.NULL_BRUSH);
                    IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                    IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                    Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);

                    IntPtr lpPoint = new IntPtr();
                    lpPoint = IntPtr.Zero;

                    locationBox = new Rectangle(trackMapLastCursorPos[runCount].X, trackMapLastCursorPos[runCount].Y, (int)trackMapCursorSize, (int)trackMapCursorSize);
                    Gdi32.Rectangle(hDC, locationBox.Left, locationBox.Top, locationBox.Left + (int)trackMapCursorSize, locationBox.Top + (int)trackMapCursorSize);

                    trackMapLastCursorPosValid[runCount] = false;
                    Gdi32.SelectObject(hDC, oldPen);
                    Gdi32.SelectObject(hDC, oldBrush);
                    Gdi32.DeleteObject(newPen);
                    Gdi32.DeleteObject(newBrush);
                    mapGraphics.ReleaseHdc();
                    #endregion

                    runCount++;
                }
            }
        }
        #endregion
        #region traction circle events
        private void tractionCircle_Paint(object sender, PaintEventArgs e)
        {
            tractionCircleStartPosValid = false;
            for (int validIdx = 0; validIdx < trackMapLastCursorPosValid.Count(); validIdx++)
            {
                trackMapLastCursorPosValid[validIdx] = false;
            }
            // nothing to display yet, or not acceleration data
            if ((globalDisplay.channelRanges.Count == 0) ||
                (!globalDisplay.channelRanges.ContainsKey("gX")) ||
                (!globalDisplay.channelRanges.ContainsKey("gY")))
            {
                return;
            }
            Pen drawPen = new Pen(Color.Black, 1);
            PointF startPt = new PointF();
            PointF endPt = new PointF();
            bool initialValue = false;
            float scaleX = (float)tractionCircleBounds.Width / (float)Math.Abs(globalDisplay.channelRanges["gX"][1] - globalDisplay.channelRanges["gX"][0]);
            float scaleY = (float)tractionCircleBounds.Height / (float)Math.Abs(globalDisplay.channelRanges["gY"][1] - globalDisplay.channelRanges["gY"][0]);
            tractionCircleScale = scaleX < scaleY ? scaleX : scaleY;
            float startTime = 0.0F;
            float endTime = 0.0F;
            int runCount = 0;

            using (Graphics tractionCircleGraphics = tractionCirclePanel.CreateGraphics())
            {
                foreach (RunData curRun in dataLogger.runData)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    drawPen = new Pen(runDisplay[runCount].runColor);
                    initialValue = false;
                    foreach (KeyValuePair<float, DataPoint> curData in curRun.channels["gX"].DataPoints)
                    {
                        endPt.X = curData.Value.PointValue - globalDisplay.channelRanges["gX"][0];
                        endPt.Y = curRun.channels["gY"].dataPoints[curData.Key].PointValue - globalDisplay.channelRanges["gY"][0];
                        endPt = globalDisplay.ScaleDataToDisplay(endPt,
                                                                 tractionCircleScale * tractionCircleScaleFactor,
                                                                 tractionCircleScale * tractionCircleScaleFactor,
                                                                 tractionCircleOffset[0],
                                                                 tractionCircleOffset[1],
                                                                 tractionCircleBounds);
                        endTime = (float)curData.Key;
                        if (stripChartExtentsX[0] != stripChartExtentsX[1])
                        {
                            if (endTime < stripChartExtentsX[0])
                            {
                                continue;
                            }
                            if (startTime > stripChartExtentsX[1])
                            {
                                break;
                            }
                        }
                        if (initialValue)
                        {
                            tractionCircleGraphics.DrawLine(drawPen, startPt, endPt);
                        }
                        startPt = endPt;
                        startTime = endTime;
                        initialValue = true;
                    }
                    runCount++;
                }
            }
        }
        private void tractionCircle_MouseMove(object sender, MouseEventArgs e)
        {
            tractionCirclePanel.Focus();
            if (e.Button == MouseButtons.Left)
            {
                if (!tractionCircleStartPosValid)
                {
                    tractionCircleStartPosValid = true;
                    tractionCircleStartPos = e.Location;
                }
            }
            tractionCircleLastPos = e.Location;
        }
        private void tractionCircle_MouseUp(object sender, MouseEventArgs e)
        {
            if (tractionCircleStartPosValid)
            {
                tractionCircleOffset[0] += ((float)(e.Location.X - tractionCircleStartPos.X) / tractionCircleScale);
                tractionCircleOffset[1] += ((float)(e.Location.Y - tractionCircleStartPos.Y) / tractionCircleScale);
            }
            tractionCircleStartPosValid = false;
            tractionCirclePanel.Invalidate();
        }
        private void tractionCircle_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '+')
            {
                tractionCircleScaleFactor *= 1.05F;
                tractionCirclePanel.Invalidate();
            }
            else if (e.KeyChar == '-')
            {
                tractionCircleScaleFactor *= 0.95F;
                tractionCirclePanel.Invalidate();
            }
            else if ((e.KeyChar == '1') || (e.KeyChar == 'R') || (e.KeyChar == 'r'))
            {
                tractionCircleScaleFactor = 1.0F;
                tractionCircleOffset[0] = 0;
                tractionCircleOffset[1] = 0;
                tractionCirclePanel.Invalidate();
            }
        }
        private void tractionCircle_Resize(object sender, EventArgs e)
        {
            tractionCircleBounds.X = tractionCircleBorder;
            tractionCircleBounds.Y = tractionCircleBorder;
            tractionCircleBounds.Width = tractionCirclePanel.Width - (2 * tractionCircleBorder);
            tractionCircleBounds.Height = tractionCirclePanel.Height - (2 * tractionCircleBorder);
            tractionCirclePanel.Invalidate();
        }
        private void tractionCircle_Layout(object sender, LayoutEventArgs e)
        {
            tractionCircleBounds.X = tractionCircleBorder;
            tractionCircleBounds.Y = tractionCircleBorder;
            tractionCircleBounds.Width = tractionCirclePanel.Width - (2 * tractionCircleBorder);
            tractionCircleBounds.Height = tractionCirclePanel.Height - (2 * tractionCircleBorder);
        }
        private void TractionCircleUpdateCursor(float xAxisValue)
        {
            Pen drawPen = new Pen(Color.Black, 1);
            PointF locationPt = new PointF();
            Rectangle locationBox = new Rectangle();
            int runCount = 0;
            float runTimeStamp = 0.0F;
            using (Graphics mapGraphics = tractionCirclePanel.CreateGraphics())
            {
                foreach (RunData curRun in dataLogger.runData)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    drawPen = new Pen(runDisplay[runCount].runColor);

                    runTimeStamp = xAxisValue - runDisplay[runCount].stipchart_Offset[0];


                    KeyValuePair<float, DataPoint> kvp = curRun.channels["gX"].DataPoints.FirstOrDefault(i => i.Key > runTimeStamp);
                    DataPoint curPoint = kvp.Value;
                    runTimeStamp = kvp.Key;
                    if (curPoint == null)
                    {
                        continue;
                    }
                    locationPt.X = curPoint.PointValue - globalDisplay.channelRanges["gX"][0];
                    locationPt.Y = curRun.channels["gY"].DataPoints[runTimeStamp].PointValue - globalDisplay.channelRanges["gY"][0];

                    locationPt = globalDisplay.ScaleDataToDisplay(locationPt,
                                                                  tractionCircleScale * tractionCircleScaleFactor,
                                                                  tractionCircleScale * tractionCircleScaleFactor,
                                                                  tractionCircleOffset[0],
                                                                  tractionCircleOffset[1],
                                                                  tractionCircleBounds);

                    #region position cursor
                    IntPtr hDC = mapGraphics.GetHdc();

                    IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                    dragZoomPenWidth,
                                                    NotRGB(runDataGrid.Rows[runCount].Cells["colTraceColor"].Style.BackColor));
                    IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.NULL_BRUSH);
                    IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                    IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                    Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);

                    IntPtr lpPoint = new IntPtr();
                    lpPoint = IntPtr.Zero;

                    if (tractionCircleLastCursorPosValid[runCount])
                    {
                        locationBox = new Rectangle(tractionCircleLastCursorPos[runCount].X, tractionCircleLastCursorPos[runCount].Y, (int)tractionCircleCursorSize, (int)tractionCircleCursorSize);
                        Gdi32.Rectangle(hDC, locationBox.Left, locationBox.Top, locationBox.Left + (int)tractionCircleCursorSize, locationBox.Top + (int)tractionCircleCursorSize);
                    }

                    locationBox = new Rectangle((int)(locationPt.X),
                                                (int)(locationPt.Y),
                                                (int)(tractionCircleCursorSize),
                                                (int)(tractionCircleCursorSize));


                    Gdi32.Rectangle(hDC, locationBox.Left, locationBox.Top, locationBox.Left + (int)tractionCircleCursorSize, locationBox.Top + (int)tractionCircleCursorSize);
                    tractionCircleLastCursorPos[runCount] = new Point(locationBox.Left, locationBox.Top);
                    tractionCircleLastCursorPosValid[runCount] = true;

                    Gdi32.SelectObject(hDC, oldPen);
                    Gdi32.SelectObject(hDC, oldBrush);
                    Gdi32.DeleteObject(newPen);
                    Gdi32.DeleteObject(newBrush);
                    mapGraphics.ReleaseHdc();
                    #endregion

                    runCount++;
                }
            }
        }
        private void TractionCircleClearCursor()
        {
            using (Graphics mapGraphics = tractionCirclePanel.CreateGraphics())
            {
                int runCount = 0;
                Rectangle locationBox = new Rectangle(0, 0, 0, 0);
                foreach (RunData curRun in dataLogger.runData)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    #region position cursor
                    IntPtr hDC = mapGraphics.GetHdc();

                    IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                    dragZoomPenWidth,
                                                    NotRGB(runDataGrid.Rows[runCount].Cells["colTraceColor"].Style.BackColor));
                    IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.NULL_BRUSH);
                    IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                    IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                    Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);

                    IntPtr lpPoint = new IntPtr();
                    lpPoint = IntPtr.Zero;

                    locationBox = new Rectangle(tractionCircleLastCursorPos[runCount].X, tractionCircleLastCursorPos[runCount].Y, (int)tractionCircleCursorSize, (int)tractionCircleCursorSize);
                    Gdi32.Rectangle(hDC, locationBox.Left, locationBox.Top, locationBox.Left + (int)tractionCircleCursorSize, locationBox.Top + (int)tractionCircleCursorSize);

                    tractionCircleLastCursorPosValid[runCount] = false;
                    Gdi32.SelectObject(hDC, oldPen);
                    Gdi32.SelectObject(hDC, oldBrush);
                    Gdi32.DeleteObject(newPen);
                    Gdi32.DeleteObject(newBrush);
                    mapGraphics.ReleaseHdc();
                    #endregion

                    runCount++;
                }
            }
        }
        #endregion
        #region stripchart events
        private void stripChart_Paint(object sender, PaintEventArgs e)
        {
            // nothing to display yet
            if (globalDisplay.channelRanges.Count == 0)
            {
                return;
            }
            int channelCount = 0;
            #region get count of displayed channels
            for (int rowIdx = 0; rowIdx < channelDataGrid.Rows.Count; rowIdx++)
            {
                if ((bool)channelDataGrid.Rows[rowIdx].Cells["displayChannel"].Value == true)
                {
                    channelCount++;
                }
            }
            if (channelCount == 0)
            {
                return;
            }
            #endregion
            stripChartStartPosValid = false;
            stripChartLastCursorPosValid = false;

            // scale for each active trace
            stripChartScaleY.Clear();


            PointF startPt = new PointF();
            PointF endPt = new PointF();
            Pen drawPen = new Pen(Color.Black, 1);
            bool initialValue = false;
            int runCount = 0;
            xChannelName = cmbXAxis.Text;
            using (Graphics mapGraphics = stripChartPanel.CreateGraphics())
            {
                if (stripChartScaleX == 0.0F)
                {
                    stripChartScaleX = ((float)(stripChartPanel.Width - (stripChartPanelBorder * 2)) / (float)Math.Abs(globalDisplay.channelRanges[xChannelName][1] - globalDisplay.channelRanges[xChannelName][0]));
                    stripChartExtentsX[0] = globalDisplay.channelRanges[xChannelName][0] - stripChartOffset[0];
                    stripChartExtentsX[1] = globalDisplay.channelRanges[xChannelName][1] + stripChartOffset[0];
                }
                runCount = 0;
                stripChartScaleY.Clear();

                foreach (RunData curRun in dataLogger.runData)
                {
                    foreach(KeyValuePair<String,DataChannel> curChan in curRun.channels)
                    {
                        yChannelName = curChan.Key;
                        // if not displayed, skip
                        if (!runDisplay[runCount].channelDisplay[yChannelName])
                        {
                            continue;
                        }
                        if (!stripChartScaleY.ContainsKey(yChannelName))
                        {
                            stripChartScaleY.Add(yChannelName, (float)(stripChartPanel.Height - (stripChartPanelBorder * 2)) / (float)Math.Abs(globalDisplay.channelRanges[yChannelName][1] - globalDisplay.channelRanges[yChannelName][0]));
                        }
                        drawPen = new Pen(runDisplay[runCount].channelColor[yChannelName]);

                        initialValue = false;
                        foreach (KeyValuePair<float, DataPoint> curData in curRun.channels[yChannelName].DataPoints)
                        {
                            // x axis is time
                            if (xChannelName == "Time")
                            {
                                endPt.X = curData.Key - (float)globalDisplay.channelRanges[xChannelName][0] + runDisplay[runCount].stipchart_Offset[0];
                                endPt.Y = curData.Value.PointValue - globalDisplay.channelRanges[yChannelName][0];
                            }
                            else
                            {
                                DataPoint tst = curRun.channels[xChannelName].dataPoints.FirstOrDefault(i => i.Key >= curData.Key).Value;
                                endPt.X = tst.PointValue - globalDisplay.channelRanges[xChannelName][0] + runDisplay[runCount].stipchart_Offset[0];// curRun.channels[xChannelName].dataPoints.FirstOrDefault(i => i.Key > curData.Key).Value.PointValue;// curData.Value.PointValue - (float)globalDisplay.channelRanges[xChannelName][0];
                                endPt.Y = curData.Value.PointValue - globalDisplay.channelRanges[yChannelName][0];
                            }
                            //
                            // at this point, the value is already offset on the X axis by any user defined time shift
                            // just need to offset by the stripchart panned position (H scrollbar)
                            //
                            endPt = globalDisplay.ScaleDataToDisplay(endPt,                                                                // point
                                                                     stripChartScaleX * stripChartScaleFactorX,                            // scale X
                                                                     stripChartScaleY[yChannelName] * stripChartScaleFactorY,              // scale Y
                                                                     stripChartOffset[0],                                                                    // offset X
                                                                     0,                                                                    // offset Y
                                                                     stripChartPanelBounds);
                            if ((initialValue) && (startPt.X < stripChartPanelBounds.Width) && (endPt.X > 0))
                            {
                                mapGraphics.DrawLine(drawPen, startPt, endPt);
                            }
                            startPt.X = endPt.X;
                            startPt.Y = endPt.Y;
                            initialValue = true;
                        }
                    }
                    runCount++;
                }
            }
        }
        private void stripChart_MouseMove(object sender, MouseEventArgs e)
        {
            //return;
            stripChartPanel.Focus();
            PointF floatPosition = new PointF(e.X, e.Y);
            // floatPosition should be offset by the stripchart hScroll position....
            floatPosition = globalDisplay.ScaleDisplayToData(floatPosition,
                                             stripChartScaleX * stripChartScaleFactorX,   // X scale
                                             1,                                           // Y scale
                                             stripChartOffset[0],                         // X offset
                                             stripChartOffset[1],                         // Y offset
                                             stripChartPanelBounds);

            #region left mouse button down - dragging zoom region 
            IntPtr lpPoint = new IntPtr();
            if (e.Button == MouseButtons.Left)
            {
                if (!stripChartStartPosValid)
                {
                    #region erase existing cursor lines
                    using (Graphics drawGraphics = stripChartPanel.CreateGraphics())
                    {
                        IntPtr hDC = drawGraphics.GetHdc();
                        IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                        dragZoomPenWidth,
                                                        NotRGB(Color.Gray));
                        IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.NULL_BRUSH);
                        IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                        IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                        Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);

                        Gdi32.MoveToEx(hDC, stripChartLastCursorPosInt.X, 0, lpPoint);
                        Gdi32.LineTo(hDC, stripChartLastCursorPosInt.X, stripChartPanel.Height);

                        Gdi32.MoveToEx(hDC, 0, stripChartLastCursorPosInt.Y, lpPoint);
                        Gdi32.LineTo(hDC, stripChartPanel.Width, stripChartLastCursorPosInt.Y);

                        Gdi32.SelectObject(hDC, oldPen);
                        Gdi32.SelectObject(hDC, oldBrush);
                        Gdi32.DeleteObject(newPen);
                        Gdi32.DeleteObject(newBrush);
                        drawGraphics.ReleaseHdc();
                    }
                    #endregion
                    stripChartStartPosValid = true;
                    stripChartStartPos = e.Location;
                    stripChartStartCursorPos = floatPosition;
                    stripChartStartCursorPosInt = e.Location;
                }
                #region zoom box - light gray filled
                using (Graphics drawGraphics = stripChartPanel.CreateGraphics())
                {
                    IntPtr hDC = drawGraphics.GetHdc();

                    IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                    dragZoomPenWidth,
                                                    NotRGB(Color.Gray));
                    IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.LTGRAY_BRUSH);
                    IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                    IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                    Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);

                    lpPoint = IntPtr.Zero;
                    // only erase if something was drawn and cursor was moved
                    if ((stripChartLastCursorPosValid) &&
                        ((e.Location.X != stripChartLastCursorPosInt.X) || (e.Location.Y != stripChartLastCursorPosInt.Y)))
                    {
                        //Gdi32.MoveToEx(hDC, stripChartLastCursorPosInt.X, 0, lpPoint);
                        //Gdi32.LineTo(hDC, stripChartLastCursorPosInt.X, stripChartPanel.Height);

                        //Gdi32.MoveToEx(hDC, 0, stripChartLastCursorPosInt.Y, lpPoint);
                        Gdi32.Rectangle(hDC, stripChartStartCursorPosInt.X, 0, stripChartLastCursorPosInt.X, stripChartPanel.Height);
                        //Gdi32.LineTo(hDC, stripChartPanel.Width, stripChartLastCursorPosInt.Y);
                    }
                    // only draw if nothing has been drawn or cursor was moved
                    if ((!stripChartLastCursorPosValid) ||
                        ((e.Location.X != stripChartLastCursorPosInt.X) || (e.Location.Y != stripChartLastCursorPosInt.Y)))
                    {
                        //Gdi32.MoveToEx(hDC, e.Location.X, 0, lpPoint);
                        //Gdi32.LineTo(hDC, e.Location.X, stripChartPanel.Height);

                        //Gdi32.MoveToEx(hDC, 0, e.Location.Y, lpPoint);
                        //Gdi32.LineTo(hDC, stripChartPanel.Width, e.Location.Y);
                        Gdi32.Rectangle(hDC, stripChartStartCursorPosInt.X, 0, e.X, stripChartPanel.Height);

                    }

                    Gdi32.SelectObject(hDC, oldPen);
                    Gdi32.SelectObject(hDC, oldBrush);
                    Gdi32.DeleteObject(newPen);
                    Gdi32.DeleteObject(newBrush);
                    drawGraphics.ReleaseHdc();
                }
                #endregion
            }
            #endregion
            #region just moving the mouse
            else
            {
                #region vertical cursor
                using (Graphics drawGraphics = stripChartPanel.CreateGraphics())
                {
                    IntPtr hDC = drawGraphics.GetHdc();

                    IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                    dragZoomPenWidth,
                                                    NotRGB(Color.Gray));
                    IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.NULL_BRUSH);
                    IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                    IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                    Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);

                    lpPoint = IntPtr.Zero;
                    // only erase if something was drawn and cursor was moved
                    if ((stripChartLastCursorPosValid) && 
                        ((e.Location.X != stripChartLastCursorPosInt.X) || (e.Location.Y != stripChartLastCursorPosInt.Y)))
                    {
                        Gdi32.MoveToEx(hDC, stripChartLastCursorPosInt.X, 0, lpPoint);
                        Gdi32.LineTo(hDC, stripChartLastCursorPosInt.X, stripChartPanel.Height);

                        Gdi32.MoveToEx(hDC, 0, stripChartLastCursorPosInt.Y, lpPoint);
                        Gdi32.LineTo(hDC, stripChartPanel.Width, stripChartLastCursorPosInt.Y);
                    }
                    // only draw if nothing has been drawn or cursor was moved
                    if ((!stripChartLastCursorPosValid) || 
                        ((e.Location.X != stripChartLastCursorPosInt.X) || (e.Location.Y != stripChartLastCursorPosInt.Y)))
                    {
                        Gdi32.MoveToEx(hDC, e.Location.X, 0, lpPoint);
                        Gdi32.LineTo(hDC, e.Location.X, stripChartPanel.Height);

                        Gdi32.MoveToEx(hDC, 0, e.Location.Y, lpPoint);
                        Gdi32.LineTo(hDC, stripChartPanel.Width, e.Location.Y);
                    }

                    Gdi32.SelectObject(hDC, oldPen);
                    Gdi32.SelectObject(hDC, oldBrush);
                    Gdi32.DeleteObject(newPen);
                    Gdi32.DeleteObject(newBrush);
                    drawGraphics.ReleaseHdc();
                }
                #endregion
            }
            #endregion
            #region update position info text
            StringBuilder positionStr = new StringBuilder();
            positionStr.AppendFormat("{0}={1}", xChannelName, floatPosition.X.ToString());

            txtCursorPos.Text = positionStr.ToString();
            #endregion
            #region update cursor positions
            stripChartLastCursorPos = floatPosition;
            TrackMapUpdateCursor(floatPosition.X);
            TractionCircleUpdateCursor(floatPosition.X);
            stripChartLastCursorPosInt = e.Location;
            stripChartLastCursorPosValid = true;
            #endregion
        }
        private void stripChart_MouseUp(object sender, MouseEventArgs e)
        {
            if(stripChartStartPosValid)
            {
                // dragged zoom rectangle
                PointF scaledStart = new PointF(stripChartStartCursorPos.X, stripChartStartCursorPos.Y);
                PointF scaledEnd = new PointF(stripChartLastCursorPos.X, stripChartLastCursorPos.Y);

                stripChartOffset[0] = -1.0F *( scaledStart.X < scaledEnd.X ? scaledStart.X : scaledEnd.X);// (float)e.Location.X / (stripChartScaleX * stripChartScaleFactorX);
                stripChartExtentsX[0] = scaledStart.X < scaledEnd.X ? scaledStart.X : scaledEnd.X;
                stripChartExtentsX[1] = scaledStart.X < scaledEnd.X ? scaledEnd.X : scaledStart.X;

                stripChartScaleX = (float)stripChartPanelBounds.Width / Math.Abs(scaledEnd.X - scaledStart.X);

                stripChartScaleFactorX = 1.0F;

                // update associated views to show only current zoomed area
                stripChartStartPosValid = false;
                stripChartPanel.Invalidate();
                mapPanel.Invalidate();
                tractionCirclePanel.Invalidate();
            }
        }
        private void stripChart_MouseLeave(object sender, EventArgs e)
        {
            // if there was a cursor, erase it prior to leaving
            // also clear cursor marks in map and traction circle
            if(stripChartLastCursorPosValid)
            {
                #region vertical cursor
                using (Graphics drawGraphics = stripChartPanel.CreateGraphics())
                {
                    IntPtr hDC = drawGraphics.GetHdc();

                    IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                    dragZoomPenWidth,// ScalePenWidth(dragZoomPenWidth, subSystems[currentSubsystem].scale),
                                                    NotRGB(Color.Gray));
                    IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.NULL_BRUSH);
                    IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                    IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                    Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);

                    IntPtr lpPoint = new IntPtr();
                    lpPoint = IntPtr.Zero;

                    Gdi32.MoveToEx(hDC, stripChartLastCursorPosInt.X, 0, lpPoint);
                    Gdi32.LineTo(hDC, stripChartLastCursorPosInt.X, stripChartPanel.Height);

                    Gdi32.SelectObject(hDC, oldPen);
                    Gdi32.SelectObject(hDC, oldBrush);
                    Gdi32.DeleteObject(newPen);
                    Gdi32.DeleteObject(newBrush);
                    drawGraphics.ReleaseHdc();
                }
                #endregion
                TrackMapClearCursor();
                TractionCircleClearCursor();
            }
            stripChartLastCursorPosValid = false;
        }
        private void stripChart_KeyDown(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '1') || (e.KeyChar == 'R') || (e.KeyChar == 'r'))
            {
                stripChartScaleFactorX = 1.0F;
                stripChartScaleX = (float)stripChartPanelBounds.Width / (globalDisplay.channelRanges["Time"][1] - globalDisplay.channelRanges["Time"][0]);
                stripChartOffset[0] = -1 * globalDisplay.channelRanges[xChannelName][0];
                stripChartOffset[1] = 0.0F;
                stripChartExtentsX[0] = globalDisplay.channelRanges[xChannelName][0];
                stripChartExtentsX[1] = globalDisplay.channelRanges[xChannelName][1]; 

                stripChartPanel.Invalidate();
                tractionCirclePanel.Invalidate();
                mapPanel.Invalidate();
            }
        }
        private void stripChart_Resize(object sender, EventArgs e)
        {
            stripChartPanelBounds.X = stripChartPanelBorder;
            stripChartPanelBounds.Y = stripChartPanelBorder;
            stripChartPanelBounds.Width = stripChartPanel.Width - (2 * stripChartPanelBorder);
            stripChartPanelBounds.Height = stripChartPanel.Height - (2 * stripChartPanelBorder);
            stripChartPanel.Invalidate();
        }
        private void stripChartPanel_Layout(object sender, LayoutEventArgs e)
        {
            stripChartPanelBounds.X = stripChartPanelBorder;
            stripChartPanelBounds.Y = stripChartPanelBorder;
            stripChartPanelBounds.Width = stripChartPanel.Width - (2 * stripChartPanelBorder);
            stripChartPanelBounds.Height = stripChartPanel.Height - (2 * stripChartPanelBorder);
        }
        private void stripchartHScroll_Scroll(object sender, ScrollEventArgs e)
        {
            stripChartOffset[0] -= ((float)(e.NewValue - e.OldValue)/(float)stripchartHScroll.Width) * (globalDisplay.channelRanges[xChannelName][1] - globalDisplay.channelRanges[xChannelName][0]);
            stripChartPanel.Invalidate();
        }
        #endregion
        #region data grid events
        private void RunDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not on button cells. 
            if (e.RowIndex >= 0 && e.ColumnIndex == runDataGrid.Columns["colTraceColor"].Index)
            {
                colorDialog1.ShowDialog();
                Color resultColor = colorDialog1.Color;
                runDataGrid.Rows[e.RowIndex].Cells["colTraceColor"].Style.BackColor = resultColor;
                //
                runDisplay[e.RowIndex].runColor = resultColor;

                runDataGrid.Invalidate();
                mapPanel.Invalidate();
                tractionCirclePanel.Invalidate();
                stripChartPanel.Invalidate();
            }
            return;
        }
        private void RunDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not on button cells. 
            if (e.RowIndex >= 0 && ((e.ColumnIndex == runDataGrid.Columns["colTraceColor"].Index) ||
                                    (e.ColumnIndex == runDataGrid.Columns["colShowRun"].Index)))
            {
                mapPanel.Invalidate();
                tractionCirclePanel.Invalidate();
                stripChartPanel.Invalidate();
            }
            return;
        }
        private void RunDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == runDataGrid.Columns["colOffsetTime"].Index)
            {
                runDisplay[e.RowIndex].stipchart_Offset[0] = Convert.ToSingle(runDataGrid.Rows[e.RowIndex].Cells["colOffsetTime"].Value);
                runDataGrid.Invalidate();
                mapPanel.Invalidate();
                tractionCirclePanel.Invalidate();
                stripChartPanel.Invalidate();
            }
            return;
        }
        private void RunDataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && ((e.ColumnIndex == runDataGrid.Columns["colTraceColor"].Index) ||
                                    (e.ColumnIndex == runDataGrid.Columns["colShowRun"].Index)))
            {
                runDataGrid.EndEdit();
            }

        }
        #endregion
        #region Y axis channel grid events
        private void YAxisChannelDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            int runNum = Convert.ToInt32(channelDataGrid.Rows[e.RowIndex].Cells["runName"].Value.ToString()) - 1;
            String channelName = channelDataGrid.Rows[e.RowIndex].Cells["channelName"].Value.ToString();
            //// Ignore clicks that are not on button cells. 
            if (e.ColumnIndex == channelDataGrid.Columns["displayChannel"].Index)
            {
                channelDataGrid.Rows[e.RowIndex].Cells["displayChannel"].Value = !(bool)channelDataGrid.Rows[e.RowIndex].Cells["displayChannel"].Value;

                runDisplay[runNum].channelDisplay[channelName] = (bool)channelDataGrid.Rows[e.RowIndex].Cells["displayChannel"].Value;

                stripChartPanel.Invalidate();
            }
            else if (e.ColumnIndex == channelDataGrid.Columns["channelColor"].Index)
            {
                if(colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Color resultColor = colorDialog1.Color;
                    channelDataGrid.Rows[e.RowIndex].Cells["channelColor"].Style.BackColor = resultColor;
                    channelDataGrid.Rows[e.RowIndex].Cells["channelColor"].Style.SelectionBackColor = resultColor;
                    runDisplay[runNum].channelColor[channelName] = resultColor;
                    channelDataGrid.Invalidate();

                }
                stripChartPanel.Invalidate();
            }
            return;
        }
        #endregion
        #region GDI support
        public static uint RGB(Color color)
        {
            uint rgb = (uint)(color.R + (color.G << 8) + (color.B << 16));
            return rgb;
        }
        public static uint NotRGB(Color color)
        {
            uint rgb = (uint)(color.R + (color.G << 8) + (color.B << 16));
            rgb = ~rgb & 0xFFFFFF;
            return rgb;
        }
        #endregion
        #region GPS to distance
        private float GPSDistance(float lat1Deg, float long1Deg, float lat2Deg, float long2Deg )
        {
            // This uses the ‘haversine’ formula to calculate the great - circle distance between two points 
            // the shortest distance over the earth’s surface – giving an ‘as- the - crow - flies’ distance between the points 
            // (ignoring any hills they fly over, of course!).
            // Haversine formula:	a = sin²(deltaLat / 2) + cos lat1 ⋅ cos lat2 ⋅ sin²(deltaLong / 2)
            // c = 2 ⋅ atan2( √a, √(1−a) )
            // d = R ⋅ c
            // where   'lat' is latitude, 'long' is longitude, R is earth’s radius (mean radius = 6371km);

            double R = 6371e3F; // metres
            R = R * 3.28084; // feet
            double lat1 = DegreesToRadians(lat1Deg);
            double lat2 = DegreesToRadians(lat2Deg);
            double long1 = DegreesToRadians(lat1Deg);
            double long2 = DegreesToRadians(lat2Deg);
            double deltaLat = lat2 - lat1;
            double deltaLong = long2 - long1;

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(deltaLong / 2) * Math.Sin(deltaLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = R * c;
            return (float)d;
        }
        private float DegreesToRadians(double deg)
        {
            double rad = (deg * Math.PI) / 180.0;
            return (float)rad;
        }
        #endregion

    }
    /// <summary>
    /// display info for all data
    /// </summary>
    public partial class GlobalDisplay_Data
    {
        // ranges for all data in a channel for all runs
        // for example, if 'Time' in run 1 is 0-120 and 'Time' in run 2 is 0-360, channelRange["Time"] will be 0-360
        public Dictionary<String, float[]> channelRanges = new Dictionary<string, float[]>();

        public void AddChannel(String channelName)
        {
            if(!channelRanges.ContainsKey(channelName))
            {
                channelRanges.Add(channelName, new float[] { float.MaxValue, float.MinValue });
            }
        }
        public void UpdateChannelRange(String channelName, float value)
        {
            channelRanges[channelName][0] = value < channelRanges[channelName][0] ? value : channelRanges[channelName][0];
            channelRanges[channelName][1] = value > channelRanges[channelName][1] ? value : channelRanges[channelName][1];
        }
        /// <summary>
        /// convert sourcePt from data units to display units
        /// </summary>
        /// <param name="sourcePt"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public PointF ScaleDataToDisplay(PointF sourcePt, float scaleX, float scaleY, float offsetX, float offsetY, Rectangle bounds)
        {
            PointF rPt = new PointF(sourcePt.X, sourcePt.Y);
            rPt.X = (rPt.X + offsetX) * scaleX + bounds.X;
            rPt.Y = bounds.Height - ((rPt.Y - offsetY) * scaleY) + bounds.Y;
            return rPt;
        }
        /// <summary>
        /// convert sourcePt from display units to data units
        /// </summary>
        /// <param name="sourcePt"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public PointF ScaleDisplayToData(PointF sourcePt, float scaleX, float scaleY, float offsetX, float offsetY, Rectangle bounds)
        {
            PointF rPt = new PointF(sourcePt.X, sourcePt.Y);
            rPt.X = (rPt.X / scaleX) - offsetX;
            rPt.Y = -1.0F * ((rPt.Y - (float)bounds.Height ) / scaleY) - offsetY;
            return rPt;
        }
        public void Reset()
        {
            channelRanges.Clear();
        }
    }
    /// <summary>
    /// display infor for runs and channels
    /// </summary>
    public partial class RunDisplay_Data
    {
        public Color runColor = Color.Black;
        public float[] stipchart_Offset = new float[] { 0.0F, 0.0F };
        public bool showRun = true;
        public Dictionary<String, bool> channelDisplay = new Dictionary<string, bool>();
        public Dictionary<String, Color> channelColor = new Dictionary<string, Color>();
        public void Reset()
        {
            stipchart_Offset = new float[] { 0.0F, 0.0F };
            channelDisplay.Clear();
            channelColor.Clear();
        }
    }
}
