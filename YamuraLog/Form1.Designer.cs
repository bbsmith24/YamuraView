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
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openLogFile = new System.Windows.Forms.OpenFileDialog();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.tractionCirclePanel = new System.Windows.Forms.Panel();
            this.stripChartPanel = new System.Windows.Forms.Panel();
            this.txtRunInfo = new System.Windows.Forms.TextBox();
            this.chkSpeed = new System.Windows.Forms.CheckBox();
            this.chkXAccel = new System.Windows.Forms.CheckBox();
            this.chkYAccel = new System.Windows.Forms.CheckBox();
            this.chkZAccel = new System.Windows.Forms.CheckBox();
            this.chkDist = new System.Windows.Forms.CheckBox();
            this.chkTime = new System.Windows.Forms.CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.runDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(904, 198);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(198, 23);
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
            this.mapPanel.Location = new System.Drawing.Point(795, 415);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(314, 480);
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
            this.tractionCirclePanel.Size = new System.Drawing.Size(209, 211);
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
            this.stripChartPanel.Location = new System.Drawing.Point(12, 415);
            this.stripChartPanel.Name = "stripChartPanel";
            this.stripChartPanel.Size = new System.Drawing.Size(777, 480);
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
            this.txtRunInfo.Size = new System.Drawing.Size(671, 211);
            this.txtRunInfo.TabIndex = 7;
            // 
            // chkSpeed
            // 
            this.chkSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSpeed.AutoSize = true;
            this.chkSpeed.Checked = true;
            this.chkSpeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpeed.Location = new System.Drawing.Point(998, 278);
            this.chkSpeed.Name = "chkSpeed";
            this.chkSpeed.Size = new System.Drawing.Size(57, 17);
            this.chkSpeed.TabIndex = 8;
            this.chkSpeed.Text = "Speed";
            this.chkSpeed.UseVisualStyleBackColor = true;
            this.chkSpeed.CheckedChanged += new System.EventHandler(this.chkSpeed_CheckedChanged);
            // 
            // chkXAccel
            // 
            this.chkXAccel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkXAccel.AutoSize = true;
            this.chkXAccel.Location = new System.Drawing.Point(998, 301);
            this.chkXAccel.Name = "chkXAccel";
            this.chkXAccel.Size = new System.Drawing.Size(95, 17);
            this.chkXAccel.TabIndex = 9;
            this.chkXAccel.Text = "X Acceleration";
            this.chkXAccel.UseVisualStyleBackColor = true;
            this.chkXAccel.CheckedChanged += new System.EventHandler(this.chkXAccel_CheckedChanged);
            // 
            // chkYAccel
            // 
            this.chkYAccel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkYAccel.AutoSize = true;
            this.chkYAccel.Location = new System.Drawing.Point(998, 324);
            this.chkYAccel.Name = "chkYAccel";
            this.chkYAccel.Size = new System.Drawing.Size(95, 17);
            this.chkYAccel.TabIndex = 10;
            this.chkYAccel.Text = "Y Acceleration";
            this.chkYAccel.UseVisualStyleBackColor = true;
            this.chkYAccel.CheckedChanged += new System.EventHandler(this.chkYAccel_CheckedChanged);
            // 
            // chkZAccel
            // 
            this.chkZAccel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkZAccel.AutoSize = true;
            this.chkZAccel.Location = new System.Drawing.Point(998, 347);
            this.chkZAccel.Name = "chkZAccel";
            this.chkZAccel.Size = new System.Drawing.Size(95, 17);
            this.chkZAccel.TabIndex = 11;
            this.chkZAccel.Text = "Z Acceleration";
            this.chkZAccel.UseVisualStyleBackColor = true;
            this.chkZAccel.CheckedChanged += new System.EventHandler(this.chkZAccel_CheckedChanged);
            // 
            // chkDist
            // 
            this.chkDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDist.AutoSize = true;
            this.chkDist.Location = new System.Drawing.Point(908, 301);
            this.chkDist.Name = "chkDist";
            this.chkDist.Size = new System.Drawing.Size(68, 17);
            this.chkDist.TabIndex = 13;
            this.chkDist.Text = "Distance";
            this.chkDist.UseVisualStyleBackColor = true;
            this.chkDist.CheckedChanged += new System.EventHandler(this.chkDist_CheckedChanged);
            // 
            // chkTime
            // 
            this.chkTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTime.AutoSize = true;
            this.chkTime.Checked = true;
            this.chkTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTime.Location = new System.Drawing.Point(908, 278);
            this.chkTime.Name = "chkTime";
            this.chkTime.Size = new System.Drawing.Size(49, 17);
            this.chkTime.TabIndex = 12;
            this.chkTime.Text = "Time";
            this.chkTime.UseVisualStyleBackColor = true;
            this.chkTime.CheckedChanged += new System.EventHandler(this.chkTime_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(909, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "X Axis (one)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(995, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Y Axis (any)";
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
            this.runDataGrid.Size = new System.Drawing.Size(1091, 180);
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
            this.txtCursorPos.Location = new System.Drawing.Point(12, 901);
            this.txtCursorPos.Name = "txtCursorPos";
            this.txtCursorPos.Size = new System.Drawing.Size(1097, 20);
            this.txtCursorPos.TabIndex = 19;
            // 
            // btnAutoAlign
            // 
            this.btnAutoAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoAlign.Location = new System.Drawing.Point(906, 227);
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
            this.txtAutoAlignThreshold.Location = new System.Drawing.Point(987, 229);
            this.txtAutoAlignThreshold.Name = "txtAutoAlignThreshold";
            this.txtAutoAlignThreshold.Size = new System.Drawing.Size(100, 20);
            this.txtAutoAlignThreshold.TabIndex = 21;
            this.txtAutoAlignThreshold.Text = "0.10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 925);
            this.Controls.Add(this.txtAutoAlignThreshold);
            this.Controls.Add(this.btnAutoAlign);
            this.Controls.Add(this.txtCursorPos);
            this.Controls.Add(this.runDataGrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkDist);
            this.Controls.Add(this.chkTime);
            this.Controls.Add(this.chkZAccel);
            this.Controls.Add(this.chkYAccel);
            this.Controls.Add(this.chkXAccel);
            this.Controls.Add(this.chkSpeed);
            this.Controls.Add(this.txtRunInfo);
            this.Controls.Add(this.stripChartPanel);
            this.Controls.Add(this.tractionCirclePanel);
            this.Controls.Add(this.mapPanel);
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
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.Panel tractionCirclePanel;
        private System.Windows.Forms.Panel stripChartPanel;
        private System.Windows.Forms.TextBox txtRunInfo;
        private System.Windows.Forms.CheckBox chkSpeed;
        private System.Windows.Forms.CheckBox chkXAccel;
        private System.Windows.Forms.CheckBox chkYAccel;
        private System.Windows.Forms.CheckBox chkZAccel;
        private System.Windows.Forms.CheckBox chkDist;
        private System.Windows.Forms.CheckBox chkTime;
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
    }
}

