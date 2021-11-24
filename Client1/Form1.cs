using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            server.receivedPath = "C:/Users/ASUS/OneDrive/caro/OneDrive/Desktop/";
        }
        public static string path;
        public static string messageCurrent = "Stopped";
        private void Form1_Load(object sender, EventArgs e)
        {
            if(server.receivedPath.Length > 0) {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Not Receive FIle");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = server.curMsg_server + Environment.NewLine + server.receivedPath;
        }
        server ser = new server();

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ser.StartServer();
        }
    }
}
