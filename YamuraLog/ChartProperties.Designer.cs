namespace YamuraLog
{
    partial class ChartProperties
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
            this.axisChannelTree = new System.Windows.Forms.TreeView();
            this.channelsContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblAxisMin = new System.Windows.Forms.ToolStripTextBox();
            this.txtAxisMinValue = new System.Windows.Forms.ToolStripTextBox();
            this.lblAxisMax = new System.Windows.Forms.ToolStripTextBox();
            this.txtAxisMaxValue = new System.Windows.Forms.ToolStripTextBox();
            this.cmbXAxis = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.axisOffsetsGrid = new System.Windows.Forms.DataGridView();
            this.axisChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.axisStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.axisEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.axisOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelsContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axisOffsetsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // axisChannelTree
            // 
            this.axisChannelTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axisChannelTree.CheckBoxes = true;
            this.axisChannelTree.ContextMenuStrip = this.channelsContext;
            this.axisChannelTree.Location = new System.Drawing.Point(12, 144);
            this.axisChannelTree.Name = "axisChannelTree";
            this.axisChannelTree.Size = new System.Drawing.Size(348, 240);
            this.axisChannelTree.TabIndex = 30;
            this.axisChannelTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.axisChannelTree_AfterCheck);
            // 
            // channelsContext
            // 
            this.channelsContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.lblAxisMin,
            this.txtAxisMinValue,
            this.lblAxisMax,
            this.txtAxisMaxValue});
            this.channelsContext.Name = "channelsContext";
            this.channelsContext.Size = new System.Drawing.Size(161, 126);
            this.channelsContext.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.channelsContext_Closed);
            this.channelsContext.Opening += new System.ComponentModel.CancelEventHandler(this.channelsContext_Opening);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // lblAxisMin
            // 
            this.lblAxisMin.Name = "lblAxisMin";
            this.lblAxisMin.Size = new System.Drawing.Size(100, 23);
            this.lblAxisMin.Text = "Axis Minimum";
            // 
            // txtAxisMinValue
            // 
            this.txtAxisMinValue.Name = "txtAxisMinValue";
            this.txtAxisMinValue.Size = new System.Drawing.Size(100, 23);
            // 
            // lblAxisMax
            // 
            this.lblAxisMax.Name = "lblAxisMax";
            this.lblAxisMax.Size = new System.Drawing.Size(100, 23);
            this.lblAxisMax.Text = "Axis Maximum";
            // 
            // txtAxisMaxValue
            // 
            this.txtAxisMaxValue.Name = "txtAxisMaxValue";
            this.txtAxisMaxValue.Size = new System.Drawing.Size(100, 23);
            // 
            // cmbXAxis
            // 
            this.cmbXAxis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbXAxis.FormattingEnabled = true;
            this.cmbXAxis.Location = new System.Drawing.Point(54, 12);
            this.cmbXAxis.Name = "cmbXAxis";
            this.cmbXAxis.Size = new System.Drawing.Size(306, 21);
            this.cmbXAxis.TabIndex = 32;
            this.cmbXAxis.SelectedIndexChanged += new System.EventHandler(this.cmbXAxis_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "X Axis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Y Axis Channels";
            // 
            // axisOffsetsGrid
            // 
            this.axisOffsetsGrid.AllowUserToAddRows = false;
            this.axisOffsetsGrid.AllowUserToDeleteRows = false;
            this.axisOffsetsGrid.AllowUserToOrderColumns = true;
            this.axisOffsetsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.axisOffsetsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.axisChannel,
            this.axisStart,
            this.axisEnd,
            this.axisOffset});
            this.axisOffsetsGrid.Location = new System.Drawing.Point(15, 41);
            this.axisOffsetsGrid.Name = "axisOffsetsGrid";
            this.axisOffsetsGrid.RowHeadersVisible = false;
            this.axisOffsetsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.axisOffsetsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.axisOffsetsGrid.Size = new System.Drawing.Size(345, 84);
            this.axisOffsetsGrid.TabIndex = 34;
            // 
            // axisChannel
            // 
            this.axisChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.axisChannel.HeaderText = "Channel";
            this.axisChannel.Name = "axisChannel";
            this.axisChannel.Width = 71;
            // 
            // axisStart
            // 
            this.axisStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.axisStart.HeaderText = "Start";
            this.axisStart.Name = "axisStart";
            this.axisStart.Width = 54;
            // 
            // axisEnd
            // 
            this.axisEnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.axisEnd.HeaderText = "End";
            this.axisEnd.Name = "axisEnd";
            this.axisEnd.Width = 51;
            // 
            // axisOffset
            // 
            this.axisOffset.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.axisOffset.HeaderText = "Offset";
            this.axisOffset.Name = "axisOffset";
            // 
            // ChartProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 396);
            this.CloseButton = false;
            this.Controls.Add(this.axisOffsetsGrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbXAxis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.axisChannelTree);
            this.Name = "ChartProperties";
            this.Text = "ChartControlAxesSelect";
            this.channelsContext.ResumeLayout(false);
            this.channelsContext.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axisOffsetsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView axisChannelTree;
        private System.Windows.Forms.ComboBox cmbXAxis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip channelsContext;
        private System.Windows.Forms.ToolStripTextBox lblAxisMin;
        private System.Windows.Forms.ToolStripTextBox txtAxisMinValue;
        private System.Windows.Forms.ToolStripTextBox lblAxisMax;
        private System.Windows.Forms.ToolStripTextBox txtAxisMaxValue;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.DataGridView axisOffsetsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisOffset;
    }
}