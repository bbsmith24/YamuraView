namespace YamuraView.UserControls
{
    partial class YAxis
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
            this.axisScroll = new System.Windows.Forms.VScrollBar();
            this.axisScale = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // axisScroll
            // 
            this.axisScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axisScroll.Location = new System.Drawing.Point(39, 0);
            this.axisScroll.Name = "axisScroll";
            this.axisScroll.Size = new System.Drawing.Size(17, 150);
            this.axisScroll.TabIndex = 0;
            // 
            // axisScale
            // 
            this.axisScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.axisScale.BackColor = System.Drawing.Color.White;
            this.axisScale.Location = new System.Drawing.Point(0, 0);
            this.axisScale.Name = "axisScale";
            this.axisScale.Size = new System.Drawing.Size(39, 150);
            this.axisScale.TabIndex = 1;
            // 
            // YAxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axisScale);
            this.Controls.Add(this.axisScroll);
            this.Name = "YAxis";
            this.Size = new System.Drawing.Size(56, 150);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel axisScale;
        public System.Windows.Forms.VScrollBar axisScroll;
    }
}
