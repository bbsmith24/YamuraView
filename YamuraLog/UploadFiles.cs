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
                if((value != "U") &&
                   (value != "D") &&
                   (value != "X"))
                {
                    return;
                }
                uploadMode = value;
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
                    System.Diagnostics.Debug.WriteLine("reading filename " + fileName);
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("error reading filename");
                    break;
                }
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
    }
}
