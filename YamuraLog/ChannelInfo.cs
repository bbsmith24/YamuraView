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
    public partial class ChannelInfoForm : Form
    {
        public float RangeMinimum
        {
            get { return Convert.ToSingle(txtChannelMin.Text); }
            set { txtChannelMin.Text = value.ToString(); }
        }
        public float RangeMaximum
        {
            get { return Convert.ToSingle(txtChannelMax.Text); }
            set { txtChannelMax.Text = value.ToString(); }
        }
        public Color ChannelColor
        {
            get { return channelColorDialog.Color; }
            set { channelColorDialog.Color = value; }
        }
        public ChannelInfoForm()
        {
            InitializeComponent();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            Color originalColor = channelColorDialog.Color;
            if(channelColorDialog.ShowDialog() == DialogResult.Cancel)
            {
                channelColorDialog.Color = originalColor;
            }
        }
    }
}
