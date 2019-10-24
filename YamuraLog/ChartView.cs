using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDI;
using Win32Interop.Methods;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing.Drawing2D;

namespace YamuraView
{
    public partial class ChartView : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public event ChartMouseMove ChartMouseMoveEvent;

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
        public enum CursorStyle
        {
            NONE,
            CROSSHAIRS,
            VERTICAL,
            HORIZONTAL,
            BOX,
            CIRCLE
        }

        public DataLogger Logger
        {
            get
            {
                return YamuraView.YamuraViewMain.dataLogger;
            }
        }

        ChartControl chartOwner;
        public ChartControl ChartOwner
        {
            get { return chartOwner; }
            set { chartOwner = value; }
        }

        string chartName = "Chart";
        public string ChartName
        {
            get { return chartName; }
            set
            {
                chartName = value;
                Text = chartName;
            }
        }

        protected int dragZoomPenWidth = 1;
        protected int chartBorder = 10;
        protected Rectangle chartBounds = new Rectangle(0, 0, 0, 0);
        protected List<bool> startMouseDrag = new List<bool>();
        protected List<bool> startMouseMove = new List<bool>();
        protected List<Point> chartLastCursorPos = new List<Point>();
        protected List<Point> chartStartCursorPos = new List<Point>();
        protected int axisThickness = 56;

        string xChannelName;
        public string XChannelName
        {
            get { return xChannelName; }
            set { xChannelName = value; }
        }
        //
        // chart display properties 
        //
        bool allowDrag = true;
        /// <summary>
        /// chart allows drag area with left mouse button
        /// </summary>
        public bool AllowDrag
        {
            get
            {
                return allowDrag;
            }

            set
            {
                allowDrag = value;
            }
        }
        // crosshairs, box, circle work
        // horizontal and vertical look weird
        CursorStyle cursorMode = CursorStyle.CROSSHAIRS;
        /// <summary>
        /// type of mouse move cursor - CROSSHAIRS, HORIZONTAL, VERTICAL, BOX, CIRCLE
        /// </summary>
        public CursorStyle CursorMode
        {
            get { return cursorMode; }
            set { cursorMode = value; }
        }
        int cursorBoxSize = 10;
        /// <summary>
        /// size of box and circle cursor
        /// </summary>
        public int CursorBoxSize
        {
            get { return cursorBoxSize; }
            set { cursorBoxSize = value; }
        }
        bool cursorUpdateSource = true;
        /// <summary>
        /// chart accepts mouse moves and raises cursor update events for listeners
        /// </summary>
        public bool CursorUpdateSource
        {
            get
            {
                return cursorUpdateSource;
            }
            set
            {
                cursorUpdateSource = value;
            }
        }
        bool showHScroll = true;
        /// <summary>
        /// show the H scrollbar
        /// </summary>
        public bool ShowHScroll
        {
            get { return showHScroll; }
            set
            {
                showHScroll = value;
                UpdateElementPositions();
            }
        }
        bool showVScroll = true;
        /// <summary>
        /// show the V scrollbar
        /// </summary>
        public bool ShowVScroll
        {
            get { return showVScroll; }
            set
            {
                showVScroll = value;
                UpdateElementPositions();
            }
        }
        bool equalScale = false;
        // true if X and Y scale are equal regardless of window size
        public bool EqualScale
        {
            get { return equalScale; }
            set { equalScale = value; }
        }
        public ChartView()
        {
            InitializeComponent();
            xChannelName = "Time";
            CursorUpdateSource = true;

            chartLastCursorPos.Add(new Point(0, 0));
            chartStartCursorPos.Add(new Point(0, 0));
            startMouseMove.Add(false);
            startMouseDrag.Add(false);
            xAxis1.axisScroll.Scroll += HScrollBar_Scroll;
        }
        #region control message handlers
        /// <summary>
        /// when the control sizes, update positions of the axes and chart it contains
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ChartView_Resize(object sender, EventArgs e)
        {
            UpdateElementPositions();
        }
        #endregion

