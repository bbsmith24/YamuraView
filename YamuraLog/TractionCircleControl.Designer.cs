namespace YamuraView
{
    public partial class TractionCircleControl
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
            this.chartControlContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.zoomAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.chartControlContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartControlContextMenu
            // 
            this.chartControlContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.chartControlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomAllMenuItem});
            this.chartControlContextMenu.Name = "chartControlContextMenu";
            this.chartControlContextMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // zoomAllMenuItem
            // 
            this.zoomAllMenuItem.Name = "zoomAllMenuItem";
            this.zoomAllMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zoomAllMenuItem.Text = "Zoom All";
            this.zoomAllMenuItem.Click += new System.EventHandler(this.zoomAllMenuItem_Click);
            // 
            // dockPanel1
            // 
            this.dockPanel1.ContextMenuStrip = this.chartControlContextMenu;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(469, 350);
            this.dockPanel1.TabIndex = 1;
            // 
            // ChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 350);
            this.Controls.Add(this.dockPanel1);
            this.Name = "ChartControl";
            this.chartControlContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip chartControlContextMenu;
        private System.Windows.Forms.ToolStripMenuItem zoomAllMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
    }
}
