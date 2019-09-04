﻿namespace YamuraLog
{
    public partial class Form1
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
            this.openLogFile = new System.Windows.Forms.OpenFileDialog();
            this.runDataGrid = new System.Windows.Forms.DataGridView();
            this.colRunNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShowRun = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTraceColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOffsetTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSourceFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.txtCursorPos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackMap = new YamuraLog.ChartControl();
            this.tractionCircle = new YamuraLog.ChartControl();
            this.stripChart = new YamuraLog.ChartControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRunsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRunsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFromLoggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadAndDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadOnlyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoAlignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // runDataGrid
            // 
            this.runDataGrid.AllowUserToAddRows = false;
            this.runDataGrid.AllowUserToDeleteRows = false;
            this.runDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.runDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRunNumber,
            this.colShowRun,
            this.colTraceColor,
            this.colDate,
            this.colMinTime,
            this.colMaxTime,
            this.colOffsetTime,
            this.colSourceFile});
            this.runDataGrid.Location = new System.Drawing.Point(11, 41);
            this.runDataGrid.Name = "runDataGrid";
            this.runDataGrid.RowHeadersVisible = false;
            this.runDataGrid.Size = new System.Drawing.Size(108, 151);
            this.runDataGrid.TabIndex = 18;
            // 
            // colRunNumber
            // 
            this.colRunNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colRunNumber.HeaderText = "Run";
            this.colRunNumber.Name = "colRunNumber";
            this.colRunNumber.ReadOnly = true;
            this.colRunNumber.Width = 52;
            // 
            // colShowRun
            // 
            this.colShowRun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colShowRun.HeaderText = "Show";
            this.colShowRun.Name = "colShowRun";
            this.colShowRun.Width = 40;
            // 
            // colTraceColor
            // 
            this.colTraceColor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colTraceColor.HeaderText = "Color";
            this.colTraceColor.Name = "colTraceColor";
            this.colTraceColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTraceColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTraceColor.Width = 37;
            // 
            // colDate
            // 
            this.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDate.HeaderText = "Date/Time";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Width = 83;
            // 
            // colMinTime
            // 
            this.colMinTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colMinTime.HeaderText = "Start";
            this.colMinTime.Name = "colMinTime";
            this.colMinTime.ReadOnly = true;
            this.colMinTime.Width = 54;
            // 
            // colMaxTime
            // 
            this.colMaxTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colMaxTime.HeaderText = "End";
            this.colMaxTime.Name = "colMaxTime";
            this.colMaxTime.ReadOnly = true;
            this.colMaxTime.Width = 51;
            // 
            // colOffsetTime
            // 
            this.colOffsetTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colOffsetTime.HeaderText = "Offset";
            this.colOffsetTime.Name = "colOffsetTime";
            this.colOffsetTime.Width = 60;
            // 
            // colSourceFile
            // 
            this.colSourceFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSourceFile.HeaderText = "Source";
            this.colSourceFile.Name = "colSourceFile";
            this.colSourceFile.ReadOnly = true;
            // 
            // txtCursorPos
            // 
            this.txtCursorPos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCursorPos.Location = new System.Drawing.Point(97, 901);
            this.txtCursorPos.Name = "txtCursorPos";
            this.txtCursorPos.Size = new System.Drawing.Size(1013, 20);
            this.txtCursorPos.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 904);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Cursor position:";
            // 
            // trackMap
            // 
            this.trackMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackMap.ChartAxes = null;
            this.trackMap.ChartName = "Chart";
            this.trackMap.ClientSize = new System.Drawing.Size(299, 406);
            this.trackMap.Location = new System.Drawing.Point(796, 198);
            this.trackMap.Logger = null;
            this.trackMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trackMap.Name = "trackMap";
            this.trackMap.Visible = false;
            // 
            // tractionCircle
            // 
            this.tractionCircle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tractionCircle.ChartAxes = null;
            this.tractionCircle.ChartName = "Chart";
            this.tractionCircle.ClientSize = new System.Drawing.Size(299, 141);
            this.tractionCircle.Location = new System.Drawing.Point(795, 12);
            this.tractionCircle.Logger = null;
            this.tractionCircle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tractionCircle.Name = "tractionCircle";
            this.tractionCircle.Visible = false;
            // 
            // stripChart
            // 
            this.stripChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.stripChart.ChartAxes = null;
            this.stripChart.ChartName = "Chart";
            this.stripChart.ClientSize = new System.Drawing.Size(762, 404);
            this.stripChart.Location = new System.Drawing.Point(11, 200);
            this.stripChart.Logger = null;
            this.stripChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stripChart.Name = "stripChart";
            this.stripChart.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1115, 24);
            this.menuStrip1.TabIndex = 37;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRunsMenuItem,
            this.clearRunsMenuItem,
            this.uploadFromLoggerToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.autoAlignToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.filesToolStripMenuItem.Text = "File";
            // 
            // addRunsMenuItem
            // 
            this.addRunsMenuItem.Name = "addRunsMenuItem";
            this.addRunsMenuItem.Size = new System.Drawing.Size(181, 22);
            this.addRunsMenuItem.Text = "Add Runs";
            this.addRunsMenuItem.Click += new System.EventHandler(this.addRunsMenuItem_Click);
            // 
            // clearRunsMenuItem
            // 
            this.clearRunsMenuItem.Name = "clearRunsMenuItem";
            this.clearRunsMenuItem.Size = new System.Drawing.Size(181, 22);
            this.clearRunsMenuItem.Text = "Clear Runs";
            this.clearRunsMenuItem.Click += new System.EventHandler(this.clearRunsMenuItem_Click);
            // 
            // uploadFromLoggerToolStripMenuItem
            // 
            this.uploadFromLoggerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadAndDeleteMenuItem,
            this.uploadOnlyMenuItem});
            this.uploadFromLoggerToolStripMenuItem.Name = "uploadFromLoggerToolStripMenuItem";
            this.uploadFromLoggerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.uploadFromLoggerToolStripMenuItem.Text = "Upload from Logger";
            // 
            // uploadAndDeleteMenuItem
            // 
            this.uploadAndDeleteMenuItem.Name = "uploadAndDeleteMenuItem";
            this.uploadAndDeleteMenuItem.Size = new System.Drawing.Size(171, 22);
            this.uploadAndDeleteMenuItem.Text = "Upload and Delete";
            this.uploadAndDeleteMenuItem.Click += new System.EventHandler(this.uploadAndDeleteMenuItem_Click);
            // 
            // uploadOnlyMenuItem
            // 
            this.uploadOnlyMenuItem.Name = "uploadOnlyMenuItem";
            this.uploadOnlyMenuItem.Size = new System.Drawing.Size(171, 22);
            this.uploadOnlyMenuItem.Text = "Upload Only";
            this.uploadOnlyMenuItem.Click += new System.EventHandler(this.uploadOnlyMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // autoAlignToolStripMenuItem
            // 
            this.autoAlignToolStripMenuItem.Name = "autoAlignToolStripMenuItem";
            this.autoAlignToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.autoAlignToolStripMenuItem.Text = "AutoAlign";
            this.autoAlignToolStripMenuItem.Click += new System.EventHandler(this.autoAlignToolStripMenuItem_Click);
            // 
            // dockPanel1
            // 
            this.dockPanel1.BackColor = System.Drawing.Color.White;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(125, 12);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(978, 658);
            this.dockPanel1.TabIndex = 38;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 682);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCursorPos);
            //this.Controls.Add(this.runDataGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Team Yamura data logger viewer";
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openLogFile;
        private System.Windows.Forms.DataGridView runDataGrid;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox txtCursorPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRunNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colShowRun;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTraceColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOffsetTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSourceFile;
        private ChartControl stripChart;
        private ChartControl tractionCircle;
        private ChartControl trackMap;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRunsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRunsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFromLoggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadAndDeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadOnlyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoAlignToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
    }
}