        #region chart message handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void chartPanel_Paint(object sender, PaintEventArgs e)
        {
            if (ChartOwner.ChartType == ChartControl.ChartControlType.Stripchart)
            {
                DrawStripChart();
            }
            else if (ChartOwner.ChartType == ChartControl.ChartControlType.TractionCirle)
            {
                DrawTractionCircle();
            }
        }
        void DrawStripChart()
        { 
            #region initialize mouse/cursor moves
                for (int moveIdx = 0; moveIdx < startMouseMove.Count; moveIdx++)
            {
                startMouseMove[moveIdx] = false;
                startMouseDrag[moveIdx] = false;
                chartStartCursorPos[moveIdx] = new Point(0, 0);
                chartLastCursorPos[moveIdx] = new Point(0, 0);
            }
            #endregion
            // nothing to display yet
            if ((ChartOwner.ChartAxes == null) || (ChartOwner.ChartAxes.Count == 0) || (Logger == null))
            {
                return;
            }
            bool initialValue = false;
            // x and y scale
            float[] displayScale = new float[] { 1.0F, 1.0F };

            PointF[] points = new PointF[] { new PointF(), new PointF() };
            Pen pathPen = new Pen(Color.Red);

            // process each axis
            foreach (KeyValuePair<string, Axis> yAxis in ChartOwner.ChartAxes)
            {
                #region skip axis if not displayed
                if (!yAxis.Value.ShowAxis)
                {
                    continue;
                }
                #endregion
                // process each associated channel
                foreach (KeyValuePair<String, ChannelInfo> curChanInfo in yAxis.Value.AssociatedChannels)
                {
                    #region skip if channel is not displayed
                    if (!curChanInfo.Value.ShowChannel)
                    {
                        continue;
                    }
                    #endregion
                    #region build unscaled path
                    if ((curChanInfo.Value.ChannelPath == null) || (curChanInfo.Value.ChannelPath.PointCount == 0))
                    {
                        DataChannel curChannel = Logger.runData[curChanInfo.Value.RunIndex].channels[curChanInfo.Value.ChannelName];
                        initialValue = true;
                        foreach (KeyValuePair<float, DataPoint> curData in curChannel.DataPoints)
                        {
                            // x axis is time - direct lookup
                            if (xChannelName == "Time")
                            {
                                points[1] = new PointF (curData.Key, curData.Value.PointValue);
                            }
                            // x axis is not time - find nearest time in axis channel, 
                            else
                            {
                                DataPoint tst = Logger.runData[curChanInfo.Value.RunIndex].channels[xChannelName].dataPoints.FirstOrDefault(i => i.Key >= curData.Key).Value;
                                if (tst == null)
                                {
                                    continue;
                                }
                                points[1] = new PointF(tst.PointValue, curData.Value.PointValue);
                            }
                            if (initialValue)
                            {
                                initialValue = false;
                                points[0] = new PointF(points[1].X, points[1].Y);
                                continue;
                            }
                            curChanInfo.Value.ChannelPath.AddLine(points[0], points[1]);
                            points[0] = new PointF(points[1].X, points[1].Y);
                        }
                    }
                    #endregion
                    #region draw to transformed graphic context
                    pathPen = new Pen(curChanInfo.Value.ChannelColor);
                    using (Graphics chartGraphics = chartPanel.CreateGraphics())
                    {
                        displayScale[0] = (float)chartBounds.Width / ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];
                        displayScale[1] = (float)chartBounds.Height / yAxis.Value.AxisDisplayRange[2];
                        if(EqualScale)
                        {
                            if(displayScale[0] < displayScale[1])
                            {
                                displayScale[1] = displayScale[0];
                            }
                            else
                            {
                                displayScale[0] = displayScale[1];
                            }
                        }
                        displayScale[1] *= -1.0F;

                        // translate to lower left corner of display area
                        chartGraphics.TranslateTransform(chartBorder,  (float)chartBounds.Height + chartBorder);
                        // scale to display range in X and Y
                        chartGraphics.ScaleTransform(displayScale[0], displayScale[1]);
                        // translate by -1 * minimum display range + axis offset (scrolling)
                        chartGraphics.TranslateTransform(-1 * ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0] +
                                                         ChartOwner.ChartAxes[xChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[0],  // offset X
                                                         -1* yAxis.Value.AxisDisplayRange[0] +
                                                         ChartOwner.ChartAxes[xChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[1]);  // offset Y
                        // set pen width to 0 (1 pixel)
                        pathPen.Width = 0;
                        // draw the path
                        chartGraphics.DrawPath(pathPen, curChanInfo.Value.ChannelPath);
                        // reset to original orientation
                        chartGraphics.ResetTransform();
                    }
                    #endregion
                }
            }
        }
        void DrawTractionCircle()
        {
            #region initialize mouse/cursor moves
            for (int moveIdx = 0; moveIdx < startMouseMove.Count; moveIdx++)
            {
                startMouseMove[moveIdx] = false;
                startMouseDrag[moveIdx] = false;
                chartStartCursorPos[moveIdx] = new Point(0, 0);
                chartLastCursorPos[moveIdx] = new Point(0, 0);
            }
            #endregion
            // nothing to display yet
            if ((ChartOwner.ChartAxes == null) || (ChartOwner.ChartAxes.Count == 0))
            {
                return;
            }
            // x and y scale
            float[] displayScale = new float[] { 1.0F, 1.0F };

            PointF[] points = new PointF[] { new PointF(), new PointF() };
            Pen pathPen = new Pen(Color.Red);

            // process each axis
            foreach (KeyValuePair<string, Axis> yAxis in ChartOwner.ChartAxes)
            {
                #region skip axis if not displayed
                if (!yAxis.Value.ShowAxis)
                {
                    continue;
                }
                #endregion
                // process each associated channel
                foreach (KeyValuePair<String, ChannelInfo> curChanInfo in yAxis.Value.AssociatedChannels)
                {
                    #region skip if channel is not displayed
                    if (!curChanInfo.Value.ShowChannel)
                    {
                        continue;
                    }
                    #endregion
                    //        #region build unscaled path
                    //        if ((curChanInfo.Value.ChannelPath == null) || (curChanInfo.Value.ChannelPath.PointCount == 0))
                    //        {
                    //            DataChannel curChannel = Logger.runData[curChanInfo.Value.RunIndex].channels[curChanInfo.Value.ChannelName];
                    //            initialValue = true;
                    //            foreach (KeyValuePair<float, DataPoint> curData in curChannel.DataPoints)
                    //            {
                    //                // x axis is time - direct lookup
                    //                if (XChannelName == "Time")
                    //                {
                    //                    points[1] = new PointF (curData.Key, curData.Value.PointValue);
                    //                }
                    //                // x axis is not time - find nearest time in axis channel, 
                    //                else
                    //                {
                    //                    DataPoint tst = Logger.runData[curChanInfo.Value.RunIndex].channels[XChannelName].dataPoints.FirstOrDefault(i => i.Key >= curData.Key).Value;
                    //                    if (tst == null)
                    //                    {
                    //                        continue;
                    //                    }
                    //                    points[1] = new PointF(tst.PointValue, curData.Value.PointValue);
                    //                }
                    //                if (initialValue)
                    //                {
                    //                    initialValue = false;
                    //                    points[0] = new PointF(points[1].X, points[1].Y);
                    //                    continue;
                    //                }
                    //                curChanInfo.Value.ChannelPath.AddLine(points[0], points[1]);
                    //                points[0] = new PointF(points[1].X, points[1].Y);
                    //            }
                    //        }
                    //        #endregion
                    #region draw to transformed graphic context
                    pathPen = new Pen(Color.Black);
                    using (Graphics chartGraphics = chartPanel.CreateGraphics())
                    {
                        displayScale[0] = (float)chartBounds.Width / ChartOwner.ChartAxes[XChannelName].AxisDisplayRange[2];
                        displayScale[1] = (float)chartBounds.Height / yAxis.Value.AxisDisplayRange[2];
                        if (EqualScale)
                        {
                            if (displayScale[0] < displayScale[1])
                            {
                                displayScale[1] = displayScale[0];
                            }
                            else
                            {
                                displayScale[0] = displayScale[1];
                            }
                        }
                        displayScale[1] *= -1.0F;

                        // translate to lower left corner of display area
                        chartGraphics.TranslateTransform(chartBorder, (float)chartBounds.Height + chartBorder);
                        // scale to display range in X and Y
                        chartGraphics.ScaleTransform(displayScale[0], displayScale[1]);
                        // translate by -1 * minimum display range + axis offset (scrolling)
                        chartGraphics.TranslateTransform(-1 * ChartOwner.ChartAxes[XChannelName].AxisDisplayRange[0] +
                                                         ChartOwner.ChartAxes[XChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + XChannelName].AxisOffset[0],  // offset X
                                                         -1 * yAxis.Value.AxisDisplayRange[0] +
                                                         ChartOwner.ChartAxes[XChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + XChannelName].AxisOffset[1]);  // offset Y
                        // set pen width to 0 (1 pixel)
                        pathPen.Width = 0;
                        // draw the path
                        chartGraphics.DrawLine(pathPen, -2, 0, 2, 0);
                        chartGraphics.DrawLine(pathPen, 0, -2, 0, 2);
                        chartGraphics.DrawEllipse(pathPen, -2, -2, 4, 4);
                        // reset to original orientation
                        chartGraphics.ResetTransform();
                    }
                    #endregion
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void chartPanel_Resize(object sender, EventArgs e)
        {
            // update the paintable area
            chartBounds.X = chartBorder;
            chartBounds.Y = chartBorder;
            chartBounds.Width = chartPanel.Width - (2 * chartBorder);
            chartBounds.Height = chartPanel.Height - (2 * chartBorder);
            // redraw
            chartPanel.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void chartPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (CursorUpdateSource == false)
            {
                return;
            }
            chartPanel.Focus();
            #region left mouse button down - dragging zoom region 
            if ((e.Button == MouseButtons.Left) && AllowDrag)
            {
                // starting mouse drag - erase old cursor if needed, save initial start and end locations
                if (!startMouseDrag[0])
                {
                    // was moving mouse with left button up, erase cursor
                    if (startMouseMove[0])
                    {
                        DrawCursorAt(chartLastCursorPos[0].X, chartLastCursorPos[0].Y);
                    }
                    // save location
                    chartStartCursorPos[0] = new Point (e.Location.X, 0);
                    chartLastCursorPos[0] = new Point(e.Location.X, chartPanel.Height);
                }
                // continue mouse drag - erase last box
                else
                {
                    DrawSelectArea(chartStartCursorPos[0].X, chartStartCursorPos[0].Y, chartLastCursorPos[0].X, chartLastCursorPos[0].Y);
                }
                // continue mouse drag, save current end location and draw new box
                chartLastCursorPos[0] = new Point(e.Location.X, chartLastCursorPos[0].Y);
                DrawSelectArea(chartStartCursorPos[0].X, chartStartCursorPos[0].Y, chartLastCursorPos[0].X, chartLastCursorPos[0].Y);
                // mouse drag has started, mouse move has stopped
                startMouseDrag[0] = true;
                startMouseMove[0] = false;
            }
            #endregion
            #region just moving the mouse with no buttons
            else
            {
                if (CursorMode != CursorStyle.NONE)
                {
                    startMouseDrag[0] = false;
                    #region erase if something was drawn
                    if (!startMouseMove[0])
                    {
                        chartLastCursorPos[0] = e.Location;
                    }
                    else
                    {
                        DrawCursorAt(chartLastCursorPos[0].X, chartLastCursorPos[0].Y);
                    }
                    #endregion
                    #region draw new cursor if in chart area
                    if (((e.Location.X >= chartBorder) && (e.Location.X <= (chartPanel.Width - chartBorder))) &&
                        ((e.Location.Y >= chartBorder) && (e.Location.Y <= (chartPanel.Height - chartBorder))))
                    {
                        startMouseMove[0] = true;
                        chartLastCursorPos[0] = e.Location;
                        DrawCursorAt(chartLastCursorPos[0].X, chartLastCursorPos[0].Y);
                    }
                    // outside of chart, don't draw and not started
                    else
                    {
                        startMouseMove[0] = false;
                    }
                    #endregion
                }
            }
            #endregion
            // up to now, dealing in screen units. convert current position to X axis value and active Y axis values and raise message
            if (ChartOwner.ChartAxes.Count == 0)
            {
                return;
            }
            // create event args
            ChartControlMouseMoveEventArgs moveEventArgs = new ChartControlMouseMoveEventArgs();

            PointF axisPoint = new PointF();
            // x and y scale
            float[] displayScale = new float[] { 1.0F, 1.0F };
            displayScale[0] = (float)chartBounds.Width / ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];

            foreach (KeyValuePair<string, ChannelInfo> channel in ChartOwner.ChartAxes[xChannelName].AssociatedChannels)
            {
                displayScale[1] = 1.0F;

                axisPoint.X = (float)e.Location.X;
                axisPoint.Y = (float)e.Location.Y;

                axisPoint = ScaleDisplayToData(axisPoint,
                                   displayScale[0],
                                   displayScale[1],
                                   0.0F,
                                   0.0F,
                                   chartBounds);
                axisPoint.X -= (-1 * ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0] +
                                       ChartOwner.ChartAxes[xChannelName].AssociatedChannels[channel.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[0]);  // offset X

                moveEventArgs.XAxisValues.Add(channel.Value.RunIndex.ToString() + "-" + channel.Value.ChannelName, axisPoint.X);
            }
            foreach (KeyValuePair<string, Axis> axis in ChartOwner.ChartAxes)
            {
                if (!axis.Value.ShowAxis)
                {
                    continue;
                }
                foreach (KeyValuePair<string, ChannelInfo> channel in axis.Value.AssociatedChannels)
                {
                    displayScale[1] = (float)chartBounds.Height / axis.Value.AxisDisplayRange[2];
                    axisPoint.X = (float)e.Location.X;
                    axisPoint.Y = (float)e.Location.Y;

                    axisPoint = ScaleDisplayToData(axisPoint,
                                       displayScale[0],
                                       displayScale[1],
                                       0.0F,
                                       0.0F,
                                       chartBounds);

                    axisPoint.Y -= (channel.Value.AxisRange[0] +
                                       ChartOwner.ChartAxes[xChannelName].AssociatedChannels[channel.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[1]);

                    moveEventArgs.YAxisValues.Add(channel.Value.RunIndex.ToString() + "-" + channel.Value.ChannelName, axisPoint.Y);
                }
            }
            ChartMouseMoveEvent(this, moveEventArgs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void chartPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if ((startMouseDrag[0]) && AllowDrag)
            {
                // original scaling
                float[] displayScale = new float[] { 1.0F, 1.0F };
                displayScale[0] = (float)chartBounds.Width / ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];

                PointF scaledStart = chartStartCursorPos[0];
                PointF scaledEnd = chartLastCursorPos[0];
                scaledStart = ScaleDisplayToData(scaledStart,
                                   displayScale[0],
                                   displayScale[1],
                                   0.0F,
                                   0.0F,
                                   chartBounds);
                scaledEnd = ScaleDisplayToData(scaledEnd,
                                   displayScale[0],
                                   displayScale[1],
                                   0.0F,
                                   0.0F,
                                   chartBounds);

                ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0] = scaledStart.X < scaledEnd.X ? scaledStart.X : scaledEnd.X;
                ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[1] = scaledStart.X < scaledEnd.X ? scaledEnd.X : scaledStart.X;
                ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2] = ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[1] - ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0];


                xAxis1.Minimum = (int)ChartOwner.ChartAxes[xChannelName].AxisValueRange[0];
                xAxis1.Maximum = (int)ChartOwner.ChartAxes[xChannelName].AxisValueRange[1];
                xAxis1.Value = (int)ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0];
                xAxis1.LargeChange = (int)ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];
                startMouseDrag[0] = false;
                chartPanel.Invalidate();
            }
        }
        #endregion

        #region scollbar message handlers
        internal void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            // original scaling
            float[] displayScale = new float[] { 1.0F, 1.0F };
            displayScale[0] = (float)chartBounds.Width / ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];

            PointF scaledStart = new PointF(e.NewValue, 0);
            ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0] = scaledStart.X;
            ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[1] = scaledStart.X + ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];
            chartPanel.Invalidate();

        }
        #endregion

        #region GDI support
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static uint RGB(Color color)
        {
            uint rgb = (uint)(color.R + (color.G << 8) + (color.B << 16));
            return rgb;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static uint NotRGB(Color color)
        {
            uint rgb = (uint)(color.R + (color.G << 8) + (color.B << 16));
            rgb = ~rgb & 0xFFFFFF;
            return rgb;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal void DrawCursorAt(int x, int y)
        {
            Rectangle locationBox = new Rectangle(0, 0, 0, 0);
            IntPtr lpPoint = new IntPtr();
            lpPoint = IntPtr.Zero;
            using (Graphics drawGraphics = chartPanel.CreateGraphics())
            {
                #region create GDI objects
                IntPtr hDC = drawGraphics.GetHdc();
                IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                dragZoomPenWidth,
                                                NotRGB(Color.Black));
                IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.BLACK_BRUSH);
                IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);
                #endregion
                #region crosshairs
                if (cursorMode == CursorStyle.CROSSHAIRS)
                {
                    // horizontal line
                    Gdi32.MoveToEx(hDC, 0, y, lpPoint);
                    Gdi32.LineTo(hDC, chartPanel.Width, y);
                    // vertical line
                    Gdi32.MoveToEx(hDC, x, chartPanel.Height, lpPoint);
                    Gdi32.LineTo(hDC, x, 0);
                }
                #endregion
                #region box
                else if (cursorMode == CursorStyle.BOX)
                {
                    locationBox = new Rectangle(x - (cursorBoxSize / 2), y - (cursorBoxSize / 2), cursorBoxSize, cursorBoxSize);
                    Gdi32.Rectangle(hDC, locationBox.Left, locationBox.Top, locationBox.Left + cursorBoxSize, locationBox.Top + cursorBoxSize);
                }
                #endregion
                #region circle
                else if (cursorMode == CursorStyle.CIRCLE)
                {
                    locationBox = new Rectangle(x - (cursorBoxSize / 2), y - (cursorBoxSize / 2), cursorBoxSize, cursorBoxSize);
                    Gdi32.Ellipse(hDC, locationBox.Left, locationBox.Top, locationBox.Left + cursorBoxSize, locationBox.Top + cursorBoxSize);
                }
                #endregion
                #region horizontal line
                else if (cursorMode == CursorStyle.HORIZONTAL)
                {
                    // horizontal line
                    Gdi32.MoveToEx(hDC, 0, y, lpPoint);
                    Gdi32.LineTo(hDC, chartPanel.Width, y);
                }
                #endregion
                #region vertical line
                else if (cursorMode == CursorStyle.VERTICAL)
                {
                    // vertical line
                    Gdi32.MoveToEx(hDC, x, 0, lpPoint);
                    Gdi32.LineTo(hDC, x, chartPanel.Height);
                }
                #endregion
                #region clean up GDI, reset context
                Gdi32.SelectObject(hDC, oldPen);
                Gdi32.SelectObject(hDC, oldBrush);
                Gdi32.DeleteObject(newPen);
                Gdi32.DeleteObject(newBrush);
                drawGraphics.ReleaseHdc();
                #endregion
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromX"></param>
        /// <param name="fromY"></param>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        internal void DrawSelectArea(int fromX, int fromY, int toX, int toY)
        {
            using (Graphics drawGraphics = chartPanel.CreateGraphics())
            {
                #region create GDI objects
                IntPtr hDC = drawGraphics.GetHdc();
                IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                dragZoomPenWidth,
                                                NotRGB(Color.Gray));
                IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.GRAY_BRUSH);
                IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                Gdi32.SetROP2(hDC, (int)DrawMode.R2_XORPEN);
                #endregion
                Gdi32.Rectangle(hDC, fromX, fromY, toX, toY);
                #region clean up GDI restore context
                Gdi32.SelectObject(hDC, oldPen);
                Gdi32.SelectObject(hDC, oldBrush);
                Gdi32.DeleteObject(newPen);
                Gdi32.DeleteObject(newBrush);
                drawGraphics.ReleaseHdc();
                #endregion
            }
        }
        #endregion

        #region scaling support
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
        internal PointF ScaleDataToDisplay(PointF sourcePt, float scaleX, float scaleY, float offsetX, float offsetY, Rectangle bounds)
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
        internal PointF ScaleDisplayToData(PointF sourcePt, float scaleX, float scaleY, float offsetX, float offsetY, Rectangle bounds)
        {
            PointF rPt = new PointF(sourcePt.X, sourcePt.Y);
            rPt.X = (rPt.X / scaleX) - offsetX;
            rPt.Y = -1.0F * ((rPt.Y - (float)bounds.Height) / scaleY) - offsetY;
            return rPt;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void UpdateElementPositions()
        {
            Rectangle chartRect = new Rectangle();
            // default shown positions
            xAxis1.Height = axisThickness;
            xAxis1.Width = Width - axisThickness;
            xAxis1.Location = new Point(axisThickness, Height - axisThickness);

            yAxis1.Height = Height - axisThickness;
            yAxis1.Width = axisThickness;
            yAxis1.Location = new Point(0, 0);

            chartRect.X = axisThickness;
            chartRect.Y = 0;
            chartRect.Width = Width - axisThickness;
            chartRect.Height = Height - axisThickness;

            if (!showHScroll)
            {
                xAxis1.Visible = false;
                chartRect.Height = Height;
            }
            if (!showVScroll)
            {
                yAxis1.Visible = false;
                chartRect.X = 0;
                chartRect.Width = Width;
            }

            chartPanel.Location = chartRect.Location;
            chartPanel.Width = chartRect.Width;
            chartPanel.Height = chartRect.Height;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnChartMouseMove(object sender, ChartControlMouseMoveEventArgs e)
        {
            #region move cursor(s)
            // position in event args is data - need to scale to screen
            float[] displayScale = new float[] { 1.0F, 1.0F };
            PointF endPt = new PointF();
            int axisIdx = 0;
            string axisFullName = "";
            int cursorIdx = -1;
            if (CursorMode != CursorStyle.NONE)
            {
                // check each Y axis
                foreach (KeyValuePair<string, Axis> yAxis in ChartOwner.ChartAxes)
                {
                    // skip if axis is not displayed
                    if (!yAxis.Value.ShowAxis)
                    {
                        continue;
                    }
                    axisFullName = axisIdx.ToString() + "-" + yAxis.Key;
                    displayScale[0] = (float)chartBounds.Width / ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];
                    displayScale[1] = (float)chartBounds.Height / yAxis.Value.AxisDisplayRange[2];
                    if(EqualScale)
                    {
                        if(displayScale[0] < displayScale[1])
                        {
                            displayScale[1] = displayScale[0];
                        }
                        else
                        {
                            displayScale[0] = displayScale[1];
                        }
                    }
                    // check each associated channel
                    foreach (KeyValuePair<String, ChannelInfo> curChanInfo in yAxis.Value.AssociatedChannels)
                    {
                        // skip if channel is not displayed
                        if (!curChanInfo.Value.ShowChannel)
                        {
                            continue;
                        }
                        cursorIdx++;
                        // add new cursor info if needed
                        if (startMouseMove.Count <= cursorIdx)
                        {
                            startMouseMove.Add(false);
                            startMouseDrag.Add(false);
                            chartStartCursorPos.Add(new Point(0, 0));
                            chartLastCursorPos.Add(new Point(0, 0));
                        }
                        DataChannel curChannel = Logger.runData[curChanInfo.Value.RunIndex].channels[curChanInfo.Value.ChannelName];

                        // x axis is time - direct lookup
                        if (axisFullName == (axisIdx.ToString() + "-Time"))
                        {
                            endPt.X = e.XAxisValues[xChannelName];// curChannel.DataPoints[].PointValue;
                            endPt.Y = curChannel.DataPoints[endPt.X].PointValue;
                        }
                        // x axis is not time - find nearest time in axis channel, 
                        else
                        {
                            DataPoint tst = Logger.runData[curChanInfo.Value.RunIndex].channels[xChannelName].dataPoints.FirstOrDefault(i => i.Key >= e.XAxisValues[curChanInfo.Value.RunIndex.ToString() + "-Time"]).Value;
                            if (tst == null)
                            {
                                continue;
                            }
                            endPt.X = tst.PointValue;
                            tst = curChannel.dataPoints.FirstOrDefault(i => i.Key >= e.XAxisValues[curChanInfo.Value.RunIndex.ToString() + "-Time"]).Value;
                            if (tst == null)
                            {
                                continue;
                            }
                            endPt.Y = tst.PointValue;
                        }
                        endPt = ScaleDataToDisplay(endPt,                                                      // point
                                                   displayScale[0],                              // scale X
                                                   displayScale[1],                              // scale Y
                                                   -1 * ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0] +
                                                               ChartOwner.ChartAxes[xChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[0],  // offset X
                                                   yAxis.Value.AxisDisplayRange[0] +
                                                               ChartOwner.ChartAxes[xChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[1],  // offset Y
                                                   chartBounds);                                 // graphics area boundary


                        startMouseDrag[cursorIdx] = false;
                        #region erase if something was drawn
                        if (!startMouseMove[cursorIdx])
                        {
                            chartLastCursorPos[cursorIdx] = new Point((int)endPt.X, (int)endPt.Y);
                        }
                        else
                        {
                            DrawCursorAt(chartLastCursorPos[cursorIdx].X, chartLastCursorPos[cursorIdx].Y);
                        }
                        #endregion
                        #region draw new cursor if in chart area
                        if (((endPt.X >= chartBorder) && (endPt.X <= (chartPanel.Width - chartBorder))) &&
                            ((endPt.Y >= chartBorder) && (endPt.Y <= (chartPanel.Height - chartBorder))))
                        {
                            startMouseMove[cursorIdx] = true;
                            chartLastCursorPos[cursorIdx] = new Point((int)endPt.X, (int)endPt.Y);
                            DrawCursorAt(chartLastCursorPos[cursorIdx].X, chartLastCursorPos[cursorIdx].Y);
                        }
                        // outside of chart, don't draw and not started
                        else
                        {
                            startMouseMove[cursorIdx] = false;
                        }
                        #endregion
                    }
                }
            }
            #endregion

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnChartXAxisChange(object sender, ChartControlXAxisChangeEventArgs e)
        {
            xChannelName = e.XAxisName;
            xAxis1.Minimum = (int)ChartOwner.ChartAxes[xChannelName].AxisValueRange[0];
            xAxis1.Maximum = (int)ChartOwner.ChartAxes[xChannelName].AxisValueRange[1];
            xAxis1.Value = (int)ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0];
            xAxis1.LargeChange = (int)ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];
            chartPanel.Invalidate();
        }
        public void OnClearGraphicsPath(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Axis> yAxis in ChartOwner.ChartAxes)
            {
                foreach (KeyValuePair<String, ChannelInfo> curChanInfo in yAxis.Value.AssociatedChannels)
                {
                    curChanInfo.Value.ChannelPath = new GraphicsPath();
                }
            }
            chartPanel.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAxisOffsetUpdate(object sender, AxisOffsetUpdateEventArgs e)
        {
            // offset channel on X axis
            string associatedAxisName = e.RunIdx.ToString() + "-" + e.ChannelName;
            if(e.AxisIdx == 0)
            {
                ChartOwner.ChartAxes[e.ChannelName].AssociatedChannels[associatedAxisName].AxisOffset[0] = e.OffsetVal;
            }
            chartPanel.Invalidate();
        }
    }
}
