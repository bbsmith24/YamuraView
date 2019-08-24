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
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.chartControlContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAxesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartControlContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartPanel
            // 
            this.chartPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartPanel.BackColor = System.Drawing.Color.White;
            this.chartPanel.ContextMenuStrip = this.chartControlContextMenu;
            this.chartPanel.Location = new System.Drawing.Point(17, 0);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(470, 367);
            this.chartPanel.TabIndex = 0;
            this.chartPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.chartPanel_Paint);
            this.chartPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartPanel_MouseMove);
            this.chartPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartPanel_MouseUp);
            this.chartPanel.Resize += new System.EventHandler(this.chartPanel_Resize);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.vScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 367);
            this.vScrollBar1.TabIndex = 1;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.Location = new System.Drawing.Point(17, 369);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(470, 17);
            this.hScrollBar1.TabIndex = 2;
            // 
            // chartControlContextMenu
            // 
            this.chartControlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAxesMenuItem});
            this.chartControlContextMenu.Name = "chartControlContextMenu";
            this.chartControlContextMenu.Size = new System.Drawing.Size(134, 26);
            // 
            // selectAxesMenuItem
            // 
            this.selectAxesMenuItem.Name = "selectAxesMenuItem";
            this.selectAxesMenuItem.Size = new System.Drawing.Size(133, 22);
            this.selectAxesMenuItem.Text = "Select Axes";
            this.selectAxesMenuItem.Click += new System.EventHandler(this.selectAxesMenuItem_Click);
            // 
            // ChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.chartPanel);
            this.Name = "ChartControl";
            this.Size = new System.Drawing.Size(487, 391);
            this.chartControlContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel chartPanel;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.ContextMenuStrip chartControlContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAxesMenuItem;
    }
}
