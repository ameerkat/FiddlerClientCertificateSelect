using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerClientCertificateSelect
{
    public abstract class BaseCertificateSelector : IClientCertificateSelector
    {
        public abstract X509Certificate2 GetCertificate(X509CertificateCollection localCertificates, string targetHost);

        internal X509Certificate2Collection PopulateCertificateCollection(X509CertificateCollection localCertificates = null)
        {
            X509Certificate2Collection collection = new X509Certificate2Collection();
            
            if (localCertificates != null && localCertificates.Count != 0)
            {
                foreach (var certificate in localCertificates)
                {
                    var certificate2 = new X509Certificate2(certificate);
                    if (certificate2.HasPrivateKey)
                    {
                        collection.Add(certificate2);
                    }
                }
            }
            else
            {
                X509Store localPersonalStore = new X509Store();
                localPersonalStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                foreach (var certificate2 in localPersonalStore.Certificates)
                {
                    if (certificate2.HasPrivateKey)
                    {
                        collection.Add(certificate2);
                    }
                }
            }

            return collection;
        }
    }
}
