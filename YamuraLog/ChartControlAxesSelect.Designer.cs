namespace YamuraLog
{
    partial class ChartControlAxesSelect
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
            this.cmbXAxis = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.channelsContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.axisExtents = new System.Windows.Forms.ToolStripMenuItem();
            this.channelInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // axisChannelTree
            // 
            this.axisChannelTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.axisChannelTree.CheckBoxes = true;
            this.axisChannelTree.ContextMenuStrip = this.channelsContext;
            this.axisChannelTree.Location = new System.Drawing.Point(12, 65);
            this.axisChannelTree.Name = "axisChannelTree";
            this.axisChannelTree.Size = new System.Drawing.Size(348, 294);
            this.axisChannelTree.TabIndex = 30;
            this.axisChannelTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.axisChannelTree_AfterCheck);
            // 
            // cmbXAxis
            // 
            this.cmbXAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbXAxis.FormattingEnabled = true;
            this.cmbXAxis.Location = new System.Drawing.Point(12, 25);
            this.cmbXAxis.Name = "cmbXAxis";
            this.cmbXAxis.Size = new System.Drawing.Size(220, 21);
            this.cmbXAxis.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "X Axis";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Display Channels";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(108, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(189, 365);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // channelsContext
            // 
            this.channelsContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.axisExtents,
            this.channelInfo});
            this.channelsContext.Name = "channelsContext";
            this.channelsContext.Size = new System.Drawing.Size(153, 70);
            this.channelsContext.Opening += new System.ComponentModel.CancelEventHandler(this.channelsContext_Opening);
            // 
            // axisExtents
            // 
            this.axisExtents.Name = "axisExtents";
            this.axisExtents.Size = new System.Drawing.Size(152, 22);
            this.axisExtents.Text = "Axis Info";
            this.axisExtents.Click += new System.EventHandler(this.axisExtents_Click);
            // 
            // channelInfo
            // 
            this.channelInfo.Name = "channelInfo";
            this.channelInfo.Size = new System.Drawing.Size(152, 22);
            this.channelInfo.Text = "Channel Info";
            this.channelInfo.Click += new System.EventHandler(this.channelInfo_Click);
            // 
            // ChartControlAxesSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 396);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbXAxis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.axisChannelTree);
            this.Name = "ChartControlAxesSelect";
            this.Text = "ChartControlAxesSelect";
            this.channelsContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView axisChannelTree;
        private System.Windows.Forms.ComboBox cmbXAxis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ContextMenuStrip channelsContext;
        private System.Windows.Forms.ToolStripMenuItem axisExtents;
        private System.Windows.Forms.ToolStripMenuItem channelInfo;
    }
}