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

namespace YamuraLog
{
    public partial class Form1 : Form
    {
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

        // one pair per run for run data
        List<RunHeader_Data> runData = new List<RunHeader_Data>();
        List<SortedList<float, DataBlock>> logEvents = new List<SortedList<float, DataBlock>>();
        // one per run for display data
        List<RunDisplay_Data> runDisplay = new List<RunDisplay_Data>();
        GlobalDisplay_Data globalDisplay = new GlobalDisplay_Data();

        #region track map
        #region mouse move points
        Point trackMapStartPos = new Point(0, 0);
        bool trackMapStartPosValid = false;
        Point trackMapLastPos = new Point(0, 0);
        bool trackMapLastPosValid = false;
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
        bool tractionCircleLastPosValid = false;
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
        bool stripChartLastPosValid = false;
        #endregion
        #region cursor
        PointF stripChartStartCursorPos = new Point(0, 0);
        PointF stripChartLastCursorPos = new Point(0, 0);
        Point stripChartLastCursorPosInt = new Point(0, 0);
        bool stripChartLastCursorPosValid = false;
        #endregion
        #region scaling
        float stripChartScaleX = 0.0F;
        List<float> stripChartScaleY = new List<float>();
        float stripChartScaleFactorX = 1.0F;
        float stripChartScaleFactorY = 1.0F;
        #endregion
        #region window
        int stripChartPanelBorder = 10;
        Rectangle stripChartPanelBounds = new Rectangle(0, 0, 0, 0);
        #endregion
        #region X axis range
        float[] stripChartOffset = new float[] { 0.0F, 0.0F };
        float[] stripChartExtents = new float[] { 0.0F, 0.0F };
        #endregion
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
            //colorButton.Click += new EventHandler(colorSelect_Click);

