using System.Security.Cryptography.X509Certificates;

namespace FiddlerClientCertificateSelect
{
    /// <summary>
    /// Uses the default windows x509 certificate selection UI (e.g. the one
    /// that shows up in IE or chrome during client certificate selection) 
    /// to select a certificate.
    /// </summary>
    public class WindowsDefaultCertificateSelector : BaseCertificateSelector
    {
        public override X509Certificate2 GetCertificate(X509CertificateCollection localCertificates, string targetHost)
        {
            X509Certificate2Collection collection = PopulateCertificateCollection(localCertificates);
            var selected = X509Certificate2UI.SelectFromCollection(collection, 
                targetHost, 
                string.Format(FiddlerClientCertificateSelectResources.ClientSelectorTitle, targetHost), 
                X509SelectionFlag.SingleSelection);

            if (selected.Count == 1)
            {
                var enumerator = selected.GetEnumerator();
                enumerator.MoveNext();
                return enumerator.Current;
            }

            return null;
        }
    }
}
