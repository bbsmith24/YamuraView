﻿using System;
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
using WeifenLuo.WinFormsUI.Docking;

namespace YamuraLog
{
    public partial class Form1 : Form
    {
        #region members
        private Button colorButton = new Button();
        List<Color> penColors = new List<Color>();
        private List<Task> tasks = new List<Task>();

        // DataLogger contains runs, which contain channels which contain data points
        DataLogger dataLogger = new DataLogger();

        float gpsDist = 0.0F;
        // runs processed to logger prior to latest read (for TXT files with start/stop markers)
        int initialRunCount = 0;

        ChartControl stripChart = new ChartControl();
        TractionCircleControl tractionCircle = new TractionCircleControl();
        ChartControl trackMap = new ChartControl();
        #endregion
        #region constructors
        public Form1()
        {
            InitializeComponent();
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

            // initialize the chart controls
            // stripchart
            stripChart.ChartName = "Stripchart";
            stripChart.Logger = dataLogger;
            stripChart.chartViewForm.CursorMode = ChartView.CursorStyle.CROSSHAIRS;
            stripChart.chartViewForm.CursorUpdateSource = true;
            stripChart.ChartAxes = new Dictionary<string, Axis>();
            stripChart.chartViewForm.ShowHScroll = true;
            stripChart.chartViewForm.ShowVScroll = false;
            stripChart.chartViewForm.EqualScale = false;
            stripChart.chartPropertiesForm.ChartXAxisChangeEvent += stripChart.chartViewForm.OnChartXAxisChange;
            stripChart.chartPropertiesForm.ClearGraphicsPathEvent += stripChart.chartViewForm.OnClearGraphicsPath;
            stripChart.chartPropertiesForm.ClearGraphicsPathEvent += tractionCircle.chartViewForm.OnClearGraphicsPath;
            stripChart.chartPropertiesForm.ClearGraphicsPathEvent += trackMap.chartViewForm.OnClearGraphicsPath;
            // traction circle
            tractionCircle.ChartName = "Traction Circle";
            tractionCircle.Logger = dataLogger;
            tractionCircle.chartViewForm.CursorMode = ChartView.CursorStyle.BOX;
            tractionCircle.chartViewForm.CursorUpdateSource = false;
            tractionCircle.ChartAxes = new Dictionary<string, Axis>();
            tractionCircle.chartViewForm.ShowHScroll = false;
            tractionCircle.chartViewForm.ShowVScroll = false;
            tractionCircle.chartViewForm.EqualScale = true;
            tractionCircle.chartPropertiesForm.ChartXAxisChangeEvent += tractionCircle.chartViewForm.OnChartXAxisChange;
            tractionCircle.chartPropertiesForm.ClearGraphicsPathEvent += stripChart.chartViewForm.OnClearGraphicsPath;
            tractionCircle.chartPropertiesForm.ClearGraphicsPathEvent += tractionCircle.chartViewForm.OnClearGraphicsPath;
            tractionCircle.chartPropertiesForm.ClearGraphicsPathEvent += trackMap.chartViewForm.OnClearGraphicsPath;
            // track map
            trackMap.ChartName = "Track Map";
            trackMap.Logger = dataLogger;
            trackMap.chartViewForm.CursorMode = ChartView.CursorStyle.BOX;
            trackMap.chartViewForm.CursorUpdateSource = false;
            trackMap.ChartAxes = new Dictionary<string, Axis>();
            trackMap.chartViewForm.ShowHScroll = false;
            trackMap.chartViewForm.ShowVScroll = false;
            trackMap.chartViewForm.EqualScale = true;
            trackMap.chartPropertiesForm.ChartXAxisChangeEvent += trackMap.chartViewForm.OnChartXAxisChange;
            trackMap.chartPropertiesForm.ClearGraphicsPathEvent += stripChart.chartViewForm.OnClearGraphicsPath;
            trackMap.chartPropertiesForm.ClearGraphicsPathEvent += tractionCircle.chartViewForm.OnClearGraphicsPath;
            trackMap.chartPropertiesForm.ClearGraphicsPathEvent += trackMap.chartViewForm.OnClearGraphicsPath;
            // stripchart mouse move event handlers
            stripChart.chartViewForm.ChartMouseMoveEvent += OnChartMouseMove;// new ChartMouseMove(OnChartMouseMove);
            stripChart.chartViewForm.ChartMouseMoveEvent += tractionCircle.chartViewForm.OnChartMouseMove;
            stripChart.chartViewForm.ChartMouseMoveEvent += trackMap.chartViewForm.OnChartMouseMove;

            dockPanel1.Dock = DockStyle.None;
            dockPanel1.BackColor = Color.White;
            dockPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dockPanel1.Dock = DockStyle.Fill;
            dockPanel1.BringToFront();
            // add charts to dock panel
            stripChart.ShowHint = DockState.Document;
            stripChart.Text = "Stripchart";
            stripChart.Show(dockPanel1);
            stripChart.DockState = DockState.Document;

            tractionCircle.ShowHint = DockState.Document;
            tractionCircle.Text = "Traction Circle";
            tractionCircle.Show(dockPanel1);
            tractionCircle.DockState = DockState.DockRightAutoHide;

            trackMap.ShowHint = DockState.Document;
            trackMap.Text = "Track Map";
            trackMap.Show(dockPanel1);
            trackMap.DockState = DockState.DockRightAutoHide;
            // add new content pane, add runDataGrid control to new content pane
            runDataGrid.ContextMenuStrip = this.runDataContext;
            runDataGrid.Dock = DockStyle.Fill;
            DockContent dockpanel2 = new DockContent();
            dockpanel2.Controls.Add(runDataGrid);
            dockpanel2.ShowHint = DockState.Document;
            dockpanel2.Text = "Run Info";
            dockpanel2.Show(dockPanel1);
            dockpanel2.DockState = DockState.DockRightAutoHide;
        }
        #endregion
        #region event handlers       
        /// <summary>
        /// handle mouse move events from stripchart
        /// do nothing for now
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChartMouseMove(object sender, ChartControlMouseMoveEventArgs e)
        {
        }
        #endregion
        #region read various log file formats
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
            initialRunCount = dataLogger.runData.Count();
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
            uint timeStampOffset = 0;
            bool timeStampOffsetSet = false;
            float timestampSeconds = 0;
            float priorLatVal = 0.0F;
            float priorLongVal = 0.0F;
            bool gpsDistanceValid = false;