            // Add a CellClick handler to handle clicks in the button column.
            runDataGrid.CellClick += new DataGridViewCellEventHandler(RunDataGrid_CellClick);
            runDataGrid.CellEndEdit += new DataGridViewCellEventHandler(RunDataGrid_CellEndEdit);
            runDataGrid.CellValueChanged += new DataGridViewCellEventHandler(RunDataGrid_CellValueChanged);
            runDataGrid.CellMouseUp += new DataGridViewCellMouseEventHandler(RunDataGrid_CellMouseUp);
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if(openLogFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            textBox1.Text = openLogFile.FileName;
            String tempLogFile = openLogFile.FileName.Replace(".txt", ".tmp");
            tempLogFile = tempLogFile.Replace(".TXT", ".TMP");
            StreamReader readLog = new StreamReader(openLogFile.FileName, true);
            StreamWriter writeLog = new StreamWriter(tempLogFile, false);
            String tmp_text = readLog.ReadToEnd();// readFile.ReadToEnd();
            StringBuilder gpx_text = new StringBuilder();
            foreach (char c in tmp_text)
            {
                if((c != 0x01) && (c != 0x11) && (c != 0x0C))
                {
                    writeLog.Write(c);
                    gpx_text.Append(c);
                }
            }
            readLog.Close();
            writeLog.Close();

            StreamReader readTemp = new StreamReader(tempLogFile, true);
            String inputStr;
            String[] splitStr;
            //logEvents.Add(new List<DataBlock>());
            int logRunsIdx = 0;
            int logRunDataIdx = 0;
            float latVal = 0.0F;
            float longVal = 0.0F;
            float gX = 0.0F;
            float gY = 0.0F;
            float gZ = 0.0F;
            ulong timestamp = 0;
            ulong timestampOffset = 0;
            float timestampSeconds = 0.0F;
            float mph = 0;
            StringBuilder strRunsList = new StringBuilder();
            while (!readTemp.EndOfStream)
            {
                inputStr = readTemp.ReadLine();
                if(inputStr.Length == 0)
                {
                    continue;
                }
                // found run start, create new data list in log events
                //                  new run data header
                //                  new display header
                if (String.Compare(inputStr, "Start", true) == 0)
                {
                    logEvents.Add(new SortedList<float, DataBlock>());
                    runData.Add(new RunHeader_Data());
                    runDisplay.Add(new RunDisplay_Data());
                    logRunsIdx = logEvents.Count - 1;
                    // set run file name in run data
                    runData[logRunsIdx].fileName = textBox1.Text;
                    // initialize run display color and offset
                    runDisplay[logRunsIdx].runColor = penColors[logRunsIdx % penColors.Count()];
                    runDisplay[logRunsIdx].stipchart_Offset[0] = 0.0F;
                    runDisplay[logRunsIdx].stipchart_Offset[1] = 0.0F;
                    continue;
                }
                else if ((String.Compare(inputStr, "Stop", true) == 0) ||
                         inputStr.StartsWith("GPS") ||
                         inputStr.StartsWith("Accel") ||
                         inputStr.StartsWith("Team Yamura"))
                {
                    timestampOffset = 0;
                    continue;
                }
                splitStr = inputStr.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                // gps+accel form - 11 fields
                // 35990532 07/11/2019 12:44:39.00 42.446449 -83.456070  0.70    183.920000  7   0.306   0.005   0.947
                //
                // gps only - 8 fields
                // 35990532 07/11/2019 12:44:39.00 42.446449 -83.456070  0.70    183.920000  7
                //
                // accel only - 4 fields
                //36050376 0.41   0.00    0.94
                // timestamp is always first
                timestamp = (ulong)BitConverter.ToUInt32(BitConverter.GetBytes(Convert.ToInt32(splitStr[0])), 0);
                if(logEvents[logRunsIdx].Count == 0)
                {
                    timestampOffset = timestamp;
                }
                timestamp -= timestampOffset;
                timestampSeconds = Convert.ToSingle(timestamp) / 1000000.0F;
                logEvents[logRunsIdx].Add(timestampSeconds, new DataBlock());
                logRunDataIdx = logEvents[logRunsIdx].Count() - 1;
                
                // block timestamp converted from unsigned long microseconds to float seconds
                logEvents[logRunsIdx][timestampSeconds].micros = timestampSeconds;
                // gps only, gps+accelerometer - get gps portion
                if ((splitStr.Count() == 8) || (splitStr.Count() == 11))
                {
                    latVal = Convert.ToSingle(splitStr[3]);
                    longVal = Convert.ToSingle(splitStr[4]);
                    mph = Convert.ToSingle(splitStr[5]);
                    logEvents[logRunsIdx][timestampSeconds].gps.dateStr = splitStr[1];
                    logEvents[logRunsIdx][timestampSeconds].gps.timeStr = splitStr[2];
                    logEvents[logRunsIdx][timestampSeconds].gps.latVal = latVal;
                    logEvents[logRunsIdx][timestampSeconds].gps.longVal = longVal;
                    logEvents[logRunsIdx][timestampSeconds].gps.mph = mph;
                    logEvents[logRunsIdx][timestampSeconds].gps.heading = Convert.ToSingle(splitStr[6]);
                    logEvents[logRunsIdx][timestampSeconds].gps.satellites = Convert.ToInt32(splitStr[7]);
                    logEvents[logRunsIdx][timestampSeconds].gps.isValid = true;
                    
                    if(runData[logRunsIdx].dateStr.Length == 0)
                    {
                        runData[logRunsIdx].dateStr = splitStr[1];
                        runData[logRunsIdx].timeStr = splitStr[2];
                    }
                }
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
                    logEvents[logRunsIdx][timestampSeconds].accel.xAccel = gX;
                    logEvents[logRunsIdx][timestampSeconds].accel.yAccel = gY;
                    logEvents[logRunsIdx][timestampSeconds].accel.zAccel = gZ;
                    logEvents[logRunsIdx][timestampSeconds].accel.isValid = true;
                }
                if((!logEvents[logRunsIdx][timestampSeconds].accel.isValid) && (!logEvents[logRunsIdx][timestamp].gps.isValid))
                {
                    logEvents[logRunsIdx].Remove(timestampSeconds);// .RemoveAt(logRunDataIdx);
                    continue;
                }
                runData[logRunsIdx].UpdateDataRanges(timestampSeconds, logEvents[logRunsIdx][timestampSeconds].gps, logEvents[logRunsIdx][timestampSeconds].accel);
                runDisplay[logRunsIdx].UpdateDataRanges(timestampSeconds, logEvents[logRunsIdx][timestampSeconds].gps, logEvents[logRunsIdx][timestampSeconds].accel);
                globalDisplay.UpdateDataRanges(timestampSeconds, logEvents[logRunsIdx][timestampSeconds].gps, logEvents[logRunsIdx][timestampSeconds].accel);
            }
            for (int runIdx = 0; runIdx < runData.Count(); runIdx++)
            {
                trackMapLastCursorPos.Add(new Point(0, 0));
                trackMapLastCursorPosValid.Add(false);
                tractionCircleLastCursorPos.Add(new Point(0, 0));
                tractionCircleLastCursorPosValid.Add(false);
            }


