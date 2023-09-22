using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp_JoshuaRea.Forms
{
    public partial class Activity : Form
    {
        public Activity()
        {
            InitializeComponent();

            DirectoryInfo info = new DirectoryInfo(".");

            string path = info + "//log.txt";
            StreamReader stream = new StreamReader(path);
            string fileData = stream.ReadToEnd();
            richTextBox1.Text = fileData.ToString();
            stream.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
