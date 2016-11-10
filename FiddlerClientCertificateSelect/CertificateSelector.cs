using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiddlerClientCertificateSelect
{
    public partial class CertificateSelector : Form
    {
        private IList<X509Certificate> selectableClientCertificates;
        private List<DataGridViewColumn> allColumns;
        private DataGridViewColumnCollection selectedColumns;

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
            this.allColumns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn column in this.CertificateGridView.Columns)
            {
                allColumns.Add(column);
            }

            selectedColumns = this.CertificateGridView.Columns;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            this.CertificateGridView.Columns.RemoveAt(0);
        }
    }
}
