using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server1
{
    public partial class client : Form
    {
        public client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                client11.SendFile(fd.FileName);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = client11.curMsg_client;
        }
    }
}
