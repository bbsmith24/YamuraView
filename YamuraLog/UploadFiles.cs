using System;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace YamuraLog
{
    public partial class UploadFiles : Form
    {
        SerialPort serialPort;
        string folderPath = "D:\\Temp\\Test Data";
        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
        }
        string uploadMode = "U";
        public string UploadMode
        {
            get { return uploadMode; }
            set
            {
                if((value != "L") &&   // list 
                   (value != "U") &&   // upload
                   (value != "D") &&   // upload and delete
                   (value != "X"))     // delete
                {
                    return;
                }
                uploadMode = value;
                if(value == "L")
                {
                    btnFolderLocator.Enabled = false;
                    btnGetFiles.Text = "List";
                    Text = "List";
                    cmbFileAction.SelectedIndex = 0;
                }
                else if (value == "U")
                {
                    btnFolderLocator.Enabled = true;
                    btnGetFiles.Text = "Upload";
                    Text = "Upload";
                    cmbFileAction.SelectedIndex = 1;
                }
                else if (value == "D")
                {
                    btnFolderLocator.Enabled = true;
                    btnGetFiles.Text = "Upload/Delete";
                    Text = "Upload and Delete";
                    cmbFileAction.SelectedIndex = 2;
                }
                else if (value == "X")
                {
                    btnFolderLocator.Enabled = false;
                    btnGetFiles.Text = "Delete";
                    Text = "Delete";
                    cmbFileAction.SelectedIndex = 3;
                }
            }
        }

        public UploadFiles()
        {
            InitializeComponent();
            foreach (string s in SerialPort.GetPortNames())
            {
                cmbPort.Items.Add(s);
            }
            txtSaveTo.Text = "D:\\Temp\\Test Data";
        }

        private void btnGetFiles_Click(object sender, EventArgs e)
        {
            byte inByte;
            string fileName;
            StringBuilder rStr = new StringBuilder();
            // Create a new SerialPort object with default settings.
            serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            serialPort.PortName = cmbPort.Text;
            serialPort.BaudRate = Convert.ToInt32(cmbBaud.Text);
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;

            textBox1.Text = "Start";

            serialPort.Open();
            while (true)
            {
                serialPort.Write("A");
                try
                {
                    fileName = serialPort.ReadLine();
                    rStr.Insert(0, fileName + System.Environment.NewLine);
                }
                catch
                {
                    break;
                }
                // upload or upload/delete
                if ((uploadMode == "U") ||
                   (uploadMode == "D"))
                {
                    fileName = fileName.Trim();
                    if (folderPath.Length > 0)
                    {
                        fileName = folderPath + "\\" + fileName;
                    }
                    using (System.IO.BinaryWriter outFile = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                    {
                        serialPort.Write(uploadMode);
                        while (true)
                        {
                            try
                            {
                                inByte = (byte)serialPort.ReadByte();
                            }
                            catch
                            {
                                break;
                            }
                            outFile.Write((byte)inByte);
                            //rStr.AppendFormat("{0:X02} ", inByte);
                        }
                        textBox1.Text = rStr.ToString();
                        outFile.Close();
                    }
                }
                else if (uploadMode == "X")
                {
                    serialPort.Write(uploadMode);
                }
            }
            serialPort.Close();
            rStr.Insert(0, "Done" + System.Environment.NewLine);
            textBox1.Text = rStr.ToString();
        }

        private void btnFolderLocator_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                txtSaveTo.Text = folderPath;
            }
        }

        private void cmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGetFiles.Enabled = true;
        }

        private void cmbFileAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            // List
            // Upload
            // Upload and Delete
            // Delete
            if (cmbFileAction.Text == "List")
            {
                UploadMode = "L";
            }
            else if (cmbFileAction.Text == "Upload")
            {
                UploadMode = "U";
            }
            else if (cmbFileAction.Text == "Upload and Delete")
            {
                UploadMode = "D";
            }
            else if (cmbFileAction.Text == "Delete")
            {
                UploadMode = "X";
            }
        }
    }
}
