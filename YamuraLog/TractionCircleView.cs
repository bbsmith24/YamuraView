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
    public partial class TractionCircleView : ChartView 
    {
        public TractionCircleView()
        {
            InitializeComponent();
            XChannelName = "Time";
            CursorUpdateSource = true;

            chartLastCursorPos.Add(new Point(0, 0));
            chartStartCursorPos.Add(new Point(0, 0));
            startMouseMove.Add(false);
            startMouseDrag.Add(false);
            hScrollBar.Scroll += HScrollBar_Scroll;
        }

        #region chart message handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal override void chartPanel_Paint(object sender, PaintEventArgs e)
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
            if ((ChartAxes == null) || (ChartAxes.Count == 0))
            {
                return;
            }
            bool initialValue = false;
            // x and y scale
            float[] displayScale = new float[] { 1.0F, 1.0F };

            PointF[] points = new PointF[] { new PointF(), new PointF() };
            Pen pathPen = new Pen(Color.Red);

            // process each axis
            foreach (KeyValuePair<string, Axis> yAxis in ChartAxes)
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
                        displayScale[0] = (float)chartBounds.Width / ChartAxes[XChannelName].AxisDisplayRange[2];
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
                        chartGraphics.TranslateTransform(-1 * ChartAxes[XChannelName].AxisDisplayRange[0] +
                                                         ChartAxes[XChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + XChannelName].AxisOffset[0],  // offset X
                                                         -1* yAxis.Value.AxisDisplayRange[0] +
                                                         ChartAxes[XChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + XChannelName].AxisOffset[1]);  // offset Y
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
        public override void OnChartMouseMove(object sender, ChartControlMouseMoveEventArgs e)
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
                foreach (KeyValuePair<string, Axis> yAxis in ChartAxes)
                {
                    // skip if axis is not displayed
                    if (!yAxis.Value.ShowAxis)
                    {
                        continue;
                    }
                    axisFullName = axisIdx.ToString() + "-" + yAxis.Key;
                    displayScale[0] = (float)chartBounds.Width / ChartAxes[XChannelName].AxisDisplayRange[2];
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
                            endPt.X = e.XAxisValues[XChannelName];// curChannel.DataPoints[].PointValue;
                            endPt.Y = curChannel.DataPoints[endPt.X].PointValue;
                        }
                        // x axis is not time - find nearest time in axis channel, 
                        else
                        {
                            DataPoint tst = Logger.runData[curChanInfo.Value.RunIndex].channels[XChannelName].dataPoints.FirstOrDefault(i => i.Key >= e.XAxisValues[curChanInfo.Value.RunIndex.ToString() + "-Time"]).Value;
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
                                                   -1 * ChartAxes[XChannelName].AxisDisplayRange[0] +
                                                               ChartAxes[XChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + XChannelName].AxisOffset[0],  // offset X
                                                   yAxis.Value.AxisDisplayRange[0] +
                                                               ChartAxes[XChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + XChannelName].AxisOffset[1],  // offset Y
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
        #endregion
    }
}
