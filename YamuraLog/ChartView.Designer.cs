namespace YamuraView
{
    partial class ChartView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chartPanel = new System.Windows.Forms.Panel();
            this.xAxis1 = new YamuraView.UserControls.XAxis();
            this.yAxis1 = new YamuraView.UserControls.YAxis();
            this.SuspendLayout();
            // 
            // chartPanel
            // 
            this.chartPanel.BackColor = System.Drawing.Color.White;
            this.chartPanel.Location = new System.Drawing.Point(82, 12);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(200, 100);
            this.chartPanel.TabIndex = 2;
            this.chartPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.chartPanel_Paint);
            this.chartPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartPanel_MouseMove);
            this.chartPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartPanel_MouseUp);
            this.chartPanel.Resize += new System.EventHandler(this.chartPanel_Resize);
            // 
            // xAxis1
            // 
            this.xAxis1.BackColor = System.Drawing.Color.Red;
            this.xAxis1.LargeChange = 10;
            this.xAxis1.Location = new System.Drawing.Point(66, 174);
            this.xAxis1.Maximum = 100;
            this.xAxis1.Minimum = 0;
            this.xAxis1.Name = "xAxis1";
            this.xAxis1.Size = new System.Drawing.Size(206, 56);
            this.xAxis1.TabIndex = 3;
            this.xAxis1.Value = 0;
            // 
            // yAxis1
            // 
            this.yAxis1.Location = new System.Drawing.Point(29, 9);
            this.yAxis1.Name = "yAxis1";
            this.yAxis1.Size = new System.Drawing.Size(56, 150);
            this.yAxis1.TabIndex = 4;
            // 
            // ChartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.yAxis1);
            this.Controls.Add(this.xAxis1);
            this.Controls.Add(this.chartPanel);
            this.Name = "ChartView";
            this.Text = "ChartView";
            this.Resize += new System.EventHandler(this.ChartView_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel chartPanel;
        public UserControls.XAxis xAxis1;
        public UserControls.YAxis yAxis1;
    }
}