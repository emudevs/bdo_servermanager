namespace ServerManager
{
    partial class ConfigEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listConfig = new System.Windows.Forms.ListBox();
            this.listValues = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listConfig
            // 
            this.listConfig.FormattingEnabled = true;
            this.listConfig.Location = new System.Drawing.Point(12, 12);
            this.listConfig.Name = "listConfig";
            this.listConfig.Size = new System.Drawing.Size(142, 290);
            this.listConfig.TabIndex = 0;
            this.listConfig.SelectedIndexChanged += new System.EventHandler(this.listConfig_SelectedIndexChanged);
            // 
            // listValues
            // 
            this.listValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnValue});
            this.listValues.FullRowSelect = true;
            this.listValues.HideSelection = false;
            this.listValues.Location = new System.Drawing.Point(160, 12);
            this.listValues.MultiSelect = false;
            this.listValues.Name = "listValues";
            this.listValues.Size = new System.Drawing.Size(469, 290);
            this.listValues.TabIndex = 2;
            this.listValues.UseCompatibleStateImageBehavior = false;
            this.listValues.View = System.Windows.Forms.View.Details;
            this.listValues.SelectedIndexChanged += new System.EventHandler(this.listValues_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 240;
            // 
            // columnValue
            // 
            this.columnValue.Text = "Value";
            this.columnValue.Width = 225;
            // 
            // ConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 314);
            this.Controls.Add(this.listValues);
            this.Controls.Add(this.listConfig);
            this.Name = "ConfigEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Config Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listConfig;
        private System.Windows.Forms.ListView listValues;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnValue;
    }
}