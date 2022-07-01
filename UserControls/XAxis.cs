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
using YamuraControls.GDI;
using Win32Interop.Methods;

namespace YamuraView.UserControls
{
    public partial class XAxis : UserControl
    {
        public enum CursorStyle
        {
            NONE,
            CROSSHAIRS,
            VERTICAL,
            HORIZONTAL,
            BOX,
            CIRCLE
        }
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

        //bool startMouseDrag = false;
        bool startMouseMove = false;
        Point axisLastCursorPos = new Point();
        Point axisStartCursorPos = new Point();
        protected Rectangle axisBounds = new Rectangle(0, 0, 0, 0);
        protected int axisBorder = 10;
        protected int dragZoomPenWidth = 1;

        string title = "X axis";
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        // power of 10 of ticks
        float majorTicks = 1;
        float minorTicks = 0;

        GraphicsPath axisPath = new GraphicsPath();

        // crosshairs, box, circle work
        // horizontal and vertical look weird
        CursorStyle cursorMode = CursorStyle.VERTICAL;
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

        public XAxis()
        {
            InitializeComponent();
        }
        private void axisScale_Paint(object sender, PaintEventArgs e)
        {
            startMouseMove = false;
            if (axisPath.PathData.Points.Count() == 0)
            {
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
            }
            float displayScale = 1;
            Pen pathPen = new Pen(Color.White, 0);
            SolidBrush labelBrush = new SolidBrush(Color.White);
            Font labelFont = new Font(FontFamily.GenericMonospace, 8);
            SizeF labelSize;
            using (Graphics axisGraphics = axisScale.CreateGraphics())
            {
                displayScale = (float)axisBounds.Width / (float)(ViewRange[1] - ViewRange[0]);

                Rectangle clipRect = axisBounds;
                clipRect.Inflate(2, axisBorder*2);
                axisGraphics.SetClip(clipRect);

                axisGraphics.TranslateTransform(axisBorder, 0);//, (float)axisBounds.Height + axisBorder);
                // scale to display range in X and Y
                axisGraphics.ScaleTransform(displayScale, 1);
                // translate by -1 * minimum display range + axis offset (scrolling)
                axisGraphics.TranslateTransform(-1 * ViewRange[0] + Minimum,  // offset X
                                                 0);                          // offset Y

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
                    PointF endPt = new PointF(labelVal, 0);
                    // x axis is time - direct lookup
                    endPt = ScaleDataToDisplay(endPt,                                        // point
                                               displayScale,                                 // scale X
                                               1,                                            // scale Y
                                               ViewRange[0],                  // offset X
                                               0,                                            // offset Y
                                               axisBounds);                                  // graphics area boundary

                    labelSize = axisGraphics.MeasureString(labelVal.ToString(), labelFont);
                    axisGraphics.DrawString(labelVal.ToString(), labelFont,
                                            labelBrush,
                                            endPt.X - labelSize.Width/2,
                                            15);
                }
                labelSize = axisGraphics.MeasureString(Title, labelFont);
                axisGraphics.DrawString(Title, labelFont, labelBrush, axisBounds.Width / 2, axisBounds.Height - (2.25F * labelSize.Height));
            }
        }
        private void axisScroll_Scroll(object sender, ScrollEventArgs e)
        {
            axisScale.Invalidate();
        }
        public virtual void OnChartMouseMove(object sender, AxisControlMouseMoveEventArgs e)
        {
            // position in event args is data - need to scale to screen
            float[] displayScale = new float[] { 1.0F, 1.0F };



            PointF endPt = new PointF();// = new PointF(, e.YAxisValues["none"]);
            foreach (KeyValuePair<string, float> kvp in e.XAxisValues)
            {
                endPt = new PointF(kvp.Value, e.YAxisValues["none"]);
                break;
            }
            System.Diagnostics.Debug.Write("Actual " + endPt.ToString());

            displayScale[0] = (float)axisBounds.Width / (float)(ViewRange[1] - ViewRange[0]);
            displayScale[1] = 1.0F;

            System.Diagnostics.Debug.Write(" Scale " + displayScale[0].ToString() + " min " + Minimum.ToString() + " view range " + ViewRange[0].ToString() + " " + ViewRange[1].ToString());

            // x axis is time - direct lookup
            endPt = ScaleDataToDisplay(endPt,                                        // point
                                       displayScale[0],                              // scale X
                                       displayScale[1],                              // scale Y
                                       -1 * Minimum + ViewRange[0],                  // offset X
                                       0,                                            // offset Y
                                       axisBounds);                                  // graphics area boundary

            System.Diagnostics.Debug.WriteLine(" Scaled " + endPt.ToString());
            #region erase if something was drawn
            if (!startMouseMove)
            {
                axisLastCursorPos = new Point((int)endPt.X, (int)endPt.Y);
            }
            else
            {
                DrawCursorAt(axisLastCursorPos.X, axisLastCursorPos.Y);
            }
            #endregion
            #region draw new cursor if in chart area
            if (((endPt.X >= axisBorder) && (endPt.X <= (axisScale.Width - axisBorder))))// &&
                //((endPt.Y >= axisBorder) && (endPt.Y <= (axisScale.Height - axisBorder))))
            {
                startMouseMove = true;
                axisLastCursorPos = new Point((int)endPt.X, (int)endPt.Y);
                DrawCursorAt(axisLastCursorPos.X, axisLastCursorPos.Y);
            }
            // outside of chart, don't draw and not started
            else
            {
                startMouseMove = false;
            }
            #endregion
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
            using (Graphics drawGraphics = axisScale.CreateGraphics())
            {
                #region create GDI objects
                IntPtr hDC = drawGraphics.GetHdc();
                IntPtr newPen = Gdi32.CreatePen((int)PenStyles.PS_SOLID,
                                                dragZoomPenWidth,
                                                GDI32.NotRGB(Color.Black));
                IntPtr newBrush = Gdi32.GetStockObject((int)StockObjects.BLACK_BRUSH);
                IntPtr oldPen = Gdi32.SelectObject(hDC, newPen);
                IntPtr oldBrush = Gdi32.SelectObject(hDC, newBrush);
                Gdi32.SetROP2(hDC, (int)YamuraControls.GDI.DrawMode.R2_XORPEN);
                #endregion
                #region crosshairs
                if (cursorMode == CursorStyle.CROSSHAIRS)
                {
                    // horizontal line
                    Gdi32.MoveToEx(hDC, 0, y, lpPoint);
                    Gdi32.LineTo(hDC, axisScale.Width, y);
                    // vertical line
                    Gdi32.MoveToEx(hDC, x, axisScale.Height, lpPoint);
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
                    Gdi32.LineTo(hDC, axisScale.Width, y);
                }
                #endregion
                #region vertical line
                else if (cursorMode == CursorStyle.VERTICAL)
                {
                    // vertical line
                    Gdi32.MoveToEx(hDC, x, 0, lpPoint);
                    Gdi32.LineTo(hDC, x, axisScale.Height);
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
            rPt.X = (rPt.X - offsetX) * scaleX + bounds.X;
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

        private void axisScale_MouseMove(object sender, MouseEventArgs e)
        {
            axisScale.Focus();
            System.Diagnostics.Debug.WriteLine(e.X.ToString());

            #region left mouse button down - dragging zoom region 
            //if ((e.Button == MouseButtons.Left) && AllowDrag)
            //{
            //    // starting mouse drag - erase old cursor if needed, save initial start and end locations
            //    if (!startMouseDrag[0])
            //    {
            //        // was moving mouse with left button up, erase cursor
            //        if (startMouseMove[0])
            //        {
            //            DrawCursorAt(chartLastCursorPos[0].X, chartLastCursorPos[0].Y);
            //        }
            //        // save location
            //        chartStartCursorPos[0] = new Point(e.Location.X, 0);
            //        chartLastCursorPos[0] = new Point(e.Location.X, axisScale.Height);
            //    }
            //    // continue mouse drag - erase last box
            //    else
            //    {
            //        DrawSelectArea(chartStartCursorPos[0].X, chartStartCursorPos[0].Y, chartLastCursorPos[0].X, chartLastCursorPos[0].Y);
            //    }
            //    // continue mouse drag, save current end location and draw new box
            //    chartLastCursorPos[0] = new Point(e.Location.X, chartLastCursorPos[0].Y);
            //    DrawSelectArea(chartStartCursorPos[0].X, chartStartCursorPos[0].Y, chartLastCursorPos[0].X, chartLastCursorPos[0].Y);
            //    // mouse drag has started, mouse move has stopped
            //    startMouseDrag[0] = true;
            //    startMouseMove[0] = false;
            //}
            #endregion
            #region just moving the mouse with no buttons
            //else
            {
                if (CursorMode != CursorStyle.NONE)
                {
                    #region erase if something was drawn
                    if (!startMouseMove)
                    {
                        axisLastCursorPos = e.Location;
                    }
                    else
                    {
                        DrawCursorAt(axisLastCursorPos.X, axisLastCursorPos.Y);
                    }
                    #endregion
                    #region draw new cursor if in chart area
                    if (((e.Location.X >= axisBorder) && (e.Location.X <= (axisScale.Width - axisBorder))) &&
                        ((e.Location.Y >= axisBorder) && (e.Location.Y <= (axisScale.Height - axisBorder))))
                    {
                        startMouseMove = true;
                        axisLastCursorPos = e.Location;
                        DrawCursorAt(axisLastCursorPos.X, axisLastCursorPos.Y);
                    }
                    // outside of chart, don't draw and not started
                    else
                    {
                        startMouseMove = false;
                    }
                    #endregion
                }
            }
            #endregion
            //// up to now, dealing in screen units. convert current position to X axis value and active Y axis values and raise message
            //if (ChartOwner.ChartAxes.Count == 0)
            //{
            //    return;
            //}
            //// create event args
            //ChartControlMouseMoveEventArgs moveEventArgs = new ChartControlMouseMoveEventArgs();

            //PointF axisPoint = new PointF();
            //// x and y scale
            //float[] displayScale = new float[] { 1.0F, 1.0F };
            //displayScale[0] = (float)chartBounds.Width / ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[2];

            //foreach (KeyValuePair<string, ChannelInfo> channel in ChartOwner.ChartAxes[xChannelName].AssociatedChannels)
            //{
            //    displayScale[1] = 1.0F;

            //    axisPoint.X = (float)e.Location.X;
            //    axisPoint.Y = (float)e.Location.Y;

            //    axisPoint = ScaleDisplayToData(axisPoint,
            //                       displayScale[0],
            //                       displayScale[1],
            //                                       0.0F,
            //                                       0.0F,
            //                       chartBounds);
            //    axisPoint.X -= (-1 * ChartOwner.ChartAxes[xChannelName].AxisDisplayRange[0] +

            //                           ChartOwner.ChartAxes[xChannelName].AssociatedChannels[channel.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[0]);  // offset X

            //    moveEventArgs.XAxisValues.Add(channel.Value.RunIndex.ToString() + "-" + channel.Value.ChannelName, axisPoint.X);
            //}
            //foreach (KeyValuePair<string, Axis> axis in ChartOwner.ChartAxes)
            //{
            //    if (!axis.Value.ShowAxis)
            //    {
            //        continue;
            //    }
            //    foreach (KeyValuePair<string, ChannelInfo> channel in axis.Value.AssociatedChannels)
            //    {
            //        displayScale[1] = (float)chartBounds.Height / axis.Value.AxisDisplayRange[2];
            //        axisPoint.X = (float)e.Location.X;
            //        axisPoint.Y = (float)e.Location.Y;

            //        axisPoint = ScaleDisplayToData(axisPoint,
            //                           displayScale[0],
            //                           displayScale[1],
            //                                               0.0F,
            //                                               0.0F,
            //                           chartBounds);

            //        axisPoint.Y -= (channel.Value.AxisRange[0] +
            //                                               ChartOwner.ChartAxes[xChannelName].AssociatedChannels[channel.Value.RunIndex.ToString() + "-" + xChannelName].AxisOffset[1]);

            //        moveEventArgs.YAxisValues.Add(channel.Value.RunIndex.ToString() + "-" + channel.Value.ChannelName, axisPoint.Y);
            //    }
            //}
            //ChartMouseMoveEvent(this, moveEventArgs);
        }

        private void axisScale_Resize(object sender, EventArgs e)
        {
            // update the paintable area
            axisBounds.X = axisBorder;
            axisBounds.Y = axisBorder;
            axisBounds.Width = axisScale.Width - (2 * axisBorder);
            axisBounds.Height = axisScale.Height - (2 * axisBorder);
            // redraw
            axisScale.Invalidate();
        }
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