            StringBuilder rangesStr = new StringBuilder();
            rangesStr.AppendFormat("Ranges{0}", System.Environment.NewLine);
            rangesStr.AppendFormat("===================={0}", System.Environment.NewLine);
            rangesStr.AppendFormat("Accel X {0} to {1} {2}", globalDisplay.minMaxAccel[0][0].ToString(), globalDisplay.minMaxAccel[0][1].ToString(), System.Environment.NewLine);
            rangesStr.AppendFormat("Accel Y {0} to {1} {2}", globalDisplay.minMaxAccel[1][0].ToString(), globalDisplay.minMaxAccel[1][1].ToString(), System.Environment.NewLine);
            rangesStr.AppendFormat("Accel Z {0} to {1} {2}", globalDisplay.minMaxAccel[2][0].ToString(), globalDisplay.minMaxAccel[2][1].ToString(), System.Environment.NewLine);
            rangesStr.AppendFormat("{0}", System.Environment.NewLine);
            rangesStr.AppendFormat("Speed {0} to {1} {2}", globalDisplay.minMaxSpeed[0].ToString(), globalDisplay.minMaxSpeed[1].ToString(), System.Environment.NewLine);
            rangesStr.AppendFormat("{0}", System.Environment.NewLine);
            for (int runIdx = 0; runIdx < runData.Count(); runIdx++)
            {
                rangesStr.AppendFormat("Run {0}{1}", runIdx, System.Environment.NewLine);
                rangesStr.AppendFormat("===================={0}", System.Environment.NewLine);
                rangesStr.AppendFormat("Accel X {0} to {1} {2}", runData[runIdx].minMaxAccel[0][0].ToString(), runData[runIdx].minMaxAccel[0][1].ToString(), System.Environment.NewLine);
                rangesStr.AppendFormat("Accel Y {0} to {1} {2}", runData[runIdx].minMaxAccel[1][0].ToString(), runData[runIdx].minMaxAccel[1][1].ToString(), System.Environment.NewLine);
                rangesStr.AppendFormat("Accel Z {0} to {1} {2}", runData[runIdx].minMaxAccel[2][0].ToString(), runData[runIdx].minMaxAccel[2][1].ToString(), System.Environment.NewLine);
                rangesStr.AppendFormat("{0}", System.Environment.NewLine);
                rangesStr.AppendFormat("Speed {0} to {1} {2}", runData[runIdx].minMaxSpeed[0].ToString(), runData[runIdx].minMaxSpeed[1].ToString(), System.Environment.NewLine);
                rangesStr.AppendFormat("{0}", System.Environment.NewLine);
            }
            runDataGrid.Rows.Clear();
            for (int runIdx = 0; runIdx < runData.Count(); runIdx++)
            {
                runDataGrid.Rows.Add();
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colRunNumber"].Value = (runIdx + 1).ToString();
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colShowRun"].Value = true;
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colDate"].Value = runData[runIdx].dateStr + " " + runData[runIdx].timeStr;
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colMinTime"].Value = runData[runIdx].minMaxTimestamp[0];
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colMaxTime"].Value = runData[runIdx].minMaxTimestamp[1];
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colOffsetTime"].Value = 0;
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colAccelX"].Value = String.Format("{0} to {1}", runData[runIdx].minMaxAccel[0][0], runData[runIdx].minMaxAccel[0][1]);
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colAccelY"].Value = String.Format("{0} to {1}", runData[runIdx].minMaxAccel[1][0], runData[runIdx].minMaxAccel[1][1]);
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colAccelZ"].Value = String.Format("{0} to {1}", runData[runIdx].minMaxAccel[2][0], runData[runIdx].minMaxAccel[2][1]);
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colSpeed"].Value = String.Format("{0}", runData[runIdx].minMaxSpeed[1]);
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colSourceFile"].Value = runData[runIdx].fileName.ToString();
                runDataGrid.Rows[runDataGrid.Rows.Count - 1].Cells["colTraceColor"].Style.BackColor = runDisplay[runDataGrid.Rows.Count - 1].runColor;
            }


            txtRunInfo.Text = rangesStr.ToString();

            DrawMap();
            DrawTraction();
            DrawSpeed();
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

            if ((globalDisplay.minMaxLat[0] == float.MaxValue) &&
               (globalDisplay.minMaxLong[0] == float.MaxValue))
            {
                return;
            }
            Pen drawPen = new Pen(Color.Black, 1);
            PointF startPt = new PointF();
            PointF endPt = new PointF();
            bool initialGPS = false;
            float scaleX = (float)trackMapBounds.Width / (float)Math.Abs(globalDisplay.minMaxLong[1] - globalDisplay.minMaxLong[0]);
            float scaleY = (float)trackMapBounds.Height / (float)Math.Abs(globalDisplay.minMaxLat[1] - globalDisplay.minMaxLat[0]);
            trackMapScale = scaleX < scaleY ? scaleX : scaleY;

            float startTime = 0.0F;
            float endTime = 0.0F;

