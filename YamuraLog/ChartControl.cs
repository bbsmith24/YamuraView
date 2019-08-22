using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YamuraLog
{
    public partial class ChartControl : UserControl
    {
        DataLogger logger;
        public DataLogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        Dictionary<string, Axis> chartAxes = new Dictionary<string, Axis>();
        public Dictionary<string, Axis> ChartAxes
        {
            get { return chartAxes; }
            set { chartAxes = value; }
        }

        int chartBorder = 10;
        Rectangle chartBounds = new Rectangle(0, 0, 0, 0);
        bool chartStartPosValid = false;
        bool chartLastCursorPosValid = false;
        string xChannelName;

        /// <summary>
        /// 
        /// </summary>
        public ChartControl()
        {
            InitializeComponent();
            xChannelName = "Time";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartPanel_Paint(object sender, PaintEventArgs e)
        {
            // nothing to display yet
            if ((chartAxes == null) || (chartAxes.Count == 0))
            {
                return;
            }
            chartStartPosValid = false;
            chartLastCursorPosValid = false;
            PointF startPt = new PointF();
            PointF endPt = new PointF();
            Pen drawPen = new Pen(Color.Black, 1);
            bool initialValue = false;
            // x and y scale
            float[] displayScale = new float[] { 1.0F, 1.0F };
            displayScale[0] = (float)chartBounds.Width / chartAxes[xChannelName].AxisRange[2];

            // get the graphics context
            using (Graphics chartGraphics = chartPanel.CreateGraphics())
            {
                // check each Y axis
                foreach (KeyValuePair<string, Axis> yAxis in chartAxes)
                {
                    // skip if axis is not displayed
                    if (!yAxis.Value.ShowAxis)
                    {
                        continue;
                    }
                    displayScale[1] = (float)chartBounds.Height / yAxis.Value.AxisRange[2];

                    // check each associated channel
                    foreach (KeyValuePair<String, ChannelInfo> curChanInfo in yAxis.Value.AssociatedChannels)
                    {
                        // skip if channel is not displayed
                        if (!curChanInfo.Value.ShowChannel)
                        {
                            continue;
                        }
                        DataChannel curChannel = logger.runData[curChanInfo.Value.RunIndex].channels[curChanInfo.Value.ChannelName];
                        drawPen = new Pen(curChanInfo.Value.ChannelColor);

                        initialValue = false;
                        foreach (KeyValuePair<float, DataPoint> curData in curChannel.DataPoints)
                        {
                            // x axis is time - direct lookup
                            if (xChannelName == "Time")
                            {
                                endPt.X = curData.Key;
                                endPt.Y = curData.Value.PointValue;
                            }
                            // x axis is not time - find nearest time in axis channel, 
                            else
                            {
                                DataPoint tst = logger.runData[0].channels[xChannelName].dataPoints.FirstOrDefault(i => i.Key >= curData.Key).Value;
                                endPt.X = tst.PointValue;
                                endPt.Y = curData.Value.PointValue;
                            }
                            //
                            // at this point, the value is already offset on the X axis by any user defined time shift
                            // just need to offset by the stripchart panned position (H scrollbar)
                            //
                            endPt = ScaleDataToDisplay(endPt,                                               // point
                                                                     displayScale[0],                       // scale X
                                                                     displayScale[1],                       // scale Y
                                                                     chartAxes[xChannelName].DisplayOffset, // offset X
                                                                     yAxis.Value.AxisRange[0],              // offset Y
                                                                     chartBounds);                          // graphics area boundary
                            if ((initialValue) && (startPt.X < chartBounds.Width) && (endPt.X > 0))
                            {
                                chartGraphics.DrawLine(drawPen, startPt, endPt);
                            }
                            startPt.X = endPt.X;
                            startPt.Y = endPt.Y;
                            initialValue = true;
                        }
                    }
                }
            }
        }

        private void chartPanel_Resize(object sender, EventArgs e)
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
        /// convert sourcePt from data units to display units
        /// </summary>
        /// <param name="sourcePt"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        private PointF ScaleDataToDisplay(PointF sourcePt, float scaleX, float scaleY, float offsetX, float offsetY, Rectangle bounds)
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
        private PointF ScaleDisplayToData(PointF sourcePt, float scaleX, float scaleY, float offsetX, float offsetY, Rectangle bounds)
        {
            PointF rPt = new PointF(sourcePt.X, sourcePt.Y);
            rPt.X = (rPt.X / scaleX) - offsetX;
            rPt.Y = -1.0F * ((rPt.Y - (float)bounds.Height) / scaleY) - offsetY;
            return rPt;
        }

        private void ChartControl_Resize(object sender, EventArgs e)
        {
            // update the paintable area
            chartBounds.X = chartBorder;
            chartBounds.Y = chartBorder;
            chartBounds.Width = chartPanel.Width - (2 * chartBorder);
            chartBounds.Height = chartPanel.Height - (2 * chartBorder);
            // redraw
            chartPanel.Invalidate();

        }

        private void ChartControl_Paint(object sender, PaintEventArgs e)
        {
            chartPanel.Invalidate();
        }
    }
}
