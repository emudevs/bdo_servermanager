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
    public partial class ConfigEditor : Form
    {
        Main main;

        string configPath;
        Dictionary<int, ConfigEntry> configs = new Dictionary<int, ConfigEntry>();

        List<string> configFile;

        TextBox tempTextBox = new TextBox();
        ComboBox tempComboBox = new ComboBox();

        public ConfigEditor(Main _main)
        {
            InitializeComponent();

            this.Icon = Resources.icon;
            this.main = _main;

            tempTextBox.Visible = false;
            tempTextBox.KeyDown += TempTextBox_KeyDown;
            tempTextBox.GotFocus += TempTextBox_GotFocus;
            this.Controls.Add(tempTextBox);

            tempComboBox.Visible = false;
            tempComboBox.Width = 50;
            tempComboBox.Items.AddRange(new[] { "false", "true" });
            tempComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tempComboBox.SelectedIndexChanged += TempComboBox_SelectedIndexChanged;
            this.Controls.Add(tempComboBox);

            configPath = main.config.ServerPath + "\\gameserver\\bin\\configs";

            GetConfigFiles();
        }

        public void GetConfigFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(configPath);

            if (dir.Exists)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    listConfig.Items.Add(file.Name);
                }
            }
            else
            {
                // ERROR
            }
        }

        public void SaveConfig()
        {
            foreach (var cfg in configs)
            {
                configFile[cfg.Key] = $"{cfg.Value.Name} = {cfg.Value.Value}";
            }

            File.WriteAllLines(configPath + $"\\{listConfig.SelectedItem}", configFile.ToArray());
        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public void UpdateTextBox()
        {
            if (tempTextBox.Text != "")
            {
                listValues.SelectedItems[0].SubItems[1].Text = tempTextBox.Text;
                configs[(int)listValues.SelectedItems[0].Tag].Value = tempTextBox.Text;
                tempTextBox.Visible = false;
                SaveConfig();
                tempTextBox.Text = "";
            }
        }

        public void UpdateComboBox()
        {
            listValues.SelectedItems[0].SubItems[1].Text = tempComboBox.Text;
            configs[(int)listValues.SelectedItems[0].Tag].Value = tempComboBox.Text;
            tempComboBox.Visible = false;
            SaveConfig();
        }

        private void listConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            configs.Clear();
            listValues.Items.Clear();
            tempTextBox.Visible = false;
            tempTextBox.Text = "";
            tempComboBox.Visible = false;
            string selectedConfig = listConfig.SelectedItem.ToString();

            configFile = File.ReadAllLines(configPath + $"\\{selectedConfig}").ToList();

            foreach (string cfg in configFile)
            {
                if (cfg != "" && !cfg.StartsWith("#"))
                {
                    string[] values = cfg.Split(new[] { " = ", "=" }, StringSplitOptions.None);

                    int index = configFile.IndexOf(cfg);
                    string name = values[0];
                    string value = values[1];
                    string fancyName = "";

                    string[] splitName = name.Split(new[] { ".", "_" }, StringSplitOptions.None);
                    foreach (string word in splitName)
                    {
                        fancyName += FirstLetterToUpper(word) + " ";
                    }

                    ListViewItem item = new ListViewItem()
                    {
                        Tag = index,
                        Name = name,
                        Text = fancyName
                    };
                    item.SubItems.Add(value);

                    listValues.Items.Add(item);

                    configs.Add(index, new ConfigEntry() { Name = name, FancyName = fancyName, Value = value });
                }
            }
        }

        private void listValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempComboBox.Visible = false;
            tempTextBox.Visible = false;

            try
            {
                ListViewItem item = listValues.SelectedItems[0];

                if (item.SubItems[1].Text == "true" || item.SubItems[1].Text == "false")
                {
                    tempComboBox.Visible = true;
                    tempComboBox.Location = new Point(item.Position.X + listValues.Location.X + listValues.Columns[0].Width, item.Position.Y + listValues.Location.Y);

                    if (item.SubItems[1].Text == "false") tempComboBox.SelectedIndex = 0;
                    else tempComboBox.SelectedIndex = 1;

                    tempComboBox.BringToFront();
                }
                else
                {
                    tempTextBox.Visible = true;
                    tempTextBox.Location = new Point(item.Position.X + listValues.Location.X + listValues.Columns[0].Width, item.Position.Y + listValues.Location.Y);
                    tempTextBox.Width = listValues.Columns[1].Width;
                    tempTextBox.Text = item.SubItems[1].Text;
                    tempTextBox.BringToFront();
                }
            } catch { }
        }

        private void TempTextBox_GotFocus(object sender, EventArgs e)
        {
            tempTextBox.SelectionStart = 0;
            tempTextBox.SelectionLength = tempTextBox.Text.Length;
        }

        private void TempTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateTextBox();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TempComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComboBox();
        }
    }

    public class ConfigEntry
    {
        public string Name;
        public string FancyName;
        public string Value;
    }
}
