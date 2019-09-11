namespace YamuraLog
{
    partial class UploadFiles
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
            this.btnFolderLocator = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBaud = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.btnGetFiles = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtSaveTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFileAction = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnSaveINI = new System.Windows.Forms.Button();
            this.btnLoadSetupFile = new System.Windows.Forms.Button();
            this.txtSetupFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDownloadINI = new System.Windows.Forms.Button();
            this.loggerINIFile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnUploadINI = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFolderLocator
            // 
            this.btnFolderLocator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderLocator.Location = new System.Drawing.Point(306, 31);
            this.btnFolderLocator.Name = "btnFolderLocator";
            this.btnFolderLocator.Size = new System.Drawing.Size(40, 23);
            this.btnFolderLocator.TabIndex = 13;
            this.btnFolderLocator.Text = "...";
            this.btnFolderLocator.UseVisualStyleBackColor = true;
            this.btnFolderLocator.Click += new System.EventHandler(this.btnFolderLocator_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(9, 89);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(337, 220);
            this.textBox1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Baud";
            // 
            // cmbBaud
            // 
            this.cmbBaud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBaud.FormattingEnabled = true;
            this.cmbBaud.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "74880",
            "115200",
            "250000",
            "230400",
            "500000",
            "1000000",
            "2000000"});
            this.cmbBaud.Location = new System.Drawing.Point(199, 13);
            this.cmbBaud.Name = "cmbBaud";
            this.cmbBaud.Size = new System.Drawing.Size(97, 21);
            this.cmbBaud.TabIndex = 10;
            this.cmbBaud.Text = "115200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Port";
            // 
            // cmbPort
            // 
            this.cmbPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(52, 13);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(97, 21);
            this.cmbPort.TabIndex = 8;
            this.cmbPort.SelectedIndexChanged += new System.EventHandler(this.cmbPort_SelectedIndexChanged);
            // 
            // btnGetFiles
            // 
            this.btnGetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetFiles.Enabled = false;
            this.btnGetFiles.Location = new System.Drawing.Point(9, 60);
            this.btnGetFiles.Name = "btnGetFiles";
            this.btnGetFiles.Size = new System.Drawing.Size(337, 23);
            this.btnGetFiles.TabIndex = 7;
            this.btnGetFiles.Text = "Get File(s)";
            this.btnGetFiles.UseVisualStyleBackColor = true;
            this.btnGetFiles.Click += new System.EventHandler(this.btnGetFiles_Click);
            // 
            // txtSaveTo
            // 
            this.txtSaveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSaveTo.Location = new System.Drawing.Point(49, 33);
            this.txtSaveTo.Name = "txtSaveTo";
            this.txtSaveTo.Size = new System.Drawing.Size(251, 20);
            this.txtSaveTo.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "To";
            // 
            // cmbFileAction
            // 
            this.cmbFileAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFileAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileAction.FormattingEnabled = true;
            this.cmbFileAction.Items.AddRange(new object[] {
            "List",
            "Upload",
            "Upload and Delete",
            "Delete"});
            this.cmbFileAction.Location = new System.Drawing.Point(49, 6);
            this.cmbFileAction.Name = "cmbFileAction";
            this.cmbFileAction.Size = new System.Drawing.Size(297, 21);
            this.cmbFileAction.TabIndex = 16;
            this.cmbFileAction.SelectedIndexChanged += new System.EventHandler(this.cmbFileAction_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Action";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(365, 341);
            this.tabControl1.TabIndex = 18;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.btnFolderLocator);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtSaveTo);
            this.tabPage1.Controls.Add(this.cmbFileAction);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnGetFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(357, 315);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log Files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnUploadINI);
            this.tabPage2.Controls.Add(this.btnSaveINI);
            this.tabPage2.Controls.Add(this.btnLoadSetupFile);
            this.tabPage2.Controls.Add(this.txtSetupFile);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.btnDownloadINI);
            this.tabPage2.Controls.Add(this.loggerINIFile);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(357, 315);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Logger Setup";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSaveINI
            // 
            this.btnSaveINI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveINI.Location = new System.Drawing.Point(223, 283);
            this.btnSaveINI.Name = "btnSaveINI";
            this.btnSaveINI.Size = new System.Drawing.Size(77, 23);
            this.btnSaveINI.TabIndex = 19;
            this.btnSaveINI.Text = "Save";
            this.btnSaveINI.UseVisualStyleBackColor = true;
            this.btnSaveINI.Click += new System.EventHandler(this.btnSaveINI_Click);
            // 
            // btnLoadSetupFile
            // 
            this.btnLoadSetupFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadSetupFile.Location = new System.Drawing.Point(311, 4);
            this.btnLoadSetupFile.Name = "btnLoadSetupFile";
            this.btnLoadSetupFile.Size = new System.Drawing.Size(40, 23);
            this.btnLoadSetupFile.TabIndex = 16;
            this.btnLoadSetupFile.Text = "...";
            this.btnLoadSetupFile.UseVisualStyleBackColor = true;
            this.btnLoadSetupFile.Click += new System.EventHandler(this.btnLoadSetupFile_Click);
            // 
            // txtSetupFile
            // 
            this.txtSetupFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSetupFile.Location = new System.Drawing.Point(71, 6);
            this.txtSetupFile.Name = "txtSetupFile";
            this.txtSetupFile.Size = new System.Drawing.Size(234, 20);
            this.txtSetupFile.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Setup File";
            // 
            // btnDownloadINI
            // 
            this.btnDownloadINI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadINI.Location = new System.Drawing.Point(57, 283);
            this.btnDownloadINI.Name = "btnDownloadINI";
            this.btnDownloadINI.Size = new System.Drawing.Size(77, 23);
            this.btnDownloadINI.TabIndex = 1;
            this.btnDownloadINI.Text = "Download";
            this.btnDownloadINI.UseVisualStyleBackColor = true;
            this.btnDownloadINI.Click += new System.EventHandler(this.btnDownloadINI_Click);
            // 
            // loggerINIFile
            // 
            this.loggerINIFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loggerINIFile.Location = new System.Drawing.Point(6, 33);
            this.loggerINIFile.Multiline = true;
            this.loggerINIFile.Name = "loggerINIFile";
            this.loggerINIFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.loggerINIFile.Size = new System.Drawing.Size(345, 244);
            this.loggerINIFile.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnUploadINI
            // 
            this.btnUploadINI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUploadINI.Location = new System.Drawing.Point(140, 283);
            this.btnUploadINI.Name = "btnUploadINI";
            this.btnUploadINI.Size = new System.Drawing.Size(77, 23);
            this.btnUploadINI.TabIndex = 20;
            this.btnUploadINI.Text = "Upload";
            this.btnUploadINI.UseVisualStyleBackColor = true;
            this.btnUploadINI.Click += new System.EventHandler(this.btnUploadINI_Click);
            // 
            // UploadFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 393);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbBaud);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPort);
            this.Name = "UploadFiles";
            this.Text = "UploadFiles";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFolderLocator;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBaud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Button btnGetFiles;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtSaveTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFileAction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSaveINI;
        private System.Windows.Forms.Button btnLoadSetupFile;
        private System.Windows.Forms.TextBox txtSetupFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDownloadINI;
        private System.Windows.Forms.TextBox loggerINIFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnUploadINI;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}