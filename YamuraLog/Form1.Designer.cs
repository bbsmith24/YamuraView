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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openLogFile = new System.Windows.Forms.OpenFileDialog();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.tractionCirclePanel = new System.Windows.Forms.Panel();
            this.stripChartPanel = new System.Windows.Forms.Panel();
            this.txtRunInfo = new System.Windows.Forms.TextBox();
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
            this.colAccelX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccelY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccelZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSourceFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.txtCursorPos = new System.Windows.Forms.TextBox();
            this.btnAutoAlign = new System.Windows.Forms.Button();
            this.txtAutoAlignThreshold = new System.Windows.Forms.TextBox();
            this.channelDataGrid = new System.Windows.Forms.DataGridView();
            this.cmbXAxis = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stripchartHScroll = new System.Windows.Forms.HScrollBar();
            this.displayChannel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.channelColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(512, 201);
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
            this.mapPanel.Location = new System.Drawing.Point(795, 409);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(314, 486);
            this.mapPanel.TabIndex = 4;
            this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.trackMap_Paint);
            this.mapPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.trackMap_Layout);
            this.mapPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trackMap_MouseMove);
            this.mapPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackMap_MouseUp);
            this.mapPanel.Resize += new System.EventHandler(this.trackMap_Resize);
            // 
            // tractionCirclePanel
            // 
            this.tractionCirclePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tractionCirclePanel.BackColor = System.Drawing.Color.White;
            this.tractionCirclePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tractionCirclePanel.Location = new System.Drawing.Point(12, 198);
            this.tractionCirclePanel.Name = "tractionCirclePanel";
            this.tractionCirclePanel.Size = new System.Drawing.Size(209, 205);
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
            this.stripChartPanel.Location = new System.Drawing.Point(12, 409);
            this.stripChartPanel.Name = "stripChartPanel";
            this.stripChartPanel.Size = new System.Drawing.Size(777, 466);
            this.stripChartPanel.TabIndex = 6;
            this.stripChartPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.stripChart_Paint);
            this.stripChartPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.stripChartPanel_Layout);
            this.stripChartPanel.MouseLeave += new System.EventHandler(this.stripChart_MouseLeave);
            this.stripChartPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.stripChart_MouseMove);
            this.stripChartPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stripChart_MouseUp);
            this.stripChartPanel.Resize += new System.EventHandler(this.stripChart_Resize);
            // 
            // txtRunInfo
            // 
            this.txtRunInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRunInfo.Location = new System.Drawing.Point(227, 198);
            this.txtRunInfo.Multiline = true;
            this.txtRunInfo.Name = "txtRunInfo";
            this.txtRunInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRunInfo.Size = new System.Drawing.Size(279, 205);
            this.txtRunInfo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(532, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Stripchart X Axis (one)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(795, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Stripchart Y Axis (any)";
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
            this.colAccelX,
            this.colAccelY,
            this.colAccelZ,
            this.colSpeed,
            this.colSourceFile});
            this.runDataGrid.Location = new System.Drawing.Point(11, 12);
            this.runDataGrid.Name = "runDataGrid";
            this.runDataGrid.RowHeadersVisible = false;
            this.runDataGrid.Size = new System.Drawing.Size(778, 180);
            this.runDataGrid.TabIndex = 18;
            // 
            // colRunNumber
            // 
            this.colRunNumber.HeaderText = "Run";
            this.colRunNumber.Name = "colRunNumber";
            this.colRunNumber.ReadOnly = true;
            this.colRunNumber.Width = 50;
            // 
            // colShowRun
            // 
            this.colShowRun.HeaderText = "Show";
            this.colShowRun.Name = "colShowRun";
            this.colShowRun.Width = 50;
            // 
            // colTraceColor
            // 
            this.colTraceColor.HeaderText = "Color";
            this.colTraceColor.Name = "colTraceColor";
            this.colTraceColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTraceColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTraceColor.Width = 50;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Date/Time";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colMinTime
            // 
            this.colMinTime.HeaderText = "Start";
            this.colMinTime.Name = "colMinTime";
            this.colMinTime.ReadOnly = true;
            // 
            // colMaxTime
            // 
            this.colMaxTime.HeaderText = "End";
            this.colMaxTime.Name = "colMaxTime";
            this.colMaxTime.ReadOnly = true;
            // 
            // colOffsetTime
            // 
            this.colOffsetTime.HeaderText = "Offset";
            this.colOffsetTime.Name = "colOffsetTime";
            // 
            // colAccelX
            // 
            this.colAccelX.HeaderText = "X";
            this.colAccelX.Name = "colAccelX";
            this.colAccelX.ReadOnly = true;
            // 
            // colAccelY
            // 
            this.colAccelY.HeaderText = "Y";
            this.colAccelY.Name = "colAccelY";
            this.colAccelY.ReadOnly = true;
            // 
            // colAccelZ
            // 
            this.colAccelZ.HeaderText = "Z";
            this.colAccelZ.Name = "colAccelZ";
            this.colAccelZ.ReadOnly = true;
            // 
            // colSpeed
            // 
            this.colSpeed.HeaderText = "Speed";
            this.colSpeed.Name = "colSpeed";
            this.colSpeed.ReadOnly = true;
            // 
            // colSourceFile
            // 
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
            this.btnAutoAlign.Location = new System.Drawing.Point(616, 201);
            this.btnAutoAlign.Name = "btnAutoAlign";
            this.btnAutoAlign.Size = new System.Drawing.Size(75, 23);
            this.btnAutoAlign.TabIndex = 20;
            this.btnAutoAlign.Text = "AutoAlign";
            this.btnAutoAlign.UseVisualStyleBackColor = true;
            this.btnAutoAlign.Click += new System.EventHandler(this.btnAutoAlign_Click);
            // 
            // txtAutoAlignThreshold
            // 
            this.txtAutoAlignThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAutoAlignThreshold.Enabled = false;
            this.txtAutoAlignThreshold.Location = new System.Drawing.Point(697, 203);
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
            this.channelName});
            this.channelDataGrid.Location = new System.Drawing.Point(795, 28);
            this.channelDataGrid.Name = "channelDataGrid";
            this.channelDataGrid.RowHeadersVisible = false;
            this.channelDataGrid.Size = new System.Drawing.Size(314, 375);
            this.channelDataGrid.TabIndex = 22;
            // 
            // cmbXAxis
            // 
            this.cmbXAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbXAxis.FormattingEnabled = true;
            this.cmbXAxis.Location = new System.Drawing.Point(535, 282);
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
            // displayChannel
            // 
            this.displayChannel.HeaderText = "Show";
            this.displayChannel.Name = "displayChannel";
            this.displayChannel.Width = 50;
            // 
            // channelColor
            // 
            this.channelColor.HeaderText = "Color";
            this.channelColor.Name = "channelColor";
            this.channelColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.channelColor.Width = 50;
            // 
            // runName
            // 
            this.runName.HeaderText = "Run";
            this.runName.Name = "runName";
            // 
            // channelName
            // 
            this.channelName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            this.channelName.DefaultCellStyle = dataGridViewCellStyle2;
            this.channelName.HeaderText = "Channel";
            this.channelName.Name = "channelName";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 925);
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
            this.Controls.Add(this.txtRunInfo);
            this.Controls.Add(this.stripChartPanel);
            this.Controls.Add(this.tractionCirclePanel);
            this.Controls.Add(this.mapPanel);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "Form1";
            this.Text = "Team Yamura data logger viewer";
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openLogFile;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.Panel tractionCirclePanel;
        private System.Windows.Forms.Panel stripChartPanel;
        private System.Windows.Forms.TextBox txtRunInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView runDataGrid;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox txtCursorPos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRunNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colShowRun;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTraceColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOffsetTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccelX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccelY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccelZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSourceFile;
        private System.Windows.Forms.Button btnAutoAlign;
        private System.Windows.Forms.TextBox txtAutoAlignThreshold;
        private System.Windows.Forms.DataGridView channelDataGrid;
        private System.Windows.Forms.ComboBox cmbXAxis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar stripchartHScroll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn displayChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn runName;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelName;
    }
}

