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

                axisGraphics.DrawString(Title, new Font(FontFamily.GenericMonospace, 8), new SolidBrush(Color.Black), Width / 2, Height / 3);
            }
        }

        private void axisScroll_Scroll(object sender, ScrollEventArgs e)
        {
            axisScale.Invalidate();
        }
    }
}
