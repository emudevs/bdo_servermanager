using ServerManager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerManager
{
    public partial class Startup : Form
    {
        Main main;

        public Startup(Main _main)
        {
            InitializeComponent();

            this.Icon = Resources.icon;
            main = _main;
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog.SelectedPath;
                txtServer.Text = folderBrowserDialog.SelectedPath + "\\server";
                main.config.SourcePath = txtPath.Text;
                main.config.ServerPath = txtServer.Text;
            }
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtServer.Text = folderBrowserDialog.SelectedPath;
                main.config.ServerPath = txtPath.Text;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            main.config.Save(main.configPath);
            this.Close();
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (txtPath.Text == "" || txtServer.Text == "") btnAccept.Enabled = false;
            else btnAccept.Enabled = true;

            if (txtPath.Text == "")
            {
                btnServer.Enabled = false;
                txtServer.Enabled = false;
            }
            else
            {
                btnServer.Enabled = true;
                txtServer.Enabled = true;
            }

            main.config.SourcePath = txtPath.Text;
        }
    }
}
