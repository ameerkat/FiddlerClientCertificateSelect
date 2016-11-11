using System.Security.Cryptography.X509Certificates;

namespace FiddlerClientCertificateSelect
{
    public interface IClientCertificateSelector
    {
        X509Certificate2 GetCertificate(X509CertificateCollection localCertificates, string targetHost);
    }
}
