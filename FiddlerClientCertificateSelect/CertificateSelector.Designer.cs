using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FiddlerClientCertificateSelect
{
    partial class CertificateSelector
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
            this.components = new System.ComponentModel.Container();
            this.CertificateGridView = new System.Windows.Forms.DataGridView();
            this.CertificateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SettingsButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.CertificateGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CertificateBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CertificateGridView
            // 
            this.CertificateGridView.AllowUserToAddRows = false;
            this.CertificateGridView.AllowUserToDeleteRows = false;
            this.CertificateGridView.AllowUserToOrderColumns = true;
            this.CertificateGridView.AutoGenerateColumns = false;
            this.CertificateGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.CertificateGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CertificateGridView.DataSource = this.CertificateBindingSource;
            this.CertificateGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CertificateGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.CertificateGridView.Location = new System.Drawing.Point(0, 0);
            this.CertificateGridView.MultiSelect = false;
            this.CertificateGridView.Name = "CertificateGridView";
            this.CertificateGridView.RowTemplate.Height = 33;
            this.CertificateGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CertificateGridView.Size = new System.Drawing.Size(1658, 835);
            this.CertificateGridView.TabIndex = 0;
            // 
            // SettingsButton
            // 
            this.SettingsButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.SettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F);
            this.SettingsButton.Location = new System.Drawing.Point(0, 0);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(250, 92);
            this.SettingsButton.TabIndex = 2;
            this.SettingsButton.Text = "Column Settings";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SelectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.SelectButton.Location = new System.Drawing.Point(1478, 0);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(180, 92);
            this.SelectButton.TabIndex = 0;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SelectButton);
            this.panel1.Controls.Add(this.SettingsButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 835);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1658, 92);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CertificateGridView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1658, 835);
            this.panel2.TabIndex = 3;
            // 
            // CertificateSelector
            // 
            this.AcceptButton = this.SelectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1658, 927);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CertificateSelector";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a Client Certificate";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.CertificateGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CertificateBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.DataGridView CertificateGridView;
        private BindingSource CertificateBindingSource;
        private Panel panel1;
        private Panel panel2;

    }
}