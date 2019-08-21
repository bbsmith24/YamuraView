namespace YamuraLog
{
    partial class ChannelInfoForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChannelMax = new System.Windows.Forms.TextBox();
            this.txtChannelMin = new System.Windows.Forms.TextBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.channelColorDialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(146, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(63, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Channel maximum";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Channel minimun";
            // 
            // txtChannelMax
            // 
            this.txtChannelMax.Location = new System.Drawing.Point(105, 32);
            this.txtChannelMax.Name = "txtChannelMax";
            this.txtChannelMax.ReadOnly = true;
            this.txtChannelMax.Size = new System.Drawing.Size(167, 20);
            this.txtChannelMax.TabIndex = 7;
            // 
            // txtChannelMin
            // 
            this.txtChannelMin.Location = new System.Drawing.Point(105, 6);
            this.txtChannelMin.Name = "txtChannelMin";
            this.txtChannelMin.ReadOnly = true;
            this.txtChannelMin.Size = new System.Drawing.Size(167, 20);
            this.txtChannelMin.TabIndex = 6;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(105, 58);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(167, 23);
            this.btnColor.TabIndex = 12;
            this.btnColor.Text = "Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // ChannelInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 121);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtChannelMax);
            this.Controls.Add(this.txtChannelMin);
            this.Name = "ChannelInfo";
            this.Text = "ChannelInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChannelMax;
        private System.Windows.Forms.TextBox txtChannelMin;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.ColorDialog channelColorDialog;
    }
}