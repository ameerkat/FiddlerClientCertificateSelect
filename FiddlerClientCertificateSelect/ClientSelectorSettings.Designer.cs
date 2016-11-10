namespace FiddlerClientCertificateSelect
{
    partial class ClientSelectorSettings
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.AddColumnButton = new System.Windows.Forms.Button();
            this.RemoveColumnButton = new System.Windows.Forms.Button();
            this.UnselectedColumnsLabel = new System.Windows.Forms.Label();
            this.SelectedColumnsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(12, 62);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(450, 629);
            this.listBox1.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 25;
            this.listBox2.Location = new System.Drawing.Point(573, 62);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(450, 629);
            this.listBox2.TabIndex = 1;
            // 
            // AddColumnButton
            // 
            this.AddColumnButton.Location = new System.Drawing.Point(468, 293);
            this.AddColumnButton.Name = "AddColumnButton";
            this.AddColumnButton.Size = new System.Drawing.Size(99, 65);
            this.AddColumnButton.TabIndex = 2;
            this.AddColumnButton.Text = "->";
            this.AddColumnButton.UseVisualStyleBackColor = true;
            this.AddColumnButton.Click += new System.EventHandler(this.AddColumnButton_Click);
            // 
            // RemoveColumnButton
            // 
            this.RemoveColumnButton.Location = new System.Drawing.Point(469, 365);
            this.RemoveColumnButton.Name = "RemoveColumnButton";
            this.RemoveColumnButton.Size = new System.Drawing.Size(99, 65);
            this.RemoveColumnButton.TabIndex = 3;
            this.RemoveColumnButton.Text = "<-";
            this.RemoveColumnButton.UseVisualStyleBackColor = true;
            this.RemoveColumnButton.Click += new System.EventHandler(this.RemoveColumnButton_Click);
            // 
            // UnselectedColumnsLabel
            // 
            this.UnselectedColumnsLabel.AutoSize = true;
            this.UnselectedColumnsLabel.Location = new System.Drawing.Point(12, 18);
            this.UnselectedColumnsLabel.Name = "UnselectedColumnsLabel";
            this.UnselectedColumnsLabel.Size = new System.Drawing.Size(210, 25);
            this.UnselectedColumnsLabel.TabIndex = 4;
            this.UnselectedColumnsLabel.Text = "Unselected Columns";
            // 
            // SelectedColumnsLabel
            // 
            this.SelectedColumnsLabel.AutoSize = true;
            this.SelectedColumnsLabel.Location = new System.Drawing.Point(573, 18);
            this.SelectedColumnsLabel.Name = "SelectedColumnsLabel";
            this.SelectedColumnsLabel.Size = new System.Drawing.Size(186, 25);
            this.SelectedColumnsLabel.TabIndex = 5;
            this.SelectedColumnsLabel.Text = "Selected Columns";
            // 
            // ClientSelectorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 714);
            this.Controls.Add(this.SelectedColumnsLabel);
            this.Controls.Add(this.UnselectedColumnsLabel);
            this.Controls.Add(this.RemoveColumnButton);
            this.Controls.Add(this.AddColumnButton);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ClientSelectorSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Columns";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button AddColumnButton;
        private System.Windows.Forms.Button RemoveColumnButton;
        private System.Windows.Forms.Label UnselectedColumnsLabel;
        private System.Windows.Forms.Label SelectedColumnsLabel;
    }
}