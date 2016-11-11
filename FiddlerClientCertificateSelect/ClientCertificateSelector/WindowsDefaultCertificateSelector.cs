using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerClientCertificateSelect
{
    /// <summary>
    /// Uses the default windows x509 certificate selection UI (e.g. the one
    /// that shows up in IE or chrome during client certificate selection) 
    /// to select a certificate.
    /// </summary>
    public class WindowsDefaultCertificateSelector : IClientCertificateSelector
    {
        public X509Certificate2 GetCertificate(X509CertificateCollection localCertificates, string targetHost)
        {
            X509Certificate2Collection collection = new X509Certificate2Collection();
            if (localCertificates != null && localCertificates.Count != 0)
            {
                foreach (var certificate in localCertificates)
                {
                    collection.Add(new X509Certificate2(certificate));
                }
            }
            else
            {
                X509Store localPersonalStore = new X509Store();
                localPersonalStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                collection = localPersonalStore.Certificates;
            }

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
