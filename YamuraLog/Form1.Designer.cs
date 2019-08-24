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
            this.btnOpenFile = new System.Windows.Forms.Button();
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
            this.btnAutoAlign = new System.Windows.Forms.Button();
            this.txtAutoAlignThreshold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.trackMap = new YamuraLog.ChartControl();
            this.tractionCircle = new YamuraLog.ChartControl();
            this.stripChart = new YamuraLog.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(699, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(90, 23);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Add Runs";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // runDataGrid
            // 
            this.runDataGrid.AllowUserToAddRows = false;
            this.runDataGrid.AllowUserToDeleteRows = false;
            this.runDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.runDataGrid.Location = new System.Drawing.Point(11, 12);
            this.runDataGrid.Name = "runDataGrid";
            this.runDataGrid.RowHeadersVisible = false;
            this.runDataGrid.Size = new System.Drawing.Size(682, 180);
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
            this.txtCursorPos.Size = new System.Drawing.Size(1012, 20);
            this.txtCursorPos.TabIndex = 19;
            // 
            // btnAutoAlign
            // 
            this.btnAutoAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoAlign.Enabled = false;
            this.btnAutoAlign.Location = new System.Drawing.Point(699, 70);
            this.btnAutoAlign.Name = "btnAutoAlign";
            this.btnAutoAlign.Size = new System.Drawing.Size(90, 23);
            this.btnAutoAlign.TabIndex = 20;
            this.btnAutoAlign.Text = "AutoAlign";
            this.btnAutoAlign.UseVisualStyleBackColor = true;
            this.btnAutoAlign.Click += new System.EventHandler(this.btnAutoAlign_Click);
            // 
            // txtAutoAlignThreshold
            // 
            this.txtAutoAlignThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAutoAlignThreshold.Enabled = false;
            this.txtAutoAlignThreshold.Location = new System.Drawing.Point(699, 99);
            this.txtAutoAlignThreshold.Name = "txtAutoAlignThreshold";
            this.txtAutoAlignThreshold.Size = new System.Drawing.Size(58, 20);
            this.txtAutoAlignThreshold.TabIndex = 21;
            this.txtAutoAlignThreshold.Text = "0.10";
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
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.Location = new System.Drawing.Point(699, 41);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(90, 23);
            this.btnClearAll.TabIndex = 26;
            this.btnClearAll.Text = "Clear Runs";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // trackMap
            // 
            this.trackMap.AllowDrag = true;
            this.trackMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trackMap.ChartAxes = null;
            this.trackMap.CursorBoxSize = 10;
            this.trackMap.CursorMode = YamuraLog.ChartControl.CursorStyle.CROSSHAIRS;
            this.trackMap.CursorUpdateSource = true;
            this.trackMap.Location = new System.Drawing.Point(795, 198);
            this.trackMap.Logger = null;
            this.trackMap.Name = "trackMap";
            this.trackMap.ShowHScroll = true;
            this.trackMap.ShowVScroll = true;
            this.trackMap.Size = new System.Drawing.Size(314, 697);
            this.trackMap.TabIndex = 36;
            // 
            // tractionCircle
            // 
            this.tractionCircle.AllowDrag = true;
            this.tractionCircle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tractionCircle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tractionCircle.ChartAxes = null;
            this.tractionCircle.CursorBoxSize = 10;
            this.tractionCircle.CursorMode = YamuraLog.ChartControl.CursorStyle.CROSSHAIRS;
            this.tractionCircle.CursorUpdateSource = true;
            this.tractionCircle.Location = new System.Drawing.Point(795, 12);
            this.tractionCircle.Logger = null;
            this.tractionCircle.Name = "tractionCircle";
            this.tractionCircle.ShowHScroll = true;
            this.tractionCircle.ShowVScroll = true;
            this.tractionCircle.Size = new System.Drawing.Size(314, 180);
            this.tractionCircle.TabIndex = 35;
            // 
            // stripChart
            // 
            this.stripChart.AllowDrag = true;
            this.stripChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stripChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stripChart.ChartAxes = null;
            this.stripChart.CursorBoxSize = 10;
            this.stripChart.CursorMode = YamuraLog.ChartControl.CursorStyle.CROSSHAIRS;
            this.stripChart.CursorUpdateSource = false;
            this.stripChart.Location = new System.Drawing.Point(11, 198);
            this.stripChart.Logger = null;
            this.stripChart.Name = "stripChart";
            this.stripChart.ShowHScroll = true;
            this.stripChart.ShowVScroll = true;
            this.stripChart.Size = new System.Drawing.Size(778, 697);
            this.stripChart.TabIndex = 34;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 925);
            this.Controls.Add(this.trackMap);
            this.Controls.Add(this.tractionCircle);
            this.Controls.Add(this.stripChart);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAutoAlignThreshold);
            this.Controls.Add(this.btnAutoAlign);
            this.Controls.Add(this.txtCursorPos);
            this.Controls.Add(this.runDataGrid);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "Form1";
            this.Text = "Team Yamura data logger viewer";
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openLogFile;
        private System.Windows.Forms.DataGridView runDataGrid;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox txtCursorPos;
        private System.Windows.Forms.Button btnAutoAlign;
        private System.Windows.Forms.TextBox txtAutoAlignThreshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearAll;
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
    }
}

