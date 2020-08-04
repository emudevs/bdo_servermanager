using Newtonsoft.Json;
using ServerManager.Objects;
using ServerManager.Properties;
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

namespace ServerManager
{
    public partial class Main : Form
    {
        public string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ServerManager\\config.json";
        public Config config;

        // Create Config Object from file in %appdata%
        public void LoadConfig()
        {
            config = new Config();

            if (File.Exists(configPath))
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
                config.Exists = true;
            }
        }

        public Main()
        {
            InitializeComponent();

            this.Icon = Resources.icon;

            // Check for first start
            LoadConfig();
            if (!config.Exists)
            {
                Startup startup = new Startup(this);
                startup.ShowDialog();
                if (!config.Exists) Environment.Exit(0);
            }

            if (!Directory.Exists(config.ServerPath) || !Directory.EnumerateFileSystemEntries(config.ServerPath).Any())
            {
                btnConfig.Enabled = false;

                if (MessageBox.Show("Seems like you don't have a server yet.\nDo you want to create it now?", "Server not found!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Create Server here
                    ServerCreator creator = new ServerCreator(this);
                    creator.ShowDialog();
                }
            }
            else btnConfig.Enabled = true;

            // Add ListView images
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(20, 20);
            imageList.Images.Add("Folder", Resources.imgFolder);
            imageList.Images.Add("File", Resources.imgFile);
            imageList.Images.Add("Java", Resources.imgJava);
            imageList.Images.Add("XML", Resources.imgXML);
            imageList.Images.Add("Json", Resources.imgJson);
            imageList.Images.Add("SQL", Resources.imgSQL);
            imageList.Images.Add("Txt", Resources.imgTxt);
            treeView.ImageList = imageList;
        }

        // Get Folders in ListView
        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            e.Node.EnsureVisible();

            // Get Source Folder
            if (e.Node.Text == "Source")
            {
                e.Node.Tag = config.SourcePath;

                TreeNode loginserver = new TreeNode("loginserver");
                TreeNode gameserver = new TreeNode("gameserver");
                loginserver.Nodes.Add("Empty");
                gameserver.Nodes.Add("Empty");

                loginserver.Tag = config.SourcePath + "\\loginserver";
                gameserver.Tag = config.SourcePath + "\\gameserver";

                e.Node.Nodes.Add(loginserver);
                e.Node.Nodes.Add(gameserver);

                return;
            }
            // Get Server Folder
            else if (e.Node.Text == "Server")
            {
                e.Node.Tag = config.ServerPath;
            }
            // Get Subfolders / Get Directories
            foreach (string dir in Directory.GetDirectories(e.Node.Tag.ToString()))
            {
                TreeNode node = new TreeNode();
                node.Nodes.Add("Empty");
                node.Text = new DirectoryInfo(dir).Name;
                node.Tag = dir;
                node.ImageKey = "Folder";
                node.SelectedImageKey = node.ImageKey;

                e.Node.Nodes.Add(node);
            }

            // Get Files
            foreach (string dir in Directory.GetFiles(e.Node.Tag.ToString()))
            {
                TreeNode node = new TreeNode();
                node.Text = new DirectoryInfo(dir).Name;
                node.Tag = dir;

                // Set Images according to file extensions
                switch (new FileInfo(dir).Extension.ToLower())
                {
                    case ".xml":
                        node.ImageKey = "XML";
                        node.SelectedImageKey = node.ImageKey;
                        break;

                    case ".sql":
                    case ".sqlite3":
                        node.ImageKey = "SQL";
                        node.SelectedImageKey = node.ImageKey;
                        break;

                    case ".java":
                        node.ImageKey = "Java";
                        node.SelectedImageKey = node.ImageKey;
                        break;

                    case ".json":
                        node.ImageKey = "Json";
                        node.SelectedImageKey = node.ImageKey;
                        break;

                    case ".properties":
                    case ".log":
                        node.ImageKey = "Txt";
                        node.SelectedImageKey = node.ImageKey;
                        break;

                    default:
                        node.ImageKey = "File";
                        node.SelectedImageKey = node.ImageKey;
                        break;
                }

                e.Node.Nodes.Add(node);
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtReader.ScrollToCaret();

            try
            {
                if (e.Node.ImageKey != "Folder")
                {
                    FileInfo info = new FileInfo(e.Node.Tag.ToString());

                    switch (info.Extension)
                    {
                        case ".xml":
                        case ".java":
                        case ".json":
                        case ".properties":
                        case ".bat":
                        case ".log":
                        case ".cfg":
                            txtReader.Lines = File.ReadAllLines(info.FullName);
                            break;
                    }
                }
            } catch { }
        }

        private void btnCreator_Click(object sender, EventArgs e)
        {
            ServerCreator creator = new ServerCreator(this);
            creator.ShowDialog();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigEditor editor = new ConfigEditor(this);
            editor.ShowDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            File.Delete(configPath);
            MessageBox.Show("Paths reset. Please restart Program.", "Reset successful");
            Environment.Exit(0);
        }
    }
}
