using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using YamuraView;

namespace YamuraView.UserControls
{
    public partial class XAxis : UserControl
    {
        public int Minimum
        {
            get { return axisScroll.Minimum; }
            set
            {
                axisScroll.Minimum = value;
                int range = axisScroll.Maximum - axisScroll.Minimum;
                majorTicks = (int)Math.Log10((double)range) - 1;
                minorTicks = (int)Math.Log10((double)range) - 2;
            }
        }
        public int Maximum
        {
            get { return axisScroll.Maximum; }
            set
            {
                axisScroll.Maximum = value;
                int range = axisScroll.Maximum - axisScroll.Minimum;
                majorTicks = (int)Math.Log10((double)range) - 1;
                minorTicks = (int)Math.Log10((double)range) - 2;
            }
        }
        public int Value
        {
            get { return axisScroll.Value; }
            set
            {
                axisScroll.Value = value;
                axisScale.Invalidate();
            }
        }
        public int[] ViewRange
        {
            get
            {
                int[] viewRange = new int[] { 0, 0 };
                viewRange[0] = axisScroll.Value;
                viewRange[1] = axisScroll.Value + axisScroll.LargeChange;
                return viewRange;
            }
        }
        public int LargeChange
        {
            get { return axisScroll.LargeChange; }
            set { axisScroll.LargeChange = value; }
        }

        bool startMouseDrag = false;
        bool startMouseMove = false;
        Point chartLastCursorPos = new Point();
        Point chartStartCursorPos = new Point();

        string title = "X axis";
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        // power of 10 of ticks
        float majorTicks = 1;
        float minorTicks = 0;
        public XAxis()
        {
            InitializeComponent();
        }
        private void axisScale_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath axisPath = new GraphicsPath();
            axisPath.AddLine(Minimum, 5, Maximum, 5);
            // major ticks
            float xVal = Minimum;
            while (true)
            {
                GraphicsPath seg = new GraphicsPath();
                seg.AddLine(xVal, 5.0F, xVal, 15.0F);
                axisPath.AddPath(seg, false);
                xVal += (float)Math.Pow(10, (double)majorTicks);
                if (xVal > Maximum)
                {
                    break;
                }
            }
            // minor ticks
            xVal = Minimum;
            while (true)
            {
                GraphicsPath seg = new GraphicsPath();
                seg.AddLine(xVal, 5.0F, xVal, 10.0F);
                axisPath.AddPath(seg, false);
                xVal += (float)Math.Pow(10, (double)minorTicks);
                if (xVal > Maximum)
                {
                    break;
                }
            }
            float displayScale = 1;
            Pen pathPen = new Pen(Color.Black, 0);
            SolidBrush labelBrush = new SolidBrush(Color.Black);
            Font labelFont = new Font(FontFamily.GenericMonospace, 8);
            SizeF labelSize;
            using (Graphics axisGraphics = axisScale.CreateGraphics())
            {
                displayScale = (float)Width / (float)(ViewRange[1] - ViewRange[0]);
                
                // scale to display range in X and Y
                axisGraphics.ScaleTransform(displayScale, 1);
                // translate by -1 * minimum display range + axis offset (scrolling)
                axisGraphics.TranslateTransform(-1 * ViewRange[0] + Minimum,  // offset X
                                                 0);  // offset Y
            
                axisGraphics.DrawPath(pathPen, axisPath);

                axisGraphics.ResetTransform();
                for (float labelVal = Minimum; labelVal < Maximum; labelVal += (float)Math.Pow(10, majorTicks))
                {
                    if (labelVal < ViewRange[0])
                    {
                        continue;
                    }
                    if (labelVal > ViewRange[1])
                    {
                        break;
                    }
                    labelSize = axisGraphics.MeasureString(labelVal.ToString(), labelFont);
                    axisGraphics.DrawString(labelVal.ToString(), labelFont,
                                            labelBrush,
                                            ((labelVal - ViewRange[0]) * displayScale) - labelSize.Width/2,
                                             15);
                }
                labelSize = axisGraphics.MeasureString(Title, labelFont);
                axisGraphics.DrawString(Title, labelFont, labelBrush, Width/2, Height - (2.25F * labelSize.Height));
            }
        }
        private void axisScroll_Scroll(object sender, ScrollEventArgs e)
        {
            axisScale.Invalidate();
        }
        //public virtual void OnChartMouseMove(object sender, AxisControlMouseMoveEventArgs e)
        //{
        //    #region move cursor(s)
        //    // position in event args is data - need to scale to screen
        //    float[] displayScale = new float[] { 1.0F, 1.0F };
        //    PointF endPt = new PointF();
        //    int axisIdx = 0;
        //    string axisFullName = "";
        //    int cursorIdx = -1;
        //    axisFullName = axisIdx.ToString() + "-" + yAxis.Key;
        //    displayScale[0] = (float)chartBounds.Width / ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];
        //    displayScale[1] = (float)chartBounds.Height / yAxis.Value.AxisDisplayRange[2];
        //    if (EqualScale)
        //    {
        //        if (displayScale[0] < displayScale[1])
        //        {
        //            displayScale[1] = displayScale[0];
        //        }
        //        else
        //        {
        //            displayScale[0] = displayScale[1];
        //        }
        //    }
        //    // x axis is time - direct lookup
        //    if (axisFullName == (axisIdx.ToString() + "-Time"))
        //    {
        //        endPt.X = e.XAxisValues[xChannelName];// curChannel.DataPoints[].PointValue;
        //        endPt.Y = curChannel.DataPoints[endPt.X].PointValue;
        //    }
        //    endPt = ScaleDataToDisplay(endPt,                                                      // point
        //                               displayScale[0],                              // scale X
        //                               displayScale[1],                              // scale Y
        //                               -1 * ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0] +
        //                                           ChartOwner.ChartAxes[xChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[0],  // offset X
        //                               yAxis.Value.AxisDisplayRange[0] +
        //                                           ChartOwner.ChartAxes[xChannelName].AssociatedChannels[curChanInfo.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[1],  // offset Y
        //                               chartBounds);                                 // graphics area boundary

        //    startMouseDrag = false;
        //    #region erase if something was drawn
        //    if (!startMouseMove)
        //    {
        //        chartLastCursorPos = new Point((int)endPt.X, (int)endPt.Y);
        //    }
        //    else
        //    {
        //        DrawCursorAt(chartLastCursorPos.X, chartLastCursorPos.Y);
        //    }
        //    #endregion
        //    #region draw new cursor if in chart area
        //    if (((endPt.X >= chartBorder) && (endPt.X <= (chartPanel.Width - chartBorder))) &&
        //        ((endPt.Y >= chartBorder) && (endPt.Y <= (chartPanel.Height - chartBorder))))
        //    {
        //        startMouseMove = true;
        //        chartLastCursorPos = new Point((int)endPt.X, (int)endPt.Y);
        //        DrawCursorAt(chartLastCursorPos.X, chartLastCursorPos.Y);
        //    }
        //    // outside of chart, don't draw and not started
        //    else
        //    {
        //        startMouseMove = false;
        //    }
        //    #endregion
        //    #endregion
        //}
    }
    public class AxisControlMouseMoveEventArgs : EventArgs
    {
        Dictionary<string, float> xAxisValues = new Dictionary<string, float>();
        public Dictionary<string, float> XAxisValues
        {
            get { return xAxisValues; }
            set { xAxisValues = value; }
        }
        Dictionary<string, float> yAxisValues = new Dictionary<string, float>();
        public Dictionary<string, float> YAxisValues
        {
            get { return yAxisValues; }
            set { yAxisValues = value; }
        }
    }
}
