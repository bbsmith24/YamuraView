namespace YamuraLog
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
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // hScrollBar
            // 
            this.hScrollBar.Location = new System.Drawing.Point(112, 244);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(80, 17);
            this.hScrollBar.TabIndex = 0;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Location = new System.Drawing.Point(9, 9);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 80);
            this.vScrollBar.TabIndex = 1;
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
            // ChartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.chartPanel);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.hScrollBar);
            this.Name = "ChartView";
            this.Text = "ChartView";
            this.Resize += new System.EventHandler(this.ChartView_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.Panel chartPanel;
    }
}