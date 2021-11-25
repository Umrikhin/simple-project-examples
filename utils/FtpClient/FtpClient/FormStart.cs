using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FtpClient
{
    public partial class FormStart : Form
    {
        string LoginFtp = "records";
        string PwdFtp = "getaccess";
        string HostFtp = "193.93.123.242";
        string PortFtp = "22021";
        

        public FormStart()
        {
            InitializeComponent();
        }

        private void buttonCreateDir_Click(object sender, EventArgs e)
        {
            string root = "Главный офис";
            var ftp = new SimpleFtpClient(LoginFtp, PwdFtp,  string.Format("{0}:{1}", HostFtp, PortFtp), @"D:\Gac");
            ftp.CreateDir(root);
            MessageBox.Show("Каталог создан", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonUploadDir_Click(object sender, EventArgs e)
        {
            string root = "Главный офис";
            var ftp = new SimpleFtpClient(LoginFtp, PwdFtp, string.Format("{0}:{1}/{2}", HostFtp, PortFtp, root), @"D:\Gac");
            ftp.UploadDirectory();
            MessageBox.Show("Каталог скопирован", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
