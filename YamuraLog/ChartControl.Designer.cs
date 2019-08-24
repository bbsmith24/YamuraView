namespace YamuraLog
{
    public partial class ChartControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.chartControlContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAxesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.chartControlContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartPanel
            // 
            this.chartPanel.BackColor = System.Drawing.Color.White;
            this.chartPanel.ContextMenuStrip = this.chartControlContextMenu;
            this.chartPanel.Location = new System.Drawing.Point(26, 0);
            this.chartPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(705, 565);
            this.chartPanel.TabIndex = 0;
            this.chartPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.chartPanel_Paint);
            this.chartPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartPanel_MouseMove);
            this.chartPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartPanel_MouseUp);
            this.chartPanel.Resize += new System.EventHandler(this.chartPanel_Resize);
            // 
            // chartControlContextMenu
            // 
            this.chartControlContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.chartControlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAxesMenuItem});
            this.chartControlContextMenu.Name = "chartControlContextMenu";
            this.chartControlContextMenu.Size = new System.Drawing.Size(173, 34);
            // 
            // selectAxesMenuItem
            // 
            this.selectAxesMenuItem.Name = "selectAxesMenuItem";
            this.selectAxesMenuItem.Size = new System.Drawing.Size(172, 30);
            this.selectAxesMenuItem.Text = "Select Axes";
            this.selectAxesMenuItem.Click += new System.EventHandler(this.selectAxesMenuItem_Click);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Location = new System.Drawing.Point(0, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 565);
            this.vScrollBar.TabIndex = 1;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Location = new System.Drawing.Point(26, 568);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(705, 17);
            this.hScrollBar.TabIndex = 2;
            // 
            // ChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.chartPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ChartControl";
            this.Size = new System.Drawing.Size(728, 598);
            this.Resize += new System.EventHandler(this.ChartControl_Resize);
            this.chartControlContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel chartPanel;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.ContextMenuStrip chartControlContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAxesMenuItem;
    }
}
