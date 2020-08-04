namespace ServerManager
{
    partial class ServerCreator
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
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnServer = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.lblLog = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(12, 59);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(78, 13);
            this.lblServer.TabIndex = 10;
            this.lblServer.Text = "Server Path:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(12, 73);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(276, 20);
            this.txtServer.TabIndex = 9;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(291, 72);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(24, 22);
            this.btnServer.TabIndex = 8;
            this.btnServer.Text = "...";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(158, 100);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 11;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(239, 100);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btn_Click);
            // 
            // lblInfo1
            // 
            this.lblInfo1.Location = new System.Drawing.Point(9, 9);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(306, 45);
            this.lblInfo1.TabIndex = 13;
            this.lblInfo1.Text = "In case you decided you want to have a different Server path you can edit it here" +
    " if you want to. The default path is your \"odoRE Path/server\".";
            this.lblInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLog
            // 
            this.lblLog.Location = new System.Drawing.Point(9, 130);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(306, 70);
            this.lblLog.TabIndex = 14;
            this.lblLog.Text = "Waiting for Action...";
            this.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServerCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 209);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.lblInfo1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.btnServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ServerCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Server Update";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblInfo1;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}