            initialRunCount = dataLogger.runData.Count();
            dataLogger.runData.Add(new RunData());

            logRunsIdx = dataLogger.runData.Count - 1;
            dataLogger.runData[logRunsIdx].AddChannel("Time", "Timestamp", "Internal", 1.0F);

            dataLogger.runData[logRunsIdx].fileName = System.IO.Path.GetFileName(fileName);


            using (BinaryReader inFile = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                // check for EOF
                while (inFile.BaseStream.Position != inFile.BaseStream.Length)
                {
                    #region read 1 char, exception on EOF
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
                        if(!timeStampOffsetSet)
                        {
                            timeStampOffset = timeStamp;
                            timeStampOffsetSet = true;
                        }
                        timeStamp -= timeStampOffset;
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
                        String ns = "";
                        float lng = 0.0F;
                        String ew = "";
                        float hd = 0.0F;
                        float speed = 0.0F;
                        int sat = 0;
                        String date = "";
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
        public bool ParseGPS_NMEA(BinaryReader inFile, out String date, out int hr, out int min, out float sec, out float lat, out String ns, out float lng, out String ew, out float hd, out float speed, out int sat)
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
                #region checksum
                // malformed, no * 
                if (dataSentence.IndexOf('*') < 0)
                {
                    continue;
                }
                int receivedChecksum = 0;
                // check for malformed, illegal char in hex value
                try
                {
                    receivedChecksum = Convert.ToInt32(dataSentence.Substring(dataSentence.IndexOf('*') + 1), 16);
                }
                catch
                {
                    continue;
                }
                // calculate checksum for characters between $ and *
                int calculatedChecksum = 0;
                int charIdx = 1;
                while(dataSentence[charIdx] != '*')
                {
                    calculatedChecksum ^= Convert.ToByte(dataSentence[charIdx]);
                    charIdx++;
                }
                // bad checksum - skip this sentence
                if(calculatedChecksum != receivedChecksum)
                {
                    continue;
                }
                #endregion

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
            #region update display info
            for (int runIdx = initialRunCount; runIdx < dataLogger.runData.Count; runIdx++)
            {
                RunData curRun = dataLogger.runData[dataLogger.runData.Count - 1];
                {
                    channelIdx = 0;
                    foreach (KeyValuePair<String, DataChannel> curChannel in curRun.channels)
                    {
                        if(curChannel.Value.DataRange[0] == curChannel.Value.DataRange[1])
                        {
                            curChannel.Value.DataRange[1] += 1;
                        }
                        String axisName = curChannel.Key;
                        #region strip chart control axes create/update
                        if (stripChart.ChartAxes.ContainsKey(axisName))
                        {
                            stripChart.ChartAxes[axisName].AxisValueRange[0] = stripChart.ChartAxes[axisName].AxisValueRange[0] < curChannel.Value.DataRange[0] ? stripChart.ChartAxes[axisName].AxisValueRange[0] : curChannel.Value.DataRange[0];
                            stripChart.ChartAxes[axisName].AxisValueRange[1] = stripChart.ChartAxes[axisName].AxisValueRange[1] > curChannel.Value.DataRange[1] ? stripChart.ChartAxes[axisName].AxisValueRange[1] : curChannel.Value.DataRange[1];
                        }
                        else
                        {
                            stripChart.ChartAxes.Add(axisName, new Axis());
                            stripChart.ChartAxes[axisName].AxisValueRange[0] = curChannel.Value.DataRange[0];
                            stripChart.ChartAxes[axisName].AxisValueRange[1] = curChannel.Value.DataRange[1];
                        }
                        stripChart.ChartAxes[axisName].AxisName = axisName;
                        stripChart.ChartAxes[axisName].AxisValueRange[2] = stripChart.ChartAxes[axisName].AxisValueRange[1] - stripChart.ChartAxes[axisName].AxisValueRange[0];
                        stripChart.ChartAxes[axisName].AxisDisplayRange[0] = stripChart.ChartAxes[axisName].AxisValueRange[0];
                        stripChart.ChartAxes[axisName].AxisDisplayRange[1] = stripChart.ChartAxes[axisName].AxisValueRange[1];
                        stripChart.ChartAxes[axisName].AxisDisplayRange[2] = stripChart.ChartAxes[axisName].AxisValueRange[2];

                        stripChart.ChartAxes[axisName].AssociatedChannels.Add((runIdx.ToString() + "-" + curChannel.Key), new ChannelInfo(runIdx, curChannel.Key));
                        stripChart.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].ChannelColor = penColors[channelIdx % penColors.Count];
                        stripChart.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisOffset[0] = 0.0F;
                        stripChart.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisOffset[1] = 0.0F;
                        stripChart.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[0] = stripChart.ChartAxes[axisName].AxisValueRange[0];
                        stripChart.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[1] = stripChart.ChartAxes[axisName].AxisValueRange[1];
                        stripChart.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[2] = stripChart.ChartAxes[axisName].AxisValueRange[2];
                        #endregion
                        #region traction circle control axes create/update
                        if (tractionCircle.ChartAxes.ContainsKey(axisName))
                        {
                            tractionCircle.ChartAxes[axisName].AxisValueRange[0] = tractionCircle.ChartAxes[axisName].AxisValueRange[0] < curChannel.Value.DataRange[0] ? tractionCircle.ChartAxes[axisName].AxisValueRange[0] : curChannel.Value.DataRange[0];
                            tractionCircle.ChartAxes[axisName].AxisValueRange[1] = tractionCircle.ChartAxes[axisName].AxisValueRange[1] > curChannel.Value.DataRange[1] ? tractionCircle.ChartAxes[axisName].AxisValueRange[1] : curChannel.Value.DataRange[1];
                        }
                        else
                        {
                            tractionCircle.ChartAxes.Add(axisName, new Axis());
                            tractionCircle.ChartAxes[axisName].AxisValueRange[0] = curChannel.Value.DataRange[0];
                            tractionCircle.ChartAxes[axisName].AxisValueRange[1] = curChannel.Value.DataRange[1];
                        }
                        tractionCircle.ChartAxes[axisName].AxisName = axisName;
                        tractionCircle.ChartAxes[axisName].AxisValueRange[2] = tractionCircle.ChartAxes[axisName].AxisValueRange[1] - tractionCircle.ChartAxes[axisName].AxisValueRange[0];
                        tractionCircle.ChartAxes[axisName].AxisDisplayRange[0] = tractionCircle.ChartAxes[axisName].AxisValueRange[0];
                        tractionCircle.ChartAxes[axisName].AxisDisplayRange[1] = tractionCircle.ChartAxes[axisName].AxisValueRange[1];
                        tractionCircle.ChartAxes[axisName].AxisDisplayRange[2] = tractionCircle.ChartAxes[axisName].AxisValueRange[2];

                        tractionCircle.ChartAxes[axisName].AssociatedChannels.Add((runIdx.ToString() + "-" + curChannel.Key), new ChannelInfo(runIdx, curChannel.Key));
                        tractionCircle.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].ChannelColor = penColors[channelIdx % penColors.Count];
                        tractionCircle.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisOffset[0] = 0.0F;
                        tractionCircle.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisOffset[1] = 0.0F;
                        tractionCircle.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[0] = tractionCircle.ChartAxes[axisName].AxisValueRange[0];
                        tractionCircle.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[1] = tractionCircle.ChartAxes[axisName].AxisValueRange[1];
                        tractionCircle.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[2] = tractionCircle.ChartAxes[axisName].AxisValueRange[2];
                        #endregion
                        #region track map control axes create/update
                        if (trackMap.ChartAxes.ContainsKey(axisName))
                        {
                            trackMap.ChartAxes[axisName].AxisValueRange[0] = trackMap.ChartAxes[axisName].AxisValueRange[0] < curChannel.Value.DataRange[0] ? trackMap.ChartAxes[axisName].AxisValueRange[0] : curChannel.Value.DataRange[0];
                            trackMap.ChartAxes[axisName].AxisValueRange[1] = trackMap.ChartAxes[axisName].AxisValueRange[1] > curChannel.Value.DataRange[1] ? trackMap.ChartAxes[axisName].AxisValueRange[1] : curChannel.Value.DataRange[1];
                        }
                        else
                        {
                            trackMap.ChartAxes.Add(axisName, new Axis());
                            trackMap.ChartAxes[axisName].AxisValueRange[0] = curChannel.Value.DataRange[0];
                            trackMap.ChartAxes[axisName].AxisValueRange[1] = curChannel.Value.DataRange[1];
                        }
                        trackMap.ChartAxes[axisName].AxisName = axisName;
                        trackMap.ChartAxes[axisName].AxisValueRange[2] = trackMap.ChartAxes[axisName].AxisValueRange[1] - trackMap.ChartAxes[axisName].AxisValueRange[0];

                        trackMap.ChartAxes[axisName].AxisDisplayRange[0] = trackMap.ChartAxes[axisName].AxisValueRange[0];
                        trackMap.ChartAxes[axisName].AxisDisplayRange[1] = trackMap.ChartAxes[axisName].AxisValueRange[1];
                        trackMap.ChartAxes[axisName].AxisDisplayRange[2] = trackMap.ChartAxes[axisName].AxisValueRange[2];

                        trackMap.ChartAxes[axisName].AssociatedChannels.Add((runIdx.ToString() + "-" + curChannel.Key), new ChannelInfo(runIdx, curChannel.Key));
                        trackMap.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].ChannelColor = penColors[channelIdx % penColors.Count];
                        trackMap.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisOffset[0] = 0.0F;
                        trackMap.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisOffset[1] = 0.0F;
                        trackMap.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[0] = trackMap.ChartAxes[axisName].AxisValueRange[0];
                        trackMap.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[1] = trackMap.ChartAxes[axisName].AxisValueRange[1];
                        trackMap.ChartAxes[axisName].AssociatedChannels[(runIdx.ToString() + "-" + curChannel.Key)].AxisRange[2] = trackMap.ChartAxes[axisName].AxisValueRange[2];
                        #endregion
                        channelIdx++;
                    }
                    stripChart.chartViewForm.ChartAxes = stripChart.ChartAxes;
                    stripChart.chartPropertiesForm.ChartAxes = stripChart.ChartAxes;
                    tractionCircle.chartViewForm.ChartAxes = tractionCircle.ChartAxes;
                    tractionCircle.chartPropertiesForm.ChartAxes = tractionCircle.ChartAxes;
                    trackMap.chartViewForm.ChartAxes = trackMap.ChartAxes;
                    trackMap.chartPropertiesForm.ChartAxes = trackMap.ChartAxes;

                    stripChart.Logger = dataLogger;
                    stripChart.chartViewForm.Logger = dataLogger;
                    stripChart.chartPropertiesForm.Logger = dataLogger;

                    tractionCircle.Logger = dataLogger;
                    tractionCircle.chartViewForm.Logger = dataLogger;
                    tractionCircle.chartPropertiesForm.Logger = dataLogger;

                    trackMap.Logger = dataLogger;
                    trackMap.chartViewForm.Logger = dataLogger;
                    trackMap.chartPropertiesForm.Logger = dataLogger;
                }
            }
            #endregion
            #region populate run data grid
            runDataGrid.Rows.Clear();
            for(int runGridIdx = 0; runGridIdx < dataLogger.runData.Count; runGridIdx++)
            {
                runDataGrid.Rows.Add();
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colRunNumber"].Value = (runGridIdx + 1).ToString();
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colDate"].Value = dataLogger.runData[runGridIdx].dateStr + " " + dataLogger.runData[runGridIdx].timeStr;
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colMinTime"].Value = dataLogger.runData[runGridIdx].channels["Time"].DataRange[0];
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colMaxTime"].Value = dataLogger.runData[runGridIdx].channels["Time"].DataRange[1];
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colSourceFile"].Value = dataLogger.runData[runGridIdx].fileName.ToString();
            }
            #endregion
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
        #region toolbar menu click handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRunsMenuItem_Click(object sender, EventArgs e)
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
            stripChart.Invalidate();
            tractionCircle.Invalidate();
            trackMap.Invalidate();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearRunsMenuItem_Click(object sender, EventArgs e)
        {
            runDataGrid.Rows.Clear();
            dataLogger.Reset();


            stripChart.Invalidate();
            tractionCircle.Invalidate();
            trackMap.Invalidate();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void loggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadFiles uploadDlg = new UploadFiles();
            uploadDlg.UploadMode = "U";
            uploadDlg.ShowDialog();

        }
        #endregion

