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
            this.cmbXAxis = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAxisMax = new System.Windows.Forms.ToolStripTextBox();
            this.lblAxisMin = new System.Windows.Forms.ToolStripTextBox();
            this.txtAxisMinValue = new System.Windows.Forms.ToolStripTextBox();
            this.txtAxisMaxValue = new System.Windows.Forms.ToolStripTextBox();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // axisChannelTree
            // 
            this.axisChannelTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axisChannelTree.CheckBoxes = true;
            this.axisChannelTree.ContextMenuStrip = this.channelsContext;
            this.axisChannelTree.Location = new System.Drawing.Point(12, 65);
            this.axisChannelTree.Name = "axisChannelTree";
            this.axisChannelTree.Size = new System.Drawing.Size(348, 319);
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
            this.channelsContext.Size = new System.Drawing.Size(161, 148);
            this.channelsContext.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.channelsContext_Closed);
            this.channelsContext.Opening += new System.ComponentModel.CancelEventHandler(this.channelsContext_Opening);
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
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Display Channels";
            // 
            // lblAxisMax
            // 
            this.lblAxisMax.Name = "lblAxisMax";
            this.lblAxisMax.Size = new System.Drawing.Size(100, 23);
            this.lblAxisMax.Text = "Axis Maximum";
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
            // txtAxisMaxValue
            // 
            this.txtAxisMaxValue.Name = "txtAxisMaxValue";
            this.txtAxisMaxValue.Size = new System.Drawing.Size(100, 23);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // ChartProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 396);
            this.CloseButton = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbXAxis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.axisChannelTree);
            this.Name = "ChartProperties";
            this.Text = "ChartControlAxesSelect";
            this.channelsContext.ResumeLayout(false);
            this.channelsContext.PerformLayout();
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
    }
}