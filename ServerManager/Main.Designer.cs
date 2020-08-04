namespace ServerManager
{
    partial class Main
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Empty");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Server", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Empty");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Source", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeView = new System.Windows.Forms.TreeView();
            this.txtReader = new System.Windows.Forms.RichTextBox();
            this.btnCreator = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(12, 12);
            this.treeView.Name = "treeView";
            treeNode1.Name = "nodeFiller1";
            treeNode1.Text = "Empty";
            treeNode2.Name = "nodeServer";
            treeNode2.Text = "Server";
            treeNode3.Name = "nodeFiller2";
            treeNode3.Text = "Empty";
            treeNode4.Name = "nodeSource";
            treeNode4.Text = "Source";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4});
            this.treeView.Size = new System.Drawing.Size(260, 426);
            this.treeView.TabIndex = 0;
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // txtReader
            // 
            this.txtReader.BackColor = System.Drawing.Color.White;
            this.txtReader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReader.Location = new System.Drawing.Point(278, 12);
            this.txtReader.Name = "txtReader";
            this.txtReader.ReadOnly = true;
            this.txtReader.Size = new System.Drawing.Size(510, 252);
            this.txtReader.TabIndex = 1;
            this.txtReader.Text = "";
            // 
            // btnCreator
            // 
            this.btnCreator.Location = new System.Drawing.Point(586, 415);
            this.btnCreator.Name = "btnCreator";
            this.btnCreator.Size = new System.Drawing.Size(98, 23);
            this.btnCreator.TabIndex = 2;
            this.btnCreator.Text = "Create / Update";
            this.btnCreator.UseVisualStyleBackColor = true;
            this.btnCreator.Click += new System.EventHandler(this.btnCreator_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Enabled = false;
            this.btnConfig.Location = new System.Drawing.Point(482, 415);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(98, 23);
            this.btnConfig.TabIndex = 3;
            this.btnConfig.Text = "Config Editor";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(690, 415);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(98, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset Paths";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnCreator);
            this.Controls.Add(this.txtReader);
            this.Controls.Add(this.treeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "odoRE Server Manager - by Nopey";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.RichTextBox txtReader;
        private System.Windows.Forms.Button btnCreator;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnReset;
    }
}

