namespace FiddlerClientCertificateSelectInstaller
{
    partial class InstallerForm
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
            this.InstallCurrentUserButton = new System.Windows.Forms.Button();
            this.InstallForAllUsers = new System.Windows.Forms.Button();
            this.UninstallButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstallCurrentUserButton
            // 
            this.InstallCurrentUserButton.Location = new System.Drawing.Point(13, 13);
            this.InstallCurrentUserButton.Name = "InstallCurrentUserButton";
            this.InstallCurrentUserButton.Size = new System.Drawing.Size(319, 111);
            this.InstallCurrentUserButton.TabIndex = 0;
            this.InstallCurrentUserButton.Text = "Install for Current User";
            this.InstallCurrentUserButton.UseVisualStyleBackColor = true;
            this.InstallCurrentUserButton.Click += new System.EventHandler(this.InstallCurrentUserButton_Click);
            // 
            // InstallForAllUsers
            // 
            this.InstallForAllUsers.Location = new System.Drawing.Point(13, 131);
            this.InstallForAllUsers.Name = "InstallForAllUsers";
            this.InstallForAllUsers.Size = new System.Drawing.Size(319, 110);
            this.InstallForAllUsers.TabIndex = 1;
            this.InstallForAllUsers.Text = "Install for All Users";
            this.InstallForAllUsers.UseVisualStyleBackColor = true;
            this.InstallForAllUsers.Click += new System.EventHandler(this.InstallForAllUsers_Click);
            // 
            // UninstallButton
            // 
            this.UninstallButton.Location = new System.Drawing.Point(339, 13);
            this.UninstallButton.Name = "UninstallButton";
            this.UninstallButton.Size = new System.Drawing.Size(250, 228);
            this.UninstallButton.TabIndex = 2;
            this.UninstallButton.Text = "Uninstall";
            this.UninstallButton.UseVisualStyleBackColor = true;
            this.UninstallButton.Click += new System.EventHandler(this.UninstallButton_Click);
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 255);
            this.Controls.Add(this.UninstallButton);
            this.Controls.Add(this.InstallForAllUsers);
            this.Controls.Add(this.InstallCurrentUserButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstallerForm";
            this.ShowIcon = false;
            this.Text = "Client Cert Extension Install";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InstallCurrentUserButton;
        private System.Windows.Forms.Button InstallForAllUsers;
        private System.Windows.Forms.Button UninstallButton;
    }
}

