namespace YamuraView.UserControls
{
    partial class XAxis
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
            this.axisScale = new System.Windows.Forms.Panel();
            this.axisScroll = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // axisScale
            // 
            this.axisScale.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axisScale.BackColor = System.Drawing.Color.White;
            this.axisScale.Location = new System.Drawing.Point(0, 17);
            this.axisScale.Name = "axisScale";
            this.axisScale.Size = new System.Drawing.Size(150, 39);
            this.axisScale.TabIndex = 1;
            this.axisScale.Paint += new System.Windows.Forms.PaintEventHandler(this.axisScale_Paint);
            // 
            // axisScroll
            // 
            this.axisScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axisScroll.Location = new System.Drawing.Point(0, 0);
            this.axisScroll.Name = "axisScroll";
            this.axisScroll.Size = new System.Drawing.Size(150, 17);
            this.axisScroll.TabIndex = 0;
            // 
            // XAxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(this.axisScale);
            this.Controls.Add(this.axisScroll);
            this.Name = "XAxis";
            this.Size = new System.Drawing.Size(150, 56);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel axisScale;
        public System.Windows.Forms.HScrollBar axisScroll;
    }
}
