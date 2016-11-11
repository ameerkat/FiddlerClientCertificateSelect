using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiddlerClientCertificateSelect
{
    public partial class CertificateSelector : Form
    {
        private IList<X509Certificate> selectableClientCertificates;
        public X509Certificate SelectedCertificate { get; set; }

        public CertificateSelector(X509CertificateCollection certificates, string host)
        {
            InitializeComponent();
            this.Text = string.Format(FiddlerClientCertificateSelectResources.ClientSelectorTitle, host);

            this.selectableClientCertificates = new List<X509Certificate>();
            foreach (var certificate in certificates)
            {
                this.selectableClientCertificates.Add(certificate);
            }

            this.CertificateBindingSource = new BindingSource();
            this.CertificateBindingSource.DataSource = certificates;
            this.CertificateGridView.DataSource = this.CertificateBindingSource;
            this.CertificateGridView.AutoGenerateColumns = true;

            // Set the default columns
            var selectedColumnsString = Properties.Settings.Default.DefaultSelectedColumns;
            if (selectedColumnsString != null)
            {
                var selectedColumnNames = selectedColumnsString.Split(new char[] { ',' });
                foreach (DataGridViewColumn column in this.CertificateGridView.Columns)
                {
                    if (!selectedColumnNames.Contains(column.Name))
                    {
                        column.Visible = false;
                    }
                }

                this.CertificateGridView.Refresh();
            }
        }

        private void CertificateGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.SelectButton_Click(sender, e);
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ClientSelectorSettings settings = new ClientSelectorSettings(this.CertificateGridView.Columns);
            settings.ShowDialog();
            this.SaveGridViewColumns();
        }

        private const string thumbprintColumnName = "Thumbprint";
        private void SelectButton_Click(object sender, EventArgs e)
        {
            this.SelectedCertificate = null;
            var selectedRows = this.CertificateGridView.SelectedRows;
            if (selectedRows.Count == 1) {
                var selectedRowEnumerator = selectedRows.GetEnumerator();
                selectedRowEnumerator.MoveNext();
                DataGridViewRow row = (DataGridViewRow)selectedRowEnumerator.Current;
                string thumbprintValue = null;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if(string.Equals(cell.OwningColumn.Name, thumbprintColumnName, StringComparison.OrdinalIgnoreCase)) {
                        thumbprintValue = (string)cell.Value;
                    }
                }

                if (thumbprintValue != null) {
                    this.SelectedCertificate = selectableClientCertificates.FirstOrDefault(x => string.Equals(((X509Certificate2)x).Thumbprint, thumbprintValue, StringComparison.OrdinalIgnoreCase));
                }
            }

            this.Close();
        }

        private void SaveGridViewColumns()
        {
            List<DataGridViewColumn> columns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn item in this.CertificateGridView.Columns)
            {
                if (item.Visible)
                {
                    columns.Add(item);
                }
            }
            var array = columns.ToArray();
            Array.Sort(array, new FuncComparer<DataGridViewColumn>(
                (x, y) => { return x.DisplayIndex - y.DisplayIndex; }));

            Properties.Settings.Default.DefaultSelectedColumns = string.Join(",", array.Select(x => x.Name));
            Properties.Settings.Default.Save();
        }
    }
}
