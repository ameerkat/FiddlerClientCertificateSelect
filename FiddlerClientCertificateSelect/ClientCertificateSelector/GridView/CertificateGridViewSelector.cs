using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerClientCertificateSelect
{
    /// <summary>
    /// Uses the customizable grid view certification selection
    /// </summary>
    public class CertificateGridViewSelector : IClientCertificateSelector
    {
        public X509Certificate2 GetCertificate(X509CertificateCollection localCertificates, string targetHost)
        {
            X509CertificateCollection collection = localCertificates;
            if (collection == null || collection.Count == 0)
            {
                X509Store localPersonalStore = new X509Store();
                localPersonalStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                collection = localPersonalStore.Certificates;
            }

            var certificateSelectorForm = new CertificateSelector(
                collection,
                targetHost);
            var dialogResult = certificateSelectorForm.ShowDialog();
            return (X509Certificate2)certificateSelectorForm.SelectedCertificate;
        }
    }
}