            using (Graphics mapGraphics = mapPanel.CreateGraphics())
            {
                int runCount = 0;
                foreach (SortedList<float, DataBlock> curPath in logEvents)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    drawPen = new Pen(runDisplay[runCount].runColor);
                    initialGPS = false;
                    startPt.X = 0;
                    startPt.Y = 0;
                    foreach (KeyValuePair<float, DataBlock> curData in curPath)
                    {
                        if (!curData.Value.gps.isValid)
                        {
                            continue;
                        }
                        if (!initialGPS)
                        {
                            startPt.X = curData.Value.gps.longVal - globalDisplay.minMaxLong[0];
                            startPt.Y = curData.Value.gps.latVal - globalDisplay.minMaxLat[0];

                            startPt = globalDisplay.ScaleDataToDisplay(startPt, 
                                                                       trackMapScale * trackMapScaleFactor, 
                                                                       trackMapScale * trackMapScaleFactor,
                                                                       trackMapOffset[0],
                                                                       trackMapOffset[1], 
                                                                       trackMapBounds);

                            startTime = curData.Key - runDisplay[runCount].stipchart_Offset[0];
                            initialGPS = true;
                            continue;
                        }
                        else
                        {
                            endPt.X = curData.Value.gps.longVal - globalDisplay.minMaxLong[0];
                            endPt.Y = curData.Value.gps.latVal - globalDisplay.minMaxLat[0];

                            endPt = globalDisplay.ScaleDataToDisplay(endPt,
                                                                     trackMapScale * trackMapScaleFactor,
                                                                     trackMapScale * trackMapScaleFactor,
                                                                     trackMapOffset[0],
                                                                     trackMapOffset[1],
                                                                     trackMapBounds);


                            endTime = curData.Key;// - runDisplay[runCount].stipchart_Offset[0];

                            if (endTime < stripChartExtents[0])
                            {
                                startPt.X = endPt.X;
                                startPt.Y = endPt.Y;
                                startTime = endTime;
                                continue;
                            }
                            if (startTime > stripChartExtents[1])
                            {
                                break;
                            }

                            mapGraphics.DrawLine(drawPen, startPt, endPt);
                            startPt.X = endPt.X;
                            startPt.Y = endPt.Y;
                            startTime = endTime;
                        }
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
            trackMapLastPosValid = true;
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
            trackMapLastPosValid = false;
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

            float runTimeStamp = 0.0F;
            using (Graphics mapGraphics = mapPanel.CreateGraphics())
            {
                int runCount = 0;
                foreach (SortedList<float, DataBlock> curPath in logEvents)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    drawPen = new Pen(runDisplay[runCount].runColor);

                    runTimeStamp = xAxisValue - runDisplay[runCount].stipchart_Offset[0];
                    DataBlock curBlock = curPath.FirstOrDefault(i => i.Key > runTimeStamp).Value;
                    if (curBlock == null)
                    {
                        continue;
                    }
                    if (!curBlock.gps.isValid)
                    {
                        int idx = curPath.IndexOfKey(curBlock.micros);
                        curBlock = null;
                        while (idx >= 0)
                        {
                            if(curPath.ElementAt(idx).Value.gps.isValid)
                            {
                                curBlock = curPath.ElementAt(idx).Value;
                                break;
                            }
                            idx--;
                        }
                    }
                    if (curBlock == null)
                    {
                        continue;
                    }
                    locationPt.X = curBlock.gps.longVal - globalDisplay.minMaxLong[0];
                    locationPt.Y = curBlock.gps.latVal - globalDisplay.minMaxLat[0];

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


                    Gdi32.Rectangle(hDC, locationBox.Left, locationBox.Top, locationBox.Left+(int)trackMapCursorSize, locationBox.Top+(int)trackMapCursorSize);
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
                foreach (SortedList<float, DataBlock> curPath in logEvents)
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
            tractionCircleLastPosValid = false;
            for (int validIdx = 0; validIdx < trackMapLastCursorPosValid.Count(); validIdx++)
            {
                trackMapLastCursorPosValid[validIdx] = false;
            }

            if ((globalDisplay.minMaxLat[0] == float.MaxValue) &&
               (globalDisplay.minMaxLong[0] == float.MaxValue))
            {
                return;
            }
            Pen drawPen = new Pen(Color.Black, 1);
            PointF startPt = new PointF();
            PointF endPt = new PointF();
            bool initialGPS = false;
            float scaleX = (float)tractionCircleBounds.Width / (float)Math.Abs(globalDisplay.minMaxAccel[0][1] - globalDisplay.minMaxAccel[0][0]);
            float scaleY = (float)tractionCircleBounds.Height / (float)Math.Abs(globalDisplay.minMaxAccel[1][1] - globalDisplay.minMaxAccel[1][0]);
            tractionCircleScale = scaleX < scaleY ? scaleX : scaleY;
            float startTime = 0.0F;
            float endTime = 0.0F;

            using (Graphics tractionCircleGraphics = tractionCirclePanel.CreateGraphics())
            {
                int runCount = 0;
                foreach (SortedList<float, DataBlock> curPath in logEvents)
                {
                    drawPen = new Pen(runDisplay[runCount].runColor);
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }

                    initialGPS = false;
                    startPt.X = 0;
                    startPt.Y = 0;
                    foreach (KeyValuePair<float, DataBlock> curData in curPath)
                    {
                        if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                        {
                            runCount++;
                            continue;
                        }
                        if (!(curData.Value.accel.isValid))
                        {
                            continue;
                        }
                        if (!initialGPS)
                        {
                            startPt.X = curData.Value.accel.xAccel - globalDisplay.minMaxAccel[0][0];
                            startPt.Y = curData.Value.accel.yAccel - globalDisplay.minMaxAccel[1][0];
                            startPt = globalDisplay.ScaleDataToDisplay(startPt,
                                                                       tractionCircleScale * tractionCircleScaleFactor,
                                                                       tractionCircleScale * tractionCircleScaleFactor,
                                                                       tractionCircleOffset[0],
                                                                       tractionCircleOffset[1],
                                                                       tractionCircleBounds);
                            startTime = (float)curData.Key;
                            initialGPS = true;
                            continue;
                        }
                        else
                        {
                            endPt.X = curData.Value.accel.xAccel - globalDisplay.minMaxAccel[0][0];
                            endPt.Y = curData.Value.accel.yAccel - globalDisplay.minMaxAccel[1][0];
                            endPt = globalDisplay.ScaleDataToDisplay(endPt,
                                                                     tractionCircleScale * tractionCircleScaleFactor,
                                                                     tractionCircleScale * tractionCircleScaleFactor,
                                                                     tractionCircleOffset[0],
                                                                     tractionCircleOffset[1],
                                                                     tractionCircleBounds);
                            endTime = (float)curData.Key;
                            if(endTime < stripChartExtents[0])
                            {
                                continue;
                            }
                            if(startTime > stripChartExtents[1])
                            {
                                break;
                            }
                            tractionCircleGraphics.DrawLine(drawPen, startPt, endPt);
                            startPt.X = endPt.X;
                            startPt.Y = endPt.Y;
                            startTime = endTime;
                        }
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
            tractionCircleLastPosValid = true;
        }
        private void tractionCircle_MouseUp(object sender, MouseEventArgs e)
        {
            if (tractionCircleStartPosValid)
            {
                tractionCircleOffset[0] += ((float)(e.Location.X - tractionCircleStartPos.X) / tractionCircleScale);
                tractionCircleOffset[1] += ((float)(e.Location.Y - tractionCircleStartPos.Y) / tractionCircleScale);
            }
            tractionCircleStartPosValid = false;
            tractionCircleLastPosValid = false;
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

            float runTimeStamp = 0.0F;
            using (Graphics mapGraphics = tractionCirclePanel.CreateGraphics())
            {
                int runCount = 0;
                foreach (SortedList<float, DataBlock> curPath in logEvents)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    drawPen = new Pen(runDisplay[runCount].runColor);

                    runTimeStamp = xAxisValue - runDisplay[runCount].stipchart_Offset[0];
                    DataBlock curBlock = curPath.FirstOrDefault(i => i.Key > runTimeStamp).Value;
                    if (curBlock == null)
                    {
                        continue;
                    }
                    if (!curBlock.accel.isValid)
                    {
                        int idx = curPath.IndexOfKey(curBlock.micros);
                        curBlock = null;
                        while (idx >= 0)
                        {
                            if (curPath.ElementAt(idx).Value.accel.isValid)
                            {
                                curBlock = curPath.ElementAt(idx).Value;
                                break;
                            }
                            idx--;
                        }
                    }
                    if (curBlock == null)
                    {
                        continue;
                    }
                    locationPt.X = curBlock.accel.xAccel - globalDisplay.minMaxAccel[0][0];
                    locationPt.Y = curBlock.accel.yAccel - globalDisplay.minMaxAccel[1][0];

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
                foreach (SortedList<float, DataBlock> curPath in logEvents)
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
            stripChartLastPosValid = false;
            stripChartStartPosValid = false;
            stripChartLastCursorPosValid = false;

            if ((globalDisplay.minMaxSpeed[0] == float.MaxValue) &&
                (globalDisplay.minMaxTimestamp[0] == ulong.MaxValue))
            {
                return;
            }
            Pen drawPen = new Pen(Color.Black, 1);
            PointF[] startPt = new PointF[4] { new PointF(), new PointF(), new PointF(), new PointF() };
            PointF[] endPt = new PointF[4] { new PointF(), new PointF(), new PointF(), new PointF() };
            bool initialGPS = false;
            if ((stripChartScaleX == 0.0F) && (runDisplay.Count() > 0))
            {
                stripChartScaleX = (float)stripChartPanelBounds.Width / (globalDisplay.minMaxTimestamp[1] - globalDisplay.minMaxTimestamp[0]) * stripChartScaleFactorX;
            }
            float valueX = 0.0F;

            // scale for each possible trace (speed; X, Y, Z accel)
            stripChartScaleY.Clear();
            stripChartScaleY.Add((float)(stripChartPanel.Height - (stripChartPanelBorder * 2)) / (float)Math.Abs(globalDisplay.minMaxSpeed[1] - globalDisplay.minMaxSpeed[0]));
            stripChartScaleY.Add((float)(stripChartPanel.Height - (stripChartPanelBorder * 2)) / (float)Math.Abs(globalDisplay.minMaxAccel[0][1] - globalDisplay.minMaxAccel[0][0]));
            stripChartScaleY.Add((float)(stripChartPanel.Height - (stripChartPanelBorder * 2)) / (float)Math.Abs(globalDisplay.minMaxAccel[1][1] - globalDisplay.minMaxAccel[1][0]));
            stripChartScaleY.Add((float)(stripChartPanel.Height - (stripChartPanelBorder * 2)) / (float)Math.Abs(globalDisplay.minMaxAccel[2][1] - globalDisplay.minMaxAccel[2][0]));
            float[] minRangeY = new float[4] {  0.0F, 0.0F ,  0.0F, 0.0F};
            minRangeY[0] = globalDisplay.minMaxSpeed[0];
            minRangeY[1] = globalDisplay.minMaxAccel[0][0];
            minRangeY[2] = globalDisplay.minMaxAccel[1][0];
            minRangeY[3] = globalDisplay.minMaxAccel[2][0];

            stripChartExtents[0] = -1.0F * stripChartOffset[0];
            stripChartExtents[1] = ((float)stripChartPanel.Width / (stripChartScaleX * stripChartScaleFactorX)) - stripChartOffset[0];

            float[] valueY = new float[4] { 0.0F, 0.0F, 0.0F, 0.0F};
            using (Graphics mapGraphics = stripChartPanel.CreateGraphics())
            {
                int runCount = 0;
                foreach (SortedList<float, DataBlock> curPath in logEvents)
                {
                    if ((bool)runDataGrid.Rows[runCount].Cells["colShowRun"].Value == false)
                    {
                        runCount++;
                        continue;
                    }
                    drawPen = new Pen(runDisplay[runCount].runColor);

                    initialGPS = false;
                    for(int ptIdx = 0; ptIdx < startPt.Count(); ptIdx++)
                    {
                        startPt[ptIdx].X = 0.0F;
                        startPt[ptIdx].Y = 0.0F;
                    }
                    foreach (KeyValuePair<float, DataBlock> curData in curPath)
                    {
                        if (!(curData.Value.gps.isValid) && !(curData.Value.accel.isValid))
                        {
                            continue;
                        }
                        valueX = curData.Value.micros;
                        if (curData.Value.gps.isValid)
                        {
                            valueY[0] = curData.Value.gps.mph;
                        }
                        if (curData.Value.accel.isValid)
                        {
                            valueY[1] = curData.Value.accel.xAccel;
                            valueY[2] = curData.Value.accel.yAccel;
                            valueY[3] = curData.Value.accel.zAccel;
                        }
                        for (int ptIdx = 0; ptIdx < startPt.Count(); ptIdx++)
                        {
                            endPt[ptIdx].X = valueX - (float)globalDisplay.minMaxTimestamp[0];
                            endPt[ptIdx].Y = valueY[ptIdx] - minRangeY[ptIdx];

                            endPt[ptIdx] = globalDisplay.ScaleDataToDisplay(endPt[ptIdx],
                                                                       stripChartScaleX * stripChartScaleFactorX,
                                                                       stripChartScaleY[ptIdx] * stripChartScaleFactorY,
                                                                       stripChartOffset[0] + runDisplay[runCount].stipchart_Offset[0],
                                                                       stripChartOffset[1] + runDisplay[runCount].stipchart_Offset[1],
                                                                       stripChartPanelBounds);
                        }


                        if (initialGPS)
                        {
                            if (chkSpeed.Checked)
                            {
                                mapGraphics.DrawLine(drawPen, startPt[0], endPt[0]);
                            }
                            if (chkXAccel.Checked)
                            {
                                mapGraphics.DrawLine(drawPen, startPt[1], endPt[1]);
                            }
                            if (chkYAccel.Checked)
                            {
                                mapGraphics.DrawLine(drawPen, startPt[2], endPt[2]);
                            }
                            if (chkZAccel.Checked)
                            {
                                mapGraphics.DrawLine(drawPen, startPt[3], endPt[3]);
                            }
                        }
                        else
                        {
                            initialGPS = true;
                        }
                        for (int ptIdx = 0; ptIdx < startPt.Count(); ptIdx++)
                        {
                            startPt[ptIdx].X = endPt[ptIdx].X;
                            startPt[ptIdx].Y = endPt[ptIdx].Y;
                        }
                    }
                    runCount++;
                }
            }
        }
        private void stripChart_MouseMove(object sender, MouseEventArgs e)
        {
            stripChartPanel.Focus();
            #region left mouse button down - dragging view 
            if (e.Button == MouseButtons.Left)
            {
                if (!stripChartStartPosValid)
                {
                    stripChartStartPosValid = true;
                    stripChartStartPos = e.Location;
                    stripChartStartCursorPos.X = StripChartMousePositionToXAxis(e.Location);
                }
                stripChartLastCursorPos.X = StripChartMousePositionToXAxis(e.Location);
                stripChartLastCursorPosInt.X = e.Location.X;
            }
            #endregion
            else
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

                    if (stripChartLastCursorPosValid)
                    {
                        Gdi32.MoveToEx(hDC, Convert.ToInt32(stripChartLastCursorPosInt.X), 0, lpPoint);
                        Gdi32.LineTo(hDC, Convert.ToInt32(stripChartLastCursorPosInt.X), stripChartPanel.Height);
                    }

                    Gdi32.MoveToEx(hDC, e.Location.X, 0, lpPoint);
                    Gdi32.LineTo(hDC, e.Location.X, stripChartPanel.Height);

                    Gdi32.SelectObject(hDC, oldPen);
                    Gdi32.SelectObject(hDC, oldBrush);
                    Gdi32.DeleteObject(newPen);
                    Gdi32.DeleteObject(newBrush);
                    drawGraphics.ReleaseHdc();
                }
                #endregion
                StringBuilder positionStr = new StringBuilder();
                float position = Convert.ToSingle(StripChartMousePositionToXAxis(e.Location));
                positionStr.AppendFormat("{0} range {1} to {2}", position, stripChartExtents[0], stripChartExtents[1]);
                txtCursorPos.Text = positionStr.ToString();
                TrackMapUpdateCursor(position);
                TractionCircleUpdateCursor(position);
            }
            stripChartLastCursorPos.X = StripChartMousePositionToXAxis(e.Location);
            stripChartLastCursorPosInt.X = e.Location.X;
            stripChartLastCursorPosValid = true;
        }
        private void stripChart_MouseUp(object sender, MouseEventArgs e)
        {
            if(stripChartStartPosValid)
            {
                // strip chart offset in X axis units (scaled)
                // drag position
                //stripChartOffset[0] += ((float)(e.Location.X - stripChartStartPos.X) / stripChartScaleX);
                // drag zoom
                PointF scaledStart = new PointF(stripChartStartCursorPos.X, stripChartStartCursorPos.Y);
                PointF scaledEnd = new PointF(stripChartLastCursorPos.X, stripChartLastCursorPos.Y);

                stripChartOffset[0] = -1.0F *( scaledStart.X < scaledEnd.X ? scaledStart.X : scaledEnd.X);// (float)e.Location.X / (stripChartScaleX * stripChartScaleFactorX);
                stripChartExtents[0] = scaledStart.X < scaledEnd.X ? scaledStart.X : scaledEnd.X;
                stripChartExtents[1] = scaledStart.X < scaledEnd.X ? scaledEnd.X : scaledStart.X;

                System.Diagnostics.Debug.Write("Zoom from " + scaledStart.ToString() + "  to " + scaledEnd.ToString() + " original scale " + stripChartScaleX.ToString());

                stripChartScaleX = (float)stripChartPanelBounds.Width / Math.Abs(scaledEnd.X - scaledStart.X);

                System.Diagnostics.Debug.Write("new scale " + stripChartScaleX.ToString());
                System.Diagnostics.Debug.WriteLine("");

                stripChartScaleFactorX = 1.0F;

                stripChartStartPosValid = false;
                stripChartLastPosValid = false;
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
            stripChartLastPosValid = false;
            stripChartLastCursorPosValid = false;
        }
        private void stripChart_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '+')
            {
                stripChartScaleFactorX *= 1.05F;
                stripChartPanel.Invalidate();
                tractionCirclePanel.Invalidate();
                mapPanel.Invalidate();
            }
            else if (e.KeyChar == '-')
            {
                stripChartScaleFactorX *= 0.95F;
                stripChartPanel.Invalidate();
                tractionCirclePanel.Invalidate();
                mapPanel.Invalidate();
            }
            else if ((e.KeyChar == '1') || (e.KeyChar == 'R') || (e.KeyChar == 'r'))
            {
                stripChartScaleFactorX = 1.0F;
                stripChartScaleX = (float)stripChartPanelBounds.Width / (globalDisplay.minMaxTimestamp[1] - globalDisplay.minMaxTimestamp[0]);
                stripChartOffset[0] = 0;
                stripChartOffset[1] = 0;
                stripChartExtents[0] = 0.0F;
                stripChartExtents[1] = globalDisplay.minMaxTimestamp[1] - globalDisplay.minMaxTimestamp[0];

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
        /// <summary>
        /// convert mouse position to run time value
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        private float StripChartMousePositionToXAxis(Point mousePos)
        {
            if ((globalDisplay.minMaxSpeed[0] == float.MaxValue) &&
                (globalDisplay.minMaxTimestamp[0] == ulong.MaxValue))
            {
                return 0.0F;
            }

            float xAxisAtMouse = 0.0F;
            xAxisAtMouse = ((float)(mousePos.X) / (stripChartScaleX * stripChartScaleFactorX)) - stripChartOffset[0];
            return xAxisAtMouse;
        }
        #endregion
        #region checkbox events
        private void chkTime_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTime.Checked)
            {
                chkDist.Checked = false;
                stripChartPanel.Invalidate();
            }
        }
        private void chkDist_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDist.Checked)
            {
                chkTime.Checked = false;
                stripChartPanel.Invalidate();
            }
        }
        private void chkSpeed_CheckedChanged(object sender, EventArgs e)
        {
            stripChartPanel.Invalidate();
        }
        private void chkXAccel_CheckedChanged(object sender, EventArgs e)
        {
            stripChartPanel.Invalidate();
        }
        private void chkYAccel_CheckedChanged(object sender, EventArgs e)
        {
            stripChartPanel.Invalidate();
        }
        private void chkZAccel_CheckedChanged(object sender, EventArgs e)
        {
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
    }

