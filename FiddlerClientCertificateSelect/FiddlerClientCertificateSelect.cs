using System;
using System.Windows.Forms;
using Fiddler;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

[assembly: Fiddler.RequiredVersion("2.2.8.6")]
namespace FiddlerClientCertificateSelect
{
    public class FiddlerClientCertificateSelect : IFiddlerExtension
    {
        private LocalCertificateSelectionCallback previousClientCertificateProvider;
        private CertificateSelector certificateSelectorForm;

        public void OnBeforeUnload()
        {
            Fiddler.FiddlerApplication.ClientCertificateProvider = previousClientCertificateProvider;
        }

        public void OnLoad()
        {
            previousClientCertificateProvider = Fiddler.FiddlerApplication.ClientCertificateProvider;
            Fiddler.FiddlerApplication.ClientCertificateProvider = this.LocalCertificateSelectionCallback;
        }

        public X509Certificate LocalCertificateSelectionCallback(object sender, 
            string targetHost, 
            X509CertificateCollection localCertificates, 
            X509Certificate remoteCertificate, 
            string[] acceptableIssuers)
        {
            X509CertificateCollection collection = localCertificates;
            if (collection.Count == 0)
            {
                X509Store localPersonalStore = new X509Store();
                localPersonalStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                collection = localPersonalStore.Certificates;
            }

            certificateSelectorForm = new CertificateSelector(collection, targetHost);
            var dialogResult = certificateSelectorForm.ShowDialog();
            return null;
        }
    }
}
