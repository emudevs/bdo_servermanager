using Ionic.Zip;
using ServerManager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerManager
{
    public partial class ServerCreator : Form
    {
        Main main;
        string serverPath;

        Thread thread;
        Stopwatch stopCreate;

        public int Wc_DownloadProgressChanged { get; private set; }

        public ServerCreator(Main _main)
        {
            InitializeComponent();

            this.Icon = Resources.icon;
            this.main = _main;

            txtServer.Text = main.config.ServerPath;

            stopCreate = new Stopwatch();
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            main.config.ServerPath = txtServer.Text;
            serverPath = txtServer.Text;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            main.config.Save(main.configPath);

            thread = new Thread(delegate()
            {
                if (btn.Text == "Create")
                {
                    if (Directory.Exists(serverPath) && MessageBox.Show("That folder already exists, are you sure you want to install the Server in it?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        Directory.Delete(serverPath, true);

                    stopCreate.Start();

                    CreateDirectory("");
                    CreateDirectory("gameserver");
                    CreateDirectory("loginserver");

                    UpdateServer();
                    CopyDirectory($"loginserver\\login", "loginserver\\bin\\login");

                    DownloadFiles();

                    CreateBatch("1-Database.bat");
                    CreateBatch("2-Loginserver.bat");
                    CreateBatch("3-Gameserver.bat");

                    stopCreate.Stop();
                    Invoke(new MethodInvoker(() => lblLog.Text = $"Server successfully created! ({(decimal)(stopCreate.ElapsedMilliseconds / 1000.00)} ms)"));
                    stopCreate.Reset();
                }

                if (btn.Text == "Update")
                {
                    stopCreate.Start();

                    UpdateServer(true);
                }
            });
            thread.Start();
        }

        public void UpdateServer(bool isUpdate = false)
        {
            RunBatch("gameserver");
            RunBatch("loginserver");

            // Update Gameserver
            string gameserverZip = "";
            string gameserverFolderName = "";
            foreach (string file in Directory.GetFiles(main.config.SourcePath + "\\gameserver\\build\\distributions"))
            {
                FileInfo info = new FileInfo(file);
                if (info.Extension == ".zip")
                {
                    gameserverZip = file;
                    gameserverFolderName = info.Name.Replace(info.Extension, "");
                }
            }

            if (Directory.Exists(main.config.SourcePath + $"\\gameserver\\build\\distributions\\{gameserverFolderName}"))
                Directory.Delete(main.config.SourcePath + $"\\gameserver\\build\\distributions\\{gameserverFolderName}", true);

            Invoke(new MethodInvoker(() => lblLog.Text = $"Extracting {gameserverFolderName}.zip..."));
            ExtractZip(gameserverZip, main.config.SourcePath + "\\gameserver\\build\\distributions");

            if (!Directory.Exists(serverPath + "\\gameserver\\bin")) CopyDirectory($"gameserver\\build\\distributions\\{gameserverFolderName}\\bin", "gameserver\\bin");
            else CopyFile($"gameserver\\build\\distributions\\{gameserverFolderName}\\bin\\gameserver.bat", "gameserver\\bin\\gameserver.bat");
            CopyDirectory($"gameserver\\build\\distributions\\{gameserverFolderName}\\lib", "gameserver\\lib");

            // Update Loginserver
            string loginserverZip = "";
            string loginserverFolderName = "";
            foreach (string file in Directory.GetFiles(main.config.SourcePath + "\\loginserver\\build\\distributions"))
            {
                FileInfo info = new FileInfo(file);
                if (info.Extension == ".zip")
                {
                    loginserverZip = file;
                    loginserverFolderName = info.Name.Replace(info.Extension, "");
                }
            }

            if (Directory.Exists(main.config.SourcePath + $"\\loginserver\\build\\distributions\\{loginserverFolderName}"))
                Directory.Delete(main.config.SourcePath + $"\\loginserver\\build\\distributions\\{loginserverFolderName}", true);

            Invoke(new MethodInvoker(() => lblLog.Text = $"Extracting {loginserverFolderName}.zip..."));
            ExtractZip(loginserverZip, main.config.SourcePath + "\\loginserver\\build\\distributions");

            if (!Directory.Exists(serverPath + "\\loginserver\\bin")) CopyDirectory($"loginserver\\build\\distributions\\{loginserverFolderName}\\bin", "loginserver\\bin");
            else CopyFile($"loginserver\\build\\distributions\\{loginserverFolderName}\\bin\\loginserver.bat", "loginserver\\binloginserver.bat");
            CopyDirectory($"loginserver\\build\\distributions\\{loginserverFolderName}\\lib", "loginserver\\lib");

            if (isUpdate)
            {
                stopCreate.Stop();
                Invoke(new MethodInvoker(() => lblLog.Text = $"Update successful. ({(decimal)(stopCreate.ElapsedMilliseconds / 1000.00)} ms)"));
                stopCreate.Reset();
            }
        }

        public void CreateDirectory(string path)
        {
            Invoke(new MethodInvoker(() => lblLog.Text = $"Creating directory: {path}"));
            if (!Directory.Exists(serverPath + $"\\{path}")) Directory.CreateDirectory(serverPath + $"\\{path}");
            Invoke(new MethodInvoker(() => lblLog.Text = "Done."));
        }

        public void CopyDirectory(string oldPath, string newPath)
        {
            DirectoryInfo dir = new DirectoryInfo(main.config.SourcePath + $"\\{oldPath}");

            if (dir.Exists)
            {
                if (Directory.Exists(serverPath + $"\\{newPath}")) Directory.Delete(serverPath + $"\\{newPath}", true);

                DirectoryInfo[] dirs = dir.GetDirectories();
                if (!Directory.Exists(serverPath + $"\\{newPath}"))
                {
                    CreateDirectory(newPath);
                }

                foreach (FileInfo file in dir.GetFiles())
                {
                    string temppath = Path.Combine(serverPath + $"\\{newPath}", file.Name);
                    Invoke(new MethodInvoker(() => lblLog.Text = $"Copying {temppath}..."));
                    file.CopyTo(temppath, true);
                }

                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(serverPath + $"\\{newPath}", subdir.Name);
                    CopyDirectory(Path.Combine($"\\{oldPath}", subdir.Name), Path.Combine($"\\{newPath}", subdir.Name));
                }

                Invoke(new MethodInvoker(() => lblLog.Text = "Done."));
            }
        }

        public void CopyFile(string oldPath, string newPath)
        {
            FileInfo file = new FileInfo(main.config.SourcePath + $"\\{oldPath}");

            if (file.Exists)
            {
                if (File.Exists(serverPath + $"\\{newPath}")) File.Delete(serverPath + $"\\{newPath}");

                Invoke(new MethodInvoker(() => lblLog.Text = $"Copying {serverPath + $"\\{newPath}"}..."));
                file.CopyTo(serverPath + $"\\{newPath}", true);
            }

            Invoke(new MethodInvoker(() => lblLog.Text = "Done."));
        }

        int totalFiles;
        int filesExtracted;

        public void ExtractZip(string file, string path)
        {
            using (ZipFile zip = ZipFile.Read(file))
            {
                totalFiles = zip.Count;
                filesExtracted = 0;
                zip.ExtractProgress += Zip_ExtractProgress;
                zip.ExtractAll(path, ExtractExistingFileAction.OverwriteSilently);
            }
        }

        string oldFile = "";

        private void Zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {
                if (oldFile != new FileInfo(e.CurrentEntry.FileName).Name) filesExtracted++;

                Invoke(new MethodInvoker(() => lblLog.Text = $"Extracting {new FileInfo(e.ArchiveName).Name}... ({new FileInfo(e.CurrentEntry.FileName).Name})\n" +
                $"{e.BytesTransferred / 1024000}MB/{e.TotalBytesToTransfer / 1024000}MB, {100 * e.BytesTransferred / e.TotalBytesToTransfer}%\n" +
                $"{filesExtracted}/{totalFiles} Files, {100 * filesExtracted / totalFiles}%"));
                oldFile = new FileInfo(e.CurrentEntry.FileName).Name;
            }
        }

        public void CreateBatch(string name)
        {
            string path = $"{serverPath}\\{name}";

            if (File.Exists(path)) File.Delete(path);

            if (name == "1-Database.bat") File.WriteAllText(path, "@echo off\nSET NAME = ODO DATABASE\nTITLE % NAME %\nREM COLOR C\nset mod =% 1\n\ndatabase\\mongod --dbpath database\\data --port 27017");
            if (name == "2-Loginserver.bat") File.WriteAllText(path, "@echo off\ncd loginserver\\bin\\\nstart loginserver.bat\nexit");
            if (name == "3-Gameserver.bat") File.WriteAllText(path, "@echo off\ncd gameserver\\bin\\\nstart gameserver.bat\nexit");
        }

        Stopwatch stopwatch = new Stopwatch();
        string downloadingFile = "";

        public void RunBatch(string server)
        {
            string buildpath = main.config.SourcePath + $"\\{server}\\build";
            string batchpath = main.config.SourcePath + $"\\{server}\\build.bat";

            if (Directory.Exists(buildpath))
            {
                Invoke(new MethodInvoker(() => lblLog.Text = $"Deleting old build version... ({buildpath})"));
                Directory.Delete(buildpath, true);
            }

            Invoke(new MethodInvoker(() => lblLog.Text = "Creating new build Process..."));
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = main.config.SourcePath + $"\\{server}";
            startInfo.FileName = batchpath;

            Process p = new Process();
            p.StartInfo = startInfo;
            p.Start();

            p.WaitForExit();

            if (Directory.Exists(buildpath))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Invoke(new MethodInvoker(() => lblLog.Text = "Build successful!"));
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Invoke(new MethodInvoker(() => lblLog.Text = "Build failed...\nMake sure you got the correct Java JDK installed (OpenJDK 12+) and JAVA_HOME set properly."));
            }
        }

        public void DownloadFiles()
        {
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += Wc_DownloadProgressChanged1;

            if (!File.Exists(main.config.SourcePath + $"\\Database.zip"))
            {
                wc.DownloadFileAsync(new Uri("http://91.200.103.159/dl/Database.zip"), main.config.SourcePath + $"\\Database.zip");
                stopwatch.Start();
                downloadingFile = "Database.zip";
                while (wc.IsBusy) { }
            }

            if (!File.Exists(main.config.SourcePath + $"\\static_data.zip"))
            {
                if (MessageBox.Show("You're about to download files over 2GB, if you don't want that please press cancel.\nYou'll be able to download those any time.", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }

                wc.DownloadFileAsync(new Uri("http://91.200.103.159/dl/static_data.zip"), main.config.SourcePath + $"\\static_data.zip");
                stopwatch.Restart();
                downloadingFile = "static_data.zip";
                while (wc.IsBusy) { }
                stopwatch.Stop();
            }

            if (!Directory.Exists(serverPath + "\\Database"))
            {
                Invoke(new MethodInvoker(() => lblLog.Text = "Extracting Database.zip..."));
                ExtractZip(main.config.SourcePath + $"\\Database.zip", serverPath);
            }
            if (!Directory.Exists(serverPath + "gameserver\\bin\\data\\static_data"))
            {
                Invoke(new MethodInvoker(() => lblLog.Text = "Extracting static_data.zip, this might take a while..."));
                ExtractZip(main.config.SourcePath + $"\\static_data.zip", serverPath + "\\gameserver\\bin\\data");
            }
        }

        private void Wc_DownloadProgressChanged1(object sender, DownloadProgressChangedEventArgs e)
        {
            double speed = Math.Round((e.BytesReceived / 1024000) / stopwatch.Elapsed.TotalSeconds, 2);

            Invoke(new MethodInvoker(() => lblLog.Text = $"Downloading {downloadingFile}...\n({e.BytesReceived / 1024000}MB/{e.TotalBytesToReceive / 1024000}MB, {speed} MB/s, {e.ProgressPercentage}%)"));
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtServer.Text = folderBrowserDialog.SelectedPath;
                main.config.ServerPath = txtServer.Text;
            }
        }
    }
}
