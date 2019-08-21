using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YamuraLog
{
    public partial class AxisInfo : Form
    {
        public float RangeMinimum
        {
            get { return Convert.ToSingle(txtAxisMin.Text); }
            set { txtAxisMin.Text = value.ToString(); }
        }
        public float RangeMaximum
        {
            get { return Convert.ToSingle(txtAxisMax.Text); }
            set { txtAxisMax.Text = value.ToString(); }
        }
        public AxisInfo()
        {
            InitializeComponent();
        }
    }
}