        private void exportFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // export to TSV file
            int rowIdx = runDataGrid.SelectedCells[0].RowIndex;
            String exportTo = runDataGrid.Rows[rowIdx].Cells["colSourceFile"].Value.ToString();
            exportTo = exportTo.Replace("YLG", "TSV");

            foreach (KeyValuePair<string, DataChannel> channel in dataLogger.runData[rowIdx].channels)
            {
                System.Diagnostics.Debug.Print(channel.Key);
                System.Diagnostics.Debug.Print("\t");
            }
            System.Diagnostics.Debug.Print(System.Environment.NewLine);
            foreach (KeyValuePair<float, DataPoint> timeStamp in dataLogger.runData[rowIdx].channels["Time"].DataPoints)
            {
                foreach(KeyValuePair<string, DataChannel> channel in dataLogger.runData[rowIdx].channels)
                {
                    if(channel.Value.dataPoints.ContainsKey(timeStamp.Key))
                    {
                        System.Diagnostics.Debug.Print(channel.Value.dataPoints[timeStamp.Key].PointValue.ToString());
                    }
                    else
                    {
                        System.Diagnostics.Debug.Print("\t");
                    }
                    System.Diagnostics.Debug.Print("\t");
                }
                System.Diagnostics.Debug.Print(System.Environment.NewLine);
            }

            //
            // do export here...
            //
        }
    }
}
