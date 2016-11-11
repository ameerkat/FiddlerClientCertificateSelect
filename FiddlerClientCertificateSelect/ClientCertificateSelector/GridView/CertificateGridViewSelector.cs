using System.Security.Cryptography.X509Certificates;

namespace FiddlerClientCertificateSelect
{
    /// <summary>
    /// Uses the customizable grid view certification selection
    /// </summary>
    public class CertificateGridViewSelector : BaseCertificateSelector
    {
        public override X509Certificate2 GetCertificate(X509CertificateCollection localCertificates, string targetHost)
        {
            X509Certificate2Collection collection = PopulateCertificateCollection(localCertificates);
            var certificateSelectorForm = new CertificateSelector(
                collection,
                targetHost);
            var dialogResult = certificateSelectorForm.ShowDialog();
            return (X509Certificate2)certificateSelectorForm.SelectedCertificate;
        }
    }
}
