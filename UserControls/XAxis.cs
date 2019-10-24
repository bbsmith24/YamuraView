using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YamuraView.UserControls
{
    public partial class XAxis : UserControl
    {
        public int Minimum
        {
            get { return axisScroll.Minimum; }
            set { axisScroll.Minimum = value; }
        }
        public int Maximum
        {
            get { return axisScroll.Maximum; }
            set { axisScroll.Maximum = value; }
        }
        public int Value
        {
            get { return axisScroll.Value; }
            set { axisScroll.Value = value; }
        }
        public int LargeChange
        {
            get { return axisScroll.LargeChange; }
            set { axisScroll.LargeChange = value; }
        }
        public XAxis()
        {
            InitializeComponent();
        }

    }
}
