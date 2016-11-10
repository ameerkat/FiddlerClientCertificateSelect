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
        private IList<X509Certificate2> selectableClientCertificates;
        public CertificateSelector(IList<X509Certificate2> certificates)
        {
            this.selectableClientCertificates = certificates;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