    public partial class GlobalDisplay_Data
    {
        public float[] minMaxLong = new float[] { float.MaxValue, float.MinValue };
        public float[] minMaxLat = new float[] { float.MaxValue, float.MinValue };
        public float[][] minMaxAccel = new float[][] {new float[] {float.MaxValue, float.MinValue},
                                       new float[] {float.MaxValue, float.MinValue},
                                       new float[] {float.MaxValue, float.MinValue}};
        public float[] minMaxTimestamp = new float[] { float.MaxValue, float.MinValue };
        public float[] minMaxSpeed = new float[] { float.MaxValue, float.MinValue };
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
            if (accel.isValid)
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

        public PointF ScaleDataToDisplay(PointF sourcePt, float scaleX, float scaleY, float offsetX, float offsetY, Rectangle bounds)
        {
            PointF rPt = new PointF(sourcePt.X, sourcePt.Y);
            rPt.X = (rPt.X + offsetX) * scaleX + bounds.X;
            rPt.Y = bounds.Height - ((rPt.Y - offsetY) * scaleY) + bounds.Y;
            return rPt;
        }
        public PointF ScaleDisplayToData(PointF sourcePt, float scaleX, float scaleY, float offsetX, float offsetY, Rectangle bounds)
        {
            PointF rPt = new PointF(sourcePt.X, sourcePt.Y);

            rPt.X = (rPt.X / scaleX) - offsetX;
            rPt.Y = bounds.Height - ((rPt.Y - offsetY) * scaleY);
            return rPt;
        }
    }

    public partial class RunDisplay_Data
    {
        public Color runColor = Color.Black;
        public float[] stipchart_Offset = new float[] { 0.0F, 0.0F };
        public float[] minMaxLong = new float[] { float.MaxValue, float.MinValue };
        public float[] minMaxLat = new float[] { float.MaxValue, float.MinValue };
        public float[][] minMaxAccel = new float[][] {new float[] {float.MaxValue, float.MinValue},
                                       new float[] {float.MaxValue, float.MinValue},
                                       new float[] {float.MaxValue, float.MinValue}};
        public float[] minMaxTimestamp = new float[] { float.MaxValue, float.MinValue };
        public float[] minMaxSpeed = new float[] { float.MaxValue, float.MinValue };
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
            if (accel.isValid)
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
}