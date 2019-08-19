namespace YamuraLog
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openLogFile = new System.Windows.Forms.OpenFileDialog();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.tractionCirclePanel = new System.Windows.Forms.Panel();
            this.stripChartPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.channelDataGrid = new System.Windows.Forms.DataGridView();
            this.displayChannel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.channelColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelsContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.channelExtents = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbXAxis = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stripchartHScroll = new System.Windows.Forms.HScrollBar();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.yAxisDataGrid = new System.Windows.Forms.DataGridView();
            this.yAxisUse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.yAxisName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.axisMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.axisMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelDataGrid)).BeginInit();
            this.channelsContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yAxisDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(1019, 7);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(90, 23);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Add Runs";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // mapPanel
            // 
            this.mapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapPanel.BackColor = System.Drawing.Color.White;
            this.mapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPanel.Location = new System.Drawing.Point(795, 574);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(314, 321);
            this.mapPanel.TabIndex = 4;
            this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.trackMap_Paint);
            this.mapPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.trackMap_Layout);
            this.mapPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trackMap_MouseMove);
            this.mapPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackMap_MouseUp);
            this.mapPanel.Resize += new System.EventHandler(this.trackMap_Resize);
            // 
            // tractionCirclePanel
            // 
            this.tractionCirclePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tractionCirclePanel.BackColor = System.Drawing.Color.White;
            this.tractionCirclePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tractionCirclePanel.Location = new System.Drawing.Point(795, 409);
            this.tractionCirclePanel.Name = "tractionCirclePanel";
            this.tractionCirclePanel.Size = new System.Drawing.Size(314, 159);
            this.tractionCirclePanel.TabIndex = 5;
            this.tractionCirclePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tractionCircle_Paint);
            this.tractionCirclePanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.tractionCircle_Layout);
            this.tractionCirclePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tractionCircle_MouseMove);
            this.tractionCirclePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tractionCircle_MouseUp);
            this.tractionCirclePanel.Resize += new System.EventHandler(this.tractionCircle_Resize);
            // 
            // stripChartPanel
            // 
            this.stripChartPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stripChartPanel.BackColor = System.Drawing.Color.White;
            this.stripChartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stripChartPanel.Location = new System.Drawing.Point(11, 198);
            this.stripChartPanel.Name = "stripChartPanel";
            this.stripChartPanel.Size = new System.Drawing.Size(778, 677);
            this.stripChartPanel.TabIndex = 6;
            this.stripChartPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.stripChart_Paint);
            this.stripChartPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.stripChartPanel_Layout);
            this.stripChartPanel.MouseLeave += new System.EventHandler(this.stripChart_MouseLeave);
            this.stripChartPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.stripChart_MouseMove);
            this.stripChartPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stripChart_MouseUp);
            this.stripChartPanel.Resize += new System.EventHandler(this.stripChart_Resize);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(795, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Stripchart X Axis (one)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(796, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Stripchart Channels (any)";
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
            this.runDataGrid.Size = new System.Drawing.Size(661, 180);
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
            this.btnAutoAlign.Location = new System.Drawing.Point(1019, 57);
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
            this.txtAutoAlignThreshold.Location = new System.Drawing.Point(957, 57);
            this.txtAutoAlignThreshold.Name = "txtAutoAlignThreshold";
            this.txtAutoAlignThreshold.Size = new System.Drawing.Size(58, 20);
            this.txtAutoAlignThreshold.TabIndex = 21;
            this.txtAutoAlignThreshold.Text = "0.10";
            // 
            // channelDataGrid
            // 
            this.channelDataGrid.AllowUserToAddRows = false;
            this.channelDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.channelDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.channelDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.displayChannel,
            this.channelColor,
            this.runName,
            this.channelName,
            this.channelDescription});
            this.channelDataGrid.ContextMenuStrip = this.channelsContext;
            this.channelDataGrid.Location = new System.Drawing.Point(795, 83);
            this.channelDataGrid.Name = "channelDataGrid";
            this.channelDataGrid.RowHeadersVisible = false;
            this.channelDataGrid.Size = new System.Drawing.Size(314, 109);
            this.channelDataGrid.TabIndex = 22;
            // 
            // displayChannel
            // 
            this.displayChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.displayChannel.HeaderText = "Show";
            this.displayChannel.Name = "displayChannel";
            this.displayChannel.Width = 40;
            // 
            // channelColor
            // 
            this.channelColor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.channelColor.HeaderText = "Color";
            this.channelColor.Name = "channelColor";
            this.channelColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.channelColor.Width = 37;
            // 
            // runName
            // 
            this.runName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.runName.HeaderText = "Run";
            this.runName.Name = "runName";
            this.runName.Width = 52;
            // 
            // channelName
            // 
            this.channelName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            this.channelName.DefaultCellStyle = dataGridViewCellStyle1;
            this.channelName.HeaderText = "Channel";
            this.channelName.Name = "channelName";
            this.channelName.Width = 71;
            // 
            // channelDescription
            // 
            this.channelDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.channelDescription.HeaderText = "Description";
            this.channelDescription.Name = "channelDescription";
            // 
            // channelsContext
            // 
            this.channelsContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelExtents});
            this.channelsContext.Name = "channelsContext";
            this.channelsContext.Size = new System.Drawing.Size(159, 26);
            // 
            // channelExtents
            // 
            this.channelExtents.Name = "channelExtents";
            this.channelExtents.Size = new System.Drawing.Size(158, 22);
            this.channelExtents.Text = "Channel Extents";
            this.channelExtents.Click += new System.EventHandler(this.channelExtents_Click);
            // 
            // cmbXAxis
            // 
            this.cmbXAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbXAxis.FormattingEnabled = true;
            this.cmbXAxis.Location = new System.Drawing.Point(795, 33);
            this.cmbXAxis.Name = "cmbXAxis";
            this.cmbXAxis.Size = new System.Drawing.Size(220, 21);
            this.cmbXAxis.TabIndex = 23;
            this.cmbXAxis.SelectedIndexChanged += new System.EventHandler(this.cmbXAxis_SelectedIndexChanged);
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
            // stripchartHScroll
            // 
            this.stripchartHScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stripchartHScroll.Location = new System.Drawing.Point(12, 878);
            this.stripchartHScroll.Name = "stripchartHScroll";
            this.stripchartHScroll.Size = new System.Drawing.Size(777, 17);
            this.stripchartHScroll.TabIndex = 25;
            this.stripchartHScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.stripchartHScroll_Scroll);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.Location = new System.Drawing.Point(1019, 31);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(90, 23);
            this.btnClearAll.TabIndex = 26;
            this.btnClearAll.Text = "Clear Runs";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // yAxisDataGrid
            // 
            this.yAxisDataGrid.AllowUserToAddRows = false;
            this.yAxisDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yAxisDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.yAxisDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.yAxisUse,
            this.yAxisName,
            this.axisMin,
            this.axisMax});
            this.yAxisDataGrid.ContextMenuStrip = this.channelsContext;
            this.yAxisDataGrid.Location = new System.Drawing.Point(795, 198);
            this.yAxisDataGrid.Name = "yAxisDataGrid";
            this.yAxisDataGrid.RowHeadersVisible = false;
            this.yAxisDataGrid.Size = new System.Drawing.Size(314, 205);
            this.yAxisDataGrid.TabIndex = 27;
            this.yAxisDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.yAxisDataGrid_CellEndEdit);
            this.yAxisDataGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.yAxisDataGrid_CellValidated);
            this.yAxisDataGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.yAxisDataGrid_CellValidating);
            this.yAxisDataGrid.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.yAxisDataGrid_RowValidated);
            this.yAxisDataGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.yAxisDataGrid_RowValidating);
            // 
            // yAxisUse
            // 
            this.yAxisUse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.yAxisUse.HeaderText = "Use";
            this.yAxisUse.Name = "yAxisUse";
            this.yAxisUse.Width = 32;
            // 
            // yAxisName
            // 
            this.yAxisName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            this.yAxisName.DefaultCellStyle = dataGridViewCellStyle2;
            this.yAxisName.HeaderText = "Axis Name";
            this.yAxisName.Name = "yAxisName";
            // 
            // axisMin
            // 
            this.axisMin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.axisMin.HeaderText = "Min";
            this.axisMin.Name = "axisMin";
            this.axisMin.Width = 49;
            // 
            // axisMax
            // 
            this.axisMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.axisMax.HeaderText = "Max";
            this.axisMax.Name = "axisMax";
            this.axisMax.Width = 52;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 925);
            this.Controls.Add(this.yAxisDataGrid);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.stripchartHScroll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbXAxis);
            this.Controls.Add(this.channelDataGrid);
            this.Controls.Add(this.txtAutoAlignThreshold);
            this.Controls.Add(this.btnAutoAlign);
            this.Controls.Add(this.txtCursorPos);
            this.Controls.Add(this.runDataGrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stripChartPanel);
            this.Controls.Add(this.tractionCirclePanel);
            this.Controls.Add(this.mapPanel);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "Form1";
            this.Text = "Team Yamura data logger viewer";
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelDataGrid)).EndInit();
            this.channelsContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.yAxisDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openLogFile;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.Panel tractionCirclePanel;
        private System.Windows.Forms.Panel stripChartPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView runDataGrid;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox txtCursorPos;
        private System.Windows.Forms.Button btnAutoAlign;
        private System.Windows.Forms.TextBox txtAutoAlignThreshold;
        private System.Windows.Forms.DataGridView channelDataGrid;
        private System.Windows.Forms.ComboBox cmbXAxis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar stripchartHScroll;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRunNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colShowRun;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTraceColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOffsetTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSourceFile;
        private System.Windows.Forms.ContextMenuStrip channelsContext;
        private System.Windows.Forms.ToolStripMenuItem channelExtents;
        private System.Windows.Forms.DataGridViewCheckBoxColumn displayChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn runName;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelDescription;
        private System.Windows.Forms.DataGridView yAxisDataGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn yAxisUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn yAxisName;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisMax;
    }
}